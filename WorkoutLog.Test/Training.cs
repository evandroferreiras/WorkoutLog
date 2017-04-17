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
        private IEnumerable<ISet> sets;


        public Training(int trainingId, IEnumerable<ISet> sets)
        {
            this.sets = sets;
            this.trainingId = trainingId;
        }

        public int TrainingId => trainingId;


        public IEnumerable<ISet> Sets => this.sets;

        public ISet GetSetById(int setId)
        {
            var set = this.sets.First(x => x.SetId == setId);
            if (set == null)
                throw new ArgumentNullException($"Não foi possivel localizar o Set de Id {setId}");
            return set;
        }
    }
}
