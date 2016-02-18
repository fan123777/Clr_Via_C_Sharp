using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices; // Для атрибута InternalsVisibleTo

namespace CLR_VIA_C_SHARP._2_Type_Design._6_MembersAndTypes
{
    class Testing
    {
    }

    public sealed class SomeType
    {      // 1
        // Вложенный класс
        private class SomeNestedType { } // 2
        // Константа, неизменяемое и статическое изменяемое поле
        // Constant, readonly, and static read/write field
        private const Int32 c_SomeConstant = 1; // 3
        private readonly String m_SomeReadOnlyField = "2"; // 4
        private static Int32 s_SomeReadWriteField = 3; // 5
        // Конструктор типа
        static SomeType() { } // 6
        // Конструкторы экземпляров
        public SomeType(Int32 x) { } // 7
        public SomeType() { } // 8
        // Экземплярный и статический методы
        private String InstanceMethod() { return null; } // 9
        public static void Main1() { } // 10
        // Необобщенное экземплярное свойство
        public Int32 SomeProp
        { // 11
            get { return 0; } // 12
            set { } // 13
        }
        // Обобщенное экземплярное свойство
        public Int32 this[String s]
        { // 14
            get { return 0; } // 15
            set { } // 16
        }
        // Экземплярное событие
        public event EventHandler SomeEvent; // 17
    }

    // Открытый тип доступен из любой сборки
    public class ThisIsAPublicType
    {

    }
    // Внутренний тип доступен только из собственной сборки
    internal class ThisIsAnInternalType
    {

    }
    // Это внутренний тип, так как модификатор доступа не указан явно
    class ThisIsAlsoAnInternalType
    {
    }

    // Внутренние типы этой сборки доступны из кода двух следующих сборок
    // (независимо от версии или региональных стандартов)
    [assembly: InternalsVisibleTo("Wintellect, PublicKey=12345678...90abcdef")]
    [assembly: InternalsVisibleTo("Microsoft, PublicKey=b77a5c56...1934e089")]
    internal sealed class SomeInternalType
    {
    }

    internal sealed class AnotherInternalType
    {
    }

    internal sealed class Foo
    {
        private static Object SomeMethod()
        {
            // Эта сборка Wintellect получает доступ к внутреннему типу
            // другой сборки, как если бы он был открытым
            SomeInternalType sit = new SomeInternalType();
            return sit;
        }
    }

    public static class AStaticClass
    {
        public static void AStaticMethod() { }
        public static String AStaticProperty
        {
            get { return s_AStaticField; }
            set { s_AStaticField = value; }
        }
        private static String s_AStaticField;
        public static event EventHandler AStaticEvent;
    }

    internal class Employee
    {
        // Невиртуальный экземплярный метод
        public Int32 GetYearsEmployed()
        {
            return 5;
        }
        // Виртуальный метод (виртуальный - значит, экземплярный)
        public virtual String GetProgressReport()
        {
            return "Jon";
        }
        // Статический метод
        public static Employee Lookup(String name)
        {
            return new Employee();
        }
    }

    internal class SomeClass
    {
        // ToString - виртуальный метод базового класса Object
        public override String ToString()
        {
            // Компилятор использует команду call для невиртуального вызова
            // метода ToString класса Object
            // Если бы компилятор вместо call использовал callvirt, этот
            // метод продолжал бы рекурсивно вызывать сам себя до переполнения стека
            return base.ToString();
        }
    }

    public class Set
    {
        private Int32 m_length = 0;
        // Этот перегруженный метод — невиртуальный
        public Int32 Find(Object value)
        {
            return Find(value, 0, m_length);
        }
        // Этот перегруженный метод — невиртуальный
        public Int32 Find(Object value, Int32 startIndex)
        {
            return Find(value, startIndex, m_length - startIndex);
        }
        // Наиболее функциональный метод сделан виртуальным
        // и может быть переопределен
        public virtual Int32 Find(Object value, Int32 startIndex, Int32 endIndex)
        {
            // Здесь находится настоящая реализация, которую можно переопределить...
            return 5;
        }
        // Другие методы
    }

    public sealed class Point
    {
        private Int32 m_x, m_y;
        public Point(Int32 x, Int32 y) { m_x = x; m_y = y; }
        public override String ToString()
        {
            return String.Format("({0}, {1})", m_x, m_y);
        }
    }

    public class Phone
    {
        public void Dial()
        {
            Console.WriteLine("Phone.Dial");
            EstablishConnection();
            // Выполнить действия по набору телефонного номера
        }
        protected virtual void EstablishConnection()
        {
            Console.WriteLine("Phone.EstablishConnection");
            // Выполнить действия по установлению соединения
        }
    }

    public class BetterPhone : Phone
    {
        public void Dial()
        {
            Console.WriteLine("BetterPhone.Dial");
            EstablishConnection();
            base.Dial();
        }

        protected virtual void EstablishConnection()
        {
            Console.WriteLine("BetterPhone.EstablishConnection");
            // Выполнить действия по набору телефонного номера
        }
    }

    public class BetterPhone1 : Phone
    {
        // Этот метод Dial никак не связан с одноименным методом класса Phone
        public new void Dial()
        {
            Console.WriteLine("BetterPhone.Dial");
            EstablishConnection();
            base.Dial();
        }

        protected new  virtual void EstablishConnection()
        {
            Console.WriteLine("BetterPhone.EstablishConnection");
            // Выполнить действия по установлению соединения
        }
    }

    public class BetterPhone2 : Phone
    {
        // Метод Dial удален (так как он наследуется от базового типа)
        // Здесь ключевое слово new удалено, а модификатор virtual заменен
        // на override, чтобы указать, что этот метод связан с методом
        // EstablishConnection из базового типа
        protected override void EstablishConnection()
        {
            Console.WriteLine("BetterPhone.EstablishConnection");
            // Выполнить действия по установлению соединения
        }
    }
}

    
