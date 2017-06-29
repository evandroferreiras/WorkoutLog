using System;

namespace WorkoutLog.Workout
{
    public abstract class RoutineExercise : IRoutineExercise
    {
        private readonly int exerciseId;

        private double weight;
        private int reps;
        private int routineExerciseId;

        protected RoutineExercise( int exerciseId, int reps, double weight)
        {
            this.exerciseId = exerciseId;
            UpdateReps(reps);
            UpdateWeight(weight);
        }

        public int Reps => reps;
        public double Weight => weight;
        public int ExerciseId => exerciseId;        

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