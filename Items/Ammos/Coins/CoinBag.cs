// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Ammos.Coins.CoinBag
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

#nullable disable
namespace Fargowiltas.Items.Ammos.Coins
{
  internal abstract class CoinBag : BaseAmmo
  {
    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Item.notAmmo = false;
      this.Item.useStyle = 0;
      this.Item.useTime = 0;
      this.Item.useAnimation = 0;
      this.Item.createTile = -1;
      this.Item.shoot = 0;
    }
  }
}
