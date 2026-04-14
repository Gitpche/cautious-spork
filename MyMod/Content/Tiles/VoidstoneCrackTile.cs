using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace MyVoidMod.Tiles // Путь в коде
{
    public class VoidstoneCrackTile : ModTile
    {
        public override void SetStaticDefaults() {
            Main.tileSolid[Type] = true; // Блок твердый
            Main.tileMergeDirt[Type] = false; // Камень обычно не сливается с землей

            AddMapEntry(new Color(20, 20, 25)); // Очень темный цвет на карте

            HitSound = SoundID.Tink; // Звук удара по камню
            MinPick = 100; // Нужна кирка покрепче, чем для ванильного камня
        }
    }
}