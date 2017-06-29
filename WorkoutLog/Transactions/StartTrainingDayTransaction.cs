using System;
using WorkoutLog.Database;
using WorkoutLog.Extensions;
using WorkoutLog.Training;
using WorkoutLog.Workout;

namespace WorkoutLog.Transactions
{
    public class StartTrainingDayTransaction : ITransaction
    {
        private readonly DateTime dayAndHour;
        private readonly DayOfWeek dayOfWeek;
        private readonly int routineId;

        public StartTrainingDayTransaction(int routineId, DayOfWeek dayOfWeek, DateTime dayAndHour)
        {
            this.routineId = routineId;
            this.dayOfWeek = dayOfWeek;
            this.dayAndHour = dayAndHour;
        }

        public void Execute()
        {
            var day = WorkoutDatabase.GetDay(routineId, dayOfWeek);
            TrainingDayDatabase.SaveTrainingDay(day.ToTrainingDay(dayAndHour));
        }

    }
}