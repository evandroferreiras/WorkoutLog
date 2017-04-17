using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLog.Test
{
    internal class NormalSet : Set
    {
        private readonly IExercise exercise;

        public NormalSet(int setId, int reps, Exercise exercise) : base(setId, reps)
        {
            this.exercise = exercise;
        }

        public IExercise Exercise
        {
            get
            {
                return exercise;
            }
        }
    }
}
