using System.IO;
using Terraria.ModLoader;
using MyMod.Content.Systems;

namespace MyMod
{
    public class MyMod : Mod
    {
        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            MessageType msgType = (MessageType)reader.ReadByte();

            switch (msgType)
            {
                case MessageType.SyncShopSeed:
                    ShopRerollSystem.ShopSeed = reader.ReadInt32();
                    break;
            }
        }

        public enum MessageType : byte
        {
            SyncShopSeed
        }
    }
}