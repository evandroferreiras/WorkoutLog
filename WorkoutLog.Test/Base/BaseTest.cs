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

        public RoutineBuilder AddDayAndNormalRoutineExercise(DayOfWeek dayOfWeek, int routineExerciseId, int exerciseId, int reps, double weight )
        {
            var id = new WorkoutIdentity(routineId, dayOfWeek, routineExerciseId);

            var nre = new NormalRoutineExercise(id, exerciseId, reps, weight);

            var day = days.FirstOrDefault(x => x.DayOfWeek == dayOfWeek);
            if (day == null)
                day = new Day(dayOfWeek, new List<NormalRoutineExercise>().ToArray());

            var reList = day.RoutineExercises.ToList();
            reList.Add(nre);
            day.RoutineExercises = reList.ToArray();
            days.Add(day);
            return this;
        }

        public IRoutine Build() {
            if (days == null)
                throw new ArgumentNullException("It's necessary define the days");

            var wId = new WorkoutIdentity(routineId);
            var routine = new Routine(wId, name);

            var art = new AddRoutineTransaction(wId, routine.Name);
            art.Execute();

            foreach (var d in days)
            {
                var adt = new AddDayTransaction(wId.RoutineId,d.DayOfWeek);
                adt.Execute();

                foreach (var re in d.RoutineExercises)
                {
                    var anret = new AddNormalRoutineExerciseTransaction(new WorkoutIdentity(routineId,d.DayOfWeek),re.ExerciseId,re.Reps,re.Weight);
                    anret.Execute();
                }
            }

            return WorkoutDatabase.GetRoutine(routineId);
        }
    }

    public class BaseTest
    {
        internal static IRoutine CreateAndReturnRoutine(WorkoutIdentity id, int exerciseId, int reps, double weight)
        {
            var routine = new RoutineBuilder( id.RoutineId, "Default")
              .AddDayAndNormalRoutineExercise(id.DayOfWeek, id.RoutineExerciseId, exerciseId, reps, weight)
              .Build();

            return routine;
        }

        internal static IRoutine ReturnFirstRoutine(int routineId)
        {
            var routineUpdated = WorkoutDatabase.GetRoutine(routineId);

            routineUpdated.Should().NotBeNull();

            return routineUpdated;
        }

        [Obsolete]
        internal static IRoutine ReturnFirstRoutine(WorkoutIdentity wId)
        {
            return ReturnFirstRoutine(wId.RoutineId);
        }

    }
}
