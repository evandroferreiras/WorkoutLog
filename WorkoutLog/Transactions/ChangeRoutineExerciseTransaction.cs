using System;
using System.Linq;
using WorkoutLog.Workout;

namespace WorkoutLog.Transactions
{
    public abstract class ChangeRoutineExerciseTransaction : ChangeDayTransaction
    {
        private readonly int routineExerciseIdx;
        private readonly WorkoutIdentity wId;

        protected ChangeRoutineExerciseTransaction(int routineExerciseIdx, WorkoutIdentity wId) : base(wId)
        {
            this.wId = wId;
            this.routineExerciseIdx = routineExerciseIdx;
        }

        public override void ChangeDay(IDay day)
        {
            IRoutineExercise routineExercise = day.RoutineExercises[routineExerciseIdx];


            if (routineExercise != null)
                ChangeRoutineExercise(routineExercise);
        }
        public abstract void ChangeRoutineExercise(IRoutineExercise routineExercise);


    }
}