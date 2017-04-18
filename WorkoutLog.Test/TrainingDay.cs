using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLog.Test
{
    internal class TrainingDay : ITrainingDay
    {
        
        private readonly DateTime beginDate;
        private readonly ITrainingSet[] trainingSets;

        private readonly int dayId;
        private readonly int trainingId;        
        private readonly int workoutId;

        public TrainingDay(int workoutId, int dayId, int trainingId, DateTime dayAndHour, ISet[] sets)
        {
            this.workoutId = workoutId;
            this.dayId = dayId;
            this.trainingId = trainingId;
            this.beginDate = dayAndHour;            
            this.trainingSets = new ITrainingSet[sets.Length];
            var i = 0;
            foreach (var set in sets)
                trainingSets[i++] = new TrainingSet(set);
        }

        public DateTime BeginDate => beginDate;


        public ITrainingSet[] TrainingSets => trainingSets;

        public int DayId => dayId;

        public int TrainingId => trainingId;

        public int WorkoutId => workoutId;
    }
}
