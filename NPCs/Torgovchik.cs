using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.GameContent.Personalities;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MyMod.Content.Systems;
using MyMod.Content.Biomes;
using MyMod.Content.Items;
using MyMod.Content.Projectiles;

namespace MyMod.Content.NPCs
{
    public class Torgovchik : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 23;

            NPC.Happiness
                .SetNPCAffection(NPCID.Demolitionist, AffectionLevel.Hate)
                .SetBiomeAffection<VoidBiome>(AffectionLevel.Love)
                .SetBiomeAffection<DesertBiome>(AffectionLevel.Hate);
        }

        public override void SetDefaults()
        {
            NPC.townNPC = true;
            NPC.friendly = true;

            NPC.width = 40;
            NPC.height = 58;

            NPC.aiStyle = 7;

            NPC.damage = 0;
            NPC.defense = 15;
            NPC.lifeMax = 250;
            NPC.knockBackResist = 0.5f;
        }

        public override List<string> SetNPCNameList()
        {
            return new List<string>
            {
                "Торговчик",
                "Секретный агент №404",
                "Ультраматус",
                "Крэки4",
                "Король посылок"
            };
        }

        public override string GetChat()
        {
            return Main.rand.Next(4) switch
            {
                0 => "Разлом открылся снова.",
                1 => "Я не знаю, что это за вещи.",
                2 => "Они сами приходят ко мне.",
                _ => "Взрыватель опять минирует мои посылки..."
            };
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = "Купить";
            button2 = "Рыба";
        }

        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            if (firstButton)
            {
                shopName = "Shop";
            }
            else
            {
                GiveFish(Main.LocalPlayer);
            }
        }

        private void GiveFish(Player player)
        {
            int[] fishList = Main.anglerQuestItemNetIDs;
            if (fishList.Length == 0)
                return;

            int fishID = fishList[Main.rand.Next(fishList.Length)];

            player.QuickSpawnItem(player.GetSource_GiftOrReward(), fishID);

            if (Main.netMode != NetmodeID.Server)
                Main.NewText("Торговчик дал тебе странную рыбу...");
        }

        public override void AddShops()
        {
            new NPCShop(Type, "Shop").Register();
        }

        // 🥚 ТОЛЬКО KINDER EGG В МАГАЗИНЕ
        public override void ModifyActiveShop(string shopName, Item[] items)
        {
            if (shopName != "Shop")
                return;

            for (int i = 0; i < items.Length; i++)
                items[i].TurnToAir();

            for (int i = 0; i < items.Length; i++)
            {
                items[i].SetDefaults(ModContent.ItemType<KinderEgg>());
                items[i].stack = 1;
            }
        }

        // 🛡️ ЗАЩИТА + АТАКА
        public override void AI()
        {
            NPC.TargetClosest();

            NPC target = null;
            float distance = 350f;

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC n = Main.npc[i];

                if (!n.active || n.friendly || n.life <= 0)
                    continue;

                float dist = Vector2.Distance(NPC.Center, n.Center);

                if (dist < distance)
                {
                    distance = dist;
                    target = n;
                }
            }

            if (target != null)
            {
                Attack(target);
            }
        }

        private void Attack(NPC target)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
                return;

            Vector2 direction = target.Center - NPC.Center;
            direction.Normalize();

            float speed = 8f;

            Projectile.NewProjectile(
                NPC.GetSource_FromThis(),
                NPC.Center,
                direction * speed,
                ModContent.ProjectileType<PebbleShot>(),
                20,
                2f
            );
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;

            if (NPC.frameCounter >= 10)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += frameHeight;

                if (NPC.frame.Y >= frameHeight * 23)
                    NPC.frame.Y = 0;
            }
        }
    }
}