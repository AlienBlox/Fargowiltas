// Decompiled with JetBrains decompiler
// Type: Fargowilta.UIManager
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

#nullable disable
namespace Fargowilta
{
  public class UIManager
  {
    public UserInterface StatSheetUserInterface;
    public UserInterface StatSheetTogglerUserInterface;
    public StatSheetUI StatSheet;
    public StatButton StatButton;
    private GameTime _lastUpdateUIGameTime;
    public Asset<Texture2D> StatsButtonTexture;
    public Asset<Texture2D> StatsButton_MouseOverTexture;

    public void LoadUI()
    {
      if (Main.dedServ)
        return;
      this.StatsButtonTexture = ModContent.Request<Texture2D>("Fargowiltas/UI/Assets/StatsButton", (AssetRequestMode) 1);
      this.StatsButton_MouseOverTexture = ModContent.Request<Texture2D>("Fargowiltas/UI/Assets/StatsButton_MouseOver", (AssetRequestMode) 1);
      this.StatSheetUserInterface = new UserInterface();
      this.StatSheetTogglerUserInterface = new UserInterface();
      this.StatSheet = new StatSheetUI();
      ((UIElement) this.StatSheet).Activate();
      this.StatButton = new StatButton();
      ((UIElement) this.StatButton).Activate();
      this.StatSheetTogglerUserInterface.SetState((UIState) this.StatButton);
    }

    public void UpdateUI(GameTime gameTime)
    {
      this._lastUpdateUIGameTime = gameTime;
      if (!Main.playerInventory)
      {
        this.CloseStatSheet();
        this.CloseStatButton();
      }
      else
        this.OpenStatButton();
      if (this.StatSheetUserInterface?.CurrentState != null)
        this.StatSheetUserInterface.Update(gameTime);
      if (this.StatSheetTogglerUserInterface?.CurrentState == null)
        return;
      this.StatSheetTogglerUserInterface.Update(gameTime);
    }

    public bool IsStatSheetOpen() => this.StatSheetUserInterface?.CurrentState == null;

    public void CloseStatSheet() => this.StatSheetUserInterface?.SetState((UIState) null);

    public void OpenStatSheet() => this.StatSheetUserInterface.SetState((UIState) this.StatSheet);

    public void OpenStatButton()
    {
      this.StatSheetTogglerUserInterface.SetState((UIState) this.StatButton);
    }

    public void CloseStatButton() => this.StatSheetTogglerUserInterface?.SetState((UIState) null);

    public void ToggleStatSheet()
    {
      if (this.IsStatSheetOpen())
      {
        SoundEngine.PlaySound(ref SoundID.MenuOpen, new Vector2?(), (SoundUpdateCallback) null);
        this.OpenStatSheet();
      }
      else
      {
        SoundEngine.PlaySound(ref SoundID.MenuClose, new Vector2?(), (SoundUpdateCallback) null);
        this.CloseStatSheet();
      }
    }

    public void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
    {
      int index1 = layers.FindIndex((Predicate<GameInterfaceLayer>) (layer => layer.Name == "Vanilla: Inventory"));
      if (index1 != -1)
      {
        // ISSUE: method pointer
        layers.Insert(index1 - 1, (GameInterfaceLayer) new LegacyGameInterfaceLayer("Fargos: Stat Sheet", new GameInterfaceDrawMethod((object) this, __methodptr(\u003CModifyInterfaceLayers\u003Eb__15_1)), (InterfaceScaleType) 1));
      }
      int index2 = layers.FindIndex((Predicate<GameInterfaceLayer>) (layer => layer.Name == "Vanilla: Mouse Text"));
      if (index2 == -1)
        return;
      // ISSUE: method pointer
      layers.Insert(index2, (GameInterfaceLayer) new LegacyGameInterfaceLayer("Fargos: Stat Sheet Toggler", new GameInterfaceDrawMethod((object) this, __methodptr(\u003CModifyInterfaceLayers\u003Eb__15_3)), (InterfaceScaleType) 1));
    }
  }
}
