using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace MyMod.Content.Items
{
    public class VoidScreenShake : ModPlayer 
    {
        public int ScreenShakeTimer;
        public override void ModifyScreenPosition() 
        {
            if (ScreenShakeTimer > 0) 
            {
                Main.screenPosition += Main.rand.NextVector2Circular(15f, 15f);
                ScreenShakeTimer--;
            }
        }
    }

    public class VoidSingularity : ModItem
    {
        public override void SetDefaults() 
        {
            Item.damage = 100; 
            Item.DamageType = DamageClass.Magic;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 60; 
            Item.useAnimation = 60;
            Item.useStyle = ItemUseStyleID.HoldUp; 
            Item.noMelee = true; 
            Item.knockBack = 15f;
            Item.value = Item.buyPrice(gold: 1);
            Item.rare = ItemRarityID.Purple;
            Item.UseSound = SoundID.Item122; 
            Item.mana = 30; 
            Item.autoReuse = false;
        }

        public override bool? UseItem(Player player) 
        {
            Vector2 targetPos = Main.MouseWorld;

            for (int i = 0; i < 150; i++) 
            {
                Vector2 speed = Main.rand.NextVector2Circular(20f, 20f);
                Dust d = Dust.NewDustPerfect(targetPos, DustID.Shadowflame, speed, 0, default, 4f);
                d.noGravity = true;
            }

            float radius = 5 * 16f; // Радиус 10 блоков
            for (int k = 0; k < Main.maxNPCs; k++) 
            {
                NPC npc = Main.npc[k];
                if (npc.active && !npc.friendly && Vector2.Distance(targetPos, npc.Center) < radius) 
                {
                    NPC.HitInfo hit = new NPC.HitInfo();
                    hit.Damage = 300;
                    hit.Knockback = 15f;
                    hit.HitDirection = (npc.Center.X > targetPos.X) ? 1 : -1;
                    npc.StrikeNPC(hit);
                }
            }

            if (Main.netMode != NetmodeID.Server) 
            {
                player.GetModPlayer<VoidScreenShake>().ScreenShakeTimer = 30;
            }

            return true;
        }

        public override void AddRecipes() 
        {
            CreateRecipe()
                .AddIngredient(ItemID.DirtBlock, 10)
                .AddIngredient(ItemID.Granite, 10)
                .AddIngredient(ItemID.FallenStar, 10)
                .AddIngredient(ItemID.FragmentNebula, 40)
                .AddIngredient(ItemID.LunarBar, 10) // ДОБАВЛЕНО: 10 люминитовых слитков
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
}