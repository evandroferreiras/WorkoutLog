using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLog.Test
{
    public class Exercise : IExercise
    {
        private int exerciseId;
        private double weight;

        public Exercise(int exerciseId, double weight)
        {
            this.exerciseId = exerciseId;
            UpdateWeight(weight);
        }

        public int ExerciseId => exerciseId;

        public double Weight => weight;

        public void UpdateWeight(double weight)
        {
            if (weight < 0)
                throw new ArgumentException("The weight should'nt be negative");

            this.weight = weight;
        }
    }
}
