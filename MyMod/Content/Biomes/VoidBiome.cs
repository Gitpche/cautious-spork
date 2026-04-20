using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyMod.Content.Biomes
{
    public class VoidBiome : ModBiome
    {
        public override int Music => MusicID.OtherworldlyEerie;

        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;

        // Привязка стиля фона
        public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle 
            => ModContent.GetInstance<VoidBackgroundStyle>();

        public override bool IsBiomeActive(Player player)
        {
            return ModContent.GetInstance<VoidBiomeSystem>().VoidTileCount >= 300;
        }
    }
}
