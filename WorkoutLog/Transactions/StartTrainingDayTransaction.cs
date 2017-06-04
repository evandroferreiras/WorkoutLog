using System;
using WorkoutLog.Database;
using WorkoutLog.Extensions;
using WorkoutLog.Training;
using WorkoutLog.Workout;

namespace WorkoutLog.Transactions
{
    public class StartTrainingDayTransaction : ITransaction
    {
        private readonly TrainingIdentity tId;

        public StartTrainingDayTransaction(TrainingIdentity tId)
        {
            this.tId = tId;
        }

        public void Execute()
        {
            var day = WorkoutDatabase.GetDay(tId.WId);
            TrainingDayDatabase.SaveTrainingDay(day.ToTrainingDay(tId));
        }

    }
}