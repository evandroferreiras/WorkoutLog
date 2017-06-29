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
        private readonly int routineId;
        private readonly DayOfWeek dayOfWeek;
        private readonly int routineExerciseIdx;

        public RemoveRoutineExerciseTransaction(int routineId, DayOfWeek dayOfWeek, int routineExerciseIdx)
        {

            this.routineExerciseIdx = routineExerciseIdx;
            this.dayOfWeek = dayOfWeek;
            this.routineId = routineId;
        }

        public void Execute()
        {
            WorkoutDatabase.RemoveRoutineExercise(routineId, dayOfWeek, routineExerciseIdx);
        }
    }
}
