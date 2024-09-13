// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Projectiles.FargoGlobalProjectile
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.Common.Configs;
using Fargowiltas.NPCs;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Projectiles
{
  public class FargoGlobalProjectile : GlobalProjectile
  {
    private bool firstTick = true;
    public bool lowRender;
    public static HashSet<int> CannotDestroyTileTypes = new HashSet<int>();
    public static HashSet<int> CannotDestroyWallTypes = new HashSet<int>();
    public static HashSet<Rectangle> CannotDestroyRectangle = new HashSet<Rectangle>();
    public float DamageMultiplier = 1f;

    public virtual bool InstancePerEntity => true;

    public virtual void SetDefaults(Projectile projectile)
    {
      if (!projectile.friendly)
        return;
      this.lowRender = true;
    }

    public virtual void OnSpawn(Projectile projectile, IEntitySource source)
    {
      FargoServerConfig instance = FargoServerConfig.Instance;
      if ((double) instance.EnemyDamage != 1.0 || (double) instance.BossDamage != 1.0)
      {
        FargoGlobalProjectile globalProjectile;
        this.DamageMultiplier = (!(source is EntitySource_Parent entitySourceParent1) || !(entitySourceParent1.Entity is NPC entity1) || (double) instance.BossDamage <= (double) instance.EnemyDamage ? 0 : (entity1.boss || entity1.type == 13 || entity1.type == 14 || entity1.type == 15 ? 1 : (!instance.BossApplyToAllWhenAlive ? 0 : (FargoGlobalNPC.AnyBossAlive() ? 1 : 0)))) == 0 ? (!(source is EntitySource_Parent entitySourceParent2) || !(entitySourceParent2.Entity is Projectile entity2) || !entity2.TryGetGlobalProjectile<FargoGlobalProjectile>(ref globalProjectile) || (double) globalProjectile.DamageMultiplier <= (double) instance.EnemyDamage ? instance.EnemyDamage : globalProjectile.DamageMultiplier) : instance.BossDamage;
      }
      if (!projectile.bobber || projectile.owner != Main.myPlayer || !FargoServerConfig.Instance.ExtraLures || !(source is EntitySource_ItemUse))
        return;
      int number;
      switch (Main.player[Main.myPlayer].HeldItem.type)
      {
        case 2292:
        case 2293:
        case 2421:
        case 4325:
        case 4442:
          number = 2;
          break;
        case 2294:
        case 2422:
          number = 5;
          break;
        case 2295:
        case 2296:
          number = 3;
          break;
        default:
          number = 1;
          break;
      }
      if (Main.player[projectile.owner].HasBuff(121))
        ++number;
      if (number <= 1)
        return;
      FargoGlobalProjectile.SplitProj(projectile, number);
    }

    public virtual bool PreAI(Projectile projectile)
    {
      if (projectile.type == 525 && FargoServerConfig.Instance.StalkerMoneyTrough)
      {
        Player player = Main.player[projectile.owner];
        float num = Vector2.Distance(((Entity) projectile).Center, ((Entity) player).Center);
        if ((double) num > 3000.0)
          ((Entity) projectile).Center = ((Entity) player).Top;
        else if (Vector2.op_Inequality(((Entity) projectile).Center, ((Entity) player).Center))
        {
          Vector2 vector2 = Vector2.op_Division(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(Vector2.op_Multiply(((Entity) projectile).DirectionFrom(((Entity) player).Center), 3f), 16f)), ((Entity) projectile).Center), (double) num < 48.0 ? 30f : 60f);
          Projectile projectile1 = projectile;
          ((Entity) projectile1).position = Vector2.op_Addition(((Entity) projectile1).position, vector2);
        }
        if (projectile.timeLeft < 2 && projectile.timeLeft > 0)
          projectile.timeLeft = 2;
      }
      if (this.firstTick)
      {
        this.firstTick = false;
        if (projectile.owner != Main.myPlayer && !projectile.hostile && !projectile.trap && projectile.friendly)
          this.lowRender = true;
      }
      if (projectile.bobber && ((Entity) projectile).lavaWet && FargoServerConfig.Instance.FasterLavaFishing && (double) projectile.ai[0] == 0.0 && (double) projectile.ai[1] == 0.0 && (double) projectile.localAI[1] < 600.0)
        ++projectile.localAI[1];
      return true;
    }

    public virtual void OnKill(Projectile projectile, int timeLeft)
    {
      if (projectile.type != 525 || !FargoServerConfig.Instance.StalkerMoneyTrough)
        return;
      foreach (Projectile projectile1 in ((IEnumerable<Projectile>) Main.projectile).Where<Projectile>((Func<Projectile, bool>) (p => ((Entity) p).active && p.type == projectile.type && p.owner == projectile.owner)))
        projectile1.timeLeft = 0;
    }

    public virtual void ModifyHitPlayer(
      Projectile projectile,
      Player target,
      ref Player.HurtModifiers modifiers)
    {
      ref StatModifier local = ref modifiers.FinalDamage;
      local = StatModifier.op_Multiply(local, this.DamageMultiplier);
    }

    public static void SplitProj(Projectile projectile, int number)
    {
      double num1 = 0.3 / (double) number;
      for (int index1 = 0; index1 < number / 2; ++index1)
      {
        for (int index2 = 0; index2 < 2; ++index2)
        {
          int num2 = index2 == 0 ? 1 : -1;
          Projectile projectile1 = FargoGlobalProjectile.NewProjectileDirectSafe(((Entity) projectile).GetSource_FromThis((string) null), ((Entity) projectile).Center, Utils.RotatedBy(((Entity) projectile).velocity, (double) num2 * num1 * (double) (index1 + 1), new Vector2()), projectile.type, projectile.damage, projectile.knockBack, projectile.owner, projectile.ai[0], projectile.ai[1]);
          if (projectile1 != null)
          {
            projectile1.friendly = true;
            projectile1.GetGlobalProjectile<FargoGlobalProjectile>().firstTick = false;
          }
        }
      }
      if (number % 2 != 0)
        return;
      ((Entity) projectile).active = false;
    }

    public static Projectile NewProjectileDirectSafe(
      IEntitySource source,
      Vector2 pos,
      Vector2 vel,
      int type,
      int damage,
      float knockback,
      int owner = 255,
      float ai0 = 0.0f,
      float ai1 = 0.0f)
    {
      int index = Projectile.NewProjectile(source, pos, vel, type, damage, knockback, owner, ai0, ai1, 0.0f);
      return index >= Main.maxProjectiles ? (Projectile) null : Main.projectile[index];
    }

    public virtual Color? GetAlpha(Projectile projectile, Color lightColor)
    {
      if (!this.lowRender || projectile.hostile || (double) FargoClientConfig.Instance.TransparentFriendlyProjectiles >= 1.0)
        return base.GetAlpha(projectile, lightColor);
      Color? alpha = (Color?) projectile.ModProjectile?.GetAlpha(lightColor);
      if (alpha.HasValue)
        return new Color?(Color.op_Multiply(alpha.Value, FargoClientConfig.Instance.TransparentFriendlyProjectiles));
      lightColor = Color.op_Multiply(lightColor, projectile.Opacity * FargoClientConfig.Instance.TransparentFriendlyProjectiles);
      return new Color?(lightColor);
    }

    public static bool OkayToDestroyTile(Tile tile)
    {
      if (Tile.op_Equality(tile, (ArgumentException) null))
        return false;
      int num1 = NPC.downedBoss3 ? 0 : (((Tile) ref tile).TileType == (ushort) 41 || ((Tile) ref tile).TileType == (ushort) 43 || ((Tile) ref tile).TileType == (ushort) 44 || ((Tile) ref tile).WallType == (ushort) 94 || ((Tile) ref tile).WallType == (ushort) 95 || ((Tile) ref tile).WallType == (ushort) 7 || ((Tile) ref tile).WallType == (ushort) 98 || ((Tile) ref tile).WallType == (ushort) 99 || ((Tile) ref tile).WallType == (ushort) 8 || ((Tile) ref tile).WallType == (ushort) 96 || ((Tile) ref tile).WallType == (ushort) 97 ? 1 : (((Tile) ref tile).WallType == (ushort) 9 ? 1 : 0));
      bool flag1 = (((Tile) ref tile).TileType == (ushort) 107 || ((Tile) ref tile).TileType == (ushort) 221 || ((Tile) ref tile).TileType == (ushort) 108 || ((Tile) ref tile).TileType == (ushort) 222 || ((Tile) ref tile).TileType == (ushort) 111 || ((Tile) ref tile).TileType == (ushort) 223) && !NPC.downedMechBossAny;
      bool flag2 = ((Tile) ref tile).TileType == (ushort) 211 && (!NPC.downedMechBoss1 || !NPC.downedMechBoss2 || !NPC.downedMechBoss3);
      bool flag3 = (((Tile) ref tile).TileType == (ushort) 226 || ((Tile) ref tile).WallType == (ushort) 87) && !NPC.downedGolemBoss;
      bool flag4 = false;
      Mod mod;
      if (Terraria.ModLoader.ModLoader.TryGetMod("CalamityMod", ref mod))
      {
        ModTile modTile1;
        mod.TryFind<ModTile>("AbyssGravel", ref modTile1);
        ModTile modTile2;
        mod.TryFind<ModTile>("Voidstone", ref modTile2);
        flag4 = (int) ((Tile) ref tile).TileType == (int) ((ModBlockType) modTile1).Type || (int) ((Tile) ref tile).TileType == (int) ((ModBlockType) modTile2).Type;
      }
      int num2 = flag1 ? 1 : 0;
      return (num1 | num2 | (flag2 ? 1 : 0) | (flag3 ? 1 : 0) | (flag4 ? 1 : 0)) == 0 && !FargoGlobalProjectile.TileBelongsToMagicStorage(tile) && !FargoGlobalProjectile.CannotDestroyTileTypes.Contains((int) ((Tile) ref tile).TileType) && !FargoGlobalProjectile.CannotDestroyWallTypes.Contains((int) ((Tile) ref tile).WallType);
    }

    public static bool OkayToDestroyTileAt(int x, int y)
    {
      Tile tile = ((Tilemap) ref Main.tile)[x, y];
      if (Tile.op_Equality(tile, (ArgumentException) null))
        return false;
      foreach (Rectangle rectangle in FargoGlobalProjectile.CannotDestroyRectangle)
      {
        if (((Rectangle) ref rectangle).Contains(x * 16, y * 16))
          return false;
      }
      return FargoGlobalProjectile.OkayToDestroyTile(tile);
    }

    public static bool TileIsLiterallyAir(Tile tile)
    {
      return ((Tile) ref tile).TileType == (ushort) 0 && ((Tile) ref tile).WallType == (ushort) 0 && ((Tile) ref tile).LiquidAmount == (byte) 0 && ((Tile) ref tile).TileFrameX == (short) 0 && ((Tile) ref tile).TileFrameY == (short) 0;
    }

    public static bool TileBelongsToMagicStorage(Tile tile)
    {
      return Fargowiltas.Fargowiltas.ModLoaded["MagicStorage"] && ((ModType) TileLoader.GetTile((int) ((Tile) ref tile).TileType))?.Mod == Terraria.ModLoader.ModLoader.GetMod("MagicStorage");
    }
  }
}
