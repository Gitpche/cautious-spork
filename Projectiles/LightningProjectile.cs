using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace MyMod.Content.Projectiles
{
    public class LightningProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 5;
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;

            Projectile.hostile = true;
            Projectile.friendly = false;

            Projectile.timeLeft = 120;
            Projectile.penetrate = 1;
        }

        public override void AI()
        {
            // ⚡ движение (если нужно — оставь как есть)
            Projectile.rotation += 0.3f * Projectile.direction;

            // 🎞 анимация 5 кадров
            Projectile.frameCounter++;

            if (Projectile.frameCounter >= 3) // скорость анимации
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;

                if (Projectile.frame >= 5)
                    Projectile.frame = 0;
            }

            // эффект молнии
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Electric);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.Electrified, 120);
        }
    }
}