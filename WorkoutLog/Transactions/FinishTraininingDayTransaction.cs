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
        private readonly DateTime dayAndHour;
        private readonly DayOfWeek dayOfWeek;
        private readonly DateTime endDate;

        public FinishTraininingDayTransaction(DayOfWeek dayOfWeek, DateTime dayAndHour, DateTime endDate)
        {
            this.endDate = endDate;
            this.dayOfWeek = dayOfWeek;
            this.dayAndHour = dayAndHour;
        }

        public void Execute()
        {
            var td = TrainingDayDatabase.GetTrainingDay(dayOfWeek, dayAndHour);
            td.EndDate = endDate;
            TrainingDayDatabase.SaveTrainingDay(td);
        }
    }
}
