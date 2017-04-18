using System.Collections.Generic;

namespace WorkoutLog.Test
{
    internal interface IDay
    {
        int DayId { get; }
        ITraining[] Trainings { get;}
    }
}