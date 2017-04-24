using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLog.Test
{
    internal class Routine : IRoutine
    {
        private readonly int routineId;
        private IRoutineExercise[] routineExercises;

        public Routine(int routineId, IRoutineExercise[] routineExercises)
        {
            this.routineExercises = routineExercises;
            this.routineId = routineId;
        }

        public int RoutineId => routineId;

        public IRoutineExercise[] RoutineExercises => this.routineExercises;
    }
}
