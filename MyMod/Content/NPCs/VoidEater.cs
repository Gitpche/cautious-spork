using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using MyMod.Content.Biomes;

namespace MyMod.Content.NPCs
{
    public class VoidEater : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 4;
        }

        public override void SetDefaults()
        {
            NPC.width = 64;
            NPC.height = 56;
            NPC.damage = 20;
            NPC.defense = 3;
            NPC.lifeMax = 45;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 60f;
            NPC.knockBackResist = 0.3f;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.aiStyle = 5;
            AIType = NPCID.DemonEye; // летит и кусает
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.Player.InModBiome<VoidBiome>() ? 0.2f : 0f;
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 0.15f;
            NPC.frame.Y = (int)(NPC.frameCounter % Main.npcFrameCount[NPC.type]) * frameHeight;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.MeatGrinder, 300));
        }
    }
}