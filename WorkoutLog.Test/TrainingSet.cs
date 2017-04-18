using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLog.Test
{
    internal class TrainingSet : ITrainingSet
    {

        private readonly ISet set;

        public TrainingSet(ISet set)
        {
            this.set = set;
        }

        public DateTime? EndDate { get; set; }

        public ISet Set
        {
            get
            {
                return set;
            }
        }
    }
}
