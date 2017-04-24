using System;

namespace WorkoutLog.Test
{
    internal class DoSetTransaction : ITransaction
    {
        private readonly int exerciseId;
        private DateTime dayAndHour;
        private int dayId;
        private int routineId;


        public DoSetTransaction( int dayId, int routineId, DateTime dayAndHour, int exerciseId)
        {
            this.dayId = dayId;
            this.routineId = routineId;
            this.dayAndHour = dayAndHour;
            this.exerciseId = exerciseId;
        }

        public void Execute()
        {
            var tre = TrainingDayDatabase.GetTrainingRoutineExercise(dayId, routineId, dayAndHour, exerciseId);
            if (tre != null) {
                tre.DoRep();
                TrainingDayDatabase.UpdateTrainingRoutine(dayId, dayAndHour, routineId, tre);
            }                                                 
        }
    }
}