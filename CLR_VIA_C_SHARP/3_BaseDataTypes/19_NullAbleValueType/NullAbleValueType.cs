using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CLR_VIA_C_SHARP._3_BaseDataTypes._19_NullAbleValueType
{
    class NullAbleValueType
    {
        public static void main()
        {
            NullAbleValueType n = new NullAbleValueType();
            n.testNullAble();
            ConversionsAndCasting();
            Operators();
            NullCoalescingOperator();
            n.testPackNullAble();
            n.testUnPackNullAble();
            n.testNullAbleGetType();
            n.testNullAbleInterface();
        }

        private void testNullAble()
        {
            Nullable<Int32> x = 5;
            Nullable<Int32> y = null;
            Console.WriteLine("x: HasValue={0}, Value={1}", x.HasValue, x.Value);
            Console.WriteLine("y: HasValue={0}, Value={1}", y.HasValue, y.GetValueOrDefault());

            Int32? x1 = 5;
            Int32? y1 = null;

            Point? p1 = new Point(1, 1);
            Point? p2 = new Point(2, 2);
            Console.WriteLine("Are points equal? " + (p1 == p2).ToString());
            Console.WriteLine("Are points not equal? " + (p1 != p2).ToString());
        }

        private static void ConversionsAndCasting()
        {
            // Неявное преобразование из типа Int32 в Nullable<Int32>
            Int32? a = 5;
            // Неявное преобразование из 'null' в Nullable<Int32>
            Int32? b = null;
            // Явное преобразование Nullable<Int32> в Int32
            Int32 c = (Int32)a;
            // Прямое и обратное приведение примитивного типа
            // в null-совместимый тип
            Double? d = 5; // Int32->Double? (d содержит 5.0 в виде double)
            Double? e = b; // Int32?->Double? (e содержит null)
        }

        private static void Operators()
        {
            Int32? a = 5;
            Int32? b = null;
            // Унарные операторы (+ ++ - -- ! ~)
            a++; // a = 6
            b = -b; // b = null
            // Бинарные операторы (+ - * / % & | ^ << >>)
            a = a + 3; // a = 9
            b = b * 3; // b = null;
            // Операторы равенства (== !=)
            if (a == null) { /* нет */ } else { /* да */ }
            if (b == null) { /* да */ } else { /* нет */ }
            if (a != b) { /* да */ } else { /* нет */ }
            // Операторы сравнения (<> <= >=)
            if (a < b) { /* нет */ } else { /* да */ }
        }

        private static Int32? NullableCodeSize(Int32? a, Int32? b)
        {
            return a + b;
        }

        private static void NullCoalescingOperator()
        {
            Int32? b = null;
            // Приведенная далее инструкция эквивалентна следующей:
            // x = (b.HasValue) ? b.Value : 123
            Int32 x = b ?? 123;
            Console.WriteLine(x); // "123"
            // Приведенная далее в инструкции строка эквивалентна следующему коду:
            // String temp = GetFilename();
            // filename = (temp != null) ? temp : "Untitled";
            // String filename = GetFilename() ?? "Untitled";
        }

        private void testPackNullAble()
        {
            // После упаковки Nullable<T> возвращается null или упакованный тип T
            Int32? n = null;
            Object o = n; // o равно null
            Console.WriteLine("o is null={0}", o == null); // "True"
            n = 5;
            o = n; // o ссылается на упакованный тип Int32
            Console.WriteLine("o's type={0}", o.GetType()); // "System.Int32"
        }

        private void testUnPackNullAble()
        {
            // Создание упакованного типа Int32
            Object o = 5;
            // Распаковка этого типа в Nullable<Int32> и в Int32
            Int32? a = (Int32?)o; // a = 5
            Int32 b = (Int32)o; // b = 5
            // Создание ссылки, инициализированной значением null
            o = null;
            // "Распаковка" ее в Nullable<Int32> и в Int32
            a = (Int32?)o; // a = null
            // b = (Int32)o; // NullReferenceException
        }

        private void testNullAbleGetType()
        {
            Int32? x = 5;
            // Эта строка выводит "System.Int32", а не "System.Nullable<Int32>"
            Console.WriteLine(x.GetType());
        }

        private void testNullAbleInterface()
        {
            Int32? n = 5;
            Int32 result = ((IComparable)n).CompareTo(5); // Компилируется
            // и выполняется
            Console.WriteLine(result); // 0
        }
    }

    internal struct Point
    {
        private Int32 m_x, m_y;
        public Point(Int32 x, Int32 y) { m_x = x; m_y = y; }
        public static Boolean operator ==(Point p1, Point p2)
        {
            return (p1.m_x == p2.m_x) && (p1.m_y == p2.m_y);
        }
        public static Boolean operator !=(Point p1, Point p2)
        {
            return !(p1 == p2);
        }
    }

    // ПримечАние
    // Адаптеры таблиц Microsoft ADO.NET поддерживают типы, допускающие присвоение null.
    // Но, к сожалению, типы в пространстве имен System.Data.SqlTypes не замещаются null-совместимыми типами отчасти из-за отсутствия однозначного соответствия между ними.
    // К примеру, тип SqlDecimal допускает максимум 38 разрядов, в то время как обычный тип Decimal — только 29.
    // А тип SqlString поддерживает собственные региональные стандарты и порядок сравнения, чего не скажешь о типе String.
    // Чтобы исправить ситуацию, в Microsoft разработали для CLR null-совместимые значимые типы (nullable value type).
    // Так как Nullable<T> также относится к значимым типам, его экземпляры достаточно производительны, поскольку экземпляры могут размещаться в стеке, а их размер совпадает с размером исходного типа, к которому приплюсован размер поля типа Boolean.
    // Имейте в виду, что в качестве параметра T типа Nullable могут использоваться только структуры — ведь переменные ссылочного типа и так могут принимать значение null.
    
    // Поддержка в C# null-совместимых значимых типов
    // Переменные x и y можно объявить и инициализировать прямо в коде, воспользовавшись знаком вопроса.
    // В C# запись Int32? аналогична записи Nullable<Int32>.
    // При этом вы можете выполнять преобразования, а также приведение null-совместимых экземпляров к другим типам.
    // Язык C# поддерживает возможность применения операторов к экземплярам null-совместимых значимых типов.
    // Еще C# позволяет применять операторы к экземплярам null-совместимых типов.
    // Вот как эти операторы интерпретирует C#:
    // - Унарные операторы (+, ++, -, --, ! , ~). Если операнд равен null, результат тоже равен null.
    // - Бинарные операторы (+, -, *, /, %, &, |, ^, <<, >>). Результат равен значению null, если этому значению равен хотя бы один операнд.
    // Исключением является случай воздействия операторов & и | на логический операнд ?.
    // В результате поведение этих двух операторов совпадает с тернарной логикой SQL.
    // Если ни один из операндов не равен null, операция проходит в обычном режиме, если же оба операнда равны null, в результате получаем null.
    // Особая ситуация возникает в случае, когда значению null равен только один из операндов.
    // В следующей таблице показаны возможные результаты, которые эти операторы дают для всех возможных комбинаций значений true, false и null.
    // - Операторы равенства (==, !=). Если оба операнда имеют значение null, они равны.
    // Если только один из них имеет это значение, операнды не равны.
    // Если ни один из них не равен null, операнды сравниваются на предмет равенства.
    // - Операторы сравнения (<, >, <=, >=). Если значение null имеет один из операндов, в результате получаем значение false.
    // Если ни один из операндов не имеет значения null, следует сравнить их значения.
    // Следует учесть, что для операций с экземплярами null-совместимых типов генерируется большой объем кода.
    
    // Оператор объединения null‑совместимых значений
    // В C# существует оператор объединения null-совместимых значений (null-coalescing operator).
    // Он обозначается знаками ?? и работает с двумя операндами.
    // Если левый операнд не равен null, оператор возвращает его значение.
    // В противном случае возвращается значение правого операнда.
    // Оператор объединения null-совместимых значений удобен при задании предлагаемого по умолчанию значения переменной.
    // Func<String> f = () => SomeMethod() ?? "Untitled";
    // Прочитать и понять эту строку намного проще, чем следующий фрагмент кода,
    // Func<String> f = () => { var temp = SomeMethod(); return temp != null ? temp : "Untitled";};
    // Во-вторых, оператор ?? лучше работает в некоторых сложных ситуациях:
    // String s = SomeMethod1() ?? SomeMethod2() ?? "Untitled";

    // Поддержка в CLR null-совместимых значимых типов
    // Упаковка null-совместимых значимых типов
    // При упаковке экземпляра Nullable<T> проверяется его равенство null и в случае положительного результата вместо упаковки возвращается null.
    // В противном случае CLR упаковывает значение экземпляра.
    // Другими словами, тип Nullable<Int32> со значением 5 упаковывается в тип Int32 с аналогичным значением.

    // Распаковка null-совместимых значимых типов
    // В CLR упакованный значимый тип T распаковывается в T или в Nullable<T>.
    // Если ссылка на упакованный значимый тип равна null и выполняется распаковка в тип Nullable<T>, CLR присваивает Nullable<T> значение null.

    // Вызов метода GetType через null-совместимый значимый тип
    // При вызове метода GetType для объекта типа Nullable<T> CLR возвращает тип T вместо Nullable<T>.

    // Вызов интерфейсных методов через null-совместимый значимый тип

}
