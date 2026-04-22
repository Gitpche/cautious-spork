using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace MyMod.Content.Projectiles
{
    public class EnormousConsumingVortex : ModProjectile
    {
        public ref float Time => ref Projectile.ai[0];
        public bool HasBeenReleased => Projectile.ai[1] == 1f;

        public override string Texture => "Terraria/Images/Projectile_617"; // Используем вихрь Небулы

        public override void SetDefaults() {
            Projectile.width = 60;
            Projectile.height = 60;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 600;
            Projectile.hide = false; // Сделаем его видимым сразу для теста
        }

        public override void AI() {
            Player player = Main.player[Projectile.owner];

            // 1. ПРИВЯЗКА К ИГРОКУ (Пока зажата правая кнопка)
            if (!HasBeenReleased) {
                if (player.channel && !player.noItems && !player.CCed) {
                    // Вихрь следует за игроком с небольшим смещением
                    Vector2 hoverPos = player.Center + new Vector2(player.direction * 50, -30);
                    Projectile.Center = Vector2.Lerp(Projectile.Center, hoverPos, 0.1f);
                    
                    // Увеличиваем масштаб по мере зарядки
                    if (Projectile.scale < 3f) Projectile.scale += 0.02f;
                    
                    // Каждые 15 кадров спавним мелкий вихрь в ближайшего врага
                    if (Time % 15 == 0) {
                        ShootSmallVortex(player);
                    }
                    
                    Time++;
                }
                else {
                    // ОТПУСТИЛИ КНОПКУ — ЗАПУСК!
                    Projectile.ai[1] = 1f; 
                    Projectile.velocity = Projectile.DirectionTo(Main.MouseWorld) * 12f;
                    Projectile.timeLeft = 180; // Живет 3 секунды после запуска
                }
            }

            // 2. ЭФФЕКТЫ (Радужное свечение)
            Projectile.rotation += 0.1f * Projectile.scale;
            Lighting.AddLight(Projectile.Center, Color.MediumPurple.ToVector3() * Projectile.scale);

        }

        private void ShootSmallVortex(Player player) {
            NPC target = null;
            float maxDist = 800f;
            for (int i = 0; i < Main.maxNPCs; i++) {
                if (Main.npc[i].CanBeChasedBy() && Vector2.Distance(Projectile.Center, Main.npc[i].Center) < maxDist) {
                    target = Main.npc[i];
                    break;
                }
            }

            if (target != null && Main.myPlayer == Projectile.owner) {
                Vector2 vel = Projectile.DirectionTo(target.Center) * 10f;
                // Спавним твой предыдущий снаряд ExoVortex2!
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, vel, ModContent.ProjectileType<ExoVortex2>(), Projectile.damage / 2, 2f, Projectile.owner, Main.rand.NextFloat());
            }
        }

        public override bool PreDraw(ref Color lightColor) {
            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
            Vector2 drawOrigin = texture.Size() / 2f;
            
            // Рисуем несколько слоев для эффекта объема
            for (int i = 0; i < 4; i++) {
                float rotation = Projectile.rotation * (i % 2 == 0 ? 1f : -1.1f);
                Color color = Main.hslToRgb((Main.GlobalTimeWrappedHourly * 0.2f + i * 0.25f) % 1f, 1f, 0.6f) * 0.5f;
                Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, null, color, rotation, drawOrigin, Projectile.scale * (1f - i * 0.1f), SpriteEffects.None, 0);
            }
            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
            if (HasBeenReleased) {
                // При попадании после запуска — мини-взрыв
                // 3 — это радиус взрыва в блоках. Можешь поставить 5, если хочешь дыру побольше.
                Projectile.ExplodeTiles(Projectile.Center, 3, (int)Projectile.position.X / 16, (int)Projectile.position.X / 16, (int)Projectile.position.Y / 16, (int)Projectile.position.Y / 16, false); // Визуальный радиус
            }
        }
    }
}