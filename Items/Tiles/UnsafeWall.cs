// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Tiles.UnsafeWall
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Items.Tiles
{
  public abstract class UnsafeWall : ModItem
  {
    private readonly string name;
    private readonly int createWall;
    private readonly int wall;
    private readonly int tile;

    protected UnsafeWall(string name, int createWall, int wall = -1, int tile = -1)
    {
      this.name = name;
      this.createWall = createWall;
      this.wall = wall;
      this.tile = tile;
    }

    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 400;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 28;
      ((Entity) this.Item).height = 14;
      this.Item.rare = 0;
      this.Item.maxStack = 999;
      this.Item.useTurn = true;
      this.Item.autoReuse = true;
      this.Item.useAnimation = 15;
      this.Item.useTime = 15;
      this.Item.useStyle = 1;
      this.Item.consumable = true;
      this.Item.createWall = this.createWall;
    }

    public virtual void AddRecipes()
    {
      if (this.wall != -1)
        Recipe.Create(this.Type, 1).AddIngredient(this.wall, 1).AddTile(18).Register();
      if (this.tile == -1)
        return;
      Recipe.Create(this.Type, 4).AddIngredient(this.tile, 1).AddTile(18).Register();
    }
  }
}
