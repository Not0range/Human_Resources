using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Entities
{
    internal abstract class Move
    {
        public int Id { get; set; }
        public Employee Employee { get; set; }
        public DateTime Date { get; set; }

        protected Move(int id, Employee employee, DateTime date)
        {
            Id = id;
            Employee = employee;
            Date = date;
        }
    }
}
