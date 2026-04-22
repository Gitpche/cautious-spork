using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using MyMod.Content.Biomes;

namespace MyMod.Content.NPCs
{
    public class MiniDevourer : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 1;
            NPCID.Sets.MPAllowedEnemies[NPC.type] = true;
        }

        public override void SetDefaults()
        {
            NPC.width = 134;
            NPC.height = 196;
            NPC.damage = 30;
            NPC.defense = 5;
            NPC.lifeMax = 100;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.value = 100f;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.aiStyle = 6; // червь
            AIType = NPCID.DevourerHead;
        }

        public override void AI()
        {
            // спавним сегменты при первом появлении
            if (NPC.ai[0] == 0)
            {
                NPC.ai[0] = 1;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    int prev = NPC.whoAmI;
                    for (int i = 0; i < 3; i++)
                    {
                        int body = NPC.NewNPC(NPC.GetSource_FromAI(),
                            (int)NPC.Center.X, (int)NPC.Center.Y,
                            ModContent.NPCType<MiniDevourerBody>(), 0, 0, 0, 0, prev);
                        Main.npc[body].ai[3] = NPC.whoAmI;
                        prev = body;
                    }
                    int tail = NPC.NewNPC(NPC.GetSource_FromAI(),
                        (int)NPC.Center.X, (int)NPC.Center.Y,
                        ModContent.NPCType<MiniDevourerTail>(), 0, 0, 0, 0, prev);
                    Main.npc[tail].ai[3] = NPC.whoAmI;
                }
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.Player.InModBiome<VoidBiome>() ? 0.1f : 0f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Gel, 1, 5, 10));
        }
    }
}