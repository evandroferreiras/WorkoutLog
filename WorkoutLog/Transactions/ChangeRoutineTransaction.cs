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
        private readonly WorkoutIdentity wId;

        protected ChangeRoutineTransaction(WorkoutIdentity wId)
        {
            this.wId = wId;
        }

        public void Execute()
        {
            var routine = WorkoutDatabase.GetRoutine(wId);
            ExecuteChange(routine);
        }

        public abstract void ExecuteChange(IRoutine routine);

    }
}
