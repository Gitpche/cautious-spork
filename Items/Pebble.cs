using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyMod.Content.Items
{
    public class Pebble : ModItem
    {
        public override void SetStaticDefaults() {
            // Описание предмета
            // "Маленький, но очень тяжелый камушек. Идеально ложится в руку."
        }

        public override void SetDefaults() {
            Item.damage = 8; // Твой урон
            Item.DamageType = DamageClass.Ranged; // Чтобы работали бонусы от Fossil Armor
            Item.width = 16;
            Item.height = 16;
            Item.useTime = 16; // Довольно быстрый бросок
            Item.useAnimation = 16;
            Item.useStyle = ItemUseStyleID.Swing; // Анимация взмаха рукой
            Item.noMelee = true; 
            Item.knockBack = 3f; // Небольшой отброс
            Item.rare = ItemRarityID.White;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true; // Можно зажать кнопку
            Item.maxStack = 9999;
            Item.consumable = true; // Камушек тратится при броске
            
            // Какой снаряд вылетает
            Item.shoot = ModContent.ProjectileType<Projectiles.PebbleShot>();
            Item.shootSpeed = 11f; // Хорошая скорость
        }

        public override void AddRecipes() {
            CreateRecipe(1)
                .AddIngredient(ItemID.StoneBlock, 1) // 1 камень = 1 камушек
                .Register();
        }
    }
}