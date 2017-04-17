using System;
using System.Collections.Generic;
using System.Linq;

namespace WorkoutLog.Test
{
    internal class Day : IDay
    {
        private readonly int dayId;
        private ITraining[] trainings;

        public Day(int dayId, IList<ITraining> trainings)
        {
            this.trainings = trainings.ToArray();
            this.dayId = dayId;
        }

        public int DayId => dayId;

        public IList<ITraining> Trainings => trainings;
    }
}