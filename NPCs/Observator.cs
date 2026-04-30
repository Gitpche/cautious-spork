using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using MyMod.Content.Projectiles;
using MyMod.Content.NPCs;

namespace MyMod.Content.NPCs
{
    public class Observator : ModNPC
    {
        private int attackTimer;
        private int attackMode;

        private bool phase2 = false;
        private int phase2Timer = 0;

        // 👁 ТЕКСТУРА ГЛАЗА КТУЛХУ
        public override string Texture => "Terraria/Images/NPC_5";

        public override void SetDefaults()
        {
            NPC.width = 120;
            NPC.height = 120;

            NPC.damage = 40;
            NPC.defense = 20;
            NPC.lifeMax = 25000;

            NPC.boss = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;

            NPC.value = 100000f;
        }

        public override void AI()
        {
            NPC.TargetClosest();
            Player player = Main.player[NPC.target];

            Vector2 moveTo = player.Center + new Vector2(0, -180);
            NPC.velocity = (moveTo - NPC.Center) * 0.05f;

            // 💀 ФАЗА 2
            if (!phase2 && NPC.life < NPC.lifeMax * 0.5f)
            {
                phase2 = true;
                NPC.defense += 30;
                Main.NewText("Обсерватор вошёл во вторую фазу!", 255, 50, 50);
            }

            if (phase2)
            {
                phase2Timer++;

                if (phase2Timer >= 1800) // 30 сек
                {
                    phase2Timer = 0;

                    for (int i = 0; i < 15; i++)
                    {
                        Vector2 pos = NPC.Center + new Vector2(Main.rand.Next(-120, 120), -200);

                        NPC.NewNPC(
                            NPC.GetSource_FromThis(),
                            (int)pos.X,
                            (int)pos.Y,
                            ModContent.NPCType<EvilBall>()
                        );
                    }
                }
            }

            attackTimer++;

            if (attackTimer >= 900) // 15 сек
            {
                attackTimer = 0;
                attackMode = (attackMode + 1) % 4;
            }

            switch (attackMode)
            {
                case 0:
                    LightningMode(player);
                    break;

                case 1:
                    AcidMode(player);
                    break;

                case 2:
                    LaserMode(player);
                    break;

                case 3:
                    VortexMode(player);
                    break;
            }
        }

        // 🎨 ЦВЕТ ГЛАЗА
        public override Color? GetAlpha(Color lightColor)
        {
            return attackMode switch
            {
                0 => Color.Yellow,
                1 => Color.Green,
                2 => Color.Red,
                3 => Color.Gray,
                _ => Color.White
            };
        }

        // ⚡ МОЛНИЯ
        private void LightningMode(Player player)
        {
            Vector2 dir = player.Center - NPC.Center;
            dir.Normalize();

            Projectile.NewProjectile(
                NPC.GetSource_FromThis(),
                NPC.Center,
                dir * 12f,
                ModContent.ProjectileType<LightningProjectile>(),
                30,
                2f
            );
        }

        // 🟢 КИСЛОТА
        private void AcidMode(Player player)
        {
            Vector2 dir = player.Center - NPC.Center;
            dir.Normalize();

            Projectile.NewProjectile(
                NPC.GetSource_FromThis(),
                NPC.Center,
                dir * 8f,
                ModContent.ProjectileType<AcidProjectile>(),
                25,
                1f
            );
        }

        // 🔴 ЛАЗЕР + SHADOWFLAME В ФАЗЕ 2
        private void LaserMode(Player player)
        {
            Vector2 dir = player.Center - NPC.Center;
            dir.Normalize();

            Projectile.NewProjectile(
                NPC.GetSource_FromThis(),
                NPC.Center,
                dir * 14f,
                ModContent.ProjectileType<LaserProjectile>(),
                40,
                3f
            );

            if (phase2)
                player.AddBuff(BuffID.ShadowFlame, 120);
        }

        // ⚪ ВИХРЬ + CONFUSED В ФАЗЕ 2
        private void VortexMode(Player player)
        {
            Projectile.NewProjectile(
                NPC.GetSource_FromThis(),
                NPC.Center,
                Vector2.Zero,
                ModContent.ProjectileType<VortexProjectile>(),
                35,
                2f
            );

            if (phase2)
                player.AddBuff(BuffID.Confused, 180);
        }
    }
}