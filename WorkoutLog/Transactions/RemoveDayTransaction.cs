using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLog.Database;
using WorkoutLog.Workout;

namespace WorkoutLog.Transactions
{
    public class RemoveDayTransaction : ITransaction
    {
        private readonly DayOfWeek dayOfWeek;
        private readonly int routineId;        

        public RemoveDayTransaction(int routineId, DayOfWeek dayOfWeek)
        {
            this.routineId = routineId;
            this.dayOfWeek = dayOfWeek;
        }

        public void Execute()
        {

            WorkoutDatabase.RemoveDay(routineId, dayOfWeek);            
        }
    }
}
