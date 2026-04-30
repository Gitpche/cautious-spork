using Terraria;
using Terraria.ModLoader;

namespace MyMod.Content.Projectiles
{
    public class LaserProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.height = 28;
            Projectile.hostile = true;
            Projectile.timeLeft = 80;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(Terraria.ID.BuffID.ShadowFlame, 120);
        }
    }
}