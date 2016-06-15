using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Threading;
using System.Security;
using System.Runtime.InteropServices;

namespace CLR_VIA_C_SHARP._3_BaseDataTypes._14_Strings
{
    class Strings
    {
        public static void main()
        {
            Strings s = new Strings();
            s.TestChar();
            s.CastChar();
            s.createString();
            s.verbatimString();
            s.testRegionalStandards();
            s.sortStrings();
            s.myCompare();
            s.createStringBuilder();
            s.testStringBuilder();
            s.testStringRegionalStandards();
            s.severalObjectsInOneString();
            s.createFormatting();
            s.testParse();
            s.testCodingEncoding();
            // s.testEncoding();
            s.testBase64Coding();
            // s.testSecureString();

            Testing test = new Testing();
            test.main();
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

        private void createString()
        {
            // String s = new String("Hi there."); // Ошибка
            String s = "Hi there.";
            Console.WriteLine(s);

            String s1 = "Hi" + Environment.NewLine + "there.";

            // Конкатенация трех литеральных строк образует одну литеральную строку
            String s2 = "Hi" + " " + "there.";

        }

        private void verbatimString()
        {
            // Задание пути к приложению
            String file = "C:\\Windows\\System32\\Notepad.exe";

            // Задание пути к приложению с помощью буквальной строки
            String file1 = @"C:\Windows\System32\Notepad.exe";
        }

        private void testRegionalStandards()
        {
            String s1 = "Strasse";
            String s2 = "Straße";
            Boolean eq;
            // CompareOrdinal возвращает ненулевое значение
            eq = String.Compare(s1, s2, StringComparison.Ordinal) == 0;
            Console.WriteLine("Ordinal comparison: '{0}' {2} '{1}'", s1, s2, eq ? "==" : "!=");
            // Сортировка строк для немецкого языка (de) в Германии (DE)
            CultureInfo ci = new CultureInfo("de-DE");
            // Compare возвращает нуль
            eq = String.Compare(s1, s2, true, ci) == 0;
            Console.WriteLine("Cultural comparison: '{0}' {2} '{1}'", s1, s2, eq ? "==" : "!=");
        }

        private void sortStrings()
        {
            String output = String.Empty;
            String[] symbol = new String[] { "<", "=", ">" };
            Int32 x;
            CultureInfo ci;
            // Следующий код демонстрирует, насколько отличается результат
            // сравнения строк для различных региональных стандартов
            String s1 = "coté";
            String s2 = "côte";
            // Сортировка строк для французского языка (Франция)
            ci = new CultureInfo("fr-FR");
            x = Math.Sign(ci.CompareInfo.Compare(s1, s2));
            output += String.Format("{0} Compare: {1} {3} {2}", ci.Name, s1, s2, symbol[x + 1]);
            output += Environment.NewLine;
            // Сортировка строк для японского языка (Япония)
            ci = new CultureInfo("ja-JP");
            x = Math.Sign(ci.CompareInfo.Compare(s1, s2));
            output += String.Format("{0} Compare: {1} {3} {2}", ci.Name, s1, s2, symbol[x + 1]);
            output += Environment.NewLine;
            // Сортировка строк по региональным стандартам потока
            ci = Thread.CurrentThread.CurrentCulture;
            x = Math.Sign(ci.CompareInfo.Compare(s1, s2));
            output += String.Format("{0} Compare: {1} {3} {2}", ci.Name, s1, s2, symbol[x + 1]);
            output += Environment.NewLine + Environment.NewLine;
            // Следующий код демонстрирует использование дополнительных возможностей
            // метода CompareInfo.Compare при работе с двумя строками
            // на японском языке
            // Эти строки представляют слово "shinkansen" (название
            // высокоскоростного поезда) в разных вариантах письма:
            // хирагане и катакане
            s1 = " "; // ("\u3057\u3093\u304b\u3093\u305b\u3093")
            s2 = " "; // ("\u30b7\u30f3\u30ab\u30f3\u30bb\u30f3")
            // Результат сравнения по умолчанию
            ci = new CultureInfo("ja-JP");
            x = Math.Sign(String.Compare(s1, s2, true, ci));
            output += String.Format("Simple {0} Compare: {1} {3} {2}", ci.Name, s1, s2, symbol[x + 1]);
            output += Environment.NewLine;
            // Результат сравнения, который игнорирует тип каны
            CompareInfo compareInfo = CompareInfo.GetCompareInfo("ja-JP");
            x = Math.Sign(compareInfo.Compare(s1, s2, CompareOptions.IgnoreKanaType));
            output += String.Format("Advanced {0} Compare: {1} {3} {2}", ci.Name, s1, s2, symbol[x + 1]);
            Console.WriteLine(output);
        }

        private void myCompare()
        {
            String s1 = "Hello";
            String s2 = "World";

            Boolean b = s1.Equals(s2);
            b = s1.Equals(s1);
            int i = String.Compare(s1, s2);
            i = String.Compare(s1, s1);
        }

        private void testInterning()
        {
            String s1 = "Hello";
            String s2 = "Hello";
            Console.WriteLine(Object.ReferenceEquals(s1, s2)); // Должно быть 'False'

            s1 = String.Intern(s1);
            s2 = String.Intern(s2);
            Console.WriteLine(Object.ReferenceEquals(s1, s2)); // 'True'
        }

        private static Int32 NumTimesWordAppearsEquals(String word, String[] wordlist)
        {
            Int32 count = 0;
            for (Int32 wordnum = 0; wordnum < wordlist.Length; wordnum++)
            {
                if (word.Equals(wordlist[wordnum], StringComparison.Ordinal))
                    count++;
            }
            return count;
        }

        private static Int32 NumTimesWordAppearsIntern(String word, String[] wordlist)
        {
            // В этом методе предполагается, что все элементы в wordlist
            // ссылаются на интернированные строки
            word = String.Intern(word);
            Int32 count = 0;
            for (Int32 wordnum = 0; wordnum < wordlist.Length; wordnum++)
            {
                if (Object.ReferenceEquals(word, wordlist[wordnum]))
                    count++;
            }
            return count;
        }

        private void testStringInfo()
        {
            // Следующая строка содержит комбинированные символы
            String s = "a\u0304\u0308bc\u0327";
            SubstringByTextElements(s);
            EnumTextElements(s);
            EnumTextElementIndexes(s);
        }

        private static void SubstringByTextElements(String s)
        {
            String output = String.Empty;
            StringInfo si = new StringInfo(s);
            for (Int32 element = 0; element < si.LengthInTextElements; element++)
            {
                output += String.Format("Text element {0} is '{1}'{2}", element, si.SubstringByTextElements(element, 1), Environment.NewLine);
            }
            Console.WriteLine(output);
        }

        private static void EnumTextElements(String s)
        {
            String output = String.Empty;
            TextElementEnumerator charEnum = StringInfo.GetTextElementEnumerator(s);
            while (charEnum.MoveNext())
            {
                output += String.Format("Character at index {0} is '{1}'{2}", charEnum.ElementIndex, charEnum.GetTextElement(), Environment.NewLine);
            }
            Console.WriteLine(output);
        }

        private static void EnumTextElementIndexes(String s)
        {
            String output = String.Empty;
            Int32[] textElemIndex = StringInfo.ParseCombiningCharacters(s);
            for (Int32 i = 0; i < textElemIndex.Length; i++)
            {
                output += String.Format(
                "Character {0} starts at index {1}{2}",
                i, textElemIndex[i], Environment.NewLine);
            }
            Console.WriteLine(output);
        }

        private void createStringBuilder()
        {
            StringBuilder sb = new StringBuilder();
            String s = sb.AppendFormat("{0} {1}", "Jeffrey", "Richter").Replace(' ', '-').Remove(4, 3).ToString();
            Console.WriteLine(s); // "Jeff-Richter"
        }

        private void testStringBuilder()
        {
            // Создаем StringBuilder для операций со строками
            StringBuilder sb = new StringBuilder();

            // Выполняем ряд действий со строками, используя StringBuilder
            sb.AppendFormat("{0} {1}", "Jeffrey", "Richter").Replace(" ", "-");

            // Преобразуем StringBuilder в String, чтобы сделать все символы прописными
            String s = sb.ToString().ToUpper();

            // Очищаем StringBuilder (выделяется память под новый массив Char)
            sb.Length = 0;

            // Загружаем строку с прописными String в StringBuilder и выполняем остальные операции
            sb.Append(s).Insert(8, "Marc-");

            // Преобразуем StringBuilder обратно в String
            s = sb.ToString();

            // Выводим String на экран для пользователя
            Console.WriteLine(s); // "JEFFREY-Marc-RICHTER"
        }

        private void testStringRegionalStandards()
        {
            Decimal price = 123.54M;
            String s = price.ToString("C", new CultureInfo("vi-VN"));
            Console.WriteLine(s);

            Decimal price1 = 123.54M;
            String s1 = price1.ToString("C", CultureInfo.InvariantCulture);
            Console.WriteLine(s1);
        }

        private void severalObjectsInOneString()
        {
            String s = String.Format("On {0}, {1} is {2} years old.", new DateTime(2012, 4, 22, 14, 35, 5), "Aidan", 9);
            Console.WriteLine(s);

            String s1 = String.Format("On {0:D}, {1} is {2:E} years old.", new DateTime(2012, 4, 22, 14, 35, 5), "Aidan", 9);
            Console.WriteLine(s1);
        }

        private void createFormatting()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(new BoldInt32s(), "{0} {1} {2:M}", "Jeff", 123, DateTime.Now);
            Console.WriteLine(sb);
        }

        internal sealed class BoldInt32s : IFormatProvider, ICustomFormatter
        {
            public Object GetFormat(Type formatType)
            {
                if (formatType == typeof(ICustomFormatter)) return this;
                return Thread.CurrentThread.CurrentCulture.GetFormat(formatType);
            }

            public String Format(String format, Object arg, IFormatProvider formatProvider)
            {
                String s;
                IFormattable formattable = arg as IFormattable;
                if (formattable == null)
                    s = arg.ToString();
                else
                    s = formattable.ToString(format, formatProvider);
                if (arg.GetType() == typeof(Int32))
                    return "<B>" + s + "</B>";
                return s;
            }
        }

        private void testParse()
        {
            Int32 x = Int32.Parse(" 123", NumberStyles.AllowLeadingWhite, null);
            Int32 y = Int32.Parse("1A", NumberStyles.HexNumber, null);
            Console.WriteLine(y); // Отображает "26".
        }

        private void testCodingEncoding()
        {
            // Кодируемая строка
            String s = "Hi there.";
            // Получаем объект, производный от Encoding, который "умеет" выполнять
            // кодирование и декодирование с использованием UTF-8
            Encoding encodingUTF8 = Encoding.UTF8;
            // Выполняем кодирование строки в массив байтов
            Byte[] encodedBytes = encodingUTF8.GetBytes(s);
            // Показываем значение закодированных байтов
            Console.WriteLine("Encoded bytes: " +
            BitConverter.ToString(encodedBytes));
            // Выполняем декодирование массива байтов обратно в строку
            String decodedString = encodingUTF8.GetString(encodedBytes);
            // Показываем декодированную строку
            Console.WriteLine("Decoded string: " + decodedString);
        }

        private void testEncoding()
        {
            foreach (EncodingInfo ei in Encoding.GetEncodings())
            {
                Encoding e = ei.GetEncoding();
                Console.WriteLine("{1}{0}" +
                "\tCodePage={2}, WindowsCodePage={3}{0}" +
                "\tWebName={4}, HeaderName={5}, BodyName={6}{0}" +
                "\tIsBrowserDisplay={7}, IsBrowserSave={8}{0}" +
                "\tIsMailNewsDisplay={9}, IsMailNewsSave={10}{0}",
                Environment.NewLine,
                e.EncodingName, e.CodePage, e.WindowsCodePage,
                e.WebName, e.HeaderName, e.BodyName,
                e.IsBrowserDisplay, e.IsBrowserSave,
                e.IsMailNewsDisplay, e.IsMailNewsSave);
            }
        }

        private void testBase64Coding()
        {
            // Получаем набор из 10 байт, сгенерированных случайным образом
            Byte[] bytes = new Byte[10];
            new Random().NextBytes(bytes);
            // Отображаем байты
            Console.WriteLine(BitConverter.ToString(bytes));
            // Декодируем байты в строку в кодировке base-64 и выводим эту строку
            String s = Convert.ToBase64String(bytes);
            Console.WriteLine(s);
            // Кодируем строку в кодировке base-64 обратно в байты и выводим их
            bytes = Convert.FromBase64String(s);
            Console.WriteLine(BitConverter.ToString(bytes));
        }

        private void testSecureString()
        {
            using (SecureString ss = new SecureString())
            {
                Console.Write("Please enter password: ");
                while (true)
                {
                    ConsoleKeyInfo cki = Console.ReadKey(true);
                    if (cki.Key == ConsoleKey.Enter) break;
                    // Присоединить символы пароля в конец SecureString
                    ss.AppendChar(cki.KeyChar);
                    Console.Write("*");
                }
                Console.WriteLine();
                // Пароль введен, отобразим его для демонстрационных целей
                DisplaySecureString(ss);
            }
            // После 'using' SecureString обрабатывается методом Disposed,
            // поэтому никаких конфиденциальных данных в памяти нет
        }

        // Этот метод небезопасен, потому что обращается к неуправляемой памяти
        private static void DisplaySecureString(SecureString ss)
        {
            //Char* pc = null;
            //try
            //{
            //    // Дешифрование SecureString в буфер неуправляемой памяти
            //    pc = (Char*)Marshal.SecureStringToCoTaskMemUnicode(ss);
            //    // Доступ к буферу неуправляемой памяти,
            //    // который хранит дешифрованную версию SecureString
            //    for (Int32 index = 0; pc[index] != 0; index++)
            //        Console.Write(pc[index]);
            //}
            //finally
            //{
            //    // Обеспечиваем обнуление и освобождение буфера неуправляемой памяти,
            //    // который хранит расшифрованные символы SecureString
            //    if (pc != null)
            //        Marshal.ZeroFreeCoTaskMemUnicode((IntPtr)pc);
            //}
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
    // В C# оператор new не может использоваться для создания объектов String из литеральных строк:
    // Для вставки специальных символов, таких как конец строки, возврат каретки, забой, в C# используются управляющие последовательности, знакомые разработчикам на C/C++.
    // Внимание
    // Задавать в коде последовательность символов конца строки и перевода каретки напрямую, как это сделано в представленном примере, не рекомендуется.
    // У типа System.Environment определено неизменяемое свойство NewLine, которое при выполнении приложения в Windows возвращает строку, состоящую из этих символов.
    // Однако свойство NewLine зависит от платформы и возвращает ту строку, которая обеспечивает создание разрыва строк на конкретной платформе.
    // Скажем, при переносе CLI в UNIX свойство NewLine должно возвращать строку, состоящую только из символа «\n».
    // Чтобы приведенный код работал на любой платформе, перепишите его следующим образом:
    // String s = "Hi" + Environment.NewLine + "there.";
    // Чтобы объединить несколько строк в одну строку, используйте оператор + языка C#:
    // Для конкатенации нескольких строк на этапе выполнения оператор + применять нежелательно, так как он создает в куче несколько строковых объектов.
    // Вместо него рекомендуется использовать тип System.Text.StringBuilder.
    // И наконец, в C# есть особый вариант объявления строки, в которой все символы между кавычками трактуются как часть строки.
    // Эти специальные объявления — буквальные строки (verbatim strings) — обычно используют при задании пути к файлу или каталогу и при работе с регулярными выражениями.
    
    // Неизменяемые строки
    // Самое важное, что нужно помнить об объекте String — то, что он неизменяем; то есть созданную однажды строку нельзя сделать длиннее или короче, в ней нельзя изменить ни одного символа.
    // Благодаря неизменности строк отпадает проблема синхронизации потоков при работе со строками.
    // класс String является запечатанным.

    // Сравнение строк
    // Мы сравниваем две строки для выяснения, равны ли они, и для сортировки (прежде всего, для представления их пользователю программы).
    // Boolean Equals(String value, StringComparison comparisonType)
    // static Boolean Equals(String a, String b, StringComparison comparisonType)
    // static Int32 Compare(String strA, String strB, StringComparison comparisonType)
    // static Int32 Compare(string strA, string strB, Boolean ignoreCase, CultureInfo culture)
    // static Int32 Compare(String strA, String strB, CultureInfo culture, CompareOptions options)
    // static Int32 Compare(String strA, Int32 indexA, String strB, Int32 indexB, Int32 length, StringComparison comparisonType)
    // static Int32 Compare(String strA, Int32 indexA, String strB, Int32 indexB, Int32 length, CultureInfo culture, CompareOptions options)
    // static Int32 Compare(String strA, Int32 indexA, String strB, Int32 indexB, Int32 length, Boolean ignoreCase, CultureInfo culture)
    // Boolean StartsWith(String value, StringComparison comparisonType)
    // Boolean StartsWith(String value, Boolean ignoreCase, CultureInfo culture)
    // Boolean EndsWith(String value, StringComparison comparisonType)
    // Boolean EndsWith(String value, Boolean ignoreCase, CultureInfo culture)
    // При сортировке всегда нужно учитывать регистр символов. Дело в том, что две строки, отличающиеся лишь регистром символов, будут считаться одинаковыми и поэтому при каждой сортировке они могут упорядочиваться в произвольном порядке, что может приводить пользователя в замешательство.
    // В аргументе comparisonType (он есть в большинстве перечисленных методов) передается одно из значений, определенных в перечислимом типе StringComparison, который определен следующим образом:
    // public enum StringComparison {
    // CurrentCulture = 0,
    // CurrentCultureIgnoreCase = 1,
    // InvariantCulture = 2,
    // InvariantCultureIgnoreCase = 3,
    // Ordinal = 4,
    // OrdinalIgnoreCase = 5
    // }
    // Аргумент options является одним из значений, определенных перечислимым типом CompareOptions:
    // [Flags]
    // public enum CompareOptions
    // {
    //     None = 0,
    //     IgnoreCase = 1,
    //     IgnoreNonSpace = 2,
    //     IgnoreSymbols = 4,
    //     IgnoreKanaType = 8,
    //     IgnoreWidth = 0x00000010,
    //     Ordinal = 0x40000000,
    //     OrdinalIgnoreCase = 0x10000000,
    //     StringSort = 0x20000000
    // }
    // Для сравнения внутренних строк следует всегда использовать флаг StringComparison.Ordinal или StringComparison.OrdinalIgnoreCase.
    // Это самый быстрый способ сравнения, так как он игнорирует лингвистические особенности и региональные стандарты.
    // С другой стороны, если требуется корректно сравнить строки с точки зрения лингвистических особенностей (обычно перед выводом их на экран для пользователя), следует использовать флаг StringComparison.CurrentCulture или StringComparison.CurrentCultureIgnoreCase.
    // Внимание
    // Обычно следует избегать использования флагов StringComparison.InvariantCulture и StringComparison.InvariantCultureIgnoreCase. 
    // Внимание
    // Если вы хотите изменить регистр символов строки перед выполнением простого сравнения, следует использовать предоставляемый String метод ToUpperInvariant или ToLowerInvariant.
    // При нормализации строк настоятельно рекомендуется использовать метод ToUpperInvariant, а не ToLowerInvariant из-за того, что в Microsoft сравнение строк в верхнем регистре оптимизировано.
    // Для представления пары «язык-страна» (как описано в спецификации RFC 1766) в .NET Framework используется тип System.Globalization.CultureInfo.
    // В частности, en-US означает американскую (США) версию английского языка.
    // - CurrentUICulture служит для получения ресурсов, видимых конечному пользователю.
    // Это свойство наиболее полезно для графического интерфейса пользователя или приложений Web Forms, так как оно обозначает язык, который следует выбрать для вывода элементов пользовательского интерфейса, таких как надписи и кнопки. 
    // Внимание
    // В типе String определено несколько вариантов перегрузки методов Equals, StartsWith, EndsWith и Compare помимо тех, что приведены ранее. Microsoft рекомендует избегать других версий (не представленных в этой книге). 
    // - CurrentCulture используется во всех случаях, в которых не используется свойство CurrentUICulture, в том числе для форматирования чисел и дат, приведения и сравнения строк.
    // ПримечАние
    // Если метод Compare не выполняет простое сравнение, то он производит расширение символов (character expansions), то есть разбивает сложные символы на несколько символов, игнорируя региональные стандарты.
    // В предыдущем случае немецкий символ ß всегда расширяется до ss. Аналогично лигатурный символ Æ всегда расширяется до AE. Поэтому в приведенном примере вызов Compare будет всегда возвращать 0 независимо от выбранных региональных стандартов.
    // ПримечАние
    // Подобные файлы с исходным кодом нельзя сохранить в кодировке ANSI, поскольку иначе японские символы будут потеряны. 
    // Помимо Compare, класс CompareInfo предлагает методы IndexOf, IsLastIndexOf, IsPrefix и IsSuffix.

    // Интернирование строк
    // Как я уже отмечал, сравнение строк используется во многих приложениях, однако эта операция может ощутимо сказаться на производительности.
    // При порядковом сравнении (ordinal comparison) CLR быстро проверяет, равно ли количество символов в строках.
    // При отрицательном результате строки точно не равны, но если длина одинакова, приходится сравнивать их символ за символом.
    // При сравнении с учетом региональных стандартов среде CLR тоже приходится посимвольно сравнить строки, потому что две строки разной длины могут оказаться равными.
    // При инициализации CLR создает внутреннюю хеш-таблицу, в которой ключами являются строки, а значениями — ссылки на строковые объекты в управляемой куче.
    // Вначале таблица, разумеется, пуста. В классе String есть два метода, предоставляющие доступ к внутренней хеш-таблице:
    // public static String Intern(String str);
    // public static String IsInterned(String str);
    // Если сборка отмечена атрибутом System.Runtime.CompilerServices.CompilationRelaxations Attribute, определяющим значение флага System.Runtime.CompilerServices.CompilationRelaxations.NoStringInterning, то в соответствии со спецификацией ECMA среда CLR может отказаться от интернирования строк, определенных в метаданных сборки.
    // Обратите внимание, что в целях повышения производительности работы приложения компилятор C# всегда при компиляции сборки определяет этот атрибут/флаг.
    
    // Создание пулов строк
    // Чтобы не допустить роста объема кода, многие компиляторы (в том числе C#) хранят литеральную строку в метаданных модуля только в одном экземпляре.
    // Все упоминания этой строки в исходном коде компилятор заменяет ссылками на ее экземпляр в метаданных.
    
    // Работа с символами и текстовыми элементами в строке
    // С подобными задачами призваны справляться несколько методов и свойств типа String, в числе которых Length, Chars (индексатор в C#), GetEnumerator, ToCharArray, Contains, IndexOf, LastIndexOf, IndexOfAny и LastIndexOfAny.
    // Для корректной работы с текстовыми элементами предназначен тип System.Globalization.StringInfo.
    
    // Прочие операции со строками
    // Методы копирования строк
    // Член - Тип метода - Описание
    // Clone - Экземплярный - Возвращает ссылку на тот же самый объект (this). Это нормально, так как объекты String неизменяемы. Этот метод реализует интерфейс ICloneable класса String
    // Copy - Статический - Возвращает новую строку — дубликат заданной строки. Используется редко и нужен только для приложений, обрабатывающих строки как лексемы. Обычно строки с одинаковым набором символов интернируются в одну строку. Этот метод, напротив, создает новый строковый объект и возвращает иной указатель (ссылку), хотя в строках содержатся одинаковые символы
    // CopyTo - Экземплярный - Копирует группу символов строки в массив символов
    // Substring - Экземплярный - Возвращает новую строку, представляющую часть исходной строки
    // ToString - Экземплярный - Возвращает ссылку на тот же объект (this)
    // Помимо этих методов, у типа String есть много статических и экземплярных методов для различных операций со строками: Insert, Remove, PadLeft, Replace, Split, Join, ToLower, ToUpper, Trim, Concat, Format и пр.
    // Еще раз повторю, что все эти методы возвращают новые строковые объекты; создать строку можно, но изменить ее нельзя (при условии использования безопасного кода).

    // Эффективное создание строк
    // для динамических операций со строками и символами при создании объектов String в FCL имеется тип System.Text.StringBuilder.
    // У объекта StringBuilder предусмотрено поле со ссылкой на массив структур Char. Используя члены StringBuilder, можно эффективно манипулировать этим массивом, сокращая строку и изменяя символы строки.
    // Сформировав свою строку с помощью объекта StringBuilder, «преобразуйте» массив символов StringBuilder в объект String, вызвав метод ToString типа StringBuilder.

    // Создание объекта StringBuilder
    // У типа StringBuilder несколько конструкторов. Задача каждого из них — выделять память и инициализировать три внутренних поля, управляемых любым объектом StringBuilder.
    // - Максимальная емкость (maximum capacity) — поле типа Int32, которое задает максимальное число символов, размещаемых в строке. По умолчанию оно равно Int32.MaxValue (около двух миллиардов).
    // Это значение обычно не изменяется,хотя можно задать и меньшее значение, ограничивающее размер создаваемой строки. Для уже созданного объекта StringBuilder это поле изменить нельзя.
    // - Емкость (capacity) — поле типа Int32, показывающее размер массива символов StringBuilder.
    // По умолчанию оно равно 16. Если известно, сколько символов предполагается разместить в StringBuilder, укажите это число при создании объекта StringBuilder.
    // При добавлении символов StringBuilder определяет, не выходит ли новый размер массива за установленный предел.
    // Если да, то StringBuilder автоматически удваивает емкость, и исходя из этого значения, выделяет память под новый массив, а затем копирует символы из исходного массива в новый.
    // Исходный массив в дальнейшем утилизируется сборщиком мусора. Динамическое увеличение массива снижает производительность, поэтому его следует избегать, задавая подходящую емкость в начале работы с объектом.
    // - Массив символов (character array) — массив структур Char, содержащий набор символов «строки».
    // Число символов всегда меньше (или равно) емкости и максимальной емкости. Количество символов в строке можно получить через свойство Length типа StringBuilder.
    // Значение Length всегда меньше или равно емкости StringBuilder. При создании StringBuilder можно инициализировать массив символов, передавая ему String как параметр.
    // Если строка не задана,массив первоначально не содержит символов и свойство Length возвращает 0.
    
    // Члены типа StringBuilder
    // Тип StringBuilder в отличие от String представляет изменяемую строку.
    // Это значит, что многие члены StringBuilder изменяют содержимое в массиве символов, не создавая новых объектов, размещаемых в управляемой куче.
    // StringBuilder выделяет память для новых объектов только в двух случаях:
    // - при динамическом построении строки, размер которой превышает установленную емкость;
    // - при вызове метода ToString типа StringBuilder.
    // Члены класса StringBuilder
    // Член - Тип члена - Описание
    // MaxCapacity - Неизменяемое свойство - Возвращает наибольшее количество символов, которое может быть размещено в строке
    // Capacity - Изменяемое свойство - Получает/устанавливает размер массива символов. При попытке установить емкость меньшую, чем длина строки, или больше,чем MaxCapacity, генерируется исключение ArgumentOutOfRangeException
    // EnsureCapacity - Метод - Гарантирует, что размер массива символов будет не меньше, чем значение параметра, передаваемого этому методу. Если значение превышает текущую емкость объекта StringBuilder, размер массива увеличивается. Если текущая емкость больше, чем значение, передаваемое этому свойству, размер массива не изменяется
    // Length - Изменяемое свойство - Возвращает количество символов в «строке». Эта величина может быть меньше текущей емкости массива символов. Присвоение этому свойству значения 0 сбрасывает содержимое и очищает строку StringBuilder
    // ToString - Метод - Версия без параметров возвращает объект String, представляющий массив символов объекта StringBuilder
    // Chars - Изменяемое свойство-индексатор - Возвращает из массива или устанавливает в массиве символ с заданным индексом. В C# это свойство-индексатор (свойство с параметром), доступ к которому осуществляется как к элементам массива (с использованием квадратных скобок [])
    // Clear - Метод - Очищает содержимое объекта StringBuilder, аналогично назначению свойству Length значения 0
    // Append - Метод - Добавляет единичный объект в массив символов, увеличивая его при необходимости. Объект преобразуется в строку с использованием общего формата и с учетом региональных стандартов, связанных с вызывающим потоком
    // Insert - Метод - Вставляет единичный объект в массив символов, увеличивая его при необходимости. Объект преобразуется в строку с использованием общего формата и с учетом региональных стандартов, связанных с вызывающим потоком
    // AppendFormat - Метод - Добавляет заданные объекты в массив символов, увеличивая его при необходимости. Объекты преобразуются в строку указанного формата и с учетом заданных региональных стандартов. Это один из наиболее часто используемых методов при работе с объектами StringBuilder
    // AppendLine - Метод - Присоединяет пустую строку в конец символьного массива, увеличивая его емкость при необходимости
    // Replace - Метод - Заменяет один символ или строку символов в массиве символов
    // Remove - Метод - Удаляет диапазон символов из массива символов
    // Equals - Метод - Возвращает true, только если объекты StringBuilder имеют одну и ту же максимальную емкость, емкость и одинаковые символы в массиве
    // CopyTo - Метод - Копирует подмножество символов StringBuilder в массив Char
    // Отмечу одно важное обстоятельство: большинство методов StringBuilder возвращают ссылку на тот же объект StringBuilder.
    // Это позволяет выстроить в цепочку сразу несколько операций 
    // У класса StringBuilder нет некоторых аналогов для методов класса String.
    // Например, у класса String есть методы ToLower, ToUpper, EndsWith, PadLeft, Trim и т. д., отсутствующие у класса StringBuilder.
    // В то же время у класса StringBuilder есть расширенный метод Replace, выполняющий замену символов и строк лишь в части строки (а не во всей строке).
    // Из-за отсутствия полного соответствия между методами иногда приходится прибегать к преобразованиям между String и StringBuilder.

    // Получение строкового представления объекта
    // Для получения представления любого объекта в виде строки надо вызвать метод ToString.
    // Типы, которые хотят представить текущее значение объекта в более содержательном виде, должны переопределить метод ToString.
    
    // Форматы и региональные стандарты
    // Тип может предложить вызывающей программе выбор форматирования и региональных стандартов, если он реализует интерфейс System.IFormattable:
    // public interface IFormattable
    // {
    //     String ToString(String format, IFormatProvider formatProvider);
    // }
    // В FCL у всех базовых типов (Byte, SByte, Int16/UInt16, Int32/UInt32, Int64/UInt64, Single, Double, Decimal и DateTime) есть реализации этого интерфейса.
    // Кроме того, есть такие реализации и у некоторых других типов, например GUID.
    // К тому же каждый перечислимый тип автоматически реализует интерфейс IFormattable, позволяющий получить строковое выражение для числового значения, содержащегося в экземпляре перечислимого типа.
    // Многие типы FCL поддерживают несколько форматов. Например, тип DateTime поддерживает следующие форматы: "d" — даты в кратком формате, "D" — даты в полном формате, "g" — даты в общем формате, "M" — формат «месяц/день», "s" — сортируемые даты, "T" — время, "u" — универсальное время в стандарте ISO 8601, "U" — универсальное время в полном формате, "Y" — формат «год/месяц» и т. д.
    // Все перечислимые типы поддерживают строки: "G" — общий формат, "F" — формат флагов, "D" — десятичный формат и "X" — шестнадцатеричный формат.
    // Кроме того, все встроенные числовые типы поддерживают следующие строки: "C" — формат валют, "D" — десятичный формат, "E" — научный (экспоненциальный) формат, "F" — формат чисел с фиксированной точкой, "G" — общий формат, "N"— формат чисел, "P" — формат процентов, "R" — обратимый (round-trip) формат и "X" — шестнадцатеричный формат.
    // Разрабатывая реализацию типа, выберите формат, который, по вашему мнению, будет использоваться чаще всего; это и будет «общий формат». Кстати, вызов метода ToString без параметров означает представление объекта в общем формате.
    // По умолчанию форматирование выполняется с учетом региональных стандартов, связанных с вызывающим потоком. Это свойственно методу ToString без параметров и методу ToString интерфейса IFormattable со значением null в качестве formatProvider.
    // При форматировании числа метод ToString «анализирует» параметр formatProvider. Если это null, метод ToString определяет региональные стандарты, связанные с вызывающим потоком, считывая свойство System.Threading.Thread.CurrentThread.CurrentCulture. Оно возвращает экземпляр типа System.Globalization.CultureInfo.
    // Получив объект, ToString считывает его свойства NumberFormat для форматирования числа или DateTimeFormat для форматирования даты.
    // Эти свойства возвращают экземпляры System.Globalization.NumberFormatInfo и System.Globalization.DateTimeFormatInfo соответственно.
    // Тип NumberFormatInfo описывает группу свойств, таких как CurrencyDecimalSeparator, CurrencySymbol, NegativeSign, NumberGroupSeparator и PercentSymbol. Аналогично, у типа DateTimeFormatInfo описаны такие свойства, как Calendar, DateSeparator, DayNames, LongDatePattern, ShortTimePattern и TimeSeparator. Метод ToString считывает эти свойства при создании и форматировании строки.
    // В FCL интерфейс IFormatProvider реализуется только тремя типами: уже упоминавшимся типом CultureInfo, а также типами NumberFormatInfo и DateTimeFormatInfo.
    
    // Форматирование нескольких объектов в одну строку
    // Чтобы расширить стандартное форматирование объекта, нужно добавить внутрь фигурных скобок строку форматирования.
    // Если вместо String для формирования строки применяется StringBuilder, можно вызывать метод AppendFormat класса StringBuilder.
    // Этот метод работает так же, как Format класса String, за исключением того, что результат форматирования добавляется к массиву символов StringBuilder.
    
    // Создание собственного средства форматирования
    
    // Получение объекта посредством разбора строки
    // Любой тип, способный разобрать строку, имеет открытый, статический метод Parse.
    // Он получает String, а на выходе возвращает экземпляр данного типа; в каком-то смысле Parse ведет себя как фабрика.
    // В FCL метод Parse поддерживается всеми числовыми типами, а также типами DateTime, TimeSpan и некоторыми другими.
    // public static Int32 Parse(String s, NumberStyles style, IFormatProvider provider);
    
    // ПримечАние
    // Некоторые разработчики сообщили в Microsoft о следующем факте: если при многократном вызове Parse этот метод генерирует исключения (из-за неверных данных, вводимых пользователями), это отрицательно сказывается на производительности приложения.
    // Для таких требующих высокой производительности случаев в Microsoft создали методы TryParse для всех числовых типов данных, для DateTime, TimeSpan и даже для IPAddress.
    // Вот как выглядит один из двух перегруженных методов TryParse типа Int32:
    // public static Boolean TryParse(String s, NumberStyles style, IFormatProvider provider, out Int32 result);
    // Как видите, метод возвращает true или false, информируя, удастся ли разобрать строку в объект Int32.
    // Если метод возвращает true, переменная, переданная по ссылке в результирующем параметре, будет содержать полученное в результате разбора числовое значение. 
    
    // Кодировки: преобразования между символами и байтами
    // Win32-программистам часто приходится писать код, преобразующий символы и строки из Unicode в Multi-Byte Character Set (MBCS).
    // В CLR все символы представлены 16-разрядными кодами Юникода, а строки состоят только из 16-разрядных символов Юникода. Это намного упрощает работу с символами и строками в период выполнения.
    // Кодирование обычно выполняется перед отправкой строки в файл или сетевой поток с помощью типов System.IO.BinaryWriter и System.IO.StreamWriter.
    // Декодирование обычно выполняется при чтении из файла или сетевого потока с помощью типов System.IO.BinaryReader и System.IO.StreamReader.
    // Если кодировка явно не указана, все эти типы по умолчанию используют код UTF-8
    // К счастью, в FCL есть типы, упрощающие операции кодирования и декодирования. К наиболее часто используемым кодировкам относят UTF-16 и UTF-8.
    // - UTF-16 кодирует каждый 16-разрядный символ в 2 байта. Заметьте также, что, используя UTF-16, можно выполнить преобразование из прямого порядка байтов (big endian) в обратный (little endian), и наоборот.
    // - UTF-8 кодирует некоторые символы одним байтом, другие — двумя байтами, третьи — тремя, а некоторые — четырьмя.
    // Хотя для большинства случаев подходят кодировки UTF-16 и UTF-8, FCL поддерживает и менее популярные кодировки.
    // - UTF-32 кодирует все символы в 4 байта. Эта кодировка используется для создания простого алгоритма прохода символов, в котором не требуется разбираться с символами, состоящими из переменного числа байтов.
    // - UTF-7 обычно используется в старых системах, где под символ отводится 7 разрядов. Этой кодировки следует избегать, поскольку обычно она приводит не к сжатию, а к раздуванию данных.
    // - ASCII кодирует 16-разрядные символы в ASCII-символы; то есть любой 16-разрядный символ со значением меньше 0x0080 переводится в одиночный байт.
    // Символы со значением больше 0x007F не поддаются этому преобразованию, и значение символа теряется.
    // Методы классов, производных от Encoding
    // Метод - Описание
    // GetPreamble - Возвращает массив байтов, показывающих, что нужно записать в поток перед записью кодированных байтов.
    // Convert - Преобразует массив байтов из одной кодировки в другую.
    // Equals - Возвращает true, если два производных от Encoding объекта представляют одну кодовую страницу и одинаковую преамбулу
    // GetHashCode - Возвращает кодовую страницу объекта кодирования
    
    // Кодирование и декодирование потоков символов и байтов
    // У всех производных от Decoder классов существует два метода: GetChars и GetCharCount.
    // Естественно, они служат для декодирования массивов байтов и работают аналогично рассмотренным ранее методам GetChars и GetCharCount класса Encoding.
    
    // Кодирование и декодирование строк в кодировке Base-64
    
    // Защищенные строки
    // Часто объекты String применяют для хранения конфиденциальных данных, таких как пароли или информация кредитной карты.
    // К сожалению, объекты String хранят массив символов в памяти, и если разрешить выполнение небезопасного или неуправляемого кода, он может просмотреть адресное пространство кода, найти строку с конфиденциальной информацией и использовать ее в своих неблаговидных целях.
    // специалисты Microsoft добавили в FCL безопасный строковый класс System.Security.SecureString.
    // При создании объекта SecureString его код выделяет блок неуправляемой памяти, которая содержит массив символов.
    // Уборщику мусора об этой неуправляемой памяти ничего не известно.
    // Символы строки шифруются для защиты конфиденциальной информации от любого потенциально опасного или неуправляемого кода.
    // Класс SecureString реализует интерфейс IDisposable, служащий для надежного уничтожения конфиденциальной информации, хранимой в строке.
    // В версии 4 инфраструктуры .NET Framework передать SecureString в качестве пароля можно:
    // - при работе с криптографическим провайдером (Cryptographic Service Provider, CSP) см. класс System.Security.Cryptography.CspParameters;
    // - при создании, импорте или экспорте сертификата в формате X.509 см. классы System.Security.Cryptography.X509Certificates.X509Certificate и System.Security.Cryptography.X509Certificates.X509Certificate2;
    // - при запуске нового процесса под определенной учетной записью пользователя см.классы System.Diagnostics.Process и System.Diagnostics.ProcessStartInfo;
    // -  при организации нового сеанса записи журнала событий см. класс System.Diagnostics.Eventing.Reader.EventLogSession;
    // - при использовании элемента управления System.Windows.Controls.PasswordBox см. класс свойства SecurePassword.
    // Класс System.Runtime.InteropServices.Marshal предоставляет 5 методов, которые служат для расшифровки символов SecureString в буфер неуправляемой памяти.
    // Метод расшифровки SecureString в буфер - Метод обнуления и освобождения буфера
    // SecureStringToBSTR - ZeroFreeBSTR
    // SecureStringToCoTaskMemAnsi - ZeroFreeCoTaskMemAnsi
    // SecureStringToCoTaskMemUnicode - ZeroFreeCoTaskMemUnicode
    // SecureStringToGlobalAllocAnsi - ZeroFreeGlobalAllocAnsi
    // SecureStringToGlobalAllocUnicode - ZeroFreeGlobalAllocUnicode
}