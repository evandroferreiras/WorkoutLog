using System;
using System.Linq;

namespace WorkoutLog.Test
{
    internal abstract class ChangeRoutineExerciseTransaction : ChangeRoutineTransaction
    {
        private readonly int routineExerciseId;

        protected ChangeRoutineExerciseTransaction(int workoutId, int dayId, int routineId, int setId) : base(workoutId, dayId, routineId)
        {
            this.routineExerciseId = setId;
        }

        internal override void ExecuteChange(IRoutine routine)
        {
            IRoutineExercise routineExercise = null;
            foreach (var item in routine.RoutineExercises)
            {
                if (item.RoutineExerciseId == routineExerciseId) 
                {
                    routineExercise = item;
                    break;
                }
            }
            if (routineExercise!= null)
                ChangeRoutineExercise(routineExercise);
        }

        internal abstract void ChangeRoutineExercise(IRoutineExercise routineExercise);
    }
}