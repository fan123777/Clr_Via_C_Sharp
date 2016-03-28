using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLR_VIA_C_SHARP._2_Type_Design._8_Methods
{
    delegate void DisplayMessage(string message);

    class Testing
    {
        public void main()
        {
            // foreach
            // The foreach statement repeats a group of embedded statements for each element in an array or an object collection that implements the System.Collections.IEnumerable or System.Collections.Generic.IEnumerable<T> interface
            // At any point within the foreach block, you can break out of the loop by using the break keyword, or step to the next iteration in the loop by using the continue keyword.
            // A foreach loop can also be exited by the goto, return, or throw statements.

            int[] fibarray = new int[] { 0, 1, 1, 2, 3, 5, 8, 13 };
            foreach (int element in fibarray)
            {
                System.Console.WriteLine(element);
            }
            System.Console.WriteLine();

            // Action
            // Encapsulates a method that has a single parameter and does not return a value.
            // You can use the Action<T> delegate to pass a method as a parameter without explicitly declaring a custom delegate.The encapsulated method must correspond to the method signature that is defined by this delegate.This means that the encapsulated method must have one parameter that is passed to it by value, and it must not return a value.

            // =>

            // типы делегаты

        }
    }
}
