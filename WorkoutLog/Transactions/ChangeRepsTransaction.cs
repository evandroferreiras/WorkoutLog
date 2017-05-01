using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLog.Workout;

namespace WorkoutLog.Transactions
{
    public class ChangeRepsTransaction : ChangeRoutineExerciseTransaction
    {
        private readonly int reps;

        public ChangeRepsTransaction(WorkoutIdentity wId, int reps) : base(wId)
        {
            this.reps = reps;
        }

        public override void ChangeRoutineExercise(IRoutineExercise set)
        {            
            set.UpdateReps(reps);
        }
    }
}
