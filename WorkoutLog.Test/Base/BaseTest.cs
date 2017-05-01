using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLog.Database;
using WorkoutLog.Transactions;
using WorkoutLog.Workout;

namespace WorkoutLog.Test.Base
{
    public class RoutineBuilder 
    {
        private readonly int workoutId;
        private readonly int routineId;
        private string name;

        private IList<IDay> days { get; set; }
        
        public RoutineBuilder(int workoutId, int routineId, string name)
        {

            days = new List<IDay>();
            this.workoutId = workoutId;
            this.routineId = routineId;
            this.name = name;
            
        }

        public RoutineBuilder AddNormalRoutineExercise(int dayId, int routineExerciseId, int exerciseId, int reps, double weight ) {
                               
            var id = new WorkoutIdentity(workoutId, routineId, dayId, routineExerciseId);

            var nre = new NormalRoutineExercise(id, exerciseId, reps, weight);
            
            var day = days.FirstOrDefault(x => x.DayId == dayId);
            if (day == null)
                day = new Day(id, new List<NormalRoutineExercise>().ToArray());

            day.AddRoutineExercise(nre);
            days.Add(day);
            return this;
        }

        public IRoutine Build() {
            if (days == null)
                throw new ArgumentNullException("It's necessary define the days");
            return new Routine(new WorkoutIdentity(workoutId,routineId), name, days.ToArray());
        }
    }

    public class BaseTest
    {
        internal static IEnumerable<IRoutine> CreateWorkOutAndRoutines(WorkoutIdentity id, int exerciseId, int reps, double weight)
        {
            var routine = new RoutineBuilder(id.WorkoutId, id.RoutineId, "Default")
              .AddNormalRoutineExercise(id.DayId, id.RoutineExerciseId, exerciseId, reps, weight)
              .Build();

            var addWorkoutTransaction = new AddWorkoutTransaction(id, new[] { routine });
            addWorkoutTransaction.Execute();

            return new[] { routine };
        }

        internal static IRoutine ReturnFirstRoutine(WorkoutIdentity id)
        {
            var workoutUpdated = WorkoutDatabase.GetWorkout(id);

            var r = workoutUpdated.Routines.First(x => x.RoutineId == id.RoutineId);
            r.Should().NotBeNull();

            return r;
        }

    }
}
