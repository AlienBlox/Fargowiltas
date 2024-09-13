// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Projectiles.RenewalBaseProj
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Projectiles
{
  public class RenewalBaseProj : ModProjectile
  {
    private readonly string name;
    private readonly int projType;
    private readonly int convertType;
    private readonly bool supreme;

    protected RenewalBaseProj(string name, int projType, int convertType, bool supreme)
    {
      this.name = name;
      this.projType = projType;
      this.convertType = convertType;
      this.supreme = supreme;
    }

    public virtual string Texture => "Fargowiltas/Items/Renewals/" + this.name;

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 20;
      ((Entity) this.Projectile).height = 20;
      this.Projectile.aiStyle = 2;
      this.Projectile.friendly = true;
      this.Projectile.penetrate = -1;
      this.Projectile.timeLeft = 170;
    }

    public virtual bool OnTileCollide(Vector2 oldVelocity)
    {
      this.Projectile.Kill();
      return true;
    }

    public virtual void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(ref SoundID.Shatter, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      int num1 = 150;
      float[] numArray1 = new float[8]
      {
        0.0f,
        0.0f,
        5f,
        5f,
        5f,
        -5f,
        -5f,
        -5f
      };
      float[] numArray2 = new float[8]
      {
        5f,
        -5f,
        0.0f,
        5f,
        -5f,
        0.0f,
        5f,
        -5f
      };
      if (Main.netMode == 0)
      {
        for (int index = 0; index < 8; ++index)
          Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center.X, ((Entity) this.Projectile).Center.Y, numArray1[index], numArray2[index], this.projType, 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
      }
      if (this.supreme)
      {
        for (int index1 = -Main.maxTilesX; index1 < Main.maxTilesX; ++index1)
        {
          for (int index2 = -Main.maxTilesY; index2 < Main.maxTilesY; ++index2)
            WorldGen.Convert((int) ((double) index1 + (double) ((Entity) this.Projectile).Center.X / 16.0), (int) ((double) index2 + (double) ((Entity) this.Projectile).Center.Y / 16.0), this.convertType, 1);
        }
      }
      else
      {
        for (int index3 = -num1; index3 <= num1; ++index3)
        {
          for (int index4 = -num1; index4 <= num1; ++index4)
          {
            int num2 = (int) ((double) index3 + (double) ((Entity) this.Projectile).Center.X / 16.0);
            int num3 = (int) ((double) index4 + (double) ((Entity) this.Projectile).Center.Y / 16.0);
            if (Math.Sqrt((double) (index3 * index3 + index4 * index4)) <= (double) num1 + 0.5)
              WorldGen.Convert(num2, num3, this.convertType, 1);
          }
        }
      }
    }
  }
}
