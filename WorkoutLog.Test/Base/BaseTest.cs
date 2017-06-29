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
    public struct TrainingIdentity
    {

        public TrainingIdentity(int routineId, DayOfWeek dayOfWeek, DateTime dayAndHour)
        {
            DayAndHour = dayAndHour;
            this.RoutineId = routineId;
            this.DayOfWeek = dayOfWeek;
        }
        public int RoutineId { get; private set; }
        public DayOfWeek DayOfWeek { get; private set; }
        public DateTime DayAndHour { get; private set; }
    }

    public struct WorkoutIdentity
    {
        public int RoutineId { get; }
        public DayOfWeek DayOfWeek { get; }
        public int RoutineExerciseId { get; }

        public WorkoutIdentity(int routineId) : this()
        {
            this.RoutineId = routineId;
        }

        public WorkoutIdentity(int routineId, DayOfWeek day) : this(routineId)
        {
            this.DayOfWeek = day;
        }

        public WorkoutIdentity(int routineId, DayOfWeek day, int routineExerciseId) : this(routineId, day)
        {
            this.RoutineExerciseId = routineExerciseId;
        }
    }

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

            var nre = new NormalRoutineExercise(exerciseId, reps, weight);

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
            var routine = new Routine(routineId, name);

            var art = new AddRoutineTransaction(routineId, routine.Name);
            art.Execute();

            foreach (var d in days)
            {
                var adt = new AddDayTransaction(wId.RoutineId,d.DayOfWeek);
                adt.Execute();

                foreach (var re in d.RoutineExercises)
                {
                    var anret = new AddNormalRoutineExerciseTransaction(routineId,d.DayOfWeek,re.ExerciseId,re.Reps,re.Weight);
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
    }
}
