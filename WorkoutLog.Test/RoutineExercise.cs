using System;

namespace WorkoutLog.Test
{
    internal abstract class RoutineExercise : IRoutineExercise
    {
        private readonly int exerciseId;
        private readonly int routineExerciseId;

        private double weight;
        private int reps;


        protected RoutineExercise(int RoutineExerciseId, int exerciseId, int reps, double weight)
        {
            this.routineExerciseId = RoutineExerciseId;
            this.exerciseId = exerciseId;
            UpdateReps(reps);
            UpdateWeight(weight);
        }

        public int Reps => reps;

        public int RoutineExerciseId => routineExerciseId;

        public double Weight
        {
            get
            {
                return weight;
            }
        }

        public int ExerciseId
        {
            get
            {
                return exerciseId;
            }
        }

        public void UpdateReps(int value) 
        {
            if (value < 0)
                throw new ArgumentException("The number of series should'nt be negative");
            reps = value;
        }

        public void UpdateWeight(double value)
        {
            if (value < 0)
                throw new ArgumentException("The weight should'nt be negative");
            weight = value;
        }
    }
}