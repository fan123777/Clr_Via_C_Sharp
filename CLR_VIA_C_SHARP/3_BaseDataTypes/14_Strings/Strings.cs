using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Threading;

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
    // ... 375

}
