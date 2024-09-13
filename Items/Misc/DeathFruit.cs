// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Misc.DeathFruit
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
  public class DeathFruit : ModItem
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
      this.CreateRecipe(1).AddIngredient(1291, 1).AddCondition(Condition.NearShimmer).Register();
    }

    public virtual bool AltFunctionUse(Player player) => true;

    public virtual bool CanUseItem(Player player)
    {
      return (this.CanUse(player) || player.altFunctionUse == 2) && (this.CanUse(player, true) || player.altFunctionUse != 2);
    }

    public virtual void HoldItem(Player player)
    {
      if (player.ConsumedLifeCrystals > 0)
        this.Item.UseSound = new SoundStyle?(this.DeathFruitSound);
      else
        this.Item.UseSound = new SoundStyle?(SoundID.Item27);
    }

    public virtual bool? UseItem(Player player)
    {
      if (player.ConsumedLifeFruit > 0)
      {
        if (player.altFunctionUse != 2)
          --player.ConsumedLifeFruit;
      }
      else if (player.ConsumedLifeCrystals > 0)
      {
        if (player.altFunctionUse != 2)
          --player.ConsumedLifeCrystals;
      }
      else
      {
        int num;
        if (player.altFunctionUse == 2)
        {
          if (!this.CanUse(player, true))
            return new bool?(false);
          num = 20;
          if (player.GetModPlayer<FargoPlayer>().DeathFruitHealth < 20)
            num = player.GetModPlayer<FargoPlayer>().DeathFruitHealth;
        }
        else
        {
          if (!this.CanUse(player))
            return new bool?(false);
          num = -20;
        }
        player.GetModPlayer<FargoPlayer>().DeathFruitHealth -= num;
        if (player.statLife > -num)
        {
          player.statLife += num;
          if (Main.myPlayer == ((Entity) player).whoAmI)
            player.HealEffect(num, true);
        }
      }
      return new bool?(true);
    }

    private bool CanUse(Player player, bool rightClick = false)
    {
      return !rightClick && this.GetLife(player) > 20 || rightClick && player.GetModPlayer<FargoPlayer>().DeathFruitHealth > 0;
    }

    private int GetLife(Player player) => player.statLifeMax - player.ConsumedLifeFruit * 5;
  }
}
