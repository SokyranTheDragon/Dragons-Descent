using System;
using Verse;



namespace DD

{
    public class DD_Stance_Stand : Stance_Busy
    {
        public DD_Stance_Stand()
        {

        }

        public DD_Stance_Stand(int ticks, LocalTargetInfo focusTarg, Verb verb)
            : base(ticks, focusTarg, verb)
        {

        }
    }
}