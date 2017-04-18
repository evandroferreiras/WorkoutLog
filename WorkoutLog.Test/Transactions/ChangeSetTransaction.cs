using System;
using System.Linq;

namespace WorkoutLog.Test
{
    internal abstract class ChangeSetTransaction : ChangeTrainingTransaction
    {
        private readonly int setId;

        protected ChangeSetTransaction(int workoutId, int dayId, int trainingId, int setId) : base(workoutId, dayId, trainingId)
        {
            this.setId = setId;
        }

        internal override void ExecuteChange(ITraining training)
        {
            ISet set = null;
            foreach (var item in training.Sets)
            {
                if (item.SetId == setId) 
                {
                    set = item;
                    break;
                }
            }
            if (set!= null)
                ChangeSet(set);
        }

        internal abstract void ChangeSet(ISet set);
    }
}