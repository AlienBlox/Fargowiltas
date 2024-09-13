// Decompiled with JetBrains decompiler
// Type: Fargowiltas.UI.UIHoverTextImageButton
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

#nullable disable
namespace Fargowiltas.UI
{
  public class UIHoverTextImageButton : UIImageButton
  {
    public readonly string Text;

    public UIHoverTextImageButton(Asset<Texture2D> texture, string text)
      : base(texture)
    {
      this.Text = text;
    }

    protected virtual void DrawSelf(SpriteBatch spriteBatch)
    {
      base.DrawSelf(spriteBatch);
      if (!((UIElement) this).IsMouseHovering)
        return;
      Main.LocalPlayer.mouseInterface = true;
      Main.hoverItemName = this.Text;
    }
  }
}
