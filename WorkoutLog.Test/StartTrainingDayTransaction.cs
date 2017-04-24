using System;

namespace WorkoutLog.Test
{
    internal class StartTrainingDayTransaction : ITransaction
    {
        private DateTime dayAndHour;
        private int dayId;
        private int workoutId;

        public StartTrainingDayTransaction(int workoutId, int dayId, DateTime dayAndHour)
        {
            this.workoutId = workoutId;
            this.dayId = dayId;

            this.dayAndHour = dayAndHour;
        }

        public void Execute()
        {
            var routines = WorkoutDatabase.GetRoutinesByDay(workoutId, dayId);
            TrainingDayDatabase.SaveTrainingDay(new TrainingDay(dayId, dayAndHour, routines));
        }
    }
}