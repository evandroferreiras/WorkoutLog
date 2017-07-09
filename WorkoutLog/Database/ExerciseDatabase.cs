using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLog.Workout;

namespace WorkoutLog.Database
{
    public class ExerciseDatabase
    {
        static readonly IList<IExercise> exercises = new List<IExercise>();

        private static IList<IExercise> getExercises {
            get {
                if (!exercises.Any())
                {
                    for (int i = 1; i <= 10; i++)
                    {
                        exercises.Add(new Exercise(i, $"Exercise-{i}"));
                    }
                }
                return exercises;                
            }
        }

        public static IExercise GetExerciseById(int exerciseId)
        {
            var exercises = getExercises;

            var exercise = exercises.FirstOrDefault(x => x.ExerciseId == exerciseId);

            return exercise;
        }



    }
}
