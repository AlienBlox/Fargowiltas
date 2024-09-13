// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Content.Buffs.Semistation
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Content.Buffs
{
  public class Semistation : ModBuff
  {
    public virtual void SetStaticDefaults()
    {
      Main.buffNoSave[this.Type] = true;
      Main.buffNoTimeDisplay[this.Type] = true;
    }

    public virtual void Update(Player player, ref int buffIndex)
    {
      if (((Entity) player).whoAmI != Main.myPlayer)
        return;
      Main.SceneMetrics.HasSunflower = false;
      player.buffImmune[146] = true;
      player.moveSpeed += 0.1f;
      player.moveSpeed *= 1.1f;
      player.sunflower = true;
      Main.SceneMetrics.HasCampfire = true;
      player.buffImmune[87] = true;
      Main.SceneMetrics.HasHeartLantern = false;
      player.buffImmune[89] = true;
      player.lifeRegen += 2;
      Main.SceneMetrics.HasStarInBottle = false;
      player.buffImmune[158] = true;
      player.manaRegenBonus += 2;
    }
  }
}
