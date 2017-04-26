using System;
using WorkoutLog.Database;

namespace WorkoutLog.Transactions
{
    public class DoSetTransaction : ITransaction
    {
        private readonly int exerciseId;
        private DateTime dayAndHour;
        private int dayId;
        private int routineId;


        public DoSetTransaction( int routineId, DateTime dayAndHour, int dayId, int exerciseId)
        {
            this.dayId = dayId;
            this.routineId = routineId;
            this.dayAndHour = dayAndHour;
            this.exerciseId = exerciseId;
        }

        public void Execute()
        {
            var tre = TrainingDayDatabase.GetTrainingRoutineExercise(routineId, dayAndHour, dayId, exerciseId);
            if (tre != null) 
            {
                tre.DoRep();
                TrainingDayDatabase.UpdateTrainingRoutineExercise(dayId, dayAndHour, routineId, tre);
            }                                                 
        }
    }
}