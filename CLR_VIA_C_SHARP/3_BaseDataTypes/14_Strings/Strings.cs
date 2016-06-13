using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLR_VIA_C_SHARP._3_BaseDataTypes._14_Strings
{
    class Strings
    {
        public static void main()
        {
            Strings s = new Strings();
            s.TestChar();
            s.CastChar();
        }

        private void TestChar()
        {
            Double d; // '\u0033' – это "цифра 3"
            d = Char.GetNumericValue('\u0033'); // Параметр '3'
            // даст тот же результат
            Console.WriteLine(d.ToString()); // Выводится "3"
            // '\u00bc' — это "простая дробь одна четвертая ('1/4')"
            d = Char.GetNumericValue('\u00bc');
            Console.WriteLine(d.ToString()); // Выводится "0.25"
            // 'A' — это "Латинская прописная буква A"
            d = Char.GetNumericValue('A');
            Console.WriteLine(d.ToString()); // Выводится "-1"
        }

        private void CastChar()
        {
            Char c;
            Int32 n;
            // Преобразование "число - символ" посредством приведения типов C#
            c = (Char)65;
            Console.WriteLine(c); // Выводится "A"
            n = (Int32)c;
            Console.WriteLine(n); // Выводится "65"
            c = unchecked((Char)(65536 + 65));
            Console.WriteLine(c); // Выводится "A"
            // Преобразование "число - символ" с помощью типа Convert
            c = Convert.ToChar(65);
            Console.WriteLine(c); // Выводится "A"
            n = Convert.ToInt32(c);
            Console.WriteLine(n); // Выводится "65"
            // Демонстрация проверки диапазона для Convert
            try
            {
                c = Convert.ToChar(70000); // Слишком много для 16 разрядов
                Console.WriteLine(c); // Этот вызов выполняться НЕ будет
            }
            catch (OverflowException)
            {
                Console.WriteLine("Can't convert 70000 to a Char.");
            }
            // Преобразование "число - символ" с помощью интерфейса IConvertible
            c = ((IConvertible)65).ToChar(null);
            Console.WriteLine(c); // Выводится "A"
            n = ((IConvertible)c).ToInt32(null);
            Console.WriteLine(n); // Выводится "65"
        }
    }

    // Символы
    // Символ представляется экземпляром структуры System.Char (значимый тип).
    // Для экземпляра Char можно вызывать статический метод GetUnicodeCategory, который возвращает значение перечислимого типа System.Globalization.UnicodeCategory, показывающее категорию символа: управляющий символ, символ валюты, буква в нижнем или верхнем регистре, знак препинания, математический символ и т. д.
    // Для облегчения работы с типом Char имеется несколько статических методов, например: IsDigit, IsLetter, IsWhiteSpace, IsUpper, IsLower, IsPunctuation, IsLetterOrDigit, IsControl, IsNumber, IsSeparator, IsSurrogate, IsLowSurrogate, IsHighSurrogate и IsSymbol.
    // Кроме того, статические методы ToLowerInvariant и ToUpperInvariant позволяют преобразовать символ в его эквивалент в нижнем или верхнем регистре без учета региональных стандартов.
    // А теперь представлю в порядке предпочтения три способа преобразования различных числовых типов в экземпляры типа Char, и наоборот.
    // - Приведение типа. Самый эффективный способ, так как компилятор генерирует IL-команды преобразования без вызовов каких-либо методов.
    // Для преобразования типа Char в числовое значение, такое как Int32, приведение подходит лучше всего.
    // - Использование типа Convert.
    // - Использование интерфейса IConvertible.

    // Тип System.String
    // System.String, — представляет неизменяемый упорядоченный набор символов.
    // Тип String реализует также несколько интерфейсов (IComparable/IComparable<String>, ICloneable, IConvertible, IEnumerable/IEnumerable<Char> и IEquatable<String>).
    // Создание строк


}
