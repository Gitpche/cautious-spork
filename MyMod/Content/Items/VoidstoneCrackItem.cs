using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MyMod.Content.Tiles; // Путь к папке с плитами

namespace MyMod.Content.Items // Путь к папке с предметами
{
    public class VoidstoneCrackItem : ModItem
    {
        public override void SetDefaults() {
            // Связываем предмет с твоим блоком
            Item.DefaultToPlaceableTile(ModContent.TileType<VoidstoneCrackTile>()); 
            Item.width = 16;
            Item.height = 16;
            Item.value = Item.sellPrice(silver: 1); // Цена продажи
        }
    }
}