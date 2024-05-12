using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using RimWorld.Planet;
using UnityEngine;
using Verse;
using VFECore.Abilities;
using Ability = VFECore.Abilities.Ability;


namespace DD

{
    public class Ability_ShootProjectileExpandable : Ability
    {
        public override void Cast(params GlobalTargetInfo[] target)


        {
            base.Cast(target);

            Projectile projectile = GenSpawn.Spawn(this.def.GetModExtension<AbilityExtension_Projectile>().projectile, this.pawn.Position, this.pawn.Map) as Projectile;
            if (projectile is AbilityProjectile abilityProjectile)
            {
                abilityProjectile.ability = this;
            }
            projectile?.Launch(this.pawn, this.pawn.DrawPos, ((LocalTargetInfo)target[0]), ((LocalTargetInfo)target[0]), ProjectileHitFlags.IntendedTarget);
            //projectile?.Launch(this.pawn, this.pawn.DrawPos, target, target, ProjectileHitFlags.IntendedTarget);
            this.pawn.stances.SetStance(new DD_Stance_Stand(this.GetDurationForPawn(), ((LocalTargetInfo)target[0]), verb));

        }

        public override void CheckCastEffects(LocalTargetInfo targetInfo, out bool cast, out bool target, out bool hediffApply)
        {
            base.CheckCastEffects(targetInfo, out cast, out _, out _);
            target = false;
            hediffApply = false;
        }
        public override bool ShowGizmoOnPawn() =>
        this.pawn != null && this.pawn.Faction == Faction.OfPlayer;

    }
}
