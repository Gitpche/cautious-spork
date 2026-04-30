using Terraria;
using Terraria.ModLoader;

namespace MyMod.Content.Projectiles
{
    public class VortexProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 116;
            Projectile.height = 116;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 180;
        }

        public override void AI()
        {
            Projectile.velocity *= 0.98f;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(Terraria.ID.BuffID.Confused, 180);
        }
    }
}