using System.Collections.Generic;

namespace WorkoutLog.Test
{
    internal class Day : IDay
    {
        private readonly int dayId;
        private IRoutine[] routines;

        public Day(int dayId, IRoutine[] routines)
        {
            this.routines = routines;
            this.dayId = dayId;
        }

        public int DayId => dayId;

        public IRoutine[] Routines => routines;
    }
}