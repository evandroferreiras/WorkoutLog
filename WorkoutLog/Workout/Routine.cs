using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLog.Workout
{
    public class Routine : IRoutine
    {
        private readonly int routineId;
        private IDay[] days;

        public Routine(int routineId, IDay[] days)
        {
            this.days = days;
            this.routineId = routineId;
        }

        public int RoutineId => routineId;

        public IDay[] Days => this.days;
    }
}
