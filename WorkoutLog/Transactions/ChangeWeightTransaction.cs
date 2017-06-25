using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLog.Workout;

namespace WorkoutLog.Transactions
{
    public class ChangeWeightTransaction : ChangeRoutineExerciseTransaction
    {
        private readonly double weight;

        public ChangeWeightTransaction(int routineExerciseIdx, WorkoutIdentity wId, double weight) : base(routineExerciseIdx,wId)
        {
            this.weight = weight;
        }

        public override void ChangeRoutineExercise(IRoutineExercise routineExercise)
        {
            routineExercise.UpdateWeight(weight);
        }
    }
}
