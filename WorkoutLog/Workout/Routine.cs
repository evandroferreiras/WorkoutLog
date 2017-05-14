using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutLog.Workout
{
    public class Routine : IRoutine
    {
        private string name;
        private readonly WorkoutIdentity wId;
        private IDay[] days;

        public Routine(WorkoutIdentity wId, string name,  IDay[] days)
        {

            this.days = days;
            this.wId = wId;
            UpdateName(name);
        }

        public int RoutineId => wId.RoutineId;

        

        public string Name => name;

        public IDay[] Days { get => days; set => days = value; }

        public void UpdateName(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("The routine name is required");
            this.name = name;
        }
    }
}
