using System.Collections.Generic;

namespace WorkoutLog.Workout
{
    public interface IRoutine
    {
        IDay[] Days { get; set; }
        int RoutineId { get; }
        string Name { get; }

        void UpdateName(string name);
    }
}