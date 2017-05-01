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
        private WorkoutIdentity wId;

        public AddWorkoutTransaction(WorkoutIdentity wId, IRoutine[] r)
        {
            this.wId = wId;
            this.r = r;
        }

        public void Execute()
        {    
            WorkoutDatabase.SaveWorkout(wId, r);
        }
    }
}