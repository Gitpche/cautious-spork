using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using MyMod.Content.Projectiles;
using Terraria.GameContent.ItemDropRules; // Необходимо для системы дропа

namespace MyMod.Content.NPCs
{
    public class DuolingoBoss : ModNPC
    {
        public override void SetStaticDefaults() {
            Main.npcFrameCount[Type] = 1; 
            NPCID.Sets.MPAllowedEnemies[Type] = true;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
        }

        public override void SetDefaults() {
            NPC.width = 80;
            NPC.height = 80;
            NPC.damage = 100;
            NPC.defense = 50;
            NPC.lifeMax = 10000; 
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            NPC.aiStyle = -1; 
            Music = MusicID.Boss2;
        }

        public override void AI() {
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active) {
                NPC.TargetClosest();
            }

            Player player = Main.player[NPC.target];

            Vector2 targetPos = player.Center + new Vector2(0, -250);
            Vector2 move = targetPos - NPC.Center;
            NPC.velocity = (NPC.velocity * 15f + move * 0.1f) / 16f;

            NPC.ai[0]++; 
            if (NPC.ai[0] > 60) { 
                ShootLeague(player);
                NPC.ai[0] = 0;
            }
        }

        private void ShootLeague(Player player) {
            if (Main.netMode == NetmodeID.MultiplayerClient) return;

            int type = Main.rand.Next(4);
            int n = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<DuoLeague>(), 0, type);
    
            if (Main.npc[n].ModNPC is DuoLeague league) {
                Main.npc[n].damage = 40 + (type * 30); 
            }
        }

        // НОВЫЙ МЕТОД ДЛЯ ДРОПА
        public override void ModifyNPCLoot(NPCLoot npcLoot) {
            // Гарантированный дроп (шанс 1 из 1) по 20 штук каждого ресурса
            npcLoot.Add(ItemDropRule.Common(ItemID.Obsidian, 1, 20, 20));     // 20 обсидиана
            npcLoot.Add(ItemDropRule.Common(ItemID.GoldBar, 1, 20, 20));      // 20 золотых слитков
            npcLoot.Add(ItemDropRule.Common(ItemID.Emerald, 1, 20, 20));      // 20 изумрудов
            npcLoot.Add(ItemDropRule.Common(ItemID.Amethyst, 1, 20, 20));     // 20 аметистов
        }

        public override void OnKill() {
            Main.NewText("делай дуолинго или я тебя сьем", Color.LimeGreen);
        }
    }
}