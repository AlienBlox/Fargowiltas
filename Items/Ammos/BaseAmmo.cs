// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Ammos.BaseAmmo
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Items.Ammos.Rockets;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Items.Ammos
{
  public abstract class BaseAmmo : ModItem
  {
    public abstract int AmmunitionItem { get; }

    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Item.CloneDefaults(this.AmmunitionItem);
      ((Entity) this.Item).width = 26;
      ((Entity) this.Item).height = 26;
      this.Item.consumable = false;
      this.Item.maxStack = 1;
      this.Item.value *= 3996;
      ++this.Item.rare;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(this.Item.type == ModContent.ItemType<MiniNuke1Box>() || this.Item.type == ModContent.ItemType<MiniNuke2Box>() ? 2 : 1).AddIngredient(this.AmmunitionItem, 3996).AddTile(125).Register();
    }
  }
}
