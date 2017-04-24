namespace WorkoutLog.Test
{
    internal interface ITrainingRoutine
    {
        int RoutineId { get; }
        ITrainingRoutineExercise[] TrainingRoutineExercises { get; }

        void UpdateTrainingRoutineExercises(ITrainingRoutineExercise[] trainingRoutineExercises);
    }
}