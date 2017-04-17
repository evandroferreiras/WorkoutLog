using System;
using System.Collections.Generic;
using System.Linq;

namespace WorkoutLog.Test
{
    internal class Workout
    {
        private IDay[] days;
        private int workoutId;

        public Workout(int workoutId, IDay[] days)
        {
            this.workoutId = workoutId;
            this.days = days;
        }

        public int WorkoutId => workoutId;

        public IDay[] Days => days;
    }
}