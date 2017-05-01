using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLog.Workout
{
    public struct WorkoutIdentity
    {
        public int WorkoutId { get;}
        public int RoutineId { get; }
        public int DayId { get; }
        public int RoutineExerciseId { get; }

        public WorkoutIdentity(int workoutId, int routineId) : this()
        {
            this.WorkoutId = workoutId;
            this.RoutineId = routineId;
        }

        public WorkoutIdentity(int workoutId, int routineId, int dayId) : this(workoutId, routineId)
        {
            this.DayId = dayId;
        }

        public WorkoutIdentity(int workoutId, int routineId, int dayId, int routineExerciseId) : this(workoutId, routineId, dayId)
        {
            this.RoutineExerciseId = routineExerciseId;
        }



        
    }
}
