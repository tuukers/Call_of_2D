using System;
using System.Numerics;


namespace Callof2d.Game.Casting
{
    public class HUD : Actor
    {
        private string HUDText;
        private Player player;
        private Zombie zombie;
        private Stats stats;
        private int hUDType;
        public HUD(Player player,Stats stats)
        {
            this.player=player;
            this.stats=stats;
        }
        public void SetHUDText(string HUDText)
        {
            this.HUDText=HUDText;
        }

        public void SetHUDType(int hUDType)
        {
            this.hUDType = hUDType;
        }

        public string GetHUDText()
        {
            return this.HUDText;
        }

        public int GetHUDType()
        {
            return this.hUDType;
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
            int round = stats.GetRound();
            this.SetText($"Round {round}");
        }

        // public void PromptHUD()
        // {

        //     this.SetText();
        // }

        public void HUDSetup(int hUDType)
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
            // else if(hUDType==3)
            // {
            //     this.PromptHUD;
            // }

        }
    }
}