// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Tiles.SiblingPylon
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Items.Tiles
{
  public class SiblingPylon : ModItem
  {
    public virtual void SetDefaults()
    {
      this.Item.DefaultToPlaceableTile(ModContent.TileType<SiblingPylonTile>(), 0);
      this.Item.SetShopValues((ItemRarityColor) 1, Item.buyPrice(0, 10, 0, 0));
    }
  }
}
