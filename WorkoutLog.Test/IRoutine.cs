using System.Collections.Generic;

namespace WorkoutLog.Test
{
    internal interface IRoutine
    {
        IRoutineExercise[] RoutineExercises { get; }
        int RoutineId { get; }

    }
}