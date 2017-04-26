using System;
using System.Collections.Generic;
using System.Linq;
using WorkoutLog.Database;
using WorkoutLog.Workout;

namespace WorkoutLog.Transactions
{
    public class AddWorkoutTransaction : ITransaction
    {
        private readonly IRoutine[] r;
        private int workoutId;

        public AddWorkoutTransaction(int workoutId, IRoutine[] r)
        {
            this.workoutId = workoutId;
            this.r = r;
        }

        public void Execute()
        {    
            WorkoutDatabase.SaveWorkout(workoutId, r);
        }
    }
}