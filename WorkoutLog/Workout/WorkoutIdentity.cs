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
        public DayOfWeek DayOfWeek { get; }
        public int RoutineExerciseId { get; }

        public WorkoutIdentity( int routineId) : this()
        {
            this.RoutineId = routineId;
        }

        public WorkoutIdentity( int routineId, DayOfWeek day) : this( routineId)
        {
            this.DayOfWeek = day;
        }

        public WorkoutIdentity( int routineId, DayOfWeek day, int routineExerciseId) : this(routineId, day)
        {
            this.RoutineExerciseId = routineExerciseId;
        }



        
    }
}
