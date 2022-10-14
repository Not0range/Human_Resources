using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Entities
{
    internal interface IState
    {
        Employee Employee { get; }
        DateTime Date { get; }
        Subdivision Subdivision { get; }
        string Position { get; }
    }

    internal static class StateHelper
    {
        public static IState CreateState(Employee employee, DateTime date, Subdivision subdivision, string position)
        {
            return new State
            {
                Date = date,
                Subdivision = subdivision,
                Employee = employee,
                Position = position
            };
        }

        class State : IState
        {
            public Employee Employee { get; set; }
            public DateTime Date { get; set; }
            public Subdivision Subdivision { get; set; }
            public string Position { get; set; }
        }
    }
}
