using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MyMod.Content.Biomes;
using Terraria.GameContent.ItemDropRules;

namespace MyMod.Content.NPCs
{
    public class VoidSlime : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 6;
        }

	public override void FindFrame(int frameHeight)
	{
    		NPC.frameCounter += 0.15f;
    		NPC.frame.Y = (int)(NPC.frameCounter % 6) * frameHeight;
	}

        public override void SetDefaults()
        {
            NPC.width = 150;
            NPC.height = 92;
            NPC.damage = 15;
            NPC.defense = 5;
            NPC.lifeMax = 60;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 50f;
            NPC.knockBackResist = 0.5f;
            NPC.aiStyle = NPCAIStyleID.Slime;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.Player.InModBiome<VoidBiome>() ? 0.3f : 0f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Gel, 1, 1, 3));
        }
    }
}