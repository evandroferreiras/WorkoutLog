using System;
using System.Collections.Generic;

namespace WorkoutLog.Workout
{
    public interface IDay
    {
        DayOfWeek DayOfWeek { get; }
        IRoutineExercise[] RoutineExercises { get; set; }
        
    }
}