using Terraria.ModLoader;
using System;
using MyMod.Content.Tiles; // Убедись, что путь к твоим плиткам верный

namespace MyMod.Content.Biomes
{
    public class VoidBiomeSystem : ModSystem
    {
        public int VoidTileCount;

        public override void TileCountsAvailable(ReadOnlySpan<int> tileCounts) {
            VoidTileCount = tileCounts[ModContent.TileType<VoidstoneCrackTile>()];
        }
    }
}
