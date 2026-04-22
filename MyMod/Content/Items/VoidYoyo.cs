using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace MyMod.Content.Items
{
    public class VoidYoyo : ModItem
    {
        public override void SetStaticDefaults() {
            ItemID.Sets.ItemNoGravity[Type] = true; 
        }

        public override void SetDefaults() {
            Item.width = 50; 
            Item.height = 36; 
            
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.noMelee = true; 
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1;

            // ВАЖНО: Без этой строки йо-йо будет возвращаться мгновенно
            Item.channel = true; 

            Item.damage = 20;
            Item.knockBack = 4.5f;
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(gold: 2);

            // Проверь, чтобы папка и имя класса снаряда точно совпадали!
            Item.shoot = ModContent.ProjectileType<Projectiles.VoidYoyoProjectile>(); 
            Item.shootSpeed = 16f;
        }

        public override void AddRecipes() {
            CreateRecipe()
                .AddIngredient(ItemID.DemoniteBar, 12)
                .AddIngredient(ItemID.ShadowScale, 5)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}