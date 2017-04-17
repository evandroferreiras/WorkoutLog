using System;

namespace WorkoutLog.Test
{
    internal abstract class Set : ISet
    {
        private int reps;
        private readonly int setId;

        protected Set(int setId, int reps)
        {
            this.setId = setId;
            UpdateReps(reps);
        }

        public int Reps => reps;

        public int SetId => setId;

        public void UpdateReps(int value) 
        {
            if (value < 0)
                throw new ArgumentException("The number of series should'nt be negative");
            reps = value;
        }
    }
}