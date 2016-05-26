using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Collections;
using System.Diagnostics;

namespace CLR_VIA_C_SHARP._2_Type_Design.Testing
{
    class Testing
    {
        public static void main()
        {
            // 4 type basics
            nsTypeBasics.TestTypeBasics testTypeBasics = new nsTypeBasics.TestTypeBasics();
            testTypeBasics.Run();
            testTypeBasics.TestCast();
            testTypeBasics.TestTypeControl();

            // 5 Primitive Reference Significant Types
            nsPrimitiveReferenceSignificantTypes.PrimitiveReferenceSignificantTypes testTypes = new nsPrimitiveReferenceSignificantTypes.PrimitiveReferenceSignificantTypes();
            testTypes.Run();

            // 6 Members and Types
            nsMembersAndTypes.MembersAndTypes testMembersAndTypes = new nsMembersAndTypes.MembersAndTypes();
            testMembersAndTypes.Run();

            // 7 Constants and Fields
            nsConstantsAndFields.ConstantsAndFields testConstantsFields = new nsConstantsAndFields.ConstantsAndFields();
            testConstantsFields.Run();

            // 8 Methods
            nsMethods.Methods testMethods = new nsMethods.Methods();
            testMethods.Run();
            testMethods.TestExtensionMethods();
            testMethods.TestPartialMethod();

            // 9 Parameters
            nsParameters.Parameters testParameters= new nsParameters.Parameters();
            testParameters.Run();

            // 10 Properties
            nsProperties.Properties testProperties = new nsProperties.Properties();
            testProperties.Run();

            // 11 Events
            nsEvents.Events testEvents = new nsEvents.Events();
            testEvents.Run();

            // 12 Generic
            nsGeneric.Generic testGeneric = new nsGeneric.Generic();
            testGeneric.Run();
        }
    }

    namespace nsTypeBasics
    {
        class TestTypeBasics
        {
            public void Run()
            {
                Employee e1 = new Employee("David", 25, 1);
                Employee e2 = new Employee("Cavin", 30, 2);
                Employee e3 = new Employee("Cavin", 30, 3);
                Employee e4 = e1;

                // Equals
                // возвращает true если указатели одинаковые, а не значения полей.
                // Equals is a virtual method, enabling any class to override its implementation.
                // Any class that represents a value, essentially any value type, or a set of values as a group, such as a complex number class, should override Equals.
                // If the type implements IComparable, it should override Equals.
                Boolean equal = e1.Equals(e4); // true
                equal = e2.Equals(e3); // false
                equal = e2.Equals(e4); // false
                // after overriding true, true, false.

                // GetHashCode
                Int32 hashCode = e1.GetHashCode(); // hash(e1) = hash(e4)
                hashCode = e2.GetHashCode(); // hash(e2) != hash(e3)
                hashCode = e3.GetHashCode();
                hashCode = e4.GetHashCode();

                // ToString
                // this.GetType().FullName по умолчанию
                String stringValue = e1.ToString();
                stringValue = e2.ToString();
                stringValue = e3.ToString();
                stringValue = e4.ToString();

                // GetType
                // not virtual, can't override
                Type type = e1.GetType();
                type = e2.GetType();
                type = e3.GetType();
                type = e4.GetType();

                // MemberwiseClone
                // The MemberwiseClone method creates a shallow copy by creating a new object, and then copying the nonstatic fields of the current object to the new object.
                // If a field is a value type, a bit-by-bit copy of the field is performed.
                // If a field is a reference type, the reference is copied but the referred object is not; therefore, the original object and its clone refer to the same object.

                // Finalize
                // You should only implement a Finalize method to clean up unmanaged resources.
                // You should not implement a Finalize method for managed objects, because the garbage collector cleans up managed resources automatically.
                // If you want the garbage collector to perform cleanup operations on your object before it reclaims the object's memory, you must override this method in your class.

                // An object's Finalize method should release all resources that are held onto by the object.
                // It should also call the Finalize method for the object's base class.
                // An object's Finalize method should not call a method on any objects other than that of its base class.
                // This is because the other objects being called could be collected at the same time as the calling object, such as in the case of a common language runtime shutdown.
                // If you allow any exceptions to escape the Finalize method, the system assumes that the method returned, and continues calling the Finalize methods of other objects.

                // Operator new:
                // 1. Вычисление количества байтов, необходимых для хранения всех экземплярных полей типа и всех его базовых типов, включая System.Object (в котором отсутствуют собственные экземплярные поля).
                // Кроме того, в каждом объекте кучи должны присутствовать дополнительные члены, называемые указателем на объект-тип (type object pointer) и индексом блока синхронизации (sync block index); они необходимы CLR для управления объектом.
                // Байты этих дополнительных членов добавляются к байтам, необходимым для размещения самого объекта.
                // - указателем на объект-тип (type object pointer)? 
                // - индексом блока синхронизации (sync block index)?
                // 2. Выделение памяти для объекта с резервированием необходимого для данного типа количества байтов в управляемой куче.
                // Выделенные байты инициализируются нулями (0).
                // 3. Инициализация указателя на объект-тип и индекса блока синхронизации.
                // 4. Вызов конструктора экземпляра типа с параметрами, указанными при вызове new.
                // Большинство компиляторов автоматически включает в конструктор код вызова конструктора базового класса.
                // Каждый конструктор выполняет инициализацию определенных в соответствующем типе полей. В частности, вызывается конструктор System.Object, но он ничего не делает и просто возвращает управление.
                // Кстати, у оператора new нет пары — оператора delete, то есть нет явного способа освобождения памяти, занятой объектом.

                // The type object pointer is a pointer to a type description of the object.
                // This is used to find out what the actual type of an object is, for example needed to do virtual calls.

                // The sync block index is an index into a table of synchronisation blocks.
                // Each object can have a sync block, that contains information used by Monitor.Enter and Monitor.Exit.
            }

            public void TestCast()
            {
                // Приведение типов
                // тип Employee не может переопределить метод GetType, чтобы тот вернул тип SuperHero.
                // CLR разрешает привести тип объекта к его собственному типу или любому из его базовых типов.
                Object o = new Person();
                Person p = (Person)o;
                // InvalidCastException if we have wrong type.

                // Приведение типов в C# с помощью операторов is и as
                Boolean b1 = (o is Object); // true
                Boolean b2 = (o is Person); // false

                if (o is Person)
                {
                    Person p1 = (Person)(o);
                }

                Person p2 = o as Person;
                if(p2 != null)
                {

                }
            }

            public void TestTypeControl()
            {
                Object o1 = new Object();
                Object o2 = new B();
                Object o3 = new D();
                Object o4 = o3;
                B b1 = new B();
                B b2 = new D();
                D d1 = new D();
                // B b3 = new Object(); // CTE
                // D d2 = new Object(); // CTE
                B b4 = d1;
                // D d3 = b2; // CTE
                D d4 = (D)d1;
                D d5 = (D)b2;
                // D d6 = (D)b1; // RTE
                // B b5 = (B)o1; // RTE
                B b6 = (D)b2;
            }
        }

        class Employee
        {
            private String mName;
            private Int32 mAge;
            private Int32 mId;

            public Employee(String name, Int32 age, Int32 id)
            {
                mName = name;
                mAge = age;
                mId = id;
            }

            // The new implementation of Equals should not throw exceptions.
            // It is recommended that any class that overrides Equals also override System.Object.GetHashCode.
            // It is also recommended that in addition to implementing Equals(object), any class also implement Equals(type) for their own type, to enhance performance. 

            public override bool Equals(Object obj)
            {
                // If parameter is null return false.
                if (obj == null)
                {
                    return false;
                }

                // If parameter cannot be cast to Point return false.
                Employee e = obj as Employee;
                if ((System.Object)e == null)
                {
                    return false;
                }

                // Return true if the fields match:
                return (mName == e.mName) && (mAge == e.mAge);
            }

            public bool Equals(Employee e)
            {
                // If parameter is null return false:
                if ((object)e == null)
                {
                    return false;
                }

                // Return true if the fields match:
                return (mName == e.mName) && (mAge == e.mAge);
            }

            public override int GetHashCode()
            {
                return mAge ^ mId;
            }

            // Overriding Operator ==
            // By default, the operator == tests for reference equality by determining if two references indicate the same object, so reference types do not need to implement operator == in order to gain this functionality.
            // When a type is immutable, meaning the data contained in the instance cannot be changed, overloading operator == to compare value equality instead of reference equality can be useful because, as immutable objects, they can be considered the same as long as they have the same value.
            // Overriding operator == in non-immutable types is not recommended.
            // Overloaded operator == implementations should not throw exceptions.
            // Any type that overloads operator == should also overload operator !=. 

            public static bool operator ==(Employee a, Employee b)
            {
                // If both are null, or both are same instance, return true.
                if (System.Object.ReferenceEquals(a, b))
                {
                    return true;
                }

                // If one is null, but not both, return false.
                if (((object)a == null) || ((object)b == null))
                {
                    return false;
                }

                // Return true if the fields match:
                return a.mName == b.mName && a.mAge == b.mAge && a.mId == b.mId;
            }

            public static bool operator !=(Employee a, Employee b)
            {
                return !(a == b);
            }

            public override string ToString()
            {
                return "Name = " + mName + "; Age = " + mAge.ToString() + "; Id = " + mId.ToString() + ";";
            }

            public Employee ShallowCopy()
            {
                return (Employee)this.MemberwiseClone();
            }

            public Employee DeepCopy()
            {
                Employee other = (Employee)this.MemberwiseClone();
                other.mName = string.Copy(mName);
                return other;
            }
        }

        // Any derived class that can call Equals on the base class should do so before finishing its comparison.
        // In the following example, Equals calls the base class Equals, which checks for a null parameter and compares the type of the parameter with the type of the derived class.
        // That leaves the implementation of Equals on the derived class the task of checking the new data field declared on the derived class

        class Manager : Employee
        {
            Int32 mLevel;

            public Manager(String name, Int32 age, Int32 id, Int32 level)
                : base(name, age, id)
            {
                mLevel = level;
            }

            public override bool Equals(Object obj)
            {
                // If parameter cannot be cast to ThreeDPoint return false:
                Manager m = obj as Manager;
                if ((object)m == null)
                {
                    return false;
                }

                // Return true if the fields match:
                return base.Equals(obj) && mLevel == m.mLevel;
            }

            public bool Equals(Manager m)
            {
                // Return true if the fields match:
                return base.Equals((Employee)m) && mLevel == m.mLevel;
            }

            public override int GetHashCode()
            {
                return base.GetHashCode() ^ mLevel;
            }
        }

        class Person
        {

        }

        class B
        {

        }

        class D : B
        {

        }
    }

    namespace nsPrimitiveReferenceSignificantTypes
    {
        class PrimitiveReferenceSignificantTypes
        {
            public void Run()
            {
                UInt32 invalid = unchecked((UInt32)(-1)); // OK

                Byte b = 100; // Выдается исключение
                // b = checked((Byte)(b + 200)); // OverflowException
                b = (Byte)checked(b + 200); // b содержит 44; нет OverflowException
            }
        }
    }

    namespace nsMembersAndTypes
    {
        class MembersAndTypes
        {
            public void Run()
            {
                Employee e = new Employee("John", 2, "Great worker");
                // Employee.registerEmployee(e);
            }
        }

        public sealed class SomeType
        {
            // Вложенный класс
            private class SomeNestedType
            {
            }

            // Константа, неизменяемое и статическое изменяемое поле
            // Constant, readonly, and static read/write field
            private const Int32 c_SomeConstant = 1;
            private readonly String m_SomeReadOnlyField = "2";
            private static Int32 s_SomeReadWriteField = 3;

            // Конструктор типа
            static SomeType()
            {

            }

            // Конструкторы экземпляров
            public SomeType()
            {

            }

            public SomeType(Int32 x)
            {

            }

            // Экземплярный и статический методы
            private String InstanceMethod()
            {
                return null;
            }

            public static void func()
            {

            }

            // Необобщенное экземплярное свойство
            public Int32 SomeProp
            {
                get { return 0; }
                set { }
            }

            // Обобщенное экземплярное свойство
            public Int32 this[String s]
            {
                get { return 0; }
                set { }
            }

            // Экземплярное событие
            public event EventHandler SomeEvent;
        }

        internal class Employee
        {
            Int32 m_YearsEmployed;
            String m_ProgressReport;
            String m_Name;

            static Dictionary<String, Employee> m_Employees;

            public Employee(String name, Int32 yearsEmployed, String progressReport)
            {
                m_Name = name;
                m_YearsEmployed = yearsEmployed;
                m_ProgressReport = progressReport;

                m_Employees[m_Name] = this;
            }

            static Employee()
            {
                m_Employees = new Dictionary<string, Employee>();
            }

            public String GetName()
            {
                return m_Name;
            }

            public static void registerEmployee(Employee e)
            {
                m_Employees[e.GetName()] = e;
            }

            // Невиртуальный экземплярный метод
            public Int32 GetYearsEmployed()
            {
                return m_YearsEmployed;
            }

            // Виртуальный метод (виртуальный - значит, экземплярный)
            public virtual String GetProgressReport()
            {
                return m_ProgressReport;
            }

            // Статический метод
            public static Employee Lookup(String name)
            {
                return m_Employees[name];
            }
        }
    }

    namespace nsConstantsAndFields
    {
        class ConstantsAndFields
        {
            public void Run()
            {
                // Неизменность поля ссылочного типа означает неизменность ссылки, которую этот тип содержит, а вовсе не объекта, на которую указывает ссылка

            }
        }

        public sealed class SomeLibraryType
        {
            public const Int32 MaxEntriesInList = 50;
            public static readonly Int32 MaxEntries = 50;
        }

        public sealed class SomeType
        {
            // Статическое неизменяемое поле. Его значение рассчитывается
            // и сохраняется в памяти при инициализации класса во время выполнения
            public static readonly Random s_random = new Random();

            // Статическое изменяемое поле
            private static Int32 s_numberOfWrites = 0;

            // Неизменяемое экземплярное поле
            public readonly String Pathname = "Untitled";

            // Изменяемое экземплярное поле
            private System.IO.FileStream m_fs;

            public SomeType(String pathname)
            {
                // Эта строка изменяет значение неизменяемого поля
                // В данном случае это возможно, так как показанный далее код
                // расположен в конструкторе
                this.Pathname = pathname;
            }

            public String DoSomething()
            {
                // Эта строка читает и записывает значение статического изменяемого поля
                s_numberOfWrites = s_numberOfWrites + 1;
                // Эта строка читает значение неизменяемого экземплярного поля
                return Pathname;
            }
        }
    }

    namespace nsMethods
    {
        class Methods
        {
            public void Run()
            {
                Rational r1 = 5; // Неявное приведение Int32 к Rational
                Rational r2 = 2.5F; // Неявное приведение Single к Rational
                Int32 x = (Int32)r1; // Явное приведение Rational к Int32
                Single s = (Single)r2; // Явное приведение Rational к Single

                // С# генерирует код вызова операторов неявного преобразования в случае, когда используется выражение приведения типов.
                // Однако операторы неявного преобразования никогда не вызываются, если используется оператор as или is.
            }

            public void TestExtensionMethods()
            {
                // Инициализирующая строка
                StringBuilder sb = new StringBuilder("Hello. My name is Jeff.");

                // Замена точки восклицательным знаком
                // и получение номера символа в первом предложении (5)
                Int32 index = sb.Replace('.', '!').IndexOf('!');

                // Показывает каждый символ в каждой строке консоли
                "Grant".ShowItems();
                // Показывает каждую строку в каждой строке консоли
                new[] { "Jeff", "Kristin" }.ShowItems();
                // Показывает каждый Int32 в каждой строчке консоли.
                new List<Int32>() { 1, 2, 3 }.ShowItems();
            }

            public void TestPartialMethod()
            {

            }
        }

        internal sealed class SomeType
        {
            // Здесь нет кода, явно инициализирующего поля
            private Int32 m_x;
            private String m_s;
            private Double m_d;
            private Byte m_b;
            // Код этого конструктора инициализирует поля значениями по умолчанию
            // Этот конструктор должен вызываться всеми остальными конструкторами
            public SomeType()
            {
                m_x = 5;
                m_s = "Hi there";
                m_d = 3.14159;
                m_b = 0xff;
            }
            // Этот конструктор инициализирует поля значениями по умолчанию,
            // а затем изменяет значение m_x
            public SomeType(Int32 x)
                : this()
            {
                m_x = x;
            }
            // Этот конструктор инициализирует поля значениями по умолчанию,
            // а затем изменяет значение m_s
            public SomeType(String s)
                : this()
            {
                m_s = s;
            }
            // Этот конструктор инициализирует поля значениями по умолчанию,
            // а затем изменяет значения m_x и m_s
            public SomeType(Int32 x, String s)
                : this()
            {
                m_x = x;
                m_s = s;
            }
        }

        public sealed class Complex
        {
            public static Complex operator+(Complex c1, Complex c2)
            {
                return new Complex();
            }
        }

        public sealed class Rational
        {
            // Создает Rational из Int32
            public Rational(Int32 num)
            {

            }

            // Создает Rational из Single
            public Rational(Single num)
            {

            }

            // Преобразует Rational в Int32
            public Int32 ToInt32()
            {
                return 5;
            }

            // Преобразует Rational в Single
            public Single ToSingle()
            {
                return 5;
            }

            // Неявно создает Rational из Int32 и возвращает полученный объект
            public static implicit operator Rational(Int32 num)
            {
                return new Rational(num);
            }
            // Неявно создает Rational из Single и возвращает полученный объект
            public static implicit operator Rational(Single num)
            {
                return new Rational(num);
            }
            // Явно возвращает объект типа Int32, полученный из Rational
            public static explicit operator Int32(Rational r)
            {
                return r.ToInt32();
            }
            // Явно возвращает объект типа Single, полученный из Rational
            public static explicit operator Single(Rational r)
            {
                return r.ToSingle();
            }
        }

        public static class StringBuilderExtensions
        {
            public static Int32 IndexOf(this StringBuilder sb, Char value)
            {
                for (Int32 index = 0; index < sb.Length; index++)
                    if (sb[index] == value)
                        return index;
                return -1;
            }

            public static void ShowItems<T>(this IEnumerable<T> collection)
            {
                foreach (var item in collection)
                    Console.WriteLine(item);
            }
        }

        // Сгенерированный при помощи инструмента программный код
        internal sealed partial class Base
        {
            private String m_name;
            // Это объявление с определением частичного метода вызывается
            // перед изменением поля m_name
            partial void OnNameChanging(String value);
            public String Name
            {
                get { return m_name; }
                set
                {
                    // Информирование класса о потенциальном изменении
                    OnNameChanging(value.ToUpper());
                    m_name = value; // Изменение поля
                }
            }
        }

        // Написанный программистом код, содержащийся в другом файле
        internal sealed partial class Base
        {
            // Это объявление с реализацией частичного метода вызывается перед тем,
            // как будет изменено поле m_name
            partial void OnNameChanging(String value)
            {
                if (String.IsNullOrEmpty(value))
                    throw new ArgumentNullException("value");
            }
        }
    }

    namespace nsParameters
    {
        public class Parameters
        {
            private static Int32 s_n = 0;

            public void Run()
            {
                // 1. Аналогично: M(9, "A", default(DateTime), new Guid());
                M();
                // 2. Аналогично: M(8, "X", default(DateTime), new Guid());
                M(8, "X");
                // 3. Аналогично: M(5, "A", DateTime.Now, Guid.NewGuid());
                M(5, guid: Guid.NewGuid(), dt: DateTime.Now);
                M(5, guid: Guid.NewGuid(), dt: DateTime.Now);
                // 4. Аналогично: M(0, "1", default(DateTime), new Guid());
                M(s_n++, s_n++.ToString());
                // 5. Аналогично: String t1 = "2"; Int32 t2 = 3;
                // M(t2, t1, default(DateTime), new Guid());
                M(s: (s_n++).ToString(), x: s_n++);

                Int32 x;
                GetVal(out x);
                Console.WriteLine(x);

                Int32 y = 5; // не скомпилируеться без присваивания, так как ref.
                AddVal(ref y);
                Console.WriteLine(y);

                // Переменное количество параметров
                Int32 sum = Add(1, 2, 3);
                Console.WriteLine(sum);

                // при написании метода, работающего с набором элементов, лучше всего объявить параметр метода,
                // используя интерфейс IEnumerable<T> вместо сильного типа данных, например List<T>,
                // или еще более сильного интерфейсного типа ICollection<T> или IList<T>:

                // Естественно, при создании метода, получающего список (а не просто любой перечислимый объект),
                // нужно объявлять тип параметра как IList<T>, в то время как типа List<T> лучше избегать.

                // Этот же подход применим к классам, опирающимся на архитектуру базовых классов.

                // В то же время, объявляя тип возвращаемого методом объекта, желательно выбирать самый сильный из доступных вариантов
                // (пытаясь не ограничиваться конкретным типом). Например, лучше объявлять метод, возвращающий объект FileStream, а не Stream:

                // Однако для метода, возвращающего объект List<String>, вполне возможно изменение реализации, после которого 
                // он начнет возвращать тип String[]. В подобных случаях следует выбирать более слабый тип возвращаемого объекта.


            }

            private static void M(Int32 x = 9, String s = "A", DateTime dt = default(DateTime), Guid guid = new Guid())
            {
                Console.WriteLine("x={0}, s={1}, dt={2}, guid={3}", x, s, dt, guid);
            }

            private static void GetVal(out Int32 v)
            {
                v = 10;
            }

            private static void AddVal(ref Int32 v)
            {
                v += 10;
            }

            static Int32 Add(params Int32[] values)
            {
                // ПРИМЕЧАНИЕ: при необходимости этот массив
                // можно передать другим методам
                Int32 sum = 0;
                if (values != null)
                {
                    for (Int32 x = 0; x < values.Length; x++)
                        sum += values[x];
                }
                return sum;
            }

            // Рекомендуется в этом методе использовать параметр слабого типа
            public void ManipulateItems<T>(IEnumerable<T> collection)
            {

            }

            // Не рекомендуется в этом методе использовать параметр сильного типа
            public void BadManipulateItems<T>(List<T> collection)
            {

            }

            // Рекомендуется в этом методе использовать параметр мягкого типа
            public void ProcessBytes(Stream someStream)
            {

            }

            // Не рекомендуется в этом методе использовать параметр сильного типа
            public void BadProcessBytes(FileStream fileStream)
            {

            }

            // Рекомендуется в этом методе использовать
            // сильный тип возвращаемого объекта
            public FileStream OpenFile()
            {
                return null;
            }

            // Не рекомендуется в этом методе использовать
            // слабый тип возвращаемого объекта
            public Stream BadOpenFile()
            {
                return null;
            }

            // Гибкий вариант: в этом методе используется
            // мягкий тип возвращаемого объекта
            public IList<String> GetStringCollection()
            {
                return null;
            }

            // Негибкий вариант: в этом методе используется
            // сильный тип возвращаемого объекта
            public List<String> BadGetStringCollection()
            {
                return null;
            }
        }
    }

    namespace nsProperties
    {
        public class Properties
        {
            public void Run()
            {
                Employee e = new Employee();
                e.Name = "Jeffrey Richter";
                String employeeName = e.Name;
                e.Age = 41;
                // e.Age = -5; // ArgumentOutOfRangeException
                Int32 employeeAge = e.Age;

                // Методы get и set свойства довольно часто манипулируют закрытым полем,
                // определенным в типе. Это поле обычно называют резервным (backing field).
                e.Test = 5;
                Int32 test = e.Test;

                Employee edy = new Employee() { Name = "Edy", Age = 25 };
                String s = new Employee { Name = "Jeff", Age = 45 }.ToString().ToUpper();

                // Кортежный тип (tuple type) — это тип, который содержит коллекцию свойств, каким-то образом связанных друг с другом.
                // Определение типа, создание сущности и инициализация свойств
                var o1 = new { Name = "Jeff", Year = 1964 };
                // Вывод свойств на консоль
                Console.WriteLine("Name={0}, Year={1}", o1.Name, o1.Year);

                String Name = "Grant";
                DateTime dt = DateTime.Now;
                // Анонимный тип с двумя свойствами
                // 1. Строковому свойству Name назначено значение Grant
                // 2. Свойству Year типа Int32 Year назначен год из dt
                var o2 = new { Name, dt.Year };

                var people = new[]
                {
                    o1, // См. ранее в этом разделе
                    new { Name = "Kristin", Year = 1970 },
                    new { Name = "Aidan", Year = 2003 },
                    new { Name = "Grant", Year = 2008 }
                };

                // Организация перебора массива анонимных типов
                // (ключевое слово var обязательно).
                foreach (var person in people)
                    Console.WriteLine("Person={0}, Year={1}", person.Name, person.Year);

                // LINQ example
                String myDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                var query =
                from pathname in Directory.GetFiles(myDocuments)
                let LastWriteTime = File.GetLastWriteTime(pathname)
                where LastWriteTime > (DateTime.Now - TimeSpan.FromDays(7))
                orderby LastWriteTime
                select new { Path = pathname, LastWriteTime };
                foreach (var file in query)
                    Console.WriteLine("LastWriteTime={0}, Path={1}",
                    file.LastWriteTime, file.Path);

                var minmax = MinMax(6, 2);
                Console.WriteLine("Min={0}, Max={1}", minmax.Item1, minmax.Item2); // Min=2, Max=6

                // Свойства с параметрами

                // Выделить массив BitArray, который может хранить 14 бит
                BitArray ba = new BitArray(14);
                // Установить все четные биты вызовом метода доступа set
                for (Int32 x = 0; x < 14; x++)
                {
                    ba[x] = (x % 2 == 0);
                }

                // Вывести состояние всех битов вызовом метода доступа get
                for (Int32 x = 0; x < 14; x++)
                {
                    Console.WriteLine("Bit " + x + " is " + (ba[x] ? "On" : "Off"));
                }
            }

            // Возвращает минимум в Item1 и максимум в Item2
            private static Tuple<Int32, Int32> MinMax(Int32 a, Int32 b)
            {
                return new Tuple<Int32, Int32>(Math.Min(a, b), Math.Max(a, b));
            }
        }

        public sealed class Employee
        {
            private String m_Name;
            private Int32 m_Age;

            public String GetName()
            {
                return m_Name;
            }

            public void SetName(String value)
            {
                m_Name = value;
            }

            public Int32 GetAge()
            {
                return m_Age;
            }

            public void SetAge(Int32 value)
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("value", value.ToString(), "The value must be greater or equal to 0");
                m_Age = value;
            }

            public String Name
            {
                get { return m_Name; }
                set { m_Name = value; } // ключевое слово value идентифицирует новое значение
            }

            public Int32 Age
            {
                get { return m_Age; }
                set
                {
                    if (value < 0)
                        throw new ArgumentOutOfRangeException("value", value.ToString(), "The value must be greater or equal to 0");
                    m_Age = value;
                }
            }

            public Int32 Test
            {
                get;
                set;
            }
        }

        public sealed class BitArray
        {
            // Закрытый байтовый массив, хранящий биты
            private Byte[] m_byteArray;
            private Int32 m_numBits;
            // Конструктор, выделяющий память для байтового массива
            // и устанавливающий все биты в 0
            public BitArray(Int32 numBits)
            {
                // Начинаем с проверки аргументов
                if (numBits <= 0)
                    throw new ArgumentOutOfRangeException("numBits must be > 0");
                // Сохранить число битов
                m_numBits = numBits;
                // Выделить байты для массива битов
                m_byteArray = new Byte[(numBits + 7) / 8];
            }
            // Индексатор (свойство с параметрами)
            public Boolean this[Int32 bitPos]
            {
                // Метод доступа get индексатора
                get
                {
                    // Сначала нужно проверить аргументы
                    if ((bitPos < 0) || (bitPos >= m_numBits))
                        throw new ArgumentOutOfRangeException("bitPos");
                    // Вернуть состояние индексируемого бита
                    return (m_byteArray[bitPos / 8] & (1 << (bitPos % 8))) != 0;
                }
                // Метод доступа set индексатора
                set
                {
                    if ((bitPos < 0) || (bitPos >= m_numBits))
                        throw new ArgumentOutOfRangeException(
                        "bitPos", bitPos.ToString());
                    if (value)
                    {
                        // Установить индексируемый бит
                        m_byteArray[bitPos / 8] = (Byte)
                        (m_byteArray[bitPos / 8] | (1 << (bitPos % 8)));
                    }
                    else
                    {
                        // Сбросить индексируемый бит
                        m_byteArray[bitPos / 8] = (Byte)
                        (m_byteArray[bitPos / 8] & ~(1 << (bitPos % 8)));
                    }
                }
            }
        }
    }

    namespace nsEvents
    {
        public class Events
        {
            public void Run()
            {
                // определения события в типе означает, что тип поддерживает следующие возможности:
                // - регистрация своей заинтересованности в событии;
                // - отмена регистрации своей заинтересованности в событии;
                // - оповещение зарегистрированных методов о произошедшем событии.

                // Делегаты обеспечивают реализацию механизма обратного вызова, безопасную по отношению к типам.

                // Этап 1. Определение типа для хранения всей дополнительной информации, передаваемой получателям уведомления о событии
                // В соответствии с соглашением, классы, содержащие информацию о событиях, передаваемую обработчику события,
                // должны наследовать от типа System.EventArgs, а имя типа должно заканчиваться словом EventArgs.

                // Этап 2. Определение члена-события
                // Так как обобщенный делегат System.EventHandler определен следующим образом:
                // public delegate void EventHandler<TEventArgs>
                // (Object sender, TEventArgs e) where TEventArgs: EventArgs;
                // Поэтому прототип метода должен выглядеть так:
                // void MethodName(Object sender, NewMailEventArgs e);

                // Этап 3. Определение метода, ответственного за уведомление зарегистрированных объектов о событии
                // В соответствии с соглашением в классе должен быть виртуальный защищенный метод, вызываемый из кода класса и его потомков при возникновении события. Этот метод принимает один параметр, объект MailMsgEventArgs, содержащий дополнительные сведения о событии.

                // Уведомление о событии, безопасное в отношении потоков

                // Этап 4. Определение метода, преобразующего входную информацию в желаемое событие

                // Реализация событий компилятором
                // В классе MailManager есть строчка кода, определяющая сам член-событие:
                // public event EventHandler<NewMailEventArgs> NewMail;

                // При компиляции этой строки компилятор превращает ее в следующие три конструкции:
                // 1. ЗАКРЫТОЕ поле делегата, инициализированное значением null
                // private EventHandler<NewMailEventArgs> NewMail = null;
                // Позволяет объектам регистрироваться для получения уведомлений о событии
                // public void add_NewMail(EventHandler<NewMailEventArgs> value) {
                // // Цикл и вызов CompareExchange – хитроумный способ добавления
                // // делегата способом, безопасным в отношении потоков
                // EventHandler<NewMailEventArgs>prevHandler;
                // EventHandler<NewMailEventArgs> newMail = this.NewMail;
                // do {
                // prevHandler = newMail;
                // EventHandler<NewMailEventArgs> newHandler =
                // (EventHandler<NewMailEventArgs>) Delegate.Combine(prevHandler, value);
                // newMail = Interlocked.CompareExchange<EventHandler<NewMailEventArgs>>(
                // ref this.NewMail, newHandler, prevHandler);
                //     } while (newMail != prevHandler);
                // }
                // 3. ОТКРЫТЫЙ метод remove_Xxx (где Xxx – это имя события)
                // // Позволяет объектам отменять регистрацию в качестве
                // // получателей уведомлений о cобытии
                // public void remove_NewMail(EventHandler<NewMailEventArgs> value) {
                // // Цикл и вызов CompareExchange – хитроумный способ
                // // удаления делегата способом, безопасным в отношении потоков
                // EventHandler<NewMailEventArgs> prevHandler;
                // EventHandler<NewMailEventArgs> newMail = this.NewMail;
                // do {
                // prevHandler = newMail;
                // EventHandler<NewMailEventArgs> newHandler =
                // (EventHandler<NewMailEventArgs>) Delegate.Remove(prevHandler, value);
                // newMail = Interlocked.CompareExchange<EventHandler<NewMailEventArgs>>(
                // ref this.NewMail, newHandler, prevHandler);
                // } while (newMail != prevHandler);
                // }

                // Оба метода — add и remove — используют хорошо известный паттерн обновления значения способом, безопасным в отношении потоков.

                // Создание типа, отслеживающего событие
                MailManager mailManager= new MailManager();
                Fax fax = new Fax(mailManager);
                mailManager.SimulateNewMail("1", "2", "3");

                // Если в вашем типе реализован метод Dispose объ екта IDisposable, уничтожение объекта должно вызвать отмену его регистрации
                // в качестве получателя уведомлений обо всех событиях (об объекте IDisposable см. также главу 21).
                fax.Unregister(mailManager);

                // Явное управление регистрацией событий
                TypeWithLotsOfEvents twle = new TypeWithLotsOfEvents();
                // Добавление обратного вызова
                twle.Foo += HandleFooEvent;
                // Проверяем работоспособность
                twle.SimulateFoo();


                Developer d = new Developer();
                QA qa = new QA(d);
                Manager m = new Manager(d);
                d.RecieveBug("lol");
            }

            private static void HandleFooEvent(object sender, FooEventArgs e)
            {
                Console.WriteLine("Handling Foo Event here...");
            }
        }

        // Этап 1. Определение типа для хранения информации,
        // которая передается получателям уведомления о событии
        internal class NewMailEventArgs : EventArgs
        {
            private readonly String m_from, m_to, m_subject;
            public NewMailEventArgs(String from, String to, String subject)
            {
                m_from = from; m_to = to; m_subject = subject;
            }
            public String From { get { return m_from; } }
            public String To { get { return m_to; } }
            public String Subject { get { return m_subject; } }
        }

        internal class MailManager
        {
            // Этап 2. Определение члена-события
            public event EventHandler<NewMailEventArgs> NewMail;

            // Этап 3. Определение метода, ответственного за уведомление
            // зарегистрированных объектов о событии
            // Если этот класс изолированный, нужно сделать метод закрытым
            // или невиртуальным
            protected virtual void OnNewMail(NewMailEventArgs e)
            {
                // Сохранить ссылку на делегата во временной переменной
                // для обеспечения безопасности потоков
                EventHandler<NewMailEventArgs> temp = Volatile.Read(ref NewMail);
                // Если есть объекты, зарегистрированные для получения
                // уведомления о событии, уведомляем их
                if (temp != null) temp(this, e);

                // используя метод расширения
                // e.Raise(this, ref m_NewMail);
            }

            // Этап 4. Определение метода, преобразующего входную
            // информацию в желаемое событие
            public void SimulateNewMail(String from, String to, String subject)
            {
                // Создать объект для хранения информации, которую
                // нужно передать получателям уведомления
                NewMailEventArgs e = new NewMailEventArgs(from, to, subject);
                // Вызвать виртуальный метод, уведомляющий объект о событии
                // Если ни один из производных типов не переопределяет этот метод,
                // объект уведомит всех зарегистрированных получателей уведомления
                OnNewMail(e);
            }
        }

        public static class EventArgExtensions
        {
            public static void Raise<TEventArgs>(this TEventArgs e,
            Object sender, ref EventHandler<TEventArgs> eventDelegate)
            {
                // Копирование ссылки на поле делегата во временное поле
                // для безопасности в отношении потоков
                EventHandler<TEventArgs> temp = Volatile.Read(ref eventDelegate);
                // Если зарегистрированный метод заинтересован в событии, уведомите его
                if (temp != null) temp(sender, e);
            }
        }

        internal sealed class Fax
        {
            // Передаем конструктору объект MailManager
            public Fax(MailManager mm)
            {
                // Создаем экземпляр делегата EventHandler<NewMailEventArgs>,
                // ссылающийся на метод обратного вызова FaxMsg
                // Регистрируем обратный вызов для события NewMail объекта MailManager
                mm.NewMail += FaxMsg;
            }
            // MailManager вызывает этот метод для уведомления
            // объекта Fax о прибытии нового почтового сообщения
            private void FaxMsg(Object sender, NewMailEventArgs e)
            {
                // 'sender' используется для взаимодействия с объектом MailManager,
                // если потребуется передать ему какую-то информацию
                // 'e' определяет дополнительную информацию о событии,
                // которую пожелает предоставить MailManager
                // Обычно расположенный здесь код отправляет сообщение по факсу
                // Тестовая реализация выводит информацию на консоль
                Console.WriteLine("Faxing mail message:");
                Console.WriteLine(" From={0}, To={1}, Subject={2}",
                e.From, e.To, e.Subject);
            }
            // Этот метод может выполняться для отмены регистрации объекта Fax
            // в качестве получтеля уведомлений о событии NewMail
            public void Unregister(MailManager mm)
            {
                // Отменить регистрацию на уведомление о событии NewMail объекта
                mm.NewMail -= FaxMsg;
            }
        }

        // Этот класс нужен для поддержания безопасности типа
        // и кода при использовании EventSet
        public sealed class EventKey : Object { }

        public sealed class EventSet
        {
            // Закрытый словарь служит для отображения EventKey -> Delegate
            private readonly Dictionary<EventKey, Delegate> m_events = new Dictionary<EventKey, Delegate>();
            // Добавление отображения EventKey -> Delegate, если его не существует
            // И компоновка делегата с существующим ключом EventKey
            public void Add(EventKey eventKey, Delegate handler)
            {
                Monitor.Enter(m_events);
                Delegate d;
                m_events.TryGetValue(eventKey, out d);
                m_events[eventKey] = Delegate.Combine(d, handler);
                Monitor.Exit(m_events);
            }
            // Удаление делегата из EventKey (если он существует)
            // и разрыв связи EventKey -> Delegate при удалении
            // последнего делегата
            public void Remove(EventKey eventKey, Delegate handler)
            {
                Monitor.Enter(m_events);
                // Вызов TryGetValue предотвращает выдачу исключения
                // при попытке удаления делегата с отсутствующим ключом EventKey.
                Delegate d;
                if (m_events.TryGetValue(eventKey, out d))
                {
                    d = Delegate.Remove(d, handler);
                    // Если делегат остается, то установить новый ключ EventKey,
                    // иначе – удалить EventKey
                    if (d != null) m_events[eventKey] = d;
                    else m_events.Remove(eventKey);
                }
                Monitor.Exit(m_events);
            }
            // Информирование о событии для обозначенного ключа EventKey
            public void Raise(EventKey eventKey, Object sender, EventArgs e)
            {
                // Не выдавать исключение при отсутствии ключа EventKey
                Delegate d;
                Monitor.Enter(m_events);
                m_events.TryGetValue(eventKey, out d);
                Monitor.Exit(m_events);
                if (d != null)
                {
                    // Из-за того что словарь может содержать несколько разных типов
                    // делегатов, невозможно создать вызов делегата, безопасный по
                    // отношению к типу, во время компиляции. Я вызываю метод
                    // DynamicInvoke типа System.Delegate, передавая ему параметры метода
                    // обратного вызова в виде массива объектов. DynamicInvoke будет
                    // контролировать безопасность типов параметров для вызываемого
                    // метода обратного вызова. Если будет найдено несоответствие типов,
                    // выдается исключение.
                    d.DynamicInvoke(new Object[] { sender, e });
                }
            }
        }

        // Определение типа, унаследованного от EventArgs для этого события
        public class FooEventArgs : EventArgs { }
        public class TypeWithLotsOfEvents
        {
            // Определение закрытого экземплярного поля, ссылающегося на коллекцию.
            // Коллекция управляет множеством пар "Event/Delegate"
            // Примечание: Тип EventSet не входит в FCL,
            // это мой собственный тип
            private readonly EventSet m_eventSet = new EventSet();
            // Защищенное свойство позволяет производным типам работать с коллекцией
            protected EventSet EventSet { get { return m_eventSet; } }
            #region Code to support the Foo event (repeat this pattern for additional events)
            // Определение членов, необходимых для события Foo.
            // 2a. Создайте статический, доступный только для чтения объект
            // для идентификации события.
            // Каждый объект имеет свой хеш-код для нахождения связанного списка
            // делегатов события в коллекции.
            protected static readonly EventKey s_fooEventKey = new EventKey();
            // 2b. Определение для события методов доступа для добавления
            // или удаления делегата из коллекции.
            public event EventHandler<FooEventArgs> Foo
            {
                add { m_eventSet.Add(s_fooEventKey, value); }
                remove { m_eventSet.Remove(s_fooEventKey, value); }
            }
            // 2c. Определение защищенного виртуального метода On для этого события.
            protected virtual void OnFoo(FooEventArgs e)
            {
                m_eventSet.Raise(s_fooEventKey, this, e);
            }
            // 2d. Определение метода, преобразующего входные данные этого события
            public void SimulateFoo() { OnFoo(new FooEventArgs()); }
            #endregion
        }

        class Developer
        {
            public event EventHandler<NewBugEventArgs> NewBug;

            public void RecieveBug(String info)
            {
                NewBugEventArgs e = new NewBugEventArgs(info);
                OnNewBug(e);
            }

            protected virtual void OnNewBug(NewBugEventArgs e)
            {
                EventHandler<NewBugEventArgs> temp = Volatile.Read(ref NewBug);
                if (temp != null)
                    temp(this, e);
            }
        }

        class QA
        {
            public QA(Developer d)
            {
                d.NewBug += OnNewBug;
            }

            public void Unregister(Developer d)
            {
                d.NewBug -= OnNewBug;
            }

            public void OnNewBug(Object sender, NewBugEventArgs e)
            {
                Console.WriteLine(e.Info);
            }

        }

        class Manager
        {
            public Manager(Developer d)
            {
                d.NewBug += OnNewBug;
            }

            public void Unregister(Developer d)
            {
                d.NewBug -= OnNewBug;
            }

            public void OnNewBug(Object sender, NewBugEventArgs e)
            {
                Console.WriteLine(e.Info);
            }
        }

        internal class NewBugEventArgs : EventArgs
        {
            private readonly String m_info;

            public NewBugEventArgs(String info)
            {
                m_info = info;
            }

            public String Info
            {
                get
                {
                    return m_info;
                }
            }
        }

    }

    namespace nsGeneric
    {
        class Generic
        {
            public delegate TResult Func<in T, out TResult>(T arg);

            public void Run()
            {
                testList();
                // ValueTypePerfTest();
                // ReferenceTypePerfTest();
                TestArray();
                TestGeneric();
                TestGenericInterface();
                TestDelegate();
                // Теперь поговорим о различных типах ограничений, которые компилятор и CLR позволяют применять к параметрам типа.
                // К параметру-типу могут применяться следующие ограничения: основное (primary), дополнительное (secondary) и/или ограничение конструктора (constructor constraint).
                TestConstraints();
            }

            private void testList()
            {
                List<DateTime> dtList = new List<DateTime>();
                dtList.Add(DateTime.Now);
                dtList.Add(DateTime.MinValue);
                // dtList.Add("1/1/2004"); // compile error
                DateTime dt = dtList[0];
                // - Защита исходного кода.
                // - Безопасность типов.
                // - Более простой и понятный код.
                // - Повышение производительности.
            }

            private void TestArray()
            {
                // Создание и инициализация массива байтов
                Byte[] byteArray = new Byte[] { 5, 1, 4, 2, 3 };

                // Вызов алгоритма сортировки Byte[]
                Array.Sort<Byte>(byteArray);

                // Вызов алгоритма двоичного поиска Byte[]
                Int32 i = Array.BinarySearch<Byte>(byteArray, 1);
                Console.WriteLine(i); // Выводит "0"
            }

            private void TestGeneric()
            {
                Object o = null;
                // Dictionary<,> — открытый тип с двумя параметрами типа
                Type t = typeof(Dictionary<,>);
                // Попытка создания экземпляра этого типа (неудачная)
                o = CreateInstance(t);
                Console.WriteLine();

                // DictionaryStringKey<> — открытый тип с одним параметром типа

                t = typeof(DictionaryStringKey<>);
                // Попытка создания экземпляра этого типа (неудачная)
                o = CreateInstance(t);
                Console.WriteLine();
                // DictionaryStringKey<Guid> — это закрытый тип
                t = typeof(DictionaryStringKey<Guid>);
                // Попытка создания экземпляра этого типа (удачная)
                o = CreateInstance(t);
                // Проверка успешности попытки
                Console.WriteLine("Object type=" + o.GetType());


                Node<Char> head = new Node<Char>('C');
                head = new Node<Char>('B', head);
                head = new Node<Char>('A', head);
                Console.WriteLine(head.ToString()); // Выводится "ABC"

                nsTypedNode.Node myHead = new nsTypedNode.TypedNode<Char>('.');
                myHead = new nsTypedNode.TypedNode<DateTime>(DateTime.Now, myHead);
                myHead = new nsTypedNode.TypedNode<String>("Today is ", myHead);
                Console.WriteLine(myHead.ToString());
            }

            private void TestGenericInterface()
            {

            }

            private void TestDelegate()
            {
                Func<Object, ArgumentException> fn1 = null;
                Func<String, Exception> fn2 = fn1; // Явного приведения типа не требуется
                Exception e = fn2("");
            }

            // internal sealed class Triangle : IEnumerator<Point>
            // {
            //     private Point[] m_vertices;
            //     // Тип свойства Current в IEnumerator<Point> - это Point
            //     public Point Current { get { return new Point(); } }
            // }

            public struct Point
            {

            }

            private Object CreateInstance(Type t)
            {
                Object o = null;
                try
                {
                    o = Activator.CreateInstance(t);
                    Console.Write("Created instance of {0}", t.ToString());
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
                return o;
            }

            private void ValueTypePerfTest()
            {
                const Int32 count = 10000000;
                using (new OperationTimer("List<Int32>"))
                {
                    List<Int32> l = new List<Int32>();
                    for (Int32 n = 0; n < count; n++)
                    {
                        l.Add(n); // Без упаковки
                        Int32 x = l[n]; // Без распаковки
                    }
                    l = null; // Для удаления в процессе уборки мусора
                }
                using (new OperationTimer("ArrayList of Int32"))
                {
                    ArrayList a = new ArrayList();
                    for (Int32 n = 0; n < count; n++)
                    {
                        a.Add(n); // Упаковка
                        Int32 x = (Int32)a[n]; // Распаковка
                    }
                    a = null; // Для удаления в процессе уборки мусора
                }
            }

            private void ReferenceTypePerfTest()
            {
                const Int32 count = 10000000;
                using (new OperationTimer("List<String>"))
                {
                    List<String> l = new List<String>();
                    for (Int32 n = 0; n < count; n++)
                    {
                        l.Add("X"); // Копирование ссылки
                        String x = l[n]; // Копирование ссылки
                    }
                    l = null; // Для удаления в процессе уборки мусора
                }
                using (new OperationTimer("ArrayList of String"))
                {
                    ArrayList a = new ArrayList();
                    for (Int32 n = 0; n < count; n++)
                    {
                        a.Add("X"); // Копирование ссылки
                        String x = (String)a[n]; // Проверка преобразования
                    } // и копирование ссылки
                    a = null; // Для удаления в процессе уборки мусора
                }
            }

            // Класс для оценки времени выполнения операций
            public sealed class OperationTimer : IDisposable
            {
                private static Int32 s_NumOperationTimersStarted;
                private Stopwatch m_sw;
                private String m_text;
                private Int32 m_collectionCount;

                public OperationTimer() : this(String.Empty) { }

                public OperationTimer(String text)
                {
                    if (Interlocked.Increment(ref s_NumOperationTimersStarted) == 1)
                        PrepareForOperation();
                    m_text = text;
                    m_collectionCount = GC.CollectionCount(0);
                    m_sw = Stopwatch.StartNew();	
                }

                public void Dispose()
                {
                    Console.WriteLine("{0,7:N0} (GCs={1,3}) {2}",
                    m_sw.Elapsed.TotalMilliseconds,
                    GC.CollectionCount(0) - m_collectionCount, m_text);
                    Interlocked.Decrement(ref s_NumOperationTimersStarted);
                    m_sw = null;
                }

                private static void PrepareForOperation()
                {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                }
            }

            private static void Swap<T>(ref T o1, ref T o2)
            {
                T temp = o1;
                o1 = o2;
                o2 = temp;
            }

            public static T Min<T>(T o1, T o2) where T : IComparable<T>
            {
                if (o1.CompareTo(o2) < 0) return o1;
                return o2;
            }

            private void TestConstraints()
            {
                // В параметре-типе можно задать не более одного основного ограничения.
                // Основным ограничением может быть ссылочный тип, указывающий на незапечатанный класс.
                // Нельзя использовать для этой цели следующие ссылочные типы: System.Object, System.Array, System.Delegate, System.MulticastDelegate,
                // System.ValueType, System.Enum и System.Void.

                // Есть два особых основных ограничения: class и struct.

                IList<String> ls = new List<String>();
                ls.Add("A String");
                // Преобразует IList<String> в IList<Object>
                IList<Object> lo = ConvertIList<String, Object>(ls);
                // Преобразует IList<String> в IList<IComparable>
                IList<IComparable> lc = ConvertIList<String, IComparable>(ls);
                // Преобразует IList<String> в IList<IComparable<String>>
                IList<IComparable<String>> lcs = ConvertIList<String, IComparable<String>>(ls);
                // Преобразует IList<String> в IList<String>
                IList<String> ls2 = ConvertIList<String, String>(ls);
                // Преобразует IList<String> в IList<Exception>
                // IList<Exception> le = ConvertIList<String, Exception>(ls); // Ошибка

                // Ограничения конструктора
            }

            private static List<TBase> ConvertIList<T, TBase>(IList<T> list)
            where T : TBase
            {
                List<TBase> baseList = new List<TBase>(list.Count);
                for (Int32 index = 0; index < list.Count; index++)
                {
                    baseList.Add(list[index]);
                }
                return baseList;
            }

            private static void CastingAGenericTypeVariable1<T>(T obj)
            {
                // Int32 x = (Int32)obj; // Ошибка
                // String s = (String)obj; // Ошибка
                Int32 x = (Int32)(Object)obj; // Ошибки нет
                String s = (String)(Object)obj; // Ошибки нет
                String s1 = obj as String; // Ошибки нет
            }

            private static void SettingAGenericTypeVariableToNull<T>()
            {
                // T temp = null; // CS0403: нельзя привести null к параметру типа T
                // because it could be a value type...
                // (Ошибка CS0403: нельзя привести null к параметру типа Т,
                // поскольку T может иметь значимый тип...)
                T temp = default(T); // Работает
            }

            private static void ComparingAGenericTypeVariableWithNull<T>(T obj)
            {
                if (obj == null)
                { /* Этот код никогда не исполняется для значимого типа */ }
            }

            private static void ComparingTwoGenericTypeVariables<T>(T o1, T o2)
            {
                // if (o1 == o2) { } // Ошибка
            }
        }

        internal sealed class PrimaryConstraintOfStream<T> where T : Stream
        {
            public void M(T stream)
            {
                stream.Close();// OK
            }
        }

        internal sealed class PrimaryConstraintOfClass<T> where T : class
        {
            public void M()
            {
                T temp = null;// Допустимо, потому что тип T должен быть ссылочным
            }
        }

        internal sealed class PrimaryConstraintOfStruct<T> where T : struct
        {
            public static T Factory()
            {
                // Допускается, потому что у каждого значимого типа неявно
                // есть открытый конструктор без параметров
                return new T();
            }
        }

        internal sealed class ConstructorConstraint<T> where T : new()
        {
            public static T Factory()
            {
                // Допустимо, потому что у всех значимых типов неявно
                // есть открытый конструктор без параметров, и потому что
                // это ограничение требует, чтобы у всех указанных ссылочных типов
                // также был открытый конструктор без параметров
                return new T();
            }
        }

        // Частично определенный открытый тип
        internal sealed class DictionaryStringKey<TValue> :
        Dictionary<String, TValue>
        {
        }

        internal sealed class GenericTypeThatRequiresAnEnum<T>
        {
            static GenericTypeThatRequiresAnEnum()
            {
                if (!typeof(T).IsEnum)
                {
                    throw new ArgumentException("T must be an enumerated type");
                }
            }
        }

        internal sealed class Node<T>
        {
            public T m_data;
            public Node<T> m_next;
            public Node(T data)
                : this(data, null)
            {

            }

            public Node(T data, Node<T> next)
            {
                m_data = data; m_next = next;
            }

            public override String ToString()
            {
                return m_data.ToString() +
                ((m_next != null) ? m_next.ToString() : null);
            }
        }

        internal sealed class GenericType<T>
        {
            private T m_value;
            public GenericType(T value) { m_value = value; }
            public TOutput Converter<TOutput>()
            {
                TOutput result = (TOutput)Convert.ChangeType(m_value, typeof(TOutput));
                return result;
            }
        }

        internal class Base
        {
            public virtual void M<T1, T2>()
                where T1 : struct
                where T2 : class
            {
            }
        }
        internal sealed class Derived : Base
        {
            public override void M<T3, T4>()
            // where T3 : EventArgs // Ошибка
            // where T4 : class // Ошибка
            { }
        }

        namespace nsTypedNode
        {
            internal class Node
            {
                protected Node m_next;
                public Node(Node next)
                {
                    m_next = next;
                }
            }

            internal sealed class TypedNode<T> : Node
            {
                public T m_data;
                public TypedNode(T data)
                    : this(data, null)
                {
                }
                public TypedNode(T data, Node next)
                    : base(next)
                {
                    m_data = data;
                }
                public override String ToString()
                {
                    return m_data.ToString() +
                ((m_next != null) ? m_next.ToString() : String.Empty);
                }
            }
        }
    }
}
