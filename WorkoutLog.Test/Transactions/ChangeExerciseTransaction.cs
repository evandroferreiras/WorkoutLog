using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLog.Test
{


    internal abstract class ChangeExerciseTransaction : ChangeSetTransaction
    {
        private readonly int exerciseId;
        protected ChangeExerciseTransaction(int workoutId, int dayId, int trainingId, int setId, int exerciseId) : base(workoutId, dayId, trainingId, setId)
        {
            this.exerciseId = exerciseId;

        }

        internal override void ChangeSet(ISet set)
        {
            var exerciseFactory = new ExerciseFactory();
            var exercise = exerciseFactory.Make(set);
            ChangeExercise(exercise);            
        }

        internal abstract void ChangeExercise(IExercise exercise);
    }
}
