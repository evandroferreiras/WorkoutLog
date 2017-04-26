using System;
using System.Linq;
using WorkoutLog.Workout;

namespace WorkoutLog.Transactions
{
    public abstract class ChangeRoutineExerciseTransaction : ChangeDayTransaction
    {
        private readonly int routineExerciseId;

        protected ChangeRoutineExerciseTransaction(int workoutId, int routineId, int dayId, int routineExerciseId) : base(workoutId, dayId, routineId)
        {
            this.routineExerciseId = routineExerciseId;
        }

        public override void ChangeDay(IDay day)
        {
            IRoutineExercise routineExercise = null;
            foreach (var item in day.RoutineExercises)
            {
                if (item.RoutineExerciseId == routineExerciseId)
                {
                    routineExercise = item;
                    break;
                }
            }
            if (routineExercise != null)
                ChangeRoutineExercise(routineExercise);
        }
        public abstract void ChangeRoutineExercise(IRoutineExercise routineExercise);


    }
}