using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLog.Workout
{
    public struct WorkoutIdentity
    {
        public int RoutineId { get; }
        public int DayId { get; }
        public int RoutineExerciseId { get; }

        public WorkoutIdentity( int routineId) : this()
        {
            this.RoutineId = routineId;
        }

        public WorkoutIdentity( int routineId, int dayId) : this( routineId)
        {
            this.DayId = dayId;
        }

        public WorkoutIdentity( int routineId, int dayId, int routineExerciseId) : this(routineId, dayId)
        {
            this.RoutineExerciseId = routineExerciseId;
        }



        
    }
}
