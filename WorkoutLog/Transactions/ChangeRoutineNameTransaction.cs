using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLog.Workout;

namespace WorkoutLog.Transactions
{
    public class ChangeRoutineNameTransaction : ChangeRoutineTransaction
    {
        private readonly string name;

        public ChangeRoutineNameTransaction(int routineId, string name) : base(routineId)
        {
            this.name = name;
        }

        public override void ExecuteChange(IRoutine routine)
        {
            routine.UpdateName(name);
        }
    }
}
