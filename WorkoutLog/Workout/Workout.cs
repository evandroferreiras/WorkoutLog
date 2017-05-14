using System;
using System.Collections.Generic;
using System.Linq;

namespace WorkoutLog.Workout
{
    public class _Workout
    {
        private IRoutine[] r;
        private WorkoutIdentity wId;

        public _Workout(WorkoutIdentity wId, IRoutine[] r)
        {
            this.wId = wId;
            this.r = r;
            r = new IRoutine[0];
        }


        public IRoutine[] Routines => r;
    }
}