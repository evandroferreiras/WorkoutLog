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

        private readonly int routineId;
        private string name;

        private IList<IDay> days { get; set; }
        
        public RoutineBuilder(int routineId, string name)
        {

            days = new List<IDay>();

            this.routineId = routineId;
            this.name = name;
            
        }

        public RoutineBuilder AddNormalRoutineExercise(int dayId, int routineExerciseId, int exerciseId, int reps, double weight ) {
                               
            var id = new WorkoutIdentity( routineId, dayId, routineExerciseId);

            var nre = new NormalRoutineExercise(id, exerciseId, reps, weight);
            
            var day = days.FirstOrDefault(x => x.DayId == dayId);
            if (day == null)
                day = new Day(id, new List<NormalRoutineExercise>().ToArray());

            var reList = day.RoutineExercises.ToList();
            reList.Add(nre);
            day.RoutineExercises = reList.ToArray();
            days.Add(day);
            return this;
        }

        public IRoutine Build() {
            if (days == null)
                throw new ArgumentNullException("It's necessary define the days");
            return new Routine(new WorkoutIdentity(routineId), name, days.ToArray());
        }
    }

    public class BaseTest
    {
        internal static IRoutine CreateAndReturnRoutine(WorkoutIdentity id, int exerciseId, int reps, double weight)
        {
            var routine = new RoutineBuilder( id.RoutineId, "Default")
              .AddNormalRoutineExercise(id.DayId, id.RoutineExerciseId, exerciseId, reps, weight)
              .Build();

            var addRoutineTransaction = new AddRoutineTransaction(id, routine.Name,routine.Days);
            addRoutineTransaction.Execute();

            return routine;
        }

        internal static IRoutine ReturnFirstRoutine(WorkoutIdentity wId)
        {
            var routineUpdated = WorkoutDatabase.GetRoutine(wId);

            routineUpdated.Should().NotBeNull();

            return routineUpdated;
        }

    }
}
