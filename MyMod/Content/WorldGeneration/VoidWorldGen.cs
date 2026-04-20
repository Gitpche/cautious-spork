using Terraria;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using System.Collections.Generic;
using Microsoft.Xna.Framework; // для MathHelper и Point

namespace MyMod.Content.WorldGeneration
{
    public class VoidWorldGen : ModSystem
    {
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            // Вставляем наш этап после "Final Cleanup"
            int index = tasks.FindIndex(pass => pass.Name.Equals("Final Cleanup"));
            if (index != -1)
            {
                tasks.Insert(index + 1, new PassLegacy("VoidBiomeGen", GenerateVoidBiome));
            }
        }

        private void GenerateVoidBiome(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = "Создание Void биома";

            int worldWidth = Main.maxTilesX;
            int worldHeight = Main.maxTilesY;

            int rectWidth = 300;  // длина по X
            int rectHeight = 200; // ширина по Y

            // Случайная X-координата так, чтобы прямоугольник влез
            int startX = WorldGen.genRand.Next(100, worldWidth - rectWidth - 100);

            // Y — примерно поверхность
            int startY = (int)Main.worldSurface - rectHeight / 2;

            // Заполняем прямоугольник плитками
            for (int x = startX; x < startX + rectWidth; x++)
            {
                for (int y = startY; y < startY + rectHeight && y < worldHeight; y++)
                {
                    Main.tile[x, y].TileType = (ushort)ModContent.TileType<MyMod.Content.Tiles.VoidstoneCrackTile>();
                    Main.tile[x, y].HasTile = true;
                }
            }
        }
    }
}
