using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace MyMod.Content.Items
{
    public class SunOfNoReturn : ModItem
    {
        public override void SetDefaults() {
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.rare = ItemRarityID.Red;
            Item.UseSound = SoundID.Item44;
            Item.consumable = false;
        }

        public override bool? UseItem(Player player) {
            if (Main.myPlayer == player.whoAmI) {
                Main.NewText("Вы пробудили Солнце. Пути назад не будет!", 255, 150, 0);
                SunSystem.SunActive = true;
                SunSystem.Instance.RecordInitialHellstone();
            }
            return true;
        }

        public override void AddRecipes() {
            CreateRecipe()
                .AddIngredient(ItemID.HellstoneBar, 20)
                .AddIngredient(ItemID.FragmentSolar, 10)
                .AddIngredient(ItemID.DirtBlock, 1)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }

    public class SunSystem : ModSystem
    {
        public static SunSystem Instance;
        public static bool SunActive = false;
        private int sunTimer = 0;
        private int totalNonHellstone = 0; // сколько не-хеллстон блоков было при активации
        private int convertedCount = 0;    // сколько мы уже превратили

        public override void OnModLoad() {
            Instance = this;
        }

        public override void OnWorldLoad() {
            SunActive = false;
            sunTimer = 0;
            totalNonHellstone = 0;
            convertedCount = 0;
        }

        // Считаем не-хеллстон блоки в момент активации предмета
        public void RecordInitialHellstone() {
            totalNonHellstone = 0;
            convertedCount = 0;
            for (int x = 0; x < Main.maxTilesX; x++) {
                for (int y = 0; y < Main.maxTilesY; y++) {
                    if (Main.tile[x, y] != null && Main.tile[x, y].HasTile
                        && Main.tile[x, y].TileType != TileID.Hellstone) {
                        totalNonHellstone++;
                    }
                }
            }
        }

        public override void PostUpdateEverything() {
            if (!SunActive) return;

            sunTimer++;
            if (sunTimer >= 300) {
                sunTimer = 0;
                int x = Main.rand.Next(0, Main.maxTilesX);
                int y = Main.rand.Next(0, Main.maxTilesY);

                if (Main.tile[x, y] != null && Main.tile[x, y].HasTile) {
                    if (Main.tile[x, y].TileType != TileID.Hellstone) {
                        convertedCount++;
                    }
                    Main.tile[x, y].TileType = TileID.Hellstone;
                    WorldGen.SquareTileFrame(x, y);
                    NetMessage.SendTileSquare(-1, x, y, 1);
                }

                if (totalNonHellstone > 0 && convertedCount >= totalNonHellstone) {
                    Main.NewText("Мир поглощён Солнцем. Здесь больше нет жизни.", 255, 0, 0);
                    SunActive = false;
                }
            }
        }
    }
}