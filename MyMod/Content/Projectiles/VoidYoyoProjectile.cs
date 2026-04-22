using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyMod.Content.Projectiles
{
    public class VoidYoyoProjectile : ModProjectile
    {
       public override void SetStaticDefaults()
        {
            // ЧИСЛА ВПИСАНЫ ВРУЧНУЮ (ТЕРРАФОРМИНГ ЗАВЕРШЕН)
            
            // 16f — это время, которое оно провисит в воздухе (около 16 секунд)
            ProjectileID.Sets.YoyosLifeTimeMultiplier[Type] = 16f; 
            
            // 350f — это ОГРОМНЫЙ радиус (примерно 22 блока). Больше чем у любого короткого меча!
            ProjectileID.Sets.YoyosMaximumRange[Type] = 350f; 
            
            // Скорость вылета к курсору
            ProjectileID.Sets.YoyosTopSpeed[Type] = 14f; 
        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            
            // Стандартный ИИ йо-йо
            Projectile.aiStyle = ProjAIStyleID.Yoyo; 
            
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.MeleeNoSpeed;
            Projectile.scale = 1f;
        }

        // Отрисовка ниточки
        public override bool PreDrawExtras() {
            Vector2 playerCenter = Main.player[Projectile.owner].MountedCenter;
            Vector2 projectileCenter = Projectile.Center;
            Vector2 directionToPlayer = playerCenter - projectileCenter;
            float distanceToPlayer = directionToPlayer.Length();
            float rotationToPlayer = directionToPlayer.ToRotation() - MathHelper.PiOver2;

            // Используем синий цвет под искры на твоем VoidstoneCrackTile
            Color stringColor = new Color(0, 150, 255); 

            Main.EntitySpriteDraw(TextureAssets.Chain.Value, projectileCenter - Main.screenPosition, null, stringColor, rotationToPlayer, new Vector2(TextureAssets.Chain.Value.Width * 0.5f, TextureAssets.Chain.Value.Height * 0.5f), 1f, SpriteEffects.None, 0);
            return false;
        }

        public override void AI() {
            // Оставляем проверку дистанции
            if ((Projectile.position - Main.player[Projectile.owner].position).Length() > 3200f) 
                Projectile.Kill();

            // Можно добавить немного синей пыли (пылинок), чтобы оно искрилось как блоки
            if (Main.rand.NextBool(5)) {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Flare_Blue);
            }
        }
    }
}