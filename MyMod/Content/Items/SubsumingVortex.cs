using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using MyMod.Content.Projectiles;

namespace MyMod.Content.Items
{
    public class SubsumingVortex : ModItem
    {
        public const int RightClickVortexCount = 3; // Количество вихрей на ЛКМ
        public const float RightClickSpeedFactor = 1.3f;
        
        // Множитель 0.5 от базового урона (100 * 0.5 = 50)
        public const float RightClickDamageFactor = 0.5f; 

        public override void SetStaticDefaults() {
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
        }

        public override void SetDefaults() {
            Item.width = 86;
            Item.height = 104;
            
            Item.damage = 60; 
            
            Item.DamageType = DamageClass.Magic;
            Item.useAnimation = 20;
            Item.useTime = 30;
            Item.shootSpeed = 7f;
            Item.mana = 40;
            Item.knockBack = 2f;

            Item.shoot = ModContent.ProjectileType<ExoVortex2>(); 
            
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item84;
            Item.rare = ItemRarityID.Purple;
            Item.value = Item.buyPrice(gold: 50);
            Item.noMelee = true;
            Item.channel = true;
            Item.autoReuse = true;
        }

        public override bool AltFunctionUse(Player player) => true;

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
            
            int smallVortex = ModContent.ProjectileType<ExoVortex2>();
            int bigVortex = ModContent.ProjectileType<EnormousConsumingVortex>();

            // ПКМ: Спавним один большой вихрь (EnormousConsumingVortex) с уроном 100
            if (player.altFunctionUse == 2) {
                Projectile.NewProjectile(source, position, velocity, bigVortex, damage, knockback, player.whoAmI);
            }
            // ЛКМ: Спавним три маленьких вихря (ExoVortex2) по 50 урона каждый
            else {
                for (int i = 0; i < RightClickVortexCount; i++) {
                    float hue = (i / (float)(RightClickVortexCount - 1f) + Main.rand.NextFloat(0.3f)) % 1f;
                    Vector2 vortexVelocity = velocity * RightClickSpeedFactor + Main.rand.NextVector2Square(-2.5f, 2.5f);
                    
                    // Расчет: 100 (damage) * 0.5 (RightClickDamageFactor) = 50 урона
                    Projectile.NewProjectile(source, position, vortexVelocity, smallVortex, (int)(damage * RightClickDamageFactor), knockback, player.whoAmI, hue);
                }
            }

            return false; 
        }

        public override void AddRecipes() {
            CreateRecipe()
                .AddIngredient(ItemID.IronBar, 3)
                .AddIngredient(ItemID.DirtBlock, 1) // Твой фирменный баланс
                .AddIngredient(ItemID.FragmentNebula, 40)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}