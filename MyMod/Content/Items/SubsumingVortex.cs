using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using MyMod.Content.Projectiles; // Убедись, что путь совпадает с твоей папкой снарядов

namespace MyMod.Content.Items
{
    public class SubsumingVortex : ModItem
    {
        public const int RightClickVortexCount = 3;
        public const float RightClickSpeedFactor = 1.3f;
        public const float RightClickDamageFactor = 0.3f;

        public override void SetStaticDefaults() {
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
        }

        public override void SetDefaults() {
            Item.width = 86;
            Item.height = 104;
            Item.damage = 460;
            Item.DamageType = DamageClass.Magic;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.shootSpeed = 7f;
            Item.mana = 22;
            Item.knockBack = 5f;

            // Указываем основной снаряд (но метод Shoot его перекроет)
            Item.shoot = ModContent.ProjectileType<ExoVortex2>(); 
            
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item84;
            Item.rare = ItemRarityID.Purple;
            Item.value = Item.buyPrice(gold: 50);
            Item.noMelee = true;
            Item.channel = true;
            Item.autoReuse = true;
        }

        public override bool AltFunctionUse(Player player) => true;

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
            
            // Получаем ID твоих кастомных снарядов напрямую из классов
            int smallVortex = ModContent.ProjectileType<ExoVortex2>();
            int bigVortex = ModContent.ProjectileType<EnormousConsumingVortex>();

            // ПРОВЕРКА: Если ПКМ (Правая кнопка мыши)
            if (player.altFunctionUse == 2) {
                // Спавним один большой вихрь
                Projectile.NewProjectile(source, position, velocity, bigVortex, damage, knockback, player.whoAmI);
            }
            // ИНАЧЕ: ЛКМ (Левая кнопка мыши)
            else {
                // Спавним 3 маленьких радужных вихря
                for (int i = 0; i < RightClickVortexCount; i++) {
                    float hue = (i / (float)(RightClickVortexCount - 1f) + Main.rand.NextFloat(0.3f)) % 1f;
                    Vector2 vortexVelocity = velocity * RightClickSpeedFactor + Main.rand.NextVector2Square(-2.5f, 2.5f);
                    
                    // Передаем hue в ai[0] для радужного эффекта
                    Projectile.NewProjectile(source, position, vortexVelocity, smallVortex, (int)(damage * RightClickDamageFactor), knockback, player.whoAmI, hue);
                }
            }

            // ВАЖНО: возвращаем false, чтобы ванильный снаряд из SetDefaults (Тайфун) не спавнился!
            return false; 
        }

        public override void AddRecipes() {
            CreateRecipe()
                .AddIngredient(ItemID.IronBar, 3)
                .AddIngredient(ItemID.DirtBlock, 1)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}