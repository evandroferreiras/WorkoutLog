using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLog.Test 
{
    public interface IExercise
    {
        int ExerciseId { get; }
        double Weight { get;}
        void UpdateWeight(double weight);
    }
}
