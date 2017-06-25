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
        private readonly DayOfWeek dayOfWeek;
        private readonly int routineId;
        private IRoutineExercise[] routineExercises;

        public AddDayTransaction(int routineId, DayOfWeek dayOfWeek)
        {

            this.routineExercises = new IRoutineExercise[] { };
            this.routineId = routineId;
            this.dayOfWeek = dayOfWeek;
        }

        public void Execute()
        {
            var routine = WorkoutDatabase.GetRoutine(routineId);
            var day = new Day(dayOfWeek, routineExercises);
            WorkoutDatabase.AddDay(routineId, day);
        }
    }
}
