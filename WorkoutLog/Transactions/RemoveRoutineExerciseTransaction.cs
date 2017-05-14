using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLog.Database;
using WorkoutLog.Workout;

namespace WorkoutLog.Transactions
{
    public class RemoveRoutineExerciseTransaction : ITransaction
    {
        private WorkoutIdentity wId;

        public RemoveRoutineExerciseTransaction(WorkoutIdentity wId)
        {
            this.wId = wId;
        }

        public void Execute()
        {
            WorkoutDatabase.RemoveRoutineExercise(wId);
        }
    }
}
