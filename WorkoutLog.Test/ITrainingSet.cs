using System;

namespace WorkoutLog.Test
{
    internal interface ITrainingSet
    {
        DateTime? EndDate { get; set; }

        ISet Set { get; }
    }
}