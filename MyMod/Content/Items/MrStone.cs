using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace MyMod.Content.Items
{
    public class MrStone : ModItem
    {
        public override void SetDefaults() {
            Item.width = 26;
            Item.height = 20;
            Item.maxStack = 9999; // Сделаем его уникальным, раз он возвращается
            Item.bait = 25;
            Item.consumable = false; // ВОТ ОНО! Ставим false, чтобы он не тратился
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 1, 0, 0);
        }

        // Добавим щепотку логики: Мистер Стоун настолько тяжелый, 
        // что леска никогда не рвется
        public override void HoldItem(Player player) {
            player.accFishingLine = true; 
        }

	public override void AddRecipes() {
            CreateRecipe()
                .AddIngredient(ItemID.StoneBlock, 1) // ТЕСТОВЫЙ РЕЦЕПТ
                .Register();
        }
    }
}