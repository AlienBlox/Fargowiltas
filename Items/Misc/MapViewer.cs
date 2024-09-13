// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Misc.MapViewer
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Items.Misc
{
  public class MapViewer : ModItem
  {
    public virtual string Texture => "Terraria/Images/Map_4";

    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.value = Item.sellPrice(0, 0, 2, 0);
      this.Item.rare = 0;
      this.Item.useAnimation = 30;
      this.Item.useTime = 30;
      this.Item.useStyle = 5;
    }

    public virtual bool? UseItem(Player player)
    {
      if (Main.netMode != 1)
      {
        for (int index1 = 0; index1 < Main.maxTilesX; ++index1)
        {
          for (int index2 = 0; index2 < Main.maxTilesY; ++index2)
          {
            if (WorldGen.InWorld(index1, index2, 0))
              Main.Map.Update(index1, index2, byte.MaxValue);
          }
        }
        Main.refreshMap = true;
      }
      else
      {
        Point tileCoordinates = Utils.ToTileCoordinates(((Entity) Main.LocalPlayer).Center);
        int num = 300;
        for (int index3 = tileCoordinates.X - num / 2; index3 < tileCoordinates.X + num / 2; ++index3)
        {
          for (int index4 = tileCoordinates.Y - num / 2; index4 < tileCoordinates.Y + num / 2; ++index4)
          {
            if (WorldGen.InWorld(index3, index4, 0))
              Main.Map.Update(index3, index4, byte.MaxValue);
          }
        }
        Main.refreshMap = true;
      }
      return new bool?(true);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(893, 1).AddIngredient(37, 1).AddIngredient(237, 1).AddIngredient(43, 1).AddIngredient(544, 1).AddTile(18).Register();
    }
  }
}
