using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLog.Workout;

namespace WorkoutLog.Transactions
{
    public abstract class ChangeDayTransaction : ChangeRoutineTransaction
    {
        private readonly int dayId;

        protected ChangeDayTransaction(int workoutId, int routineId, int dayId) : base(workoutId, routineId)
        {
            this.dayId = dayId;
        }

        public override void ExecuteChange(IRoutine routine)
        {
            IDay day = null;
            foreach (var item in routine.Days)
            {
                if (item.DayId == dayId)
                {
                    day = item;
                    break;
                }
            }

            if (day != null)
                ChangeDay(day);
        }

        public abstract void ChangeDay(IDay day);
    }
}
