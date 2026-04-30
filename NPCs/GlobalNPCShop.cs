using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyMod.Content.NPCs
{
    public class GlobalNPCShop : GlobalNPC
    {
        public override void ModifyActiveShop(NPC npc, string shopName, Item[] items)
        {
            // --- 1. ТОРГОВЕЦ (Merchant) ---
            if (npc.type == NPCID.Merchant)
            {
                Item wings = new Item();
                wings.SetDefaults(ItemID.CreativeWings);
                wings.shopCustomPrice = 100000; 

                Item conch = new Item();
                conch.SetDefaults(ItemID.MagicConch);
                conch.shopCustomPrice = 10000;

                AddToShop(items, wings);
                AddToShop(items, conch);
            }

            // --- 2. ОРУЖЕЙНИК (Arms Dealer) ---
            if (npc.type == NPCID.ArmsDealer)
            {
                if (NPC.downedBoss3)
                {
                    Item phoenix = new Item();
                    phoenix.SetDefaults(ItemID.PhoenixBlaster);
                    phoenix.shopCustomPrice = 150000; 
                    AddToShop(items, phoenix);
                }
            }

            // --- 3. ВОЛШЕБНИК (Wizard) ---
            if (npc.type == NPCID.Wizard)
            {
                Item frostStaff = new Item();
                frostStaff.SetDefaults(ItemID.FrostStaff);
                frostStaff.shopCustomPrice = 200000;

                AddToShop(items, frostStaff);
            }
         } // <--- ВОТ ЭТОЙ СКОБКИ НЕ ХВАТАЛО (закрывает метод ModifyActiveShop)

        private void AddToShop(Item[] items, Item newItem)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == null || items[i].type == ItemID.None)
                {
                    items[i] = newItem;
                    break;
                }
            }
        }
    } 
}