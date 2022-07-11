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
        public HUD(Player player)
        {
            this.player=player;
        }
        public void SetHUDText(string HUDText)
        {
            this.HUDText=HUDText;
        }

        public string GetHUDText()
        {
            return this.HUDText;
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
            float currentHealth = zombie.GetHealth();

            if (currentHealth == 0) {
                stats.AddPoints(Program.BASE_SCORE, 0, 5);
            };
            // else if ([TAKE DAMAGE FLAG]) {
            //     stats.AddPoints(Program.BASE_SCORE, 0, 1);
            // };

            int score = stats.GetScore();
            this.SetText($"HIGH SCORE: {score}");
        }
    }
}