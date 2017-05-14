using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLog.Database;
using WorkoutLog.Workout;

namespace WorkoutLog.Transactions
{
    public class AddDayTransaction : ITransaction
    {
        private WorkoutIdentity wId;
        private IRoutineExercise[] routineExercises;

        public AddDayTransaction(WorkoutIdentity wId, IRoutineExercise[] routineExercises)
        {
            this.wId = wId;
            this.routineExercises = routineExercises;
        }

        public void Execute()
        {
            var day = new Day(wId, routineExercises);
            WorkoutDatabase.AddDay(wId, day);
        }
    }
}
