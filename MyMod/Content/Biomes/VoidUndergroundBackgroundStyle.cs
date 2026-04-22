using Terraria.ModLoader;

namespace MyMod.Content.Biomes
{
    public class VoidUndergroundBackgroundStyle : ModUndergroundBackgroundStyle
    {
        public override void FillTextureArray(int[] textureSlots)
        {
            textureSlots[0] = BackgroundTextureLoader.GetBackgroundSlot("MyMod/Content/Backgrounds/VoidUg");
            textureSlots[1] = BackgroundTextureLoader.GetBackgroundSlot("MyMod/Content/Backgrounds/VoidUg");
            textureSlots[2] = BackgroundTextureLoader.GetBackgroundSlot("MyMod/Content/Backgrounds/VoidUg");
            textureSlots[3] = BackgroundTextureLoader.GetBackgroundSlot("MyMod/Content/Backgrounds/VoidUg");
        }
    }
}