using Terraria.ModLoader;
using Microsoft.Xna.Framework; // для MathHelper

namespace MyMod.Content.Biomes
{
    public class VoidBackgroundStyle : ModSurfaceBackgroundStyle
    {
        public override void ModifyFarFades(float[] fades, float transitionSpeed)
        {
            // Плавно выключаем все остальные фоны
            for (int i = 0; i < fades.Length; i++) {
                fades[i] = MathHelper.Lerp(fades[i], 0f, transitionSpeed);
            }

            // Включаем наш фон
            int mySlot = Slot;
            fades[mySlot] = MathHelper.Lerp(fades[mySlot], 1f, transitionSpeed);
        }

        public override int ChooseFarTexture()
            => BackgroundTextureLoader.GetBackgroundSlot("MyMod/Content/Biomes/AstralFar");

        public override int ChooseMiddleTexture()
            => BackgroundTextureLoader.GetBackgroundSlot("MyMod/Content/Biomes/AstralMiddle");

        public override int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b)
            => BackgroundTextureLoader.GetBackgroundSlot("MyMod/Content/Biomes/AstralClose");
    }
}
