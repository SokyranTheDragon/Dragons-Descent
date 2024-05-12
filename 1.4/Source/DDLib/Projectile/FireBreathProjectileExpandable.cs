﻿using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using Verse;
using Verse.Noise;
using Verse.Sound;
using VFECore;


namespace DD
{

    public class FireBreathProjectileExpandable: ExpandableProjectile
    {
        public override void DoDamage(IntVec3 pos)
        {
            base.DoDamage(pos);
            try
            {

                if (pos != this.launcher.Position && this.launcher.Map != null && GenGrid.InBounds(pos, this.launcher.Map))
                    if (Rand.Chance(0.011f))
                    {
                        FleckMaker.ThrowSmoke(Position.ToVector3Shifted() + Gen.RandomHorizontalVector(0.5f), Map, 2f);
                    

                    }
                if (Rand.Chance(0.0035f))
                    {
                        FilthMaker.TryMakeFilth(pos, launcher.Map, ThingDefOf.Filth_Water, 1, FilthSourceFlags.None);

                    //FilthMaker.TryMakeFilth(pos, launcher.Map, ThingDefOf.Filth_Ash, 1, FilthSourceFlags.None);
                }
                    if (Rand.Chance(0.33f))
                    {
                  
                    FleckMaker.ThrowMicroSparks(Position.ToVector3(), launcher.Map);
                  

                }

               
                {
                    var list = this.launcher.Map.thingGrid.ThingsListAt(pos);
                    for (int num = list.Count - 1; num >= 0; num--)
                    {

                        if (IsDamagable(list[num]))
                        {
                            if (!list.Where(x => x.def == ThingDefOf.Fire).Any())
                            this.customImpact = true;
                            base.Impact(list[num]);
                            this.customImpact = false;
                            if (list[num] is Pawn pawn)
                            {
                               // if (Rand.Chance(0.032f))
                               // HealthUtility.AdjustSeverity(pawn, HediffDefOf.Burn, 0.01f);
                                //HealthUtility.AdjustSeverity(pawn, HediffDef.Named("Heatstroke"), 0.001f);
                            }
                            //Fire explosion should be tiny.
                            if (this.def.projectile.explosionEffect != null)
                               // Find.CameraDriver.shaker.DoShake(0f);

                            {
                                Find.TickManager.slower.SignalForceNormalSpeedShort();
                                Effecter effecter = this.def.projectile.explosionEffect.Spawn();
                                effecter.Trigger(new TargetInfo(this.Position, Map, false), new TargetInfo(this.Position, Map, false));
                                effecter.Cleanup();
                                FleckMaker.ThrowMicroSparks(Position.ToVector3(), launcher.Map);

                            }
                            if (Rand.Chance(0.0015f))
                                GenExplosion.DoExplosion(this.Position, Map, this.def.projectile.explosionRadius, this.def.projectile.damageDef, this.launcher, this.def.projectile.GetDamageAmount(1, null), this.def.projectile.GetArmorPenetration(1, null), this.def.projectile.soundExplode, this.equipmentDef, this.def, null, this.def.projectile.postExplosionSpawnThingDef, this.def.projectile.postExplosionSpawnChance, this.def.projectile.postExplosionSpawnThingCount, null, this.def.projectile.applyDamageToExplosionCellsNeighbors, this.def.projectile.preExplosionSpawnThingDef, this.def.projectile.preExplosionSpawnChance, this.def.projectile.preExplosionSpawnThingCount, this.def.projectile.explosionChanceToStartFire, this.def.projectile.explosionDamageFalloff);
                               // Find.CameraDriver.shaker.DoShake(0f);

                        }

                        {
                            
                        }
                    }
                }
            }

            catch { };

        }
        public override bool IsDamagable(Thing t)
        {
            return base.IsDamagable(t) && t.def != ThingDefOf.Fire;
        }

        //public override bool IsDamagable(Thing t)
        //{
        //    return t is Pawn && base.IsDamagable(t) || t.def == DD_ThingDefOf.DraconicExplosion;
        //}

    }
}



