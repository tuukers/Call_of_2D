using System;
using System.Collections.Generic;
using System.Data;
using System.Numerics;
using Callof2d.Game.Casting;
using Callof2d.Game.Services;
using Callof2d.Game.Directing;

namespace Callof2d.Game.Scripting;
public class HandlePlayerHealthAction : Action
    {

        /// <summary>
        /// Handles the player's health bar
        /// </summary>
        private ContactService contactService;
        private DateTime lastZombieHit;
        private int zombieAttackFrequancy;
        private int regenDelay;
        bool gameOver = false;
        private Stats stats;
        private Round round;

        public HandlePlayerHealthAction(ContactService contactService, Stats stats, Round round)
        {
            this.lastZombieHit = DateTime.Now;
            this.zombieAttackFrequancy = 60;
            this.regenDelay = 4;
            this.stats = stats;
            this.round = round;
        }
        

        public void Execute(Cast cast, Script script)
        {
            Player player = (Player) cast.GetFirstActor("player");
            List<Actor> zombies = cast.GetActors("zombie");
            Wall healthbar = (Wall) cast.GetFirstActor("healthbar");

            Vector2 playerPosition = player.GetPosition();
            float playerRadius = player.GetRadius();

            int playerHealth = player.GetPlayerHealth();
            int playerMaxHealth = player.GetPlayerMaxHealth();
            bool takeDamage = false;
            

            foreach(Zombie zombie in zombies)
            {
                Vector2 zombiePosition = zombie.GetPosition();
                float zombieRadius = zombie.GetRadius();

                float distance = Vector2.Distance(zombiePosition,playerPosition);

                if (distance < playerRadius+zombieRadius)
                {
                    takeDamage=true;
                }
            }

            TimeSpan timeSpan = DateTime.Now - lastZombieHit;

            if (timeSpan.Seconds >= 60/zombieAttackFrequancy && takeDamage)
            {
                player.PlayerTakeDamage();
                lastZombieHit = DateTime.Now;
            }
            else if (timeSpan.Seconds >= 60/regenDelay)
            {
                player.PlayerRegen();
            }

            healthbar.SetWidth(player.GetPlayerHealth() * 2);

            if (gameOver)
            {
                script.RemoveAllActions();
            }

            if (playerHealth <= 0)
            {
                HUD hUD = new HUD(player,stats,round);
                hUD.SetColor(Program.RED);
                hUD.SetPosition(new Vector2(Program.MAX_X/3,Program.MAX_Y/2));
                hUD.SetFontSize(50);
                hUD.SetHUDType(3);
                hUD.SetHUDText("GAME OVER");

                cast.AddActor("HUD",hUD);
                gameOver =true;
            }
        }
    }