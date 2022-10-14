using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Entities
{
    internal class Employee
    {
        public static List<Employee> Employees = new List<Employee>();

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Passport { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public Employee(int id, string name, DateTime birthDate, string passport, string address, string phone)
        {
            Id = id;
            Name = name;
            BirthDate = birthDate;
            Passport = passport;
            Address = address;
            Phone = phone;
        }

        public bool Check()
        {
            return Hiring.Hirings.Any(t => t.Employee == this) &&
                PositionChange.PositionChanges.Any(t => t.Employee == this) &&
                SubdivisionChange.SubdivisionChanges.Any(t => t.Employee == this) &&
                BusinessTrip.BusinessTrips.Any(t => t.Employee == this) &&
                Vacation.Vacations.Any(t => t.Employee == this) &&
                Dismissal.Dismissals.Any(t => t.Employee == this);
        }

        public IState CurrentState()
        {
            var moves = Hiring.Hirings.Where(t => t.Employee == this).Cast<IState>().Union(
                PositionChange.PositionChanges.Where(t => t.Employee == this).Cast<IState>()).Union(
                SubdivisionChange.SubdivisionChanges.Where(t => t.Employee == this).Cast<IState>()).Union(
                Dismissal.Dismissals.Where(t => t.Employee == this).Cast<IState>()).OrderByDescending(t => t.Date);
            string position = null;
            Subdivision subdivision = null;
            foreach (var item in moves)
            {
                if (position == null && item.Position != null)
                    position = item.Position;
                if (subdivision == null && item.Subdivision != null)
                    subdivision = item.Subdivision;

                if (item.Position == Dismissal.DISMISSALED || position != null && subdivision != null)
                    break;
            }
            return StateHelper.CreateState(this, DateTime.Now, subdivision, position);
        }

        public override string ToString()
        {
            var state = CurrentState();
            var s = state.Subdivision != null && state.Position != null ? " (" + state.Subdivision.Name + ": " + state.Position + ")" : "";
            return Name + s;
        }
    }
}
