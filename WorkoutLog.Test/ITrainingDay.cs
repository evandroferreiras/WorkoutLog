using System;

namespace WorkoutLog.Test
{
    internal interface ITrainingDay
    {
        DateTime BeginDate { get; }
        int DayId { get; }
        int TrainingId { get; }
        int WorkoutId { get; }

        ITrainingSet[] TrainingSets { get; }
    }
}