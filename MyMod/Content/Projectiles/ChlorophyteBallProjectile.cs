using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Audio;

namespace MyMod.Content.Projectiles // Указывает на новую папку
{
    public class ChlorophyteBallProjectile : ModProjectile
    {
        public override void SetStaticDefaults() {
            // Имя снаряда для бестиария или логов берется из локализации
        }

        public override void SetDefaults() {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.friendly = true;
            Projectile.penetrate = -1; // Бесконечные пробития
            Projectile.timeLeft = 300; // Летает 5 секунд
            Projectile.aiStyle = 0; 
        }

        public override void AI() {
            Projectile.rotation += 0.4f; // Вращение

            // САМОНАВЕДЕНИЕ (Радиус 600)
            float maxDetectRadius = 600f;
            NPC closestNPC = FindClosestNPC(maxDetectRadius);
            if (closestNPC != null) {
                Vector2 targetDir = (closestNPC.Center - Projectile.Center).SafeNormalize(Vector2.Zero);
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, targetDir * 14f, 0.08f);
            }

            // Эффекты хлорофита
            if (Main.rand.NextBool(2)) {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.ChlorophyteWeapon);
            }
        }

        public NPC FindClosestNPC(float maxDetectRadius) {
            NPC closestNPC = null;
            float shortestDistance = maxDetectRadius;
            foreach (NPC npc in Main.npc) {
                if (npc.active && !npc.friendly && !npc.dontTakeDamage) {
                    float distance = Vector2.Distance(npc.Center, Projectile.Center);
                    if (distance < shortestDistance) {
                        shortestDistance = distance;
                        closestNPC = npc;
                    }
                }
            }
            return closestNPC;
        }

        public override bool OnTileCollide(Vector2 oldVelocity) {
            // Вечный отскок от стен
            if (Projectile.velocity.X != oldVelocity.X) Projectile.velocity.X = -oldVelocity.X;
            if (Projectile.velocity.Y != oldVelocity.Y) Projectile.velocity.Y = -oldVelocity.Y;
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            return false; 
        }
    }
}