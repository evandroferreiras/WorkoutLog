using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLog.Database;
using WorkoutLog.Workout;

namespace WorkoutLog.Transactions
{
    public class AddNormalRoutineExerciseTransaction : ITransaction
    {
        private WorkoutIdentity wId;
        private int exerciseId;
        private int reps;
        private double weight;

        public AddNormalRoutineExerciseTransaction(WorkoutIdentity wId, int exerciseId, int reps, double weight)
        {
            this.wId = wId;
            this.exerciseId = exerciseId;
            this.reps = reps;
            this.weight = weight;
        }

        public void Execute()
        {

            
            var nre = new NormalRoutineExercise(wId, exerciseId, reps, weight);
            WorkoutDatabase.AddRoutineExercise(wId, nre);
        }
    }
}
