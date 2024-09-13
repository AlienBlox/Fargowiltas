// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Buffs.FargoGlobalBuff
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Common.Configs;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Buffs
{
  public class FargoGlobalBuff : GlobalBuff
  {
    private static bool BuffCanBeHidden(Player player, int buffIndex)
    {
      int num = player.buffTime[buffIndex];
      int index = player.buffType[buffIndex];
      return num <= 2 && (!Main.debuff[index] || index == 25) && !Main.buffNoTimeDisplay[index] && !BuffID.Sets.TimeLeftDoesNotDecrease[index];
    }

    public virtual void Update(int type, Player player, ref int buffIndex)
    {
      if (type == 257 && (double) player.GetModPlayer<FargoPlayer>().luckPotionBoost > 0.0 && player.buffTime[buffIndex] > 2)
        player.buffTime[buffIndex] = 2;
      if (((Entity) player).whoAmI != Main.myPlayer || !FargoGlobalBuff.BuffCanBeHidden(player, buffIndex) || !FargoClientConfig.Instance.HideUnlimitedBuffs || buffIndex + 1 >= player.buffType.Length)
        return;
      int buffIndex1 = buffIndex + 1;
      if (FargoGlobalBuff.BuffCanBeHidden(player, buffIndex1))
        return;
      for (int buffIndex2 = buffIndex; buffIndex2 >= 0; --buffIndex2)
      {
        bool flag = FargoGlobalBuff.BuffCanBeHidden(player, buffIndex2);
        if (!flag || buffIndex2 == 0)
        {
          if (!flag)
            ++buffIndex2;
          int num1 = player.buffType[buffIndex2];
          int num2 = player.buffTime[buffIndex2];
          player.buffType[buffIndex2] = player.buffType[buffIndex + 1];
          player.buffTime[buffIndex2] = player.buffTime[buffIndex + 1];
          player.buffType[buffIndex + 1] = num1;
          player.buffTime[buffIndex + 1] = num2;
          break;
        }
      }
    }

    public virtual bool PreDraw(
      SpriteBatch spriteBatch,
      int type,
      int buffIndex,
      ref BuffDrawParams drawParams)
    {
      return !FargoGlobalBuff.BuffCanBeHidden(Main.LocalPlayer, buffIndex) || !FargoClientConfig.Instance.HideUnlimitedBuffs;
    }
  }
}
