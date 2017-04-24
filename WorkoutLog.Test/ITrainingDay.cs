using System;

namespace WorkoutLog.Test
{
    internal interface ITrainingDay
    {
        DateTime BeginDate { get; }
        int DayId { get;  }

        ITrainingRoutine[] TrainingRoutines { get; }
    }
}