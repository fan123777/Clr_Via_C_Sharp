using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLR_VIA_C_SHARP._2_Type_Design._4_TypeBasics
{
    class Testing
    {
    }

    class Employee
    {
        public Employee(string name)
        {

        }
        public Employee()
        {

        }

        public Int32 GetYearsEmployed()
        {
            return 5;
        }
        public virtual String GetProgressReport()
        {
            return "Joe";
        }
        public static Employee Lookup(String name)
        {
            return new Employee();
        }
    }

    class Manager : Employee
    {
        public Manager()
        {

        }

        public override String GetProgressReport()
        {
            return "Oh";
        }
    }

    class B
    {

    }

    class D : B
    {

    }
}