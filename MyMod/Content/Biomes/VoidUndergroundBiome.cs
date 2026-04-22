using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyMod.Content.Biomes
{
    public class VoidUndergroundBiome : ModBiome
    {
        public override int Music => MusicID.OtherworldlyEerie;

        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;

        public override ModUndergroundBackgroundStyle UndergroundBackgroundStyle
            => ModContent.GetInstance<VoidUndergroundBackgroundStyle>();

        public override bool IsBiomeActive(Player player)
        {
            return (player.ZoneRockLayerHeight || player.ZoneDirtLayerHeight)
                && ModContent.GetInstance<VoidBiomeSystem>().VoidTileCount >= 300;
        }
    }
}