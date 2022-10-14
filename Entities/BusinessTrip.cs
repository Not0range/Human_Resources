using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Entities
{
    internal class BusinessTrip : Move
    {
        public static List<BusinessTrip> BusinessTrips = new List<BusinessTrip>();
        public int Duration { get; set; }
        public string Place { get; set; }

        public BusinessTrip(int id, Employee employee, DateTime date,int duration, string place) : base(id, employee, date)
        {
            Duration = duration;
            Place = place;
        }
    }
}
