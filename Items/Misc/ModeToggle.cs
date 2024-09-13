// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Misc.ModeToggle
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Items.Misc
{
  public class ModeToggle : ModItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual string Texture => "Fargowiltas/Items/Misc/ModeToggle_0";

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 32;
      ((Entity) this.Item).height = 32;
      this.Item.value = Item.buyPrice(1, 0, 0, 0);
      this.Item.rare = 1;
      this.Item.useAnimation = 20;
      this.Item.useTime = 20;
      this.Item.useStyle = 10;
      this.Item.noUseGraphic = true;
      this.Item.consumable = false;
      this.Item.shoot = ModContent.ProjectileType<WorldTokenProj>();
    }

    public virtual bool CanUseItem(Player player)
    {
      for (int index = 0; index < Main.maxNPCs; ++index)
      {
        if (((Entity) Main.npc[index]).active && Main.npc[index].boss)
          return false;
      }
      return true;
    }

    public virtual bool? UseItem(Player player)
    {
      string str;
      switch (Main.GameMode)
      {
        case 0:
          Main.GameMode = 1;
          ModeToggle.ChangeAllPlayerDifficulty((byte) 0);
          player.difficulty = (byte) 0;
          str = DiffText("Expert");
          break;
        case 1:
          Main.GameMode = 2;
          ModeToggle.ChangeAllPlayerDifficulty((byte) 0);
          str = DiffText("Master");
          break;
        case 2:
          Main.GameMode = 3;
          ModeToggle.ChangeAllPlayerDifficulty((byte) 3);
          str = DiffText("Journey");
          break;
        default:
          Main.GameMode = 0;
          ModeToggle.ChangeAllPlayerDifficulty((byte) 0);
          str = DiffText("Normal");
          break;
      }
      switch (Main.netMode)
      {
        case 0:
          Main.NewText((object) str, new Color?(new Color(175, 75, (int) byte.MaxValue)));
          break;
        case 2:
          ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(str), new Color(175, 75, (int) byte.MaxValue), -1);
          NetMessage.SendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
          break;
      }
      SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) player).Center), (SoundUpdateCallback) null);
      return new bool?(true);

      static string DiffText(string difficulty)
      {
        return Language.GetTextValue("Mods.Fargowiltas.Items.ModeToggle." + difficulty);
      }
    }

    private static void ChangeAllPlayerDifficulty(byte diff)
    {
      for (int index = 0; index < (int) byte.MaxValue; ++index)
      {
        Player player = Main.player[index];
        if (((Entity) player).active)
          player.difficulty = diff;
      }
    }

    public virtual bool PreDrawInInventory(
      SpriteBatch spriteBatch,
      Vector2 position,
      Rectangle frame,
      Color drawColor,
      Color itemColor,
      Vector2 origin,
      float scale)
    {
      DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(34, 1);
      interpolatedStringHandler.AppendLiteral("Fargowiltas/Items/Misc/ModeToggle_");
      interpolatedStringHandler.AppendFormatted<int>(Main.GameMode);
      Texture2D texture2D = Asset<Texture2D>.op_Explicit(ModContent.Request<Texture2D>(interpolatedStringHandler.ToStringAndClear(), (AssetRequestMode) 2));
      spriteBatch.Draw(texture2D, position, new Rectangle?(frame), drawColor, 0.0f, origin, scale, (SpriteEffects) 0, 0.0f);
      return false;
    }

    public virtual bool PreDrawInWorld(
      SpriteBatch spriteBatch,
      Color lightColor,
      Color alphaColor,
      ref float rotation,
      ref float scale,
      int whoAmI)
    {
      DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(34, 1);
      interpolatedStringHandler.AppendLiteral("Fargowiltas/Items/Misc/ModeToggle_");
      interpolatedStringHandler.AppendFormatted<int>(Main.GameMode);
      Texture2D texture2D = Asset<Texture2D>.op_Explicit(ModContent.Request<Texture2D>(interpolatedStringHandler.ToStringAndClear(), (AssetRequestMode) 2));
      Vector2 vector2 = Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Item).position, Main.screenPosition), new Vector2(16f, 16f));
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, 0, 32, 32);
      spriteBatch.Draw(texture2D, vector2, new Rectangle?(rectangle), lightColor, rotation, new Vector2(16f, 16f), scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
