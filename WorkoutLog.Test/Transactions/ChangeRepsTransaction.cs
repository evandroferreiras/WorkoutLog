using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLog.Test
{
    internal class ChangeRepsTransaction : ChangeSetTransaction
    {
        private readonly int reps;

        public ChangeRepsTransaction(int workoutId, int dayId, int trainingId, int setId, int reps) : base(workoutId, dayId, trainingId, setId)
        {
            this.reps = reps;
        }

        internal override void ChangeSet(ISet set)
        {            
            set.UpdateReps(reps);
        }
    }
}
