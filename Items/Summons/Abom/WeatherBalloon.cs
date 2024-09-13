// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Summons.Abom.WeatherBalloon
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Items.Summons.Abom
{
  public class WeatherBalloon : ModItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 3;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.maxStack = 20;
      this.Item.value = Item.sellPrice(0, 0, 2, 0);
      this.Item.rare = 1;
      this.Item.useAnimation = 30;
      this.Item.useTime = 30;
      this.Item.useStyle = 5;
      this.Item.consumable = true;
    }

    public virtual bool CanUseItem(Player player) => !Main.IsItRaining && !Main.IsItStorming;

    public virtual bool? UseItem(Player player)
    {
      LanternNight.GenuineLanterns = false;
      LanternNight.ManualLanterns = false;
      Main.rainTime = (double) (86400 / 24 * 12);
      Main.raining = true;
      Main.maxRaining = Main.cloudAlpha = 0.9f;
      if (Main.netMode == 2)
      {
        NetMessage.SendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
        Main.SyncRain();
      }
      FargoUtils.PrintLocalization("MessageInfo.StartRain", new Color(175, 75, (int) byte.MaxValue));
      SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) player).position), (SoundUpdateCallback) null);
      return new bool?(true);
    }
  }
}
