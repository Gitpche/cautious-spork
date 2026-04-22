using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyMod.Content.NPCs
{
    public class MiniDevourerBody : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 1;
        }

        public override void SetDefaults()
        {
            NPC.width = 114;
            NPC.height = 88;
            NPC.damage = 20;
            NPC.defense = 5;
            NPC.lifeMax = 100;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.aiStyle = 6;
            AIType = NPCID.DevourerBody;
        }
    }
}