using SubworldLibrary;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using Terraria.IO;
using System.Collections.Generic;

namespace MyMod.Content.Subworlds
{
    public class FlatStoneSubworld : Subworld
    {
        public override int Width => 800;
        public override int Height => 400;

        public override bool ShouldSave => true;
        public override bool NormalUpdates => true;

        public override void OnLoad()
        {
            Main.dayTime = true;
            Main.time = 27000;
        }

        public override List<GenPass> Tasks => new()
        {
            new PassLegacy("Flat stone world", Generate)
        };

        private void Generate(GenerationProgress progress, GameConfiguration config)
        {
            progress.Message = "Генерация плоского мира...";

            for (int x = 0; x < Main.maxTilesX; x++)
            {
                for (int y = 0; y < Main.maxTilesY; y++)
                {
                    // 🌌 всё очищаем
                    Main.tile[x, y].ClearEverything();

                    // 🪨 делаем ровную землю
                    if (y == Main.maxTilesY / 2)
                    {
                        Main.tile[x, y].HasTile = true;
                        Main.tile[x, y].TileType = TileID.Stone;
                    }
                }
            }
        }
    }
}