namespace WorkoutLog.Test
{
    internal interface ITrainingRoutineExercise
    {
        int ExerciseId { get;  }
        int NumberOfPendingRepetitions { get; }

        void DoRep();
    }
}