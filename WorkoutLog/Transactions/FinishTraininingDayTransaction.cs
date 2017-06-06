using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLog.Database;
using WorkoutLog.Training;

namespace WorkoutLog.Transactions
{
    public class FinishTraininingDayTransaction : ITransaction
    {
        private readonly DateTime endDate;
        private readonly TrainingIdentity tId;

        public FinishTraininingDayTransaction(TrainingIdentity tId, DateTime endDate)
        {
            this.tId = tId;
            this.endDate = endDate;
        }

        public void Execute()
        {

            var td = TrainingDayDatabase.GetTrainingDay(tId);
            td.EndDate = endDate;
            TrainingDayDatabase.SaveTrainingDay(td);
        }
    }
}
