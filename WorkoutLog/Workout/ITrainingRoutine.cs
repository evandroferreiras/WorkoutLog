using System;

namespace WorkoutLog.Workout
{
    public interface ITrainingRoutine
    {
        DateTime BeginDate { get; }
        int RoutineId { get; }
        ITrainingDay[] TrainingDays { get; }

        void UpdateTrainingDays(ITrainingDay[] td);
    }
}