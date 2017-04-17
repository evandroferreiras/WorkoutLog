using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLog.Test
{
    public abstract class ChangeTrainingTransaction : ITransaction
    {
        private readonly int trainingId;
        private readonly int dayId;
        private readonly int workoutId;

        protected ChangeTrainingTransaction(int workoutId, int dayId, int trainingId)
        {
            this.workoutId = workoutId;
            this.dayId = dayId;
            this.trainingId = trainingId;
        }

        public void Execute()
        {
            var workout = WorkoutDatabase.GetWorkout(workoutId);

            var day = workout.Days.First(x => x.DayId.Equals(dayId));
            var training = day.Trainings.First(x => x.TrainingId.Equals(trainingId));

            ExecuteChange(training);

        }

        internal abstract void ExecuteChange(ITraining training);

    }
}
