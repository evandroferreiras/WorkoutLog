using System;
using System.Collections.Generic;
using System.Linq;

namespace WorkoutLog.Workout
{
    public class Workout
    {
        private IRoutine[] r;
        private WorkoutIdentity wId;

        public Workout(WorkoutIdentity wId, IRoutine[] r)
        {
            this.wId = wId;
            this.r = r;
            r = new IRoutine[0];
        }

        public int WorkoutId => wId.WorkoutId;

        public IRoutine[] Routines => r;
    }
}