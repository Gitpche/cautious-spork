using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using MyMod.Content.Projectiles;

namespace MyMod.Content.NPCs
{
    public class DuolingoBoss : ModNPC
    {
        public override void SetStaticDefaults() {
            // Имя в бестиарии
            Main.npcFrameCount[Type] = 1; 
            NPCID.Sets.MPAllowedEnemies[Type] = true;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
        }

        public override void SetDefaults() {
            NPC.width = 80;
            NPC.height = 80;
            NPC.damage = 100;
            NPC.defense = 50;
            NPC.lifeMax = 10000; // Твои 10к ХП
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0f;
            NPC.boss = true;
            NPC.aiStyle = -1; // Кастомный ИИ
            Music = MusicID.Boss2;
        }

        public override void AI() {
            // Поиск игрока
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active) {
                NPC.TargetClosest();
            }

            Player player = Main.player[NPC.target];

            // 1. ПЕРЕДВИЖЕНИЕ: Плавное следование над игроком
            Vector2 targetPos = player.Center + new Vector2(0, -250);
            Vector2 move = targetPos - NPC.Center;
            NPC.velocity = (NPC.velocity * 15f + move * 0.1f) / 16f;

            // 2. ЛОГИКА АТАКИ (Лиги)
            NPC.ai[0]++; // Таймер стрельбы
            if (NPC.ai[0] > 60) { // Раз в секунду
                ShootLeague(player);
                NPC.ai[0] = 0;
            }
        }

        private void ShootLeague(Player player) {
            if (Main.netMode == NetmodeID.MultiplayerClient) return;

            int type = Main.rand.Next(4);
            // Спавним лигу как NPC
            int n = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<DuoLeague>(), 0, type);
    
            if (Main.npc[n].ModNPC is DuoLeague league) {
                Main.npc[n].damage = 40 + (type * 30); // Обсидиан бьет больнее
        }
    }

        public override void OnKill() {
            // Твоя коронная фраза при смерти
            Main.NewText("делай дуолинго или я тебя сьем", Color.LimeGreen);
        }
    }
}