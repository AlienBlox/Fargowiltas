// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Misc.KohaCrystal
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Items.Misc
{
  public class KohaCrystal : ModItem
  {
    private SoundStyle DeathFruitSound = new SoundStyle("Fargowiltas/Assets/Sounds/DeathFruit", (SoundType) 0);

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 18;
      ((Entity) this.Item).height = 18;
      this.Item.maxStack = 99;
      this.Item.rare = 1;
      this.Item.useStyle = 4;
      this.Item.useAnimation = 30;
      this.Item.useTime = 30;
      this.Item.consumable = true;
      this.Item.UseSound = new SoundStyle?(SoundID.Item27);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(109, 1).AddCondition(Condition.NearShimmer).Register();
    }

    public virtual void HoldItem(Player player)
    {
      if (player.ConsumedManaCrystals > 0)
        this.Item.UseSound = new SoundStyle?(this.DeathFruitSound);
      else
        this.Item.UseSound = new SoundStyle?(SoundID.Item27);
    }

    public virtual bool? UseItem(Player player)
    {
      if (player.ConsumedManaCrystals > 0 && player.altFunctionUse != 2)
      {
        player.ManaEffect(-20);
        --player.ConsumedManaCrystals;
      }
      return new bool?(true);
    }

    public virtual bool CanUseItem(Player player) => player.ConsumedManaCrystals > 0;
  }
}
