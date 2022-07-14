using System;
using System.Numerics;
using Callof2d.Game.Directing;


namespace Callof2d.Game.Casting
{
    public class HUD : Actor
    {
        private string HUDText;
        private Player player;
        private Zombie zombie;
        private Stats stats;
        private int hUDType;
        private int fontSize;
        private Round round;
        public HUD(Player player,Stats stats,Round round)
        {
            this.player=player;
            this.stats=stats;
            this.round=round;
        }
        public void SetHUDText(string HUDText)
        {
            this.HUDText=HUDText;
        }

        public void SetHUDType(int hUDType)
        {
            this.hUDType = hUDType;
        }

        public void SetFontSize(int fontSize)
        {
            this.fontSize= fontSize;
        }

        public string GetHUDText()
        {
            return this.HUDText;
        }

        public int GetHUDType()
        {
            return this.hUDType;
        }

        public int GetFontSize()
        {
            return fontSize;
        }

        public void WeaponHUD()
        {
            Weapon weapon = player.GetHeldWeapon();
            int magazineCount = weapon.GetMagazineCount();
            int magazineCapacity = weapon.GetMagazineCapacity();
            int ammoCount = weapon.GetAmmoCount();
            string weaponName = weapon.GetWeaponName();
            this.SetText($"{magazineCount}/{ammoCount} {weaponName}");
        }

        public void ScoreHUD()
        {
            //float currentHealth = zombie.GetHealth();

            // if (currentHealth == 0) {
            //     stats.AddPoints(Program.BASE_SCORE, 0, 5);
            // };
            // // else if ([TAKE DAMAGE FLAG]) {
            // //     stats.AddPoints(Program.BASE_SCORE, 0, 1);
            // // };

            int score = stats.GetScore();
            this.SetText($"Score: {score}");
        }

        public void LivesHUD()
        {
            int lives = stats.GetLives();
            this.SetText($"Lives: {lives}");
        }

        public void RoundHUD()
        {
            int roundNum = round.GetRound();
            this.SetText($"{roundNum + 1}");
        }

        public void GameOverHUD()
        {
            this.SetText("GAME OVER");
        }

        // public void PromptHUD()
        // {

        //     this.SetText();
        // }

        public void HUDSetup()
        {
            if (hUDType==0)
            {
                this.WeaponHUD();
            }
            else if(hUDType==1)
            {
                this.ScoreHUD();
            }
            else if(hUDType==2)
            {
                this.RoundHUD();
            }
            else if (hUDType==3)
            {
                this.GameOverHUD();
            }
            // else if(hUDType==3)
            // {
            //     this.PromptHUD;
            // }

        }
    }
}