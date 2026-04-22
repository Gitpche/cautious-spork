using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyMod.Content.Items
{
    public class ApocalypseClock : ModItem
    {
        public override void SetDefaults() {
            Item.width = 20;
            Item.height = 20;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.HoldUp; // Поднимаем часы вверх
            Item.rare = ItemRarityID.Red;
            Item.UseSound = SoundID.Item4; // Звук использования маны/магии

            Item.consumable = false; // Часы бесконечные!
        }

	public override bool? UseItem(Player player) {
    		// Используем полное имя класса вместе с пространством имен
    		player.AddBuff(ModContent.BuffType<global::MyMod.Content.Buffs.ApocalypseBuff>(), 5 * 60 * 60);
    		return true;
	}

        public override void AddRecipes() {
            CreateRecipe()
                .AddIngredient(ItemID.DirtBlock, 1)
                .Register();
        }
    }
}