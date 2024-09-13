// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Utilities.Extensions.PlayerExtensions
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Terraria;

#nullable disable
namespace Fargowiltas.Utilities.Extensions
{
  public static class PlayerExtensions
  {
    public static bool IsTileWithinRange(this Player player, int x, int y)
    {
      int tileBoost = player.HeldItem.tileBoost;
      int num1 = Utils.ToTileCoordinates(((Entity) player).Left).X - Player.tileRangeX - tileBoost <= x ? 1 : 0;
      bool flag1 = (double) (Utils.ToTileCoordinates(((Entity) player).Right).X + Player.tileRangeX + tileBoost) - 1.0 >= (double) x;
      bool flag2 = Utils.ToTileCoordinates(((Entity) player).Top).Y - Player.tileRangeY - tileBoost <= y;
      bool flag3 = (double) (Utils.ToTileCoordinates(((Entity) player).Bottom).Y + Player.tileRangeY + tileBoost) - 2.0 >= (double) y;
      int num2 = flag1 ? 1 : 0;
      return (num1 & num2 & (flag2 ? 1 : 0) & (flag3 ? 1 : 0)) != 0;
    }
  }
}
