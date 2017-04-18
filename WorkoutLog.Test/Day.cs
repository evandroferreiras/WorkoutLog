using System.Collections.Generic;

namespace WorkoutLog.Test
{
    internal class Day : IDay
    {
        private readonly int dayId;
        private ITraining[] trainings;

        public Day(int dayId, ITraining[] trainings)
        {
            this.trainings = trainings;
            this.dayId = dayId;
        }

        public int DayId => dayId;

        public ITraining[] Trainings => trainings;
    }
}