using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MyMod.Content.NPCs;

namespace MyMod.Content.Items
{
    public class VoidEye : ModItem
    {
        public override void SetDefaults() {
            Item.width = 30;
            Item.height = 20;
            Item.maxStack = 20;
            Item.rare = ItemRarityID.Purple;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = true;
        }

        public override bool CanUseItem(Player player) {
            return !NPC.AnyNPCs(ModContent.NPCType<VoidOverseer>());
        }

        public override bool? UseItem(Player player) {
            if (player.whoAmI == Main.myPlayer) {
                Main.NewText("Пустота вглядывается в тебя... Хранитель пустоты пробужден!", 150, 50, 250);
                NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<VoidOverseer>());
            }
            return true;
        }

        public override void AddRecipes() {
            CreateRecipe()
                .AddIngredient(ItemID.DirtBlock, 5)      // 5 земли
                .AddIngredient(ItemID.LunarBar, 2)     // 2 люминита
                .AddTile(TileID.LunarCraftingStation)    // Манипулятор Древних
                .Register();
        }
    }
}