// Decompiled with JetBrains decompiler
// Type: Fargowiltas.InputManager
// Assembly: Fargowiltas, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D54AAE1B-FAA8-4FB5-AF8B-AFF4A04833B1
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Common.Configs;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas
{
  public class InputManager : ModPlayer
  {
    public int latestXDirPressed = 0;
    public int latestXDirReleased = 0;
    private bool LeftLastPressed = false;
    private bool RightLastPressed = false;
    private int lastSetBonusTimer = 0;

    public virtual void ProcessTriggers(TriggersSet triggersSet)
    {
      int index = Main.ReversedUpDownArmorSetBonuses ? 1 : 0;
      if (Fargowiltas.Fargowiltas.SetBonusKey.JustPressed)
        Main.LocalPlayer.KeyDoubleTap(index);
      if (Fargowiltas.Fargowiltas.SetBonusKey.Current)
      {
        if (Main.LocalPlayer.holdDownCardinalTimer[index] != this.lastSetBonusTimer + 1)
          ++Main.LocalPlayer.holdDownCardinalTimer[index];
        Main.LocalPlayer.KeyHoldDown(index, Main.LocalPlayer.holdDownCardinalTimer[index]);
        this.lastSetBonusTimer = Main.LocalPlayer.holdDownCardinalTimer[index];
      }
      else if (FargoClientConfig.Instance.DoubleTapSetBonusDisabled)
        Main.LocalPlayer.doubleTapCardinalTimer[0] = Main.LocalPlayer.doubleTapCardinalTimer[1] = 0;
      if (Main.LocalPlayer.controlLeft && !this.LeftLastPressed)
        this.latestXDirPressed = -1;
      if (Main.LocalPlayer.controlRight && !this.RightLastPressed)
        this.latestXDirPressed = 1;
      if (!Main.LocalPlayer.controlLeft && !Main.LocalPlayer.releaseLeft)
        this.latestXDirReleased = -1;
      if (!Main.LocalPlayer.controlRight && !Main.LocalPlayer.releaseRight)
        this.latestXDirReleased = 1;
      this.LeftLastPressed = Main.LocalPlayer.controlLeft;
      this.RightLastPressed = Main.LocalPlayer.controlRight;
    }
  }
}
