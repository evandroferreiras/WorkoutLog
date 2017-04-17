using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLog.Test
{
    internal class ChangeWeightTransaction : ChangeExerciseTransaction
    {
        private readonly double weight;

        public ChangeWeightTransaction(int workoutId, int dayId, int trainingId, int setId, int exerciseId, double weight) : base(workoutId, dayId, trainingId, setId, exerciseId)
        {
            this.weight = weight;
        }

        internal override void ChangeExercise(IExercise exercise)
        {
            exercise.UpdateWeight(weight);
        }
    }
}
