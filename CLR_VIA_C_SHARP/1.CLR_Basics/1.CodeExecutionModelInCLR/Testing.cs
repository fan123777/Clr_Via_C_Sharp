using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLR_VIA_C_SHARP.CLR_Basics.Testing
{
    public class Date
    {
        private int month = 7; // Backing store

        public int Month
        {
            // Метод доступа get должен заканчиваться оператором return или throw, а элемент управления не должен выходить за основную часть метода доступа.
            // Метод доступа get можно использовать для возвращения значения поля или для вычисления и возвращения этого значения.
            get
            {
                return month;
            }
            // Когда свойству присваивается значение, выполняется вызов метода доступа set с помощью аргумента, предоставляющего новое значение.
            set
            {
                if((value > 0) && (value < 13))
                {
                    month = value;
                }
            }
        }
    }

    public class Employee
    {
        public static int NumberOfEmployees;
        private static int counter;
        private string name;

        // A read-write instance property:
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        // A read-only static property:
        public static int Counter
        {
            get { return counter; }
        }

        // A Constructor:
        public Employee()
        {
            // Calculate the employee's number:
            counter = ++counter + NumberOfEmployees;
        }
    }

    public class Manager : Employee
    {
        private string name;

        // Notice the use of the new modifier:
        public new string Name
        {
            get { return name; }
            set { name = value + ", Manager"; }
        }
    }

    abstract class Shape
    {
        public abstract double Area
        {
            get;
            set;
        }
    }

    class Square : Shape
    {
        public double side;

        public Square(double s)  //constructor
        {
            side = s;
        }

        public override double Area
        {
            get
            {
                return side * side;
            }
            set
            {
                side = System.Math.Sqrt(value);
            }
        }
    }

    class Cube : Shape
    {
        public double side;

        public Cube(double s)
        {
            side = s;
        }

        public override double Area
        {
            get
            {
                return 6 * side * side;
            }
            set
            {
                side = System.Math.Sqrt(value / 6);
            }
        }
    }

    class ClassCounter  //Это класс - в котором производится счет.
    {
        //Синтаксис по сигнатуре метода, на который мы создаем делегат: 
        //delegate <выходной тип> ИмяДелегата(<тип входных параметров>);
        //Мы создаем на void Message(). Он должен запуститься, когда условие выполнится.

        public delegate void MethodContainer();

        //Событие OnCount c типом делегата MethodContainer.
        public event MethodContainer onCount;

        public void Count()
        {
            for (int i = 0; i < 100; i++)
            {
                if (i == 71)
                {
                    onCount();
                }
            }
        }
    }

    class Handler_I //Это класс, реагирующий на событие (счет равен 71) записью строки в консоли.
    {
        public void Message()
        {
            //Не забудьте using System 
            //для вывода в консольном приложении
            Console.WriteLine("action, now is 71!");
        }
    }

    class Handler_II
    {
        public void Message()
        {
            Console.WriteLine("action, now is 71!");
        }
    }

    public class SampleEventArgs
    {
        public SampleEventArgs(string s) { Text = s; }
        public String Text { get; private set; } // readonly
    }

    public class Publisher
    {
        // Declare the delegate (if using non-generic pattern).
        public delegate void SampleEventHandler(object sender, SampleEventArgs e);

        // Declare the event.
        public event SampleEventHandler SampleEvent;

        // Wrap the event in a protected virtual method
        // to enable derived classes to raise the event.
        protected virtual void RaiseSampleEvent()
        {
            // Raise the event by using the () operator.
            if (SampleEvent != null)
                SampleEvent(this, new SampleEventArgs("Hello"));
        }
    }

    class Counter
    {
        private int threshold;
        private int total;

        public Counter(int passedThreshold)
        {
            threshold = passedThreshold;
        }

        public void Add(int x)
        {
            total += x;
            if (total >= threshold)
            {
                ThresholdReachedEventArgs args = new ThresholdReachedEventArgs();
                args.Threshold = threshold;
                args.TimeReached = DateTime.Now;
                OnThresholdReached(args);
            }
        }

        protected virtual void OnThresholdReached(ThresholdReachedEventArgs e)
        {
            EventHandler<ThresholdReachedEventArgs> handler = ThresholdReached;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler<ThresholdReachedEventArgs> ThresholdReached;
    }

    public class ThresholdReachedEventArgs : EventArgs
    {
        public int Threshold { get; set; }
        public DateTime TimeReached { get; set; }
    }

    // Define a class to hold custom event info
    public class CustomEventArgs : EventArgs
    {
        public CustomEventArgs(string s)
        {
            message = s;
        }
        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; }
        }
    }

    // Class that publishes an event
    class MyPublisher
    {

        // Declare the event using EventHandler<T>
        public event EventHandler<CustomEventArgs> RaiseCustomEvent;

        public void DoSomething()
        {
            // Write some code that does something useful here
            // then raise the event. You can also raise an event
            // before you execute a block of code.
            OnRaiseCustomEvent(new CustomEventArgs("Did something"));

        }

        // Wrap event invocations inside a protected virtual method
        // to allow derived classes to override the event invocation behavior
        protected virtual void OnRaiseCustomEvent(CustomEventArgs e)
        {
            // Make a temporary copy of the event to avoid possibility of
            // a race condition if the last subscriber unsubscribes
            // immediately after the null check and before the event is raised.
            EventHandler<CustomEventArgs> handler = RaiseCustomEvent;

            // Event will be null if there are no subscribers
            if (handler != null)
            {
                // Format the string to send inside the CustomEventArgs parameter
                e.Message += String.Format(" at {0}", DateTime.Now.ToString());

                // Use the () operator to raise the event.
                handler(this, e);
            }
        }
    }

    //Class that subscribes to an event
    class Subscriber
    {
        private string id;
        public Subscriber(string ID, MyPublisher pub)
        {
            id = ID;
            // Subscribe to the event using C# 2.0 syntax
            pub.RaiseCustomEvent += HandleCustomEvent;
        }

        // Define what actions to take when the event is raised.
        void HandleCustomEvent(object sender, CustomEventArgs e)
        {
            Console.WriteLine(id + " received this message: {0}", e.Message);
        }
    }

    class SampleCollection<T>
    {
        // Declare an array to store the data elements.
        private T[] arr = new T[100];

        // Define the indexer, which will allow client code
        // to use [] notation on the class instance itself.
        // (See line 2 of code in Main below.)        
        public T this[int i]
        {
            get
            {
                // This indexer is very simple, and just returns or sets
                // the corresponding element from the internal array.
                return arr[i];
            }
            set
            {
                arr[i] = value;
            }
        }
    }
}
