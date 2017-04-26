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
        private readonly int routineId;
        private readonly int workoutId;

        protected ChangeRoutineTransaction(int workoutId,int routineId)
        {
            this.workoutId = workoutId;
            this.routineId = routineId;
        }

        public void Execute()
        {
            var routine = WorkoutDatabase.GetRoutine(workoutId,routineId);
            ExecuteChange(routine);
        }

        public abstract void ExecuteChange(IRoutine routine);

    }
}
