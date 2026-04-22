using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using MyMod.Content.Projectiles;

namespace MyMod.Content.Items
{
    public class ChlorophyteBall : ModItem
    {
        public override void SetDefaults() {
            Item.damage = 40; // Твой урон
            Item.DamageType = DamageClass.Magic;
            Item.width = 24;
            Item.height = 24;
            Item.useTime = 25; 
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true; 
            Item.knockBack = 5f;
            Item.value = Item.buyPrice(gold: 5);
            Item.rare = ItemRarityID.Lime;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true; 
            
            // Настройки маны и снаряда
            Item.mana = 25; // Как ты и просил — 15 маны
            Item.shoot = ModContent.ProjectileType<ChlorophyteBallProjectile>();
            Item.shootSpeed = 12f;
            
            Item.noUseGraphic = true; // Роуг-стайл (предмет исчезает из руки при броске)
        }

        public override void AddRecipes() {
            CreateRecipe()
                .AddIngredient(ItemID.ChlorophyteBar, 20) // 20 слитков хлорофита
                .AddTile(TileID.WorkBenches) // На верстаке
                .Register();
        }
    }
}