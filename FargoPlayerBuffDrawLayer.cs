// Decompiled with JetBrains decompiler
// Type: Fargowiltas.FargoPlayerBuffDrawLayer
// Assembly: Fargowiltas, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D54AAE1B-FAA8-4FB5-AF8B-AFF4A04833B1
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Common.Configs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas
{
  public class FargoPlayerBuffDrawLayer : PlayerDrawLayer
  {
    private readonly int[] debuffsToIgnore = new int[12]
    {
      87,
      89,
      146,
      157,
      158,
      25,
      147,
      28,
      34,
      215,
      321,
      332
    };
    private Dictionary<int, Tuple<int, int>> memorizedDebuffDurations = new Dictionary<int, Tuple<int, int>>();

    public virtual bool IsHeadLayer => false;

    public virtual bool GetDefaultVisibility(PlayerDrawSet drawInfo)
    {
      return !Main.hideUI && ((Entity) drawInfo.drawPlayer).whoAmI == Main.myPlayer && ((Entity) drawInfo.drawPlayer).active && !drawInfo.drawPlayer.dead && !drawInfo.drawPlayer.ghost && (double) drawInfo.shadow == 0.0 && (double) FargoClientConfig.Instance.DebuffOpacity > 0.0 && ((IEnumerable<int>) drawInfo.drawPlayer.buffType).Count<int>((Func<int, bool>) (d => Main.debuff[d] && !((IEnumerable<int>) this.debuffsToIgnore).Contains<int>(d))) > 0;
    }

    public virtual PlayerDrawLayer.Position GetDefaultPosition()
    {
      return (PlayerDrawLayer.Position) new PlayerDrawLayer.Between();
    }

    protected virtual void Draw(ref PlayerDrawSet drawInfo)
    {
      Player drawPlayer = drawInfo.drawPlayer;
      List<int> list = ((IEnumerable<int>) drawPlayer.buffType).Where<int>((Func<int, bool>) (d => Main.debuff[d])).Except<int>((IEnumerable<int>) this.debuffsToIgnore).ToList<int>();
      int num1 = 0;
      for (int index1 = 0; index1 < list.Count; index1 += 10)
      {
        int num2 = Math.Min(10, list.Count - index1);
        float num3 = (float) ((double) num2 / 2.0 - 0.5);
        for (int index2 = 0; index2 < num2; ++index2)
        {
          int debuffID = list[index1 + index2];
          Vector2 vector2_1 = (double) drawPlayer.gravDir > 0.0 ? ((Entity) drawPlayer).Top : ((Entity) drawPlayer).Bottom;
          vector2_1.Y -= (32f + (float) num1) * drawPlayer.gravDir;
          vector2_1.X += (float) (32.0 * ((double) index2 - (double) num3));
          Vector2 vector2_2 = Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(Utils.RotatedBy(Vector2.op_Subtraction(vector2_1, drawPlayer.MountedCenter), -(double) drawPlayer.fullRotation, new Vector2()), drawPlayer.MountedCenter), Main.screenPosition), Vector2.op_Multiply(Vector2.UnitY, drawPlayer.gfxOffY));
          if (TextureAssets.Buff[debuffID].IsLoaded)
          {
            Texture2D texture2D = TextureAssets.Buff[debuffID].Value;
            Color color = Color.op_Multiply(Color.White, FargoClientConfig.Instance.DebuffOpacity);
            int index3 = Array.FindIndex<int>(drawPlayer.buffType, (Predicate<int>) (id => id == debuffID));
            int num4 = drawPlayer.buffTime[index3];
            float num5 = ((double) drawPlayer.gravDir > 0.0 ? 0.0f : 3.14159274f) - drawPlayer.fullRotation;
            SpriteEffects spriteEffects = (double) drawPlayer.gravDir > 0.0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
            float debuffFaderRatio = FargoClientConfig.Instance.DebuffFaderRatio;
            if ((double) debuffFaderRatio > 0.0 && !Main.buffNoTimeDisplay[debuffID])
            {
              if (num4 <= 1)
              {
                if (this.memorizedDebuffDurations.TryGetValue(debuffID, out Tuple<int, int> _))
                {
                  this.memorizedDebuffDurations.Remove(debuffID);
                  color = Color.op_Multiply(color, 1f - debuffFaderRatio);
                }
              }
              else
              {
                Tuple<int, int> tuple;
                if (this.memorizedDebuffDurations.TryGetValue(debuffID, out tuple) && tuple.Item1 >= num4 && tuple.Item2 > num4)
                {
                  int num6 = tuple.Item2;
                  float num7 = (float) num4 / (float) num6;
                  int num8 = 0;
                  int num9 = (int) ((double) texture2D.Bounds.Height * (1.0 - (double) num7));
                  int width = texture2D.Bounds.Width;
                  int num10 = (int) ((double) texture2D.Bounds.Height * (double) num7);
                  if (num9 + num10 > texture2D.Bounds.Height)
                    num9 = texture2D.Bounds.Height - num10;
                  Rectangle rectangle;
                  // ISSUE: explicit constructor call
                  ((Rectangle) ref rectangle).\u002Ector(num8, num9, width, num10);
                  Vector2 vector2_3 = Vector2.op_Addition(vector2_2, Vector2.op_Multiply((float) num9, Utils.RotatedBy(Vector2.UnitY, (double) num5, new Vector2())));
                  Color.op_Multiply(color, debuffFaderRatio);
                  drawInfo.DrawDataCache.Add(new DrawData(texture2D, vector2_3, new Rectangle?(rectangle), color, num5, Vector2.op_Division(Utils.Size(texture2D.Bounds), 2f), 1f, spriteEffects, 0.0f));
                  color = Color.op_Multiply(color, 1f - debuffFaderRatio);
                  this.memorizedDebuffDurations[debuffID] = new Tuple<int, int>(num4, num6);
                }
                else
                  this.memorizedDebuffDurations[debuffID] = new Tuple<int, int>(num4, num4);
              }
            }
            drawInfo.DrawDataCache.Add(new DrawData(texture2D, vector2_2, new Rectangle?(texture2D.Bounds), color, num5, Vector2.op_Division(Utils.Size(texture2D.Bounds), 2f), 1f, spriteEffects, 0.0f));
          }
        }
        num1 += (int) (32.0 * (double) drawPlayer.gravDir);
      }
    }
  }
}
