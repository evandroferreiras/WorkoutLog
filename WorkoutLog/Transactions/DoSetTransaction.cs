using System;
using WorkoutLog.Database;
using WorkoutLog.Training;

namespace WorkoutLog.Transactions
{
    public class DoSetTransaction : ITransaction
    {
        private readonly DateTime dayAndHour;
        private readonly DayOfWeek dayOfWeek;
        private readonly double weight;
        private readonly int exerciseId;

        
        public DoSetTransaction(DayOfWeek dayOfWeek, DateTime dayAndHour , int exerciseId, double weight )
        {            
            this.exerciseId = exerciseId;            
            this.weight = weight;
            this.dayOfWeek = dayOfWeek;
            this.dayAndHour = dayAndHour;
        }

        public void Execute()
        {
            var tre = TrainingDayDatabase.GetTrainingRoutineExercise(dayOfWeek, dayAndHour , exerciseId);
            if (tre != null) 
                tre.DoRep(weight);        
        }
    }
}