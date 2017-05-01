using System;

namespace WorkoutLog.Training
{
    public interface ITrainingRoutine
    {
        DateTime BeginDate { get; }
        int RoutineId { get; }
        ITrainingDay[] TrainingDays { get; }

        void UpdateTrainingDays(ITrainingDay[] td);
    }
}