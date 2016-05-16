using System;
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
                // 

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

    }


    // repeat and practice with everything from part 2.
    
    
    // !!!


}
