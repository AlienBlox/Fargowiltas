// Decompiled with JetBrains decompiler
// Type: Fargowiltas.UI.StatSheetUI
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Items.Misc;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

#nullable disable
namespace Fargowiltas.UI
{
  public class StatSheetUI : UIState
  {
    public int BackWidth = 650;
    public int BackHeight = 380;
    public const int HowManyPerColumn = 14;
    public const int HowManyColumns = 2;
    public int LineCounter;
    public int ColumnCounter;
    public UISearchBar SearchBar;
    public UIDragablePanel BackPanel;
    public UIPanel InnerPanel;

    public virtual void OnInitialize()
    {
      Vector2 vector2;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2).\u002Ector((float) (Main.screenWidth / 2) - (float) this.BackWidth * 0.75f, (float) (Main.screenHeight / 2) - (float) this.BackHeight * 0.75f);
      this.BackPanel = new UIDragablePanel();
      ((StyleDimension) ref ((UIElement) this.BackPanel).Left).Set(vector2.X, 0.0f);
      ((StyleDimension) ref ((UIElement) this.BackPanel).Top).Set(vector2.Y, 0.0f);
      ((StyleDimension) ref ((UIElement) this.BackPanel).Width).Set((float) this.BackWidth, 0.0f);
      ((StyleDimension) ref ((UIElement) this.BackPanel).Height).Set((float) this.BackHeight, 0.0f);
      ((UIElement) this.BackPanel).PaddingLeft = ((UIElement) this.BackPanel).PaddingRight = ((UIElement) this.BackPanel).PaddingTop = ((UIElement) this.BackPanel).PaddingBottom = 0.0f;
      this.BackPanel.BackgroundColor = Color.op_Multiply(new Color(29, 33, 70), 0.7f);
      ((UIElement) this).Append((UIElement) this.BackPanel);
      this.SearchBar = new UISearchBar(this.BackWidth - 8, 26);
      ((StyleDimension) ref this.SearchBar.Left).Set(4f, 0.0f);
      ((StyleDimension) ref this.SearchBar.Top).Set(6f, 0.0f);
      ((UIElement) this.BackPanel).Append((UIElement) this.SearchBar);
      this.InnerPanel = new UIPanel();
      ((StyleDimension) ref ((UIElement) this.InnerPanel).Left).Set(6f, 0.0f);
      ((StyleDimension) ref ((UIElement) this.InnerPanel).Top).Set(34f, 0.0f);
      ((StyleDimension) ref ((UIElement) this.InnerPanel).Width).Set((float) (this.BackWidth - 12), 0.0f);
      ((StyleDimension) ref ((UIElement) this.InnerPanel).Height).Set((float) (this.BackHeight - 12 - 28), 0.0f);
      ((UIElement) this.InnerPanel).PaddingLeft = ((UIElement) this.InnerPanel).PaddingRight = ((UIElement) this.InnerPanel).PaddingTop = ((UIElement) this.InnerPanel).PaddingBottom = 0.0f;
      this.InnerPanel.BackgroundColor = Color.op_Multiply(new Color(73, 94, 171), 0.9f);
      ((UIElement) this.BackPanel).Append((UIElement) this.InnerPanel);
      ((UIElement) this).OnInitialize();
    }

    public virtual void Update(GameTime gameTime)
    {
      ((UIElement) this).Update(gameTime);
      if ((long) Main.GameUpdateCount % (!this.SearchBar.IsEmpty ? 2L : 4L) != 0L)
        return;
      this.RebuildStatList();
    }

    public void RebuildStatList()
    {
      Player player = Main.LocalPlayer;
      FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();
      ((UIElement) this.InnerPanel).RemoveAllChildren();
      this.ColumnCounter = this.LineCounter = 0;
      this.AddStat("MeleeDamage", 3508, (object) Damage(DamageClass.Melee));
      this.AddStat("MeleeCritical", 3508, (object) Crit(DamageClass.Melee));
      this.AddStat("MeleeSpeed", 3508, (object) (int) Math.Round((double) player.GetAttackSpeed(DamageClass.Melee) * 100.0));
      this.AddStat("RangedDamage", 3504, (object) Damage(DamageClass.Ranged));
      this.AddStat("RangedCritical", 3504, (object) Crit(DamageClass.Ranged));
      this.AddStat("MagicDamage", 3069, (object) Damage(DamageClass.Magic));
      this.AddStat("MagicCritical", 3069, (object) Crit(DamageClass.Magic));
      this.AddStat("ManaCostReduction", 3069, (object) Math.Round((1.0 - (double) player.manaCost) * 100.0));
      this.AddStat("SummonDamage", 1309, (object) Damage(DamageClass.Summon));
      if (Fargowiltas.Fargowiltas.ModLoaded["FargowiltasSouls"])
        this.AddStat("SummonCritical", 1309, (object) (int) Terraria.ModLoader.ModLoader.GetMod("FargowiltasSouls").Call(new object[1]
        {
          (object) "GetSummonCrit"
        }));
      else
        this.AddStat("");
      this.AddStat("MaxMinions", 1309, (object) player.maxMinions);
      this.AddStat("MaxSentries", 1309, (object) player.maxTurrets);
      this.AddStat("ArmorPenetration", 3212, (object) player.GetArmorPenetration(DamageClass.Generic));
      this.AddStat("Aggro", 3016, (object) player.aggro);
      this.AddStat("Life", 29, (object) player.statLifeMax2);
      this.AddStat("LifeRegen", 49, (object) (player.lifeRegen / 2));
      this.AddStat("Mana", 109, (object) player.statManaMax2);
      this.AddStat("ManaRegen", 109, (object) (player.manaRegen / 2));
      this.AddStat("Defense", 156, (object) player.statDefense);
      float num = 100f;
      Mod mod;
      if (Terraria.ModLoader.ModLoader.TryGetMod("FargowiltasSouls", ref mod) && mod.Version >= Version.Parse("1.6.1"))
      {
        if ((bool) mod.Call(new object[1]
        {
          (object) "EternityMode"
        }))
          num = 75f;
      }
      string textValue = (double) num < 100.0 ? Language.GetTextValue("Mods.Fargowiltas.UI.DRCap", (object) num) : "";
      this.AddStat("DamageReduction", 3224, (object) Math.Round((double) player.endurance * 100.0), (object) textValue);
      this.AddStat("Luck", 8, (object) Math.Round((double) player.luck, 2));
      this.AddStat("FishingQuests", 2374, (object) player.anglerQuestsFinished);
      this.AddStat("BattleCry", ModContent.ItemType<BattleCry>(), modPlayer.BattleCry ? (object) ("[c/ff0000:" + Language.GetTextValue("Mods.Fargowiltas.Items.BattleCry.Battle") + "]") : (modPlayer.CalmingCry ? (object) ("[c/00ffff:" + Language.GetTextValue("Mods.Fargowiltas.Items.BattleCry.Calming") + "]") : (object) Language.GetTextValue("Mods.Fargowiltas.UI.BattleCryNone")));
      this.AddStat("MaxSpeed", 54, (object) (int) (((double) player.accRunSpeed + (double) player.maxRunSpeed) / 2.0 * (double) player.moveSpeed * 3.0));
      this.AddStat("WingTime", 493, player.wingTimeMax / 60 > 60 || player.empressBrooch && !Fargowiltas.Fargowiltas.ModLoaded["CalamityMod"] ? (object) Language.GetTextValue("Mods.Fargowiltas.UI.WingTimeMoreThan60Sec") : (object) Language.GetTextValue("Mods.Fargowiltas.UI.WingTimeActual", (object) RenderWingStat(Math.Round((double) player.wingTimeMax / 60.0, 2))));
      this.AddStat("WingMaxSpeed", 493, (object) RenderWingStat(Math.Round((double) modPlayer.StatSheetWingSpeed * 32.0 / 6.25)));
      this.AddStat("WingAscentModifier", 493, (object) RenderWingStat(Math.Round((double) modPlayer.StatSheetMaxAscentMultiplier * 100.0)));
      this.AddStat("WingHover", 493, !modPlayer.CanHover.HasValue ? (object) Language.GetTextValue("Mods.Fargowiltas.UI.WingNull") : (modPlayer.CanHover.Value ? (object) Language.GetTextValue("Mods.Fargowiltas.UI.WingHoverTrue") : (object) Language.GetTextValue("Mods.Fargowiltas.UI.WingHoverFalse")));
      foreach (StatSheetUI.Stat modStat in Fargowiltas.Fargowiltas.Instance.ModStats)
        this.AddStat(modStat.TextFunction(), modStat.ItemID);

      double Damage(DamageClass damageClass)
      {
        StatModifier totalDamage = player.GetTotalDamage(damageClass);
        double additive = (double) ((StatModifier) ref totalDamage).Additive;
        totalDamage = player.GetTotalDamage(damageClass);
        double multiplicative = (double) ((StatModifier) ref totalDamage).Multiplicative;
        return Math.Round(additive * multiplicative * 100.0 - 100.0);
      }

      int Crit(DamageClass damageClass) => (int) player.GetTotalCritChance(damageClass);

      static string RenderWingStat(double stat)
      {
        return stat > 0.0 ? stat.ToString() : Language.GetTextValue("Mods.Fargowiltas.UI.WingNull");
      }
    }

    public void AddStat(string key, int item = -1, params object[] args)
    {
      this.AddStat(Language.GetTextValue("Mods.Fargowiltas.UI." + key, args), item);
    }

    public void AddStat(string text, int item = -1)
    {
      int num1 = 8 + this.ColumnCounter * ((this.BackWidth - 8) / 2);
      int num2 = 8 + this.LineCounter * 23;
      this.BackHeight = 25 * (this.LineCounter + 1) + 26 + 4;
      if (++this.ColumnCounter == 2)
      {
        ++this.LineCounter;
        this.ColumnCounter = 0;
      }
      string str;
      if (item <= -1)
      {
        str = text;
      }
      else
      {
        DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 2);
        interpolatedStringHandler.AppendLiteral("[i:");
        interpolatedStringHandler.AppendFormatted<int>(item);
        interpolatedStringHandler.AppendLiteral("] ");
        interpolatedStringHandler.AppendFormatted(text);
        str = interpolatedStringHandler.ToStringAndClear();
      }
      UIText uiText = new UIText(str, 1f, false);
      ((StyleDimension) ref ((UIElement) uiText).Left).Set((float) num1, 0.0f);
      ((StyleDimension) ref ((UIElement) uiText).Top).Set((float) num2, 0.0f);
      string[] source = text.Split(' ', StringSplitOptions.None);
      if (!this.SearchBar.IsEmpty)
      {
        if (((IEnumerable<string>) source).Any<string>((Func<string, bool>) (s => s.StartsWith(this.SearchBar.Input, StringComparison.OrdinalIgnoreCase))))
        {
          Color color = Color.Lerp(Color.Yellow, Color.Goldenrod, MathHelper.Lerp(0.1f, 0.9f, (float) (Math.Sin((double) Main.GameUpdateCount / 10.0) + 1.0) / 2f));
          uiText.TextColor = color;
        }
        else
          uiText.TextColor = Color.op_Multiply(Color.Gray, 1.5f);
      }
      ((StyleDimension) ref ((UIElement) this.BackPanel).Height).Set((float) this.BackHeight, 0.0f);
      ((StyleDimension) ref ((UIElement) this.InnerPanel).Height).Set((float) (this.BackHeight - 12 - 28), 0.0f);
      ((UIElement) this.InnerPanel).Append((UIElement) uiText);
    }

    public class Stat
    {
      public int ItemID;
      public Func<string> TextFunction;

      public Stat(int itemID, Func<string> textFunction)
      {
        this.ItemID = itemID;
        this.TextFunction = textFunction;
      }
    }
  }
}
