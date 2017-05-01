using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLog.Workout;

namespace WorkoutLog.Training
{
    public struct TrainingIdentity
    {
        public TrainingIdentity(WorkoutIdentity workoutIdentity, DateTime dayAndHour)
        {
            WId = workoutIdentity;
            DayAndHour = dayAndHour;
        }

        public WorkoutIdentity WId { get; private set; }
        public DateTime DayAndHour { get; private set; }


    }
}
