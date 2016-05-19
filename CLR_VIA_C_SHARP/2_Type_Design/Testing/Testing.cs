﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
