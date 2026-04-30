using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;        // ← вот это нужно
using Terraria.ModLoader;
using MyMod.Content.Items; // Путь к папке с предметами

namespace MyMod.Content.Tiles // Путь в коде
{
    public class VoidstoneCrackTile : ModTile
    {
        public override void SetStaticDefaults() {
            Main.tileSolid[Type] = true; // Блок твердый
            Main.tileMergeDirt[Type] = false;
            
            RegisterItemDrop(ModContent.ItemType<VoidstoneCrackItem>());

            AddMapEntry(new Color(20, 20, 25)); // Очень темный цвет на карте

            HitSound = SoundID.Tink; // Звук удара по камню
            MinPick = 225; // Нужна кирка покрепче, чем для ванильного камня
        }
    }
}