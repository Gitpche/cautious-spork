using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyMod.Content.Items
{
    public class KinderEgg : ModItem
    {
        public override void SetDefaults() 
        {
            // Размеры спрайта
            Item.width = 26;
            Item.height = 28;
            
            Item.rare = ItemRarityID.Orange;
            Item.maxStack = 9999;
            Item.consumable = true; // Предмет тратится при использовании
            Item.value = Item.buyPrice(copper: 1);
        }

        public override bool CanRightClick() => true;

        public override void RightClick(Player player) 
        {
            // Выбираем абсолютно случайный предмет из всей игры (включая Calamity и другие моды)
            int randomID = Main.rand.Next(1, ItemLoader.ItemCount);
            
            // Спавним предмет прямо перед игроком
            player.QuickSpawnItem(player.GetSource_FromThis(), randomID, 1);
        }

        public override void AddRecipes() 
        {
            // Рецепт №1: Из земли
            CreateRecipe()
                .AddIngredient(ItemID.DirtBlock, 1)
                .AddTile(TileID.WorkBenches)
                .Register();

        }
    }
}