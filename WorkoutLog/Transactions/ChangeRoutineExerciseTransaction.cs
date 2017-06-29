using System;
using System.Linq;
using WorkoutLog.Workout;

namespace WorkoutLog.Transactions
{
    public abstract class ChangeRoutineExerciseTransaction : ChangeDayTransaction
    {
        private readonly int routineExerciseIdx;

        protected ChangeRoutineExerciseTransaction(int routineId, DayOfWeek dayOfWeek, int routineExerciseIdx) : base(routineId, dayOfWeek)
        {
            this.routineExerciseIdx = routineExerciseIdx;
        }

        public override void ChangeDay(IDay day)
        {
            var routineExercise = day.RoutineExercises[routineExerciseIdx];

            if (routineExercise != null)
                ChangeRoutineExercise(routineExercise);
        }
        public abstract void ChangeRoutineExercise(IRoutineExercise routineExercise);


    }
}