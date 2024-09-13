// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Summons.Abom.MatsuriLantern
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Items.Summons.Abom
{
  public class MatsuriLantern : ModItem
  {
    public virtual void SetStaticDefaults()
    {
      Main.RegisterItemAnimation(this.Item.type, (DrawAnimation) new DrawAnimationVertical(6, 4, false));
      ItemID.Sets.AnimatesAsSoul[this.Item.type] = true;
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 3;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.maxStack = 1;
      this.Item.value = Item.sellPrice(0, 0, 2, 0);
      this.Item.rare = 1;
      this.Item.useAnimation = 30;
      this.Item.useTime = 30;
      this.Item.useStyle = 4;
      this.Item.consumable = true;
    }

    public virtual bool ConsumeItem(Player player) => false;

    public virtual bool CanUseItem(Player player) => !FargoWorld.Matsuri;

    public virtual bool? UseItem(Player player)
    {
      FargoWorld.Matsuri = true;
      FargoUtils.PrintLocalization("MessageInfo.StartLanternNight", new Color(175, 75, (int) byte.MaxValue));
      if (Main.netMode == 2)
        NetMessage.SendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
      SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) player).position), (SoundUpdateCallback) null);
      this.Item.SetDefaults(ModContent.ItemType<SpentLantern>());
      return new bool?(true);
    }
  }
}
