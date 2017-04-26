using System;
using System.Collections.Generic;
using System.Linq;

namespace WorkoutLog.Workout
{
    public class Workout
    {
        private IRoutine[] r;
        private int workoutId;

        public Workout(int workoutId, IRoutine[] r)
        {
            this.workoutId = workoutId;
            this.r = r;
        }

        public int WorkoutId => workoutId;

        public IRoutine[] Routines => r;
    }
}