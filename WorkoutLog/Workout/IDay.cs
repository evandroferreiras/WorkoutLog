using System.Collections.Generic;

namespace WorkoutLog.Workout
{
    public interface IDay
    {
        int DayId { get; }
        IRoutineExercise[] RoutineExercises { get;}
    }
}