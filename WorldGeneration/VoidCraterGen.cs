using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using Terraria.GameContent.Generation;
using Terraria.IO;
using MyMod.Content.Tiles;

namespace MyMod.Content.WorldGeneration
{
    public class VoidCraterGen : ModSystem
    {
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {
            tasks.Add(new PassLegacy("Void Crater", GenerateVoidCrater));
        }

        private void GenerateVoidCrater(GenerationProgress progress, GameConfiguration config)
        {
            progress.Message = "Void awakens...";

            int x = WorldGen.genRand.Next(300, Main.maxTilesX - 300);

            int y = 10;
            while (y < (int)Main.worldSurface + 50)
            {
                if (WorldGen.InWorld(x, y) && Main.tile[x, y].HasTile)
                    break;
                y++;
            }

            int radiusX = 60;
            int radiusY = 35;
            int holeX = 35;
            int holeY = 80;

            for (int i = x - radiusX; i <= x + radiusX; i++)
            {
                for (int j = y - radiusY; j <= y + radiusY; j++)
                {
                    if (!WorldGen.InWorld(i, j)) continue;

                    float dist = ((float)(i - x) * (i - x)) / (radiusX * radiusX)
                               + ((float)(j - y) * (j - y)) / (radiusY * radiusY);

                    if (dist <= 1f)
                    {
                        Tile tile = Main.tile[i, j];
                        tile.HasTile = true;
                        tile.TileType = (ushort)ModContent.TileType<VoidstoneCrackTile>();
                    }
                }
            }

            for (int i = x - holeX; i <= x + holeX; i++)
            {
                for (int j = y - radiusY; j <= y + holeY; j++)
                {
                    if (!WorldGen.InWorld(i, j)) continue;

                    float dist = ((float)(i - x) * (i - x)) / (holeX * holeX)
                               + ((float)(j - y) * (j - y)) / (holeY * holeY);

                    if (dist <= 1f)
                    {
                        Tile tile = Main.tile[i, j];
                        tile.HasTile = false;
                    }
                }
            }

            for (int i = x - radiusX; i <= x + radiusX; i++)
            {
                for (int j = y - radiusY; j <= y + holeY; j++)
                {
                    if (!WorldGen.InWorld(i, j)) continue;
                    WorldGen.SquareTileFrame(i, j);
                }
            }
        }
    }
}