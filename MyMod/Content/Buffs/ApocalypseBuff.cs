using Terraria;
using Terraria.ModLoader;

namespace MyMod.Content.Buffs
{
    public class ApocalypseBuff : ModBuff
    {
        public override string Texture => "Terraria/Images/Buff_153"; 

        public override void SetStaticDefaults() {
            Main.buffNoTimeDisplay[Type] = false;
            // Делаем это дебаффом, чтобы его нельзя было просто отменить кликом ПКМ
            Main.debuff[Type] = true; 
        }

        public override void Update(Player player, ref int buffIndex) {
            // 1. Штраф к скорости передвижения -20%
            player.moveSpeed *= 0.8f; 

            // 2. Штраф к скорости регенерации здоровья -20%
            // В Terraria реген работает через целочисленный показатель lifeRegen
            if (player.lifeRegen > 0) {
                player.lifeRegen = (int)(player.lifeRegen * 0.8f);
            }
            
            // Добавим визуальный эффект Shadowflame, чтобы соответствовать иконке
            if (Main.rand.NextBool(5)) {
                Dust d = Dust.NewDustDirect(player.position, player.width, player.height, 27);
                d.noGravity = true;
                d.velocity *= 0.5f;
            }
        }
    }
}