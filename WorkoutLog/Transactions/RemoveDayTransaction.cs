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
        private readonly WorkoutIdentity wId;

        public RemoveDayTransaction(WorkoutIdentity wId)
        {
            this.wId = wId;
        }

        public void Execute()
        {

            WorkoutDatabase.RemoveDay(wId);            
        }
    }
}
