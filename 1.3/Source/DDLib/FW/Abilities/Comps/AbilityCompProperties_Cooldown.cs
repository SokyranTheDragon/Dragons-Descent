﻿using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using Verse.AI;

namespace DD
{
    public class AbilityCompProperties_Cooldown : CompProperties_AbilityEffect
    {
        public IntRange cooldownTicksRange;
    }
}
