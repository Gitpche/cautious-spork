using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyMod.Content.NPCs
{
    public class MiniDevourerTail : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 1;
        }

        public override void SetDefaults()
        {
            NPC.width = 86;
            NPC.height = 148;
            NPC.damage = 15;
            NPC.defense = 5;
            NPC.lifeMax = 100;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.aiStyle = 6;
            AIType = NPCID.DevourerTail;
        }
    }
}