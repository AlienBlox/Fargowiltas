// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Items.Tiles.OmnistationSheet2
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Items.Tiles
{
  public class OmnistationSheet2 : OmnistationSheet
  {
    public override Color color => new Color(102, 116, 130);

    public virtual void MouseOver(int i, int j)
    {
      Player localPlayer = Main.LocalPlayer;
      localPlayer.noThrow = 2;
      localPlayer.cursorItemIconEnabled = true;
      localPlayer.cursorItemIconID = ModContent.ItemType<Omnistation2>();
    }

    public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
    {
      Tile tile = ((Tilemap) ref Main.tile)[i, j];
      Vector2 zero;
      // ISSUE: explicit constructor call
      ((Vector2) ref zero).\u002Ector((float) Main.offScreenRange, (float) Main.offScreenRange);
      if (Main.drawToScreen)
        zero = Vector2.Zero;
      int num = ((Tile) ref tile).TileFrameY == (short) 36 ? 18 : 16;
      Main.spriteBatch.Draw(ModContent.Request<Texture2D>(((ModTexturedType) this).Texture + "_Glow", (AssetRequestMode) 2).Value, Vector2.op_Addition(new Vector2((float) (i * 16 - (int) Main.screenPosition.X), (float) (j * 16 - (int) Main.screenPosition.Y)), zero), new Rectangle?(new Rectangle((int) ((Tile) ref tile).TileFrameX, (int) ((Tile) ref tile).TileFrameY, 16, num)), Color.White, 0.0f, Vector2.Zero, 1f, (SpriteEffects) 0, 0.0f);
    }
  }
}
