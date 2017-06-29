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
        private readonly DayOfWeek dayOfWeek;
        private readonly int routineId;

        protected ChangeDayTransaction(int routineId, DayOfWeek dayOfWeek) : base(routineId)
        {
            this.routineId = routineId;
            this.dayOfWeek = dayOfWeek;
        }

        public override void ExecuteChange(IRoutine routine)
        {
            IDay day = null;
            foreach (var item in routine.Days)
            {
                if (item.DayOfWeek == dayOfWeek)
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
