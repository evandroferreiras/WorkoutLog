using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLog.Test
{
    //TODO: Retirar todos os LIST das classes e transformar em Array.

    internal abstract class ChangeExerciseTransaction : ChangeSetTransaction
    {
        private readonly int exerciseId;
        private readonly IExerciseFactory exerciseFactory;
        protected ChangeExerciseTransaction(int workoutId, int dayId, int trainingId, int setId, int exerciseId) : base(workoutId, dayId, trainingId, setId)
        {
            this.exerciseId = exerciseId;
            exerciseFactory = new ExerciseFactory();
        }

        internal override void ChangeSet(ISet set)
        {
            var exercise = exerciseFactory.Make(set);
            ChangeExercise(exercise);            
        }

        internal abstract void ChangeExercise(IExercise exercise);
    }
}
