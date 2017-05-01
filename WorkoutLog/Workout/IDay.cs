using System.Collections.Generic;

namespace WorkoutLog.Workout
{
    public interface IDay
    {
        int DayId { get; }
        IRoutineExercise[] RoutineExercises { get; set; }

        void UpdateRoutineExercise(IRoutineExercise re);
        void AddRoutineExercise(IRoutineExercise re);
    }
}