using System.Collections.Generic;

namespace WorkoutLog.Test
{
    internal interface ITraining
    {
        IEnumerable<ISet> Sets { get; }
        int TrainingId { get; }

        ISet GetSetById(int setId);
    }
}