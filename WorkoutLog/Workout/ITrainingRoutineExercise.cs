namespace WorkoutLog.Workout
{
    public interface ITrainingRoutineExercise
    {
        int ExerciseId { get;  }
        int NumberOfPendingRepetitions { get; }

        void DoRep();
    }
}