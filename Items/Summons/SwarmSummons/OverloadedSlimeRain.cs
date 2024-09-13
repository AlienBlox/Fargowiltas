// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Summons.SwarmSummons.OverloadedSlimeRain
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Items.Summons.Abom;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Items.Summons.SwarmSummons
{
  public class OverloadedSlimeRain : ModItem
  {
    public virtual string Texture => "Fargowiltas/Items/Summons/Abom/SlimyBarometer";

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.maxStack = 1;
      this.Item.value = 1000;
      this.Item.rare = 1;
      this.Item.useAnimation = 30;
      this.Item.useTime = 30;
      this.Item.useStyle = 4;
      this.Item.consumable = false;
    }

    public virtual bool? UseItem(Player player)
    {
      if (FargoWorld.OverloadedSlimeRain)
      {
        Main.StopSlimeRain(true);
        FargoWorld.OverloadedSlimeRain = false;
      }
      else
      {
        if (Main.netMode != 1)
        {
          Main.StartSlimeRain(true);
          Main.slimeWarningDelay = 1;
          Main.slimeWarningTime = 1;
          Main.slimeRainKillCount = -10000;
        }
        else
          NetMessage.SendData(61, -1, -1, (NetworkText) null, ((Entity) player).whoAmI, -1f, 0.0f, 0.0f, 0, 0, 0);
        FargoWorld.OverloadedSlimeRain = true;
        SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) player).position), (SoundUpdateCallback) null);
      }
      return new bool?(true);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(ModContent.ItemType<SlimyBarometer>(), 1).AddIngredient((Mod) null, "Overloader", 10).AddTile(125).Register();
    }
  }
}
