using System;
using System.Collections.Generic;
using System.Linq;
using WorkoutLog.Database;
using WorkoutLog.Workout;

namespace WorkoutLog.Transactions
{
    public class AddRoutineTransaction : ITransaction
    {
        private readonly string name;
        private WorkoutIdentity wId;

        public AddRoutineTransaction(WorkoutIdentity wId, string name)
        {
            this.wId = wId;
            this.name = name;
        }

        public void Execute()
        {
            WorkoutDatabase.SaveRoutine(wId, name);
        }
    }
}