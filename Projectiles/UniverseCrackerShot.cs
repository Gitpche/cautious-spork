using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace MyMod.Content.Projectiles
{
    public class UniverseCrackerShot : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 74;
            Projectile.height = 74;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = -1; // Прошивает всех врагов
            Projectile.timeLeft = 300;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false; // Летит сквозь стены
        }

        public override void AI()
        {
            // Фиолетовое свечение
            Lighting.AddLight(Projectile.Center, 0.6f, 0.1f, 0.6f);
            
            // Частицы как у Пустотной Катаны
            if (Main.rand.NextBool(3))
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.PurpleCrystalShard);
            }
            Projectile.rotation += 0.4f;
        }
    }
}