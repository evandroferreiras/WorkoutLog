using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLog.Test
{
    internal class ChangeWeightTransaction : ChangeRoutineExerciseTransaction
    {
        private readonly double weight;

        public ChangeWeightTransaction(int workoutId, int dayId, int routineId, int setId, double weight) : base(workoutId, dayId, routineId, setId)
        {
            this.weight = weight;
        }

        internal override void ChangeRoutineExercise(IRoutineExercise routineExercise)
        {
            routineExercise.UpdateWeight(weight);
        }
    }
}
