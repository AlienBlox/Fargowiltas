// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Content.Buffs.Omnistation
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Items.Tiles;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Content.Buffs
{
  public class Omnistation : ModBuff
  {
    public virtual void SetStaticDefaults()
    {
      Main.buffNoSave[this.Type] = true;
      Main.buffNoTimeDisplay[this.Type] = true;
    }

    public virtual void Update(Player player, ref int buffIndex)
    {
      if (((Entity) player).whoAmI == Main.myPlayer)
      {
        player.buffImmune[ModContent.BuffType<Semistation>()] = true;
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
        player.HasGardenGnomeNearby = true;
        Main.SceneMetrics.HasCatBast = false;
        player.buffImmune[215] = true;
        Player player1 = player;
        player1.statDefense = Player.DefenseStat.op_Addition(player1.statDefense, 5);
        player.ladyBugLuckTimeLeft = 86400.0;
        player.AddBuff(192, 2, true, false);
      }
      Tile tileSafely = Framing.GetTileSafely(((Entity) player).Center);
      int num = (int) ((Tile) ref tileSafely).TileType;
      if (num != ModContent.TileType<OmnistationSheet>() && num != ModContent.TileType<OmnistationSheet2>())
        return;
      player.AddBuff(48, 1800, true, false);
    }
  }
}
