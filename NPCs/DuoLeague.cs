using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyMod.Content.NPCs
{
    public class DuoLeague : ModNPC
    {
        // 0 - Золото, 1 - Изумруд, 2 - Аметист, 3 - Обсидиан
        public int LeagueType => (int)NPC.ai[0];

        public override void SetStaticDefaults() {
            NPCID.Sets.CantTakeLunchMoney[Type] = true; // Не дропают монеты
        }

        public override void SetDefaults() {
            NPC.width = 30;
            NPC.height = 30;
            NPC.damage = 40;
            NPC.defense = 0;
            NPC.lifeMax = 500; // Те самые 500 ХП
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0.5f;
            NPC.aiStyle = -1;
        }

        public override void AI() {
            NPC.TargetClosest();
            Player player = Main.player[NPC.target];

            if (!player.active || player.dead) {
                NPC.velocity *= 0.9f; // Тормозим, если игрок умер
            return;
        }

            // 1. ОПРЕДЕЛЯЕМ ДИСТАНЦИЮ
            Vector2 vectorToPlayer = player.Center - NPC.Center;
            float distance = vectorToPlayer.Length();

            // 2. НАСТРОЙКА СКОРОСТИ (Обсидиан все еще быстрее, но в разумных пределах)
            // Золото: 3f, Изумруд: 4f, Аметист: 5f, Обсидиан: 6f
            float speedCap = 1.5f + (LeagueType * 0.5f); 
    
            // Если лига слишком близко (меньше 40 пикселей), она замедляется, чтобы не "накладываться"
            if (distance < 40f) {
                speedCap *= 0.5f; 
            }

            // 3. ПЛАВНЫЙ ПОВОРОТ (Inertia)
            // Чем выше число, тем медленнее сова поворачивает за тобой (дает шанс увернуться)
            float inertia = 20f; 
            vectorToPlayer.Normalize();
            vectorToPlayer *= speedCap;

            NPC.velocity = (NPC.velocity * (inertia - 1f) + vectorToPlayer) / inertia;

            // 4. ВИЗУАЛ (чтобы сова смотрела в сторону движения)
            NPC.rotation = NPC.velocity.X * 0.1f;
    
            // Установка цвета (белая сова красится здесь)
            NPC.color = LeagueType switch {
                0 => Color.Gold,
                1 => Color.LimeGreen,
                2 => Color.Purple,
                _ => new Color(40, 40, 40) // Обсидиан (темно-серый)
            };
    }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor) {
            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
            Vector2 drawPos = NPC.Center - screenPos;
            // Используем NPC.color для покраски белой совы
            spriteBatch.Draw(texture, drawPos, null, NPC.color, NPC.rotation, texture.Size() / 2f, NPC.scale, SpriteEffects.None, 0);
            return false;
        }
    }
}