// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Summons.Abom.SpentLantern
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Items.Summons.Abom
{
  public class SpentLantern : MatsuriLantern
  {
    public override void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 3;
    }

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Item.consumable = false;
    }

    public override bool CanUseItem(Player player) => FargoWorld.Matsuri;

    public override bool? UseItem(Player player)
    {
      FargoWorld.Matsuri = false;
      FargoUtils.PrintLocalization("MessageInfo.StopLanternNight", new Color(175, 75, (int) byte.MaxValue));
      if (Main.netMode == 2)
        NetMessage.SendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
      SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) player).position), (SoundUpdateCallback) null);
      return new bool?(true);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(ModContent.ItemType<MatsuriLantern>(), 1).AddCondition(Condition.NearWater).Register();
    }
  }
}
