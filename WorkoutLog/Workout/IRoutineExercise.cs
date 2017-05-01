﻿namespace WorkoutLog.Workout
{
    public interface IRoutineExercise
    {
        int RoutineExerciseId { get; }
        int ExerciseId { get; }
        int Reps { get; }
        double Weight { get; }

        void UpdateReps(int value);
        void UpdateWeight(double weight);
    }
}