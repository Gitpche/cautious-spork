using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MyMod.Content.NPCs; // Убедись, что путь к папке с NPC верный

namespace MyMod.Content.Items
{
    public class DuoPhone : ModItem
    {
        public override void SetStaticDefaults() {
            // Название и описание можно добавить в .hjson файл локализации
        }

        public override void SetDefaults() {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 20;
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.Green;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = true;
        }

        public override bool CanUseItem(Player player) {
            // Нельзя вызвать второго, если один уже жив
            return !NPC.AnyNPCs(ModContent.NPCType<DuolingoBoss>());
        }

        public override bool? UseItem(Player player) {
            if (player.whoAmI == Main.myPlayer) {
                Main.NewText("Ты пропустил урок испанского... Дуолинго уже летит за тобой!", 100, 255, 100);
                NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<DuolingoBoss>());
            }
            return true;
        }

        public override void AddRecipes() {
            CreateRecipe()
                .AddIngredient(ItemID.DirtBlock, 5)     // 5 земли
                .AddIngredient(ItemID.CobaltBar, 2)      // 2 кобальта
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}