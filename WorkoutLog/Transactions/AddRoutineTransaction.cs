using System;
using System.Collections.Generic;
using System.Linq;
using WorkoutLog.Database;
using WorkoutLog.Workout;

namespace WorkoutLog.Transactions
{
    public class AddRoutineTransaction : ITransaction
    {
        private readonly IDay[] days;
        private readonly string name;
        private WorkoutIdentity wId;

        public AddRoutineTransaction(WorkoutIdentity wId, string name, IDay[] days)
        {
            this.wId = wId;
            this.name = name;
            this.days = days;
        }

        public void Execute()
        {    
            WorkoutDatabase.SaveRoutine(wId, name,days);
        }
    }
}