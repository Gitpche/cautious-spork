using Terraria;
using Terraria.ModLoader;
using MyMod.Content.Buffs;

namespace MyMod
{
    public class ApocalypseGlobalNPC : GlobalNPC
    {
        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns) {
            // Если на игроке висит наш бафф
            if (player.HasBuff(ModContent.BuffType<ApocalypseBuff>())) {
                // ВНИМАНИЕ: чем МЕНЬШЕ spawnRate, тем БЫСТРЕЕ спавн.
                // 1 — это спавн почти каждый кадр.
                spawnRate = 1; 
                
                // А maxSpawns — это лимит мобов на экране. Увеличиваем в 5 раз.
                maxSpawns *= 5; 
            }
        }
    }
}