using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLog.Test
{
    internal class ChangeRepsTransaction : ChangeRoutineExerciseTransaction
    {
        private readonly int reps;

        public ChangeRepsTransaction(int workoutId, int dayId, int routineId, int setId, int reps) : base(workoutId, dayId, routineId, setId)
        {
            this.reps = reps;
        }

        internal override void ChangeRoutineExercise(IRoutineExercise set)
        {            
            set.UpdateReps(reps);
        }
    }
}
