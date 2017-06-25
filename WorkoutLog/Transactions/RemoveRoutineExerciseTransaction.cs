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
        private readonly int routineExerciseIdx;
        private WorkoutIdentity wId;

        public RemoveRoutineExerciseTransaction(WorkoutIdentity wId, int routineExerciseIdx)
        {
            this.wId = wId;
            this.routineExerciseIdx = routineExerciseIdx;
        }

        public void Execute()
        {
            WorkoutDatabase.RemoveRoutineExercise(wId, routineExerciseIdx);
        }
    }
}
