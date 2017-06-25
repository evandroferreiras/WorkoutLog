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
        private readonly TrainingIdentity _tId;
        private readonly DayOfWeek dayOfWeek;
        private ITrainingRoutineExercise[] tre;
        private DateTime _endDate;

        public TrainingDay(TrainingIdentity tId, DayOfWeek dayOfWeek, ITrainingRoutineExercise[] tre)
        {
            this.dayOfWeek = dayOfWeek;

            this.tre = tre;
            this._tId = tId;
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
                return _tId.DayAndHour;
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
