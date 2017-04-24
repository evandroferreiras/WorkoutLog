using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLog.Test
{
    public abstract class ChangeRoutineTransaction : ITransaction
    {
        private readonly int routineId;
        private readonly int dayId;
        private readonly int workoutId;

        protected ChangeRoutineTransaction(int workoutId, int dayId, int routineId)
        {
            this.workoutId = workoutId;
            this.dayId = dayId;
            this.routineId = routineId;
        }

        public void Execute()
        {
            var training = WorkoutDatabase.GetRoutine(workoutId, dayId, routineId);
            ExecuteChange(training);
        }

        internal abstract void ExecuteChange(IRoutine training);

    }
}
