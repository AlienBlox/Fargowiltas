// Decompiled with JetBrains decompiler
// Type: Fargowiltas.UI.StatButton
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;

#nullable disable
namespace Fargowiltas.UI
{
  public class StatButton : UIState
  {
    public UIImage Icon;
    public UIHoverTextImageButton IconHighlight;

    public virtual void OnActivate()
    {
      this.Icon = new UIImage(Fargowiltas.Fargowiltas.UserInterfaceManager.StatsButtonTexture);
      ((StyleDimension) ref ((UIElement) this.Icon).Left).Set(570f, 0.0f);
      ((StyleDimension) ref ((UIElement) this.Icon).Top).Set(245f, 0.0f);
      ((UIElement) this).Append((UIElement) this.Icon);
      this.IconHighlight = new UIHoverTextImageButton(Fargowiltas.Fargowiltas.UserInterfaceManager.StatsButton_MouseOverTexture, Language.GetTextValue("Mods.Fargowiltas.UI.StatButton"));
      ((StyleDimension) ref ((UIElement) this.IconHighlight).Left).Set(-2f, 0.0f);
      ((StyleDimension) ref ((UIElement) this.IconHighlight).Top).Set(-2f, 0.0f);
      this.IconHighlight.SetVisibility(1f, 0.0f);
      // ISSUE: method pointer
      ((UIElement) this.IconHighlight).OnLeftClick += new UIElement.MouseEvent((object) this, __methodptr(IconHighlight_OnClick));
      ((UIElement) this.Icon).Append((UIElement) this.IconHighlight);
      ((UIElement) this).OnActivate();
    }

    private void IconHighlight_OnClick(UIMouseEvent evt, UIElement listeningElement)
    {
      if (!Main.playerInventory)
        return;
      Fargowiltas.Fargowiltas.UserInterfaceManager.ToggleStatSheet();
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
      if (!Main.playerInventory)
        return;
      ((UIElement) this).Draw(spriteBatch);
    }
  }
}
