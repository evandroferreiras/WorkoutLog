using System.Collections.Generic;

namespace WorkoutLog.Workout
{
    public class Day : IDay
    {
        private readonly int dayId;
        private IRoutineExercise[] re;

        public Day(int dayId, IRoutineExercise[] re)
        {
            this.re = re;
            this.dayId = dayId;
        }

        public int DayId => dayId;

        public IRoutineExercise[] RoutineExercises => re;
    }
}