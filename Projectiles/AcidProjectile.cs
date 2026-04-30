using Terraria;
using Terraria.ModLoader;

namespace MyMod.Content.Projectiles
{
    public class AcidProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 7;
        }

        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;

            Projectile.hostile = true;
            Projectile.timeLeft = 200;
        }

        public override void AI()
        {
            Projectile.frameCounter++;

            if (Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;

                if (Projectile.frame >= 7)
                    Projectile.frame = 0;
            }

            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 44);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(Terraria.ID.BuffID.Poisoned, 120);
        }
    }
}