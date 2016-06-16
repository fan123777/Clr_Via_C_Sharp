using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace CLR_VIA_C_SHARP._3_BaseDataTypes._15_EnumsBitFlags
{
    class EnumsBitFlags
    {
        public static void main()
        {
            EnumsBitFlags e = new EnumsBitFlags();
            e.testEnum();
            e.enumParseDefine();
            e.testBitFlags();
            e.bitParse();
            e.bitExtentionMethod();
        }

        private void testEnum()
        {
            // Эта строка выводит "System.Byte"
            Console.WriteLine(Enum.GetUnderlyingType(typeof(MyColor)));

            Color c = Color.Blue;
            Console.WriteLine(c); // "Blue" (Общий формат)
            Console.WriteLine(c.ToString()); // "Blue" (Общий формат)
            Console.WriteLine(c.ToString("G")); // "Blue" (Общий формат)
            Console.WriteLine(c.ToString("D")); // "3" (Десятичный формат)
            Console.WriteLine(c.ToString("X")); // "03" (Шестнадцатеричный формат)

            // В результате выводится строка "Blue"
            Console.WriteLine(Enum.Format(typeof(Color), 3, "G"));

            Color[] colors = (Color[])Enum.GetValues(typeof(Color));
            Console.WriteLine("Number of symbols defined: " + colors.Length);
            Console.WriteLine("Value\tSymbol\n-----\t------");
            foreach (Color col in colors)
            {
                // Выводим каждый идентификатор в десятичном и общем форматах
                Console.WriteLine("{0,5:D}\t{0:G}", col);
            }

            Color[] myColors = GetEnumValues<Color>();
        }

        private void enumParseDefine()
        {
            // Так как Orange определен как 4, 'c' присваивается значение 4
            Color c = (Color)Enum.Parse(typeof(Color), "orange", true);
            // Так как Brown не определен, генерируется исключение ArgumentException
            // c = (Color)Enum.Parse(typeof(Color), "Brown", false);
            // Создается экземпляр перечисления Color со значением 1
            Enum.TryParse<Color>("1", false, out c);
            // Создается экземпляр перечисления Color со значение 23
            Enum.TryParse<Color>("23", false, out c);

            // идентификатор Red определен как 1
            Console.WriteLine(Enum.IsDefined(typeof(Color), 1));
            // Выводит "True", так как в перечислении Colorидентификатор White определен как 0
            Console.WriteLine(Enum.IsDefined(typeof(Color), "White"));
            // Выводит "False", так как выполняется проверка с учетом регистра
            Console.WriteLine(Enum.IsDefined(typeof(Color), "white"));
            // Выводит "False", так как в перечислении Color отсутствует идентификатор со значением 10
            // Console.WriteLine(Enum.IsDefined(typeof(Color), (Byte)10));
        }

        private void testBitFlags()
        {
            String file = Assembly.GetEntryAssembly().Location;
            FileAttributes attributes = File.GetAttributes(file);
            Console.WriteLine("Is {0} hidden? {1}", file, (attributes & FileAttributes.Hidden) != 0);

            // File.SetAttributes(file, FileAttributes.ReadOnly | FileAttributes.Hidden);

            Actions actions = Actions.Read | Actions.Delete; // 0x0005
            Console.WriteLine(actions.ToString()); // "Read, Delete"

            MyActions myActions = MyActions.Read | MyActions.Delete; // 0x0005
            Console.WriteLine(myActions.ToString("F")); // "Read, Delete"
        }

        private void bitParse()
        {
            // Так как Query определяется как 8, 'a' получает начальное значение 8
            Actions a = (Actions)Enum.Parse(typeof(Actions), "Query", true);
            Console.WriteLine(a.ToString()); // "Query"
            // Так как у нас определены и Query, и Read, 'a' получает
            // начальное значение 9
            Enum.TryParse<Actions>("Query, Read", false, out a);
            Console.WriteLine(a.ToString()); // "Read, Query"
            // Создаем экземпляр перечисления Actions enum со значением 28
            a = (Actions)Enum.Parse(typeof(Actions), "28", false);
            Console.WriteLine(a.ToString()); // "Delete, Query, Sync"
        }

        private void bitExtentionMethod()
        {
            FileAttributes fa = FileAttributes.System;
            fa = fa.Set(FileAttributes.ReadOnly);
            fa = fa.Clear(FileAttributes.System);
            fa.ForEach(f => Console.WriteLine(f));
        }

        public static TEnum[] GetEnumValues<TEnum>() where TEnum : struct
        {
            return (TEnum[])Enum.GetValues(typeof(TEnum));
        }

        public void SetColor(Color c)
        {
            if (!Enum.IsDefined(typeof(Color), c))
            {
                throw (new ArgumentOutOfRangeException("c", c, "Invalid Color value."));
            }
            // Задать цвет, как White, Red, Green, Blue или Orange
        }
    }

    internal enum Color
    {
        White, // Присваивается значение 0
        Red, // Присваивается значение 1
        Green, // Присваивается значение 2
        Blue, // Присваивается значение 3
        Orange // Присваивается значение 4
    }

    internal enum MyColor : byte
    {
        White,
        Red,
        Green,
        Blue,
        Orange
    }

    [Flags] // Компилятор C# допускает значение "Flags" или "FlagsAttribute"
    internal enum Actions
    {
        None = 0,
        Read = 0x0001,
        Write = 0x0002,
        ReadWrite = Actions.Read | Actions.Write,
        Delete = 0x0004,
        Query = 0x0008,
        Sync = 0x0010
    }

    // [Flags] // Теперь это просто комментарий
    internal enum MyActions
    {
        None = 0,
        Read = 0x0001,
        Write = 0x0002,
        ReadWrite = MyActions.Read | MyActions.Write,
        Delete = 0x0004,
        Query = 0x0008,
        Sync = 0x0010
    }

    internal static class FileAttributesExtensionMethods
    {
        public static Boolean IsSet(this FileAttributes flags, FileAttributes flagToTest)
        {
            if (flagToTest == 0)
                throw new ArgumentOutOfRangeException(
                "flagToTest", "Value must not be 0");
            return (flags & flagToTest) == flagToTest;
        }

        public static Boolean IsClear(this FileAttributes flags, FileAttributes flagToTest)
        {
            if (flagToTest == 0)
                throw new ArgumentOutOfRangeException(
                "flagToTest", "Value must not be 0");
            return !IsSet(flags, flagToTest);
        }

        public static Boolean AnyFlagsSet(this FileAttributes flags, FileAttributes testFlags)
        {
            return ((flags & testFlags) != 0);
        }

        public static FileAttributes Set(this FileAttributes flags, FileAttributes setFlags)
        {
            return flags | setFlags;
        }

        public static FileAttributes Clear(this FileAttributes flags, FileAttributes clearFlags)
        {
            return flags & ~clearFlags;
        }

        public static void ForEach(this FileAttributes flags, Action<FileAttributes> processFlag)
        {
            if (processFlag == null) throw new ArgumentNullException("processFlag");
            for (UInt32 bit = 1; bit != 0; bit <<= 1)
            {
                UInt32 temp = ((UInt32)flags) & bit;
                if (temp != 0) processFlag((FileAttributes)temp);
            }
        }
    }

    // Перечислимые типы и битовые флаги
    
    // Перечислимые типы
    // Перечислимым (enumerated type) называют тип, в котором описан набор пар, состоящих из символьных имен и значений.
    // Конечно, в программе можно вместо White написать 0, вместо Green — 1 и т. д.
    // Однако перечислимый тип все-таки лучше жестко заданных в исходном коде числовых значений по крайней мере по двум причинам.
    // - Программу, где используются перечислимые типы, проще написать и понять, а у разработчиков возникает меньше проблем с ее сопровождением.
    // - Перечислимые типы подвергаются строгой проверке типов.
    // Например, компилятор сообщит об ошибке, если в качестве значения я попытаюсь передать методу тип Color.Orange (оранжевый цвет), когда метод ожидает перечислимый тип Fruit (фрукт).
    // Каждый перечислимый тип напрямую наследует от типа System.Enum, производного от System.ValueType, а тот, в свою очередь, — от System.Object.
    // Из этого следует, что перечислимые типы относятся к значимым типам и могут выступать как в неупакованной, так и в упакованной формах.
    // Однако в отличие от других значимых типов, у перечислимого типа не может быть методов, свойств и событий.
    // Впрочем, как вы увидите в конце данной главы, наличие метода у перечислимого типа можно имитировать при помощи механизма методов расширения.
    // При компиляции перечислимого типа компилятор C# превращает каждый идентификатор в константное поле типа.
    // internal struct Color : System.Enum
    // {
    //     // Далее перечислены открытые константы,
    //     // определяющие символьные имена и значения
    //     public const Color White = (Color)0;
    //     public const Color Red = (Color)1;
    //     public const Color Green = (Color)2;
    //     public const Color Blue = (Color)3;
    //     public const Color Orange = (Color)4;
    //     // Далее находится открытое поле экземпляра со значением переменной Color
    //     // Код с прямой ссылкой на этот экземпляр невозможен
    //     public Int32 value__;
    // }
    // Константные поля попадают в метаданные сборки, откуда их можно извлечь с помощью механизма отражения.
    // Внимание
    // Описанные перечислимым типом символы являются константами.
    // Встречая в коде символическое имя перечислимого типа, компилятор заменяет его числовым значением.
    // В результате определяющая перечислимый тип сборка может оказаться ненужной во время выполнения.
    // Но если в коде присутствует ссылка не на определенные перечислимым типом символические имена, а на сам тип, присутствие сборки на стадии выполнения будет обязательным.
    // То есть возникает проблема версий, связанная с тем, что символы перечислимого типа являются константами, а не значениями, предназначенными только для чтения. 
    // Однако компилятор C# пропустит только примитивный тип; задание базового класса FCL (например, Int32) приведет к сообщению об ошибке.
    // Компилятор C# считает перечислимые типы примитивными, поэтому для операций с их экземплярами применяются уже знакомые нам операторы (==, !=, <, >, <=, >=, +, –, ^, &, |, ~, ++ и ––).
    // Все они применяются к полю value__ экземпляра перечисления, а компилятор C# допускает приведение экземпляров одного перечислимого типа к другому. Также поддерживается явное и неявное приведение к числовому типу.
    // ПримечАние
    // При работе с шестнадцатеричным форматом метод ToString всегда возвращает прописные буквы.
    // Количество возвращенных цифр зависит от типа, лежащего в основе перечисления.
    // Для типов byte/sbyte — это две цифры, для типов short/ushort — четыре, для типов int/uint — восемь, а для типов long/ulong — снова две.
    // При необходимости добавляются ведущие нули.
    // ПримечАние
    // Можно объявить перечисление, различные идентификаторы которого имеют одинаковое числовое значение.
    // В процессе преобразования числового значения в символ посредством общего форматирования методы типа вернут один из символов, правда, неизвестно какой. Если соответствия не обнаруживается, возвращается строка с числовым значением.
    // Статический метод GetValues типа System.Enum и метод GetEnumValues экземпляра System.Type создают массив, элементами которого становятся символьные имена перечисления.
    // Помимо метода GetValues, типы System.Enum и System.Type предоставляют еще два метода для получения символических имен перечислимых типов:
    // // Возвращает строковое представление числового значения
    // public static String GetName(Type enumType, Object value); // Определен в System.Enum
    // public String GetEnumName(Object value); // Определен в System.Type
    // // Возвращает массив строк: по одной на каждое символьное имя из перечисления
    // public static String[] GetNames(Type enumType); // Определен в System.Enum
    // public String[] GetEnumNames(); // Определен в System.Type
    // Преобразование идентификатора в экземпляр перечислимого типа легко реализуется статическими методами Parse и TryParse типа Enum:
    // public static Object Parse(Type enumType, String value);
    // public static Object Parse(Type enumType, String value, Boolean ignoreCase);
    // public static Boolean TryParse<TEnum>(String value, out TEnum result) where TEnum: struct;
    // public static Boolean TryParse<TEnum>(String value, Boolean ignoreCase, out TEnum result) where TEnum : struct;
    // Наконец, рассмотрим статический метод IsDefined типа Enum и метод IsEnumDefined типа Type:
    // public static Boolean IsDefined(Type enumType, Object value); // Определен в System.Enum
    // public Boolean IsEnumDefined(Object value); // Определен в System.Type
    // Метод IsDefined часто используется для проверки параметров.
    // Внимание
    // При всем удобстве метода IsDefined применять его следует с осторожностью.
    // Вопервых, он всегда выполняет поиск с учетом регистра, во-вторых, работает крайне медленно, так как в нем используется отражение.
    // Самостоятельно написав код проверки возможных значений, вы повысите производительность своего приложения.
    // Кроме того, метод работает только для перечислимых типов, определенных в той сборке, из которой он вызывается. 
    // Напоследок упомянем набор статических методов ToObject типа System.Enum, преобразующих экземпляры типа Byte, SByte, Int16, UInt16, Int32, UInt32, Int64 или UInt64 в экземпляры перечислимого типа.

    // Битовые флаги
    // ПримечАние
    // В классе Enum имеется метод HasFlag, определяемый следующим образом:
    // public Boolean HasFlag(Enum flag);
    // С его помощью можно переписать вызов метода ConsoleWriteLine:
    // Console.WriteLine("Is {0} hidden? {1}", file, attributes.HasFlag(FileAttributes.Hidden));
    // Однако я не рекомендую использовать метод HasFlag. Дело в том, что он принимает параметры типа Enum, а значит, передаваемые ему значения должны быть упакованы, что требует дополнительных затрат памяти.
    // Определяя перечислимый тип, предназначенный для идентификации битовых флагов, каждому идентификатору следует явно присвоить числовое значение.
    // Настоятельно рекомендуется применять к перечислимому типу специализированный атрибут типа System.FlagsAttribute.
    // В предыдущем разделе мы рассмотрели метод ToString и привели три способа форматирования выходной строки: "G" (общий), "D" (десятичный) и "X" (шестнадцатеричный).
    // Форматируя экземпляр перечислимого типа с использованием общего формата, метод сначала определяет наличие атрибута [Flags].
    // Если атрибут не указан, отыскивается и возвращается идентификатор, соответствующий данному числовому значению.
    // Обнаружив же данный атрибут, ToString действует по следующему алгоритму:
    // 1. Получает набор числовых значений, определенных в перечислении, и сортирует их в нисходящем порядке.
    // 2. Для каждого значения выполняется операция конъюнкции (AND) с экземпляром перечисления.
    // В случае равенства результата числовому значению связанная с ним строка добавляется в итоговую строку, соответствующие же биты считаются учтенными и сбрасываются.
    // Операция повторяется до завершения проверки всех числовых значений или до сброса все битов экземпляров перечисления.
    // 3. Если после проверки всех числовых значений экземпляр перечисления все еще не равен нулю, это означает наличие несброшенных битов, которым не сопоставлены идентификаторы.
    // В этом случае метод возвращает исходное число экземпляра перечисления в виде строки.
    // 4. Если исходное значение экземпляра перечисления не равно нулю, метод возвращает набор символов, разделенных запятой.
    // 5. Если исходным значением экземпляра перечисления был ноль, а в перечислимом типе есть идентификатор с таким значением, метод возвращает этот идентификатор.
    // 6. Если алгоритм доходит до данного шага, возвращается 0.
    // Чтобы получить правильную результирующую строку, тип Actions можно определить и без атрибута [Flags]. Для этого достаточно указать формат "F".
    // Однако вы можете также получить числовое значение строки, содержащей разделенные запятой идентификаторы, воспользовавшись статическим методом Parse типа Enum или методом TryParse.
    // При вызове методов Parse и TryParse выполняются следующие действия:
    // 1. Удаляются все пробелы в начале и конце строки.
    // 2. Если первым символом в строке является цифра, знак «плюс» (+) или знак «минус» (–), строка считается числом и возвращается экземпляр перечисления, числовое значение которого совпадает с числом, полученным в результате преобразования строки.
    // 3. Переданная строка разбивается на разделенные запятыми лексемы, и у каждой лексемы удаляются все пробелы в начале и конце.
    // 4. Выполняется поиск каждой строки лексемы среди идентификаторов перечисления.
    // Если символ найти не удается, метод Parse генерирует исключение System.ArgumentException, а метод TryParse возвращает значение false.
    // При обнаружении символа его числовое значение путем дизъюнкции (OR) присоединяется к результирующему значению, и метод переходит к анализу следующего символа.
    // 5. После обнаружения и проверки всех лексем результат возвращается программе.
    // Никогда не следует применять метод IsDefined с перечислимыми типами битовых флагов. Это не будет работать по двум причинам:
    // - Переданную ему строку метод не разбивает на отдельные лексемы, а ищет целиком, вместе с запятыми.
    // Однако в перечислении не может присутствовать идентификатор, содержащий запятые, а значит, результат поиска всегда будет нулевым.
    // После передачи ему числового значения метод ищет всего один символ перечислимого типа, значение которого совпадает с переданным числом.
    // Для битовых флагов вероятность получения положительного результата при таком сравнении ничтожно мала, и обычно метод возвращает значение false.

    // Добавление методов к перечислимым типам
    // В начале главы уже упоминалось, что определить метод как часть перечислимого типа невозможно.
    // К счастью, теперь его можно обойти при помощи относительно нового для C# механизма методов расширения (extension method).
    // 
}
