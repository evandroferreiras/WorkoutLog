using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLog.Test
{
    internal class Training : ITraining
    {
        private readonly int trainingId;
        private ISet[] sets;


        public Training(int trainingId, ISet[] sets)
        {
            this.sets = sets;
            this.trainingId = trainingId;
        }

        public int TrainingId => trainingId;

        public ISet[] Sets => this.sets;
    }
}
