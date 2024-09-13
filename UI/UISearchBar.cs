﻿// Decompiled with JetBrains decompiler
// Type: Fargowiltas.UI.UISearchBar
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.Localization;
using Terraria.UI;

#nullable disable
namespace Fargowiltas.UI
{
  public class UISearchBar : UIElement
  {
    public UIPanel BackPanel;
    public string Input;
    public bool Focused;
    public int CursorBlinkTimer;
    public bool ShowCursorBlink;

    public string HintText => Language.GetTextValue("Mods.Fargowiltas.UI.SearchText");

    public bool IsEmpty => string.IsNullOrEmpty(this.Input);

    public event UISearchBar.TextChangeDelegate OnTextChange;

    public UISearchBar(int width, int height)
    {
      ((StyleDimension) ref this.Width).Set((float) width, 0.0f);
      ((StyleDimension) ref this.Height).Set((float) height, 0.0f);
      this.BackPanel = new UIPanel();
      ((StyleDimension) ref ((UIElement) this.BackPanel).Width).Set((float) width, 0.0f);
      ((StyleDimension) ref ((UIElement) this.BackPanel).Height).Set((float) height, 0.0f);
      this.BackPanel.BackgroundColor = new Color(22, 25, 55);
      ((UIElement) this.BackPanel).PaddingLeft = ((UIElement) this.BackPanel).PaddingRight = ((UIElement) this.BackPanel).PaddingTop = ((UIElement) this.BackPanel).PaddingBottom = 0.0f;
      this.Append((UIElement) this.BackPanel);
    }

    public virtual void Update(GameTime gameTime)
    {
      base.Update(gameTime);
      if (PlayerInput.Triggers.Current.MouseLeft)
      {
        if (this.ContainsPoint(Main.MouseScreen))
        {
          Main.clrInput();
          this.Focused = true;
        }
        else
          this.Focused = false;
      }
      if (PlayerInput.Triggers.Current.MouseRight)
      {
        this.Focused = true;
        this.Input = "";
      }
      if (!PlayerInput.Triggers.Current.Inventory)
        return;
      this.Focused = false;
    }

    protected virtual void DrawChildren(SpriteBatch spriteBatch)
    {
      base.DrawChildren(spriteBatch);
      PlayerInput.WritingText = this.Focused;
      Main.LocalPlayer.mouseInterface = this.Focused;
      if (this.Focused)
      {
        Main.instance.HandleIME();
        string inputText = Main.GetInputText(this.Input, false);
        if (inputText != this.Input)
        {
          UISearchBar.TextChangeDelegate onTextChange = this.OnTextChange;
          if (onTextChange != null)
            onTextChange(this.Input, inputText);
          this.Input = inputText;
        }
      }
      CalculatedStyle dimensions = this.GetDimensions();
      Vector2 vector2 = Vector2.op_Addition(((CalculatedStyle) ref dimensions).Position(), new Vector2(6f, 4f));
      string str = this.Input ?? "";
      if (string.IsNullOrEmpty(str) && !this.Focused)
        Utils.DrawBorderString(spriteBatch, this.HintText, vector2, Color.DarkGray, 1f, 0.0f, 0.0f, -1);
      if (this.Focused && ++this.CursorBlinkTimer >= 20)
      {
        this.ShowCursorBlink = !this.ShowCursorBlink;
        this.CursorBlinkTimer = 0;
      }
      if (this.Focused && this.ShowCursorBlink)
        str += "|";
      Utils.DrawBorderString(spriteBatch, str, vector2, Color.White, 1f, 0.0f, 0.0f, -1);
    }

    public delegate void TextChangeDelegate(string oldText, string currentText);
  }
}
