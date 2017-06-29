using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutLog.Database;

namespace WorkoutLog.Training
{
    public class TrainingDay : ITrainingDay
    {      
        private readonly DayOfWeek dayOfWeek;
        private ITrainingRoutineExercise[] tre;
        private DateTime _endDate;
        private DateTime _dayAndHour;

        public TrainingDay(DateTime dayAndHour, DayOfWeek dayOfWeek, ITrainingRoutineExercise[] tre)
        {
            this.dayOfWeek = dayOfWeek;

            this.tre = tre;
            this._dayAndHour = dayAndHour;
        }

        public DayOfWeek DayOfWeek
        {
            get
            {
                return dayOfWeek;
            }
        }

        public DateTime BeginDate
        {
            get {
                return _dayAndHour;
            }
        }

        public ITrainingRoutineExercise[] TrainingRoutineExercises
        {
            get
            {
                return tre;
            }
        }

        public DateTime EndDate {
            get => _endDate;
            set => _endDate = value;
        }

        public ITrainingRoutineExercise GetNextExercise()
        {
            return TrainingDayDatabase.GetNextExercise(tre);
        }
    }
}
