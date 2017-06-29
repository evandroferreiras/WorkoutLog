using System;
using System.Collections.Generic;
using System.Linq;
using WorkoutLog.Database;
using WorkoutLog.Workout;

namespace WorkoutLog.Transactions
{
    public class AddRoutineTransaction : ITransaction
    {
        private readonly int routineId;
        private readonly string name;
        

        public AddRoutineTransaction(int routineId, string name)
        {
            
            this.name = name;
            this.routineId = routineId;
        }

        public void Execute()
        {
            WorkoutDatabase.SaveRoutine(routineId, name);
        }
    }
}