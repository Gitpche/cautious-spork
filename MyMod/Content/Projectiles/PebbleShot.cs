using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace MyMod.Content.Projectiles
{
    public class PebbleShot : ModProjectile
    {
        public override void SetDefaults() {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.aiStyle = 1; // Физика стрелы (летит по дуге вниз)
            Projectile.penetrate = 1; // Исчезает при первом попадании
            Projectile.timeLeft = 600;
        }

        public override void AI() {
            // Заставляем камушек красиво вращаться в полете
            Projectile.rotation += 0.2f * (float)Projectile.direction;
        }

        public override void OnKill(int timeLeft) {
            // Эффект серых искр (пыли) при ударе о стену или врага
            for (int i = 0; i < 4; i++) {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Stone);
                dust.velocity *= 0.5f;
            }
            // Глухой звук удара камня
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        }
    }
}