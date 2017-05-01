using System;
using WorkoutLog.Database;
using WorkoutLog.Extensions;
using WorkoutLog.Training;
using WorkoutLog.Workout;

namespace WorkoutLog.Transactions
{
    public class StartRoutineTransaction : ITransaction
    {
        private readonly TrainingIdentity tId;

        public StartRoutineTransaction(TrainingIdentity tId)
        {
            this.tId = tId;
        }

        public void Execute()
        {
            var days = WorkoutDatabase.GetDaysByRotine(tId.WId);
            var trainingRoutines = days.ToTrainingDay();
            TrainingDayDatabase.SaveTrainingRoutine(new TrainingRoutine(tId, trainingRoutines));
        }

    }
}