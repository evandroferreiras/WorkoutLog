using System.Collections.Generic;

namespace WorkoutLog.Test
{
    internal interface ITraining
    {
        ISet[] Sets { get; }
        int TrainingId { get; }

    }
}