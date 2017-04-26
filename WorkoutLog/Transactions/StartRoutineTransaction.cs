using System;
using WorkoutLog.Database;
using WorkoutLog.Extensions;
using WorkoutLog.Workout;

namespace WorkoutLog.Transactions
{
    public class StartRoutineTransaction : ITransaction
    {
        private DateTime dayAndHour;
        private int routineId;
        private int workoutId;

        public StartRoutineTransaction(int workoutId, int routineId, DateTime dayAndHour)
        {
            this.workoutId = workoutId;
            this.routineId = routineId;
            this.dayAndHour = dayAndHour;
        }

        public void Execute()
        {
            var days = WorkoutDatabase.GetDaysByRotine(workoutId, routineId);
            var trainingRoutines = days.ToTrainingDay();
            TrainingDayDatabase.SaveTrainingRoutine(new TrainingRoutine(routineId, dayAndHour, trainingRoutines));
        }

    }
}