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
        private readonly WorkoutIdentity wId;

        protected ChangeDayTransaction(WorkoutIdentity wId) : base(wId)
        {
            this.wId = wId;
        }

        public override void ExecuteChange(IRoutine routine)
        {
            IDay day = null;
            foreach (var item in routine.Days)
            {
                if (item.DayId == wId.DayId)
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
