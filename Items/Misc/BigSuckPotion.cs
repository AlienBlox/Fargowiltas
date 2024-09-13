// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Misc.BigSuckPotion
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Content.Buffs;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Items.Misc
{
  public class BigSuckPotion : ModItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 20;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 14;
      ((Entity) this.Item).height = 24;
      this.Item.maxStack = 30;
      this.Item.rare = 1;
      this.Item.useStyle = 9;
      this.Item.useAnimation = 17;
      this.Item.useTime = 17;
      this.Item.consumable = true;
      this.Item.useTurn = true;
      this.Item.UseSound = new SoundStyle?(SoundID.Item3);
      this.Item.value = Item.buyPrice(0, 0, 2, 0);
    }

    public virtual bool? UseItem(Player player)
    {
      player.AddBuff(ModContent.BuffType<BigSuckBuff>(), 180, true, false);
      return new bool?(true);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(126, 1).AddIngredient(116, 1).AddIngredient(75, 1).AddTile(13).Register();
    }
  }
}
