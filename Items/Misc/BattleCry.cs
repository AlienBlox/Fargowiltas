// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Misc.BattleCry
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Items.Misc
{
  public class BattleCry : ModItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 28;
      ((Entity) this.Item).height = 38;
      this.Item.value = Item.sellPrice(0, 0, 2, 0);
      this.Item.rare = 5;
      this.Item.useAnimation = 30;
      this.Item.useTime = 30;
      this.Item.useStyle = 5;
    }

    public virtual bool AltFunctionUse(Player player) => true;

    public static void GenerateText(bool isBattle, Player player, bool cry)
    {
      string textValue1 = Language.GetTextValue("Mods.Fargowiltas.Items.BattleCry." + (isBattle ? "Battle" : "Calming"));
      string textValue2 = Language.GetTextValue("Mods.Fargowiltas.Items.BattleCry." + (cry ? "Activated" : "Deactivated"));
      string textValue3 = Language.GetTextValue("Mods.Fargowiltas.MessageInfo.Common." + (isBattle ? "Exclamation" : "Period"));
      FargoUtils.PrintText(Language.GetTextValue("Mods.Fargowiltas.Items.BattleCry.CryText", new object[4]
      {
        (object) textValue1,
        (object) textValue2,
        (object) player.name,
        (object) textValue3
      }), isBattle ? new Color((int) byte.MaxValue, 0, 0) : new Color(0, (int) byte.MaxValue, (int) byte.MaxValue));
    }

    public static void SyncCry(Player player)
    {
      if (((Entity) player).whoAmI != Main.myPlayer || Main.netMode != 1)
        return;
      FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();
      ModPacket packet = ((ModType) modPlayer).Mod.GetPacket(256);
      ((BinaryWriter) packet).Write((byte) 8);
      ((BinaryWriter) packet).Write(((Entity) player).whoAmI);
      ((BinaryWriter) packet).Write(modPlayer.BattleCry);
      ((BinaryWriter) packet).Write(modPlayer.CalmingCry);
      packet.Send(-1, -1);
    }

    private void ToggleCry(bool isBattle, Player player, ref bool cry)
    {
      cry = !cry;
      switch (Main.netMode)
      {
        case 0:
          BattleCry.GenerateText(isBattle, player, cry);
          break;
        case 1:
          if (((Entity) player).whoAmI != Main.myPlayer)
            break;
          ModPacket packet = ((ModType) this).Mod.GetPacket(256);
          ((BinaryWriter) packet).Write((byte) 7);
          ((BinaryWriter) packet).Write(isBattle);
          ((BinaryWriter) packet).Write(((Entity) player).whoAmI);
          ((BinaryWriter) packet).Write(cry);
          packet.Send(-1, -1);
          BattleCry.SyncCry(player);
          break;
      }
    }

    public virtual bool? UseItem(Player player)
    {
      if (((Entity) player).whoAmI == Main.myPlayer)
      {
        FargoPlayer fargoPlayer = player.GetFargoPlayer();
        if (player.altFunctionUse == 2)
        {
          if (fargoPlayer.BattleCry)
            this.ToggleCry(true, player, ref fargoPlayer.BattleCry);
          this.ToggleCry(false, player, ref fargoPlayer.CalmingCry);
        }
        else
        {
          if (fargoPlayer.CalmingCry)
            this.ToggleCry(false, player, ref fargoPlayer.CalmingCry);
          this.ToggleCry(true, player, ref fargoPlayer.BattleCry);
        }
      }
      if (!Main.dedServ)
      {
        SoundStyle soundStyle = new SoundStyle("Fargowiltas/Assets/Sounds/Horn", (SoundType) 0);
        SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) player).Center), (SoundUpdateCallback) null);
      }
      return new bool?(true);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(300, 15).AddIngredient(148, 5).AddIngredient(2324, 15).AddIngredient(3117, 5).AddTile(26).Register();
    }
  }
}
