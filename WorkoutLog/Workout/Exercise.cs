using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLog.Workout
{
    public class Exercise : IExercise
    {


        public Exercise(int exerciseId, string name)
        {
            this.ExerciseId = exerciseId;
            this.Name = name;
        }

        public int ExerciseId { get; set; }
        public string Name { get; set; }
    }
}
