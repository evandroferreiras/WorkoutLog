using System;
using System.Collections.Generic;
using WorkoutLog.Database;

namespace WorkoutLog.Workout
{
    public class Day : IDay
    {
        private readonly WorkoutIdentity wId;
        private IRoutineExercise[] res;

        public Day(WorkoutIdentity wId, IRoutineExercise[] res)
        {
            this.res = res;
            this.wId = wId;
        }

        public int DayId => wId.DayId;

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

        public void AddRoutineExercise(IRoutineExercise re)
        {
            res = WorkoutDatabase.AddRoutineExercise(res, re);
        }

        public void UpdateRoutineExercise(IRoutineExercise re)
        {
            res = WorkoutDatabase.UpdateRoutineExercise(res, re);
        }
    }
}