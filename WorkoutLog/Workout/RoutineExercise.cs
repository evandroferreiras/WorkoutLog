using System;

namespace WorkoutLog.Workout
{
    public abstract class RoutineExercise : IRoutineExercise
    {
        private readonly int exerciseId;
        private readonly WorkoutIdentity wId;
        private double weight;
        private int reps;


        protected RoutineExercise(WorkoutIdentity wId, int exerciseId, int reps, double weight)
        {
            this.wId = wId;
            this.exerciseId = exerciseId;
            UpdateReps(reps);
            UpdateWeight(weight);
        }

        public int Reps => reps;
        public int RoutineExerciseId => wId.RoutineExerciseId;
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