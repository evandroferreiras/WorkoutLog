using System;
using System.Collections.Generic;
using WorkoutLog.Database;

namespace WorkoutLog.Workout
{
    public class Day : IDay
    {
        private readonly DayOfWeek dayOfWeek;
        private IRoutineExercise[] res;

        public Day(DayOfWeek dayOfWeek, IRoutineExercise[] res)
        {
            

            this.res = res;
            this.dayOfWeek = dayOfWeek;
        }

        public DayOfWeek DayOfWeek => dayOfWeek;

        public IRoutineExercise[] RoutineExercises
        {
            get
            {
                return res;
            }

            set
            {
                res = value;
            }
        }

    }
}