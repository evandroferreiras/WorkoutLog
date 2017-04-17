using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLog.Test
{
    internal class ExerciseFactory : IExerciseFactory
    {
        public IExercise Make(ISet set)
        {
            if (set is NormalSet)
            {
                var normalSet = (NormalSet)set;
                return normalSet.Exercise;
            }
            return new NullExercise();
        }
    }
}
