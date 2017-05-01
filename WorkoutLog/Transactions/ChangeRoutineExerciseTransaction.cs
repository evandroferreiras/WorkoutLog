using System;
using System.Linq;
using WorkoutLog.Workout;

namespace WorkoutLog.Transactions
{
    public abstract class ChangeRoutineExerciseTransaction : ChangeDayTransaction
    {
        private readonly WorkoutIdentity wId;

        protected ChangeRoutineExerciseTransaction(WorkoutIdentity wId) : base(wId)
        {
            this.wId = wId;
        }

        public override void ChangeDay(IDay day)
        {
            IRoutineExercise routineExercise = null;
            foreach (var item in day.RoutineExercises)
            {
                if (item.RoutineExerciseId == wId.RoutineExerciseId)
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