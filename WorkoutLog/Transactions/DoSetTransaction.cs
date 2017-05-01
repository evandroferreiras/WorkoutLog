using System;
using WorkoutLog.Database;
using WorkoutLog.Training;

namespace WorkoutLog.Transactions
{
    public class DoSetTransaction : ITransaction
    {
        private readonly double weight;
        private readonly int exerciseId;
        private readonly TrainingIdentity tId;
        
        public DoSetTransaction( TrainingIdentity tId, int exerciseId, double weight )
        {
            
            this.exerciseId = exerciseId;
            this.tId = tId;
            this.weight = weight;
        }

        public void Execute()
        {
            var tre = TrainingDayDatabase.GetTrainingRoutineExercise(tId, exerciseId);
            if (tre != null) 
            {
                tre.DoRep(weight);
                TrainingDayDatabase.UpdateTrainingRoutineExercise(tId, tre);
            }                                                 
        }
    }
}