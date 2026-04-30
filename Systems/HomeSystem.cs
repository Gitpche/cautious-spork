using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace MyMod.Content.Systems
{
    public class HomeSystem : ModSystem
    {
        public static bool HasFreeHouse;

        public override void PostUpdateWorld()
        {
            HasFreeHouse = false;

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];

                if (npc.active && npc.townNPC)
                {
                    if (npc.homeless)
                    {
                        HasFreeHouse = true;
                        break;
                    }
                }
            }
        }
    }
}