using System.Collections.Generic;

namespace WorkoutLog.Workout
{
    public interface IRoutine
    {
        IDay[] Days { get; }
        int RoutineId { get; }
        string Name { get; }

        void UpdateName(string name);
    }
}