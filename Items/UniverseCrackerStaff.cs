using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyMod.Content.Items
{
    public class UniverseCrackerStaff : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 15000;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 0;
            Item.width = 40;
            Item.height = 42;
            Item.useTime = 5;
            Item.useAnimation = 5;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.noMelee = true;
            Item.knockBack = 6f;
            Item.value = Item.buyPrice(platinum: 2);
            Item.rare = ItemRarityID.Purple;
            Item.UseSound = SoundID.Item43;
            Item.autoReuse = true;
            
            // Указываем на магический снаряд
            Item.shoot = ModContent.ProjectileType<Projectiles.UniverseCrackerShot>();
            Item.shootSpeed = 12f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.DirtBlock, 1)
		.AddIngredient(ItemID.LunarBar, 300)
		.AddIngredient(ItemID.FragmentNebula, 300)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}