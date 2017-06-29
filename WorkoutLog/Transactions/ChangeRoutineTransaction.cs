using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLog.Database;
using WorkoutLog.Workout;

namespace WorkoutLog.Transactions
{
    public abstract class ChangeRoutineTransaction : ITransaction
    {
        private int routineId;

        protected ChangeRoutineTransaction(int routineId)
        {
            this.routineId = routineId;
        }

        public void Execute()
        {
            var routine = WorkoutDatabase.GetRoutine(routineId);
            ExecuteChange(routine);
        }

        public abstract void ExecuteChange(IRoutine routine);

    }
}
