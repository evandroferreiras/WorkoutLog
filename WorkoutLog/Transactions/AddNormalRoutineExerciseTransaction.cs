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
        private readonly DayOfWeek dayOfWeek;
        private readonly int routineId;
        private int exerciseId;
        private int reps;
        private double weight;

        public AddNormalRoutineExerciseTransaction(int routineId, DayOfWeek dayOfWeek, int exerciseId, int reps, double weight)
        {

            this.exerciseId = exerciseId;
            this.reps = reps;
            this.weight = weight;
            this.routineId = routineId;
            this.dayOfWeek = dayOfWeek;
        }

        public void Execute()
        {            
            var nre = new NormalRoutineExercise( exerciseId, reps, weight);
            WorkoutDatabase.AddRoutineExercise(routineId, dayOfWeek , nre);
        }
    }
}
