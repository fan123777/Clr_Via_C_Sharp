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
}
