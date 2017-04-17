using System;
using System.Collections.Generic;
using System.Linq;

namespace WorkoutLog.Test
{
    internal class AddWorkoutTransaction : ITransaction
    {
        private readonly IDay[] days;
        private int workoutId;

        public AddWorkoutTransaction(int workoutId, IDay[] days)
        {
            this.workoutId = workoutId;
            this.days = days;
        }

        public void Execute()
        {    
            WorkoutDatabase.SaveWorkout(workoutId, days);
        }
    }
}