using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Entities
{
    internal class PositionChange : Move, IState
    {
        public static List<PositionChange> PositionChanges = new List<PositionChange>();
        public string Position { get; set; }

        public Subdivision Subdivision => null;

        public PositionChange(int id, Employee employee, DateTime date,string position) : base(id, employee, date)
        {
            Position = position;
        }
    }
}
