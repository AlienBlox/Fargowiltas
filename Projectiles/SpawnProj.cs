// Decompiled with JetBrains decompiler
// Type: Fargowiltas.Projectiles.SpawnProj
// Assembly: Fargowiltas, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0B0A4C12-991D-4E65-BD28-A3D99D016C3E
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\Fargowiltas.dll

using Fargowiltas.NPCs;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace Fargowiltas.Projectiles
{
  public class SpawnProj : ModProjectile
  {
    private readonly int[] bosses = new int[18]
    {
      50,
      4,
      13,
      266,
      222,
      668,
      35,
      134,
      (int) sbyte.MaxValue,
      125,
      126,
      262,
      245,
      370,
      439,
      398,
      657,
      636
    };

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 2;
      ((Entity) this.Projectile).height = 2;
      this.Projectile.aiStyle = -1;
      this.Projectile.timeLeft = 1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.hide = true;
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual void OnKill(int timeLeft)
    {
      if (Main.netMode == 1)
        return;
      if ((int) this.Projectile.ai[0] == 439 && NPC.downedAncientCultist)
      {
        int index = NPC.NewNPC(NPC.GetBossSpawnSource(Main.myPlayer), (int) ((Entity) this.Projectile).Center.X, (int) ((Entity) this.Projectile).Center.Y, (int) this.Projectile.ai[0], 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
        if (index == Main.maxNPCs)
          return;
        Main.npc[index].GetGlobalNPC<FargoGlobalNPC>().PillarSpawn = false;
        if (Main.netMode != 2)
          return;
        NetMessage.SendData(23, -1, -1, (NetworkText) null, index, 0.0f, 0.0f, 0.0f, 0, 0, 0);
      }
      else if ((double) this.Projectile.ai[1] == 2.0)
      {
        for (int index = 0; index < 7; ++index)
        {
          int num = NPC.NewNPC(NPC.GetBossSpawnSource(Main.myPlayer), (int) ((Entity) this.Projectile).Center.X, (int) ((Entity) this.Projectile).Center.Y, this.bosses[index], 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
          if (num != Main.maxNPCs && Main.netMode == 2)
            NetMessage.SendData(23, -1, -1, (NetworkText) null, num, 0.0f, 0.0f, 0.0f, 0, 0, 0);
        }
        NPC.SpawnWOF(((Entity) Main.player[this.Projectile.owner]).Center);
      }
      else if ((double) this.Projectile.ai[1] == 3.0)
      {
        foreach (int boss in this.bosses)
        {
          int index = NPC.NewNPC(NPC.GetBossSpawnSource(Main.myPlayer), (int) ((Entity) this.Projectile).Center.X, (int) ((Entity) this.Projectile).Center.Y, boss, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
          if (boss == 439)
          {
            Main.npc[index].GetGlobalNPC<FargoGlobalNPC>().PillarSpawn = false;
            if (index != Main.maxNPCs && Main.netMode == 2)
              NetMessage.SendData(23, -1, -1, (NetworkText) null, index, 0.0f, 0.0f, 0.0f, 0, 0, 0);
          }
        }
        NPC.SpawnWOF(((Entity) Main.player[this.Projectile.owner]).Center);
      }
      else
      {
        int num = NPC.NewNPC(NPC.GetBossSpawnSource(Main.myPlayer), (int) ((Entity) this.Projectile).Center.X, (int) ((Entity) this.Projectile).Center.Y, (int) this.Projectile.ai[0], 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
        if (num == Main.maxNPCs || Main.netMode != 2)
          return;
        NetMessage.SendData(23, -1, -1, (NetworkText) null, num, 0.0f, 0.0f, 0.0f, 0, 0, 0);
      }
    }
  }
}
