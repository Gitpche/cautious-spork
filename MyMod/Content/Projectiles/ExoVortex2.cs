using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace MyMod.Content.Projectiles // Четкий путь к папке
{
    public class ExoVortex2 : ModProjectile
    {
        public float Hue => Projectile.ai[0];
        public ref float Time => ref Projectile.ai[1];

        public override void SetStaticDefaults() {
            // Включаем хвост (шлейф)
            ProjectileID.Sets.TrailingMode[Type] = 2;
            ProjectileID.Sets.TrailCacheLength[Type] = 18; 
        }

        public override void SetDefaults() {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = -1; // Прошивает врагов
            Projectile.tileCollide = false; // Летает сквозь стены
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 300; // Исчезнет через 5 секунд
            Projectile.MaxUpdates = 2; // Плавность "дрифта"
        }

        public override void AI() {
            Time++;

            // 1. ПОИСК ЖЕРТВЫ
            NPC target = null;
            float maxDistance = 1100f;
            for (int i = 0; i < Main.maxNPCs; i++) {
                NPC npc = Main.npc[i];
                if (npc.CanBeChasedBy()) {
                    float dist = Vector2.Distance(Projectile.Center, npc.Center);
                    if (dist < maxDistance) {
                        maxDistance = dist;
                        target = npc;
                    }
                }
            }

            // 2. УМНОЕ НАВЕДЕНИЕ (Тот самый дрифт)
            if (target != null) {
                Vector2 desiredVelocity = Projectile.DirectionTo(target.Center) * 15f;
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, desiredVelocity, 0.06f);
            }

            // 3. ВИЗУАЛ
            Projectile.rotation += 0.2f;
            Lighting.AddLight(Projectile.Center, Main.hslToRgb(Hue, 1f, 0.6f).ToVector3() * 1.2f);
        }

        // РИСУЕМ РАДУЖНЫЙ ШЛЕЙФ (Крэк-эффект)
        public override bool PreDraw(ref Color lightColor) {
            Main.instance.LoadProjectile(Projectile.type);
            // Если своей картинки нет, подтянет стандартный спрайт
            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
            Vector2 drawOrigin = texture.Size() / 2f;

            for (int i = 0; i < Projectile.oldPos.Length; i++) {
                Vector2 drawPos = Projectile.oldPos[i] - Main.screenPosition + Projectile.Size / 2f;
                Color color = Main.hslToRgb((Hue + i * 0.03f) % 1f, 1f, 0.5f) * ((float)(Projectile.oldPos.Length - i) / Projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, null, color * 0.6f, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
            }
            return true;
        }
    }
}