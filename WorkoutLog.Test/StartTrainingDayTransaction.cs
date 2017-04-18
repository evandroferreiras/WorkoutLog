using System;

namespace WorkoutLog.Test
{
    internal class StartTrainingDayTransaction : ITransaction
    {
        private DateTime dayAndHour;
        private int dayId;
        private int trainingId;
        private int workoutId;

        public StartTrainingDayTransaction(int workoutId, int dayId, int trainingId, DateTime dayAndHour)
        {
            this.workoutId = workoutId;
            this.dayId = dayId;
            this.trainingId = trainingId;
            this.dayAndHour = dayAndHour;
        }

        public void Execute()
        {
            var sets = WorkoutDatabase.GetSets(workoutId, dayId, trainingId);

            WorkoutDatabase.SaveTrainingDay(new TrainingDay(workoutId, dayId, trainingId, dayAndHour, sets));
        }
    }
}