﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLR_VIA_C_SHARP._2_Type_Design._7_ConstantsAndFields
{
    class ConstantsAndFields
    {
        public static void main()
        {
            testConstant();
        }

        public static void testConstant()
        {
            Console.WriteLine("Max entries supported in list: " + SomeLibraryType.MaxEntriesInList);
        }
    }

    public sealed class SomeType
    {
        // Некоторые типы не являются элементарными, но С# допускает существование
        // константных переменных этих типов после присваивания значения null
        public const SomeType Empty = null;

        // Статическое неизменяемое поле. Его значение рассчитывается
        // и сохраняется в памяти при инициализации класса во время выполнения
        public static readonly Random s_random = new Random();

        // Статическое изменяемое поле
        private static Int32 s_numberOfWrites = 0;

        // Неизменяемое экземплярное поле
        public readonly String Pathname = "Untitled";

        // Изменяемое экземплярное поле
        private System.IO.FileStream m_fs;

        public SomeType(String pathname) {
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

    public sealed class SomeLibraryType
    {
        // ПРИМЕЧАНИЕ: C# не позволяет использовать для констант модификатор
        // static, поскольку всегда подразумевается, что константы являются
        // статическими
        public const Int32 MaxEntriesInList = 50;
        // Модификатор static необходим, чтобы поле
        // ассоциировалось с типом, а не экземпляром
        public static readonly Int32 MaxEntriesInListField = 50;
    }

    public sealed class АТуре
    {
        // InvalidChars всегда ссылается на один объект массива
        public static readonly Char[] InvalidChars = new Char[] { 'А', 'В', 'C' };
    }

    public sealed class AnotherType {
        public static void M()
        {
            // Следующие строки кода вполне корректны, компилируются
            // и успешно изменяют символы в массиве InvalidChars
            АТуре.InvalidChars[0] = 'X';
            АТуре.InvalidChars[1] = 'Y';
            АТуре.InvalidChars[2] = 'Z';
            // Следующая строка некорректна и не скомпилируется,
            // так как ссылка InvalidChars изменяться не может
            // АТуре.InvalidChars = new Char[] { 'X', 'Y', 'Z' };
        }
    }
    // Константы
    // Константа (constant) — это идентификатор, значение которого никогда не меняется. Значение, связанное с именем константы, должно определяться во время компиляции. Затем компилятор сохраняет значение константы в метаданных модуля. Это значит, что константы можно определять только для таких типов, которые компилятор считает примитивными. В C# следующие типы считаются примитивными и могут использоваться для определения констант: Boolean, Char, Byte, SByte, Int16, UInt16, Int32, UInt32, Int64, UInt64, Single, Double, Decimal и String.
    // Тем не менее C# позволяет определить константную переменную, не относящуюся к элементарному типу, если присвоить ей значение null
    // Так как значение констант никогда не меняется, константы всегда считаются частью типа. Иначе говоря, константы считаются статическими, а не экземплярными членами. Определение константы приводит в конечном итоге к созданию метаданных.
    // Если разработчик изменит значение константы MaxEntriesInList на 1000 и перестроит только DLL-сборку, это не повлияет на код самого приложения. Для того чтобы в приложении использовалось новое значение константы, его тоже необходимо перекомпилировать. Нельзя применять константы во время выполнения (а не во время компиляции), если модуль должен задействовать значение, определенное в другом модуле. В этом случае вместо констант следует использовать предназначенные только для чтения поля, о которых речь идет в следующем разделе.

    // Поля
    // Поле (field) — это член данных, который хранит экземпляр значимого типа или ссылку на ссылочный тип. В табл. 7.1 приведены модификаторы, применяемые по отношению к полям.
    // Модификаторы полей
    // Термин CLR - Термин C# - Описание
    // Static - static - Поле является частью состояния типа, а не объекта
    // Instance - (по умолчанию) - Поле связано с экземпляром типа, а не самим типом
    // InitOnly - readonly - Запись в поле разрешается только из кода конструктора
    // Volatile - volatile - Код, обращающийся к полю, не должен оптимизироваться компилятором, CLR или оборудованием с целью обеспечения безопасности потоков. Неустойчивыми (volatile) могут объявляться только следующие типы: все ссылочные типы, Single, Boolean, Byte, SByte, Int16, UInt16, Int32, UInt32, Char, а также все перечислимые типы, основанные на типе Byte, SByte, Int16, UInt16, Int32 или UInt32.
    // CLR поддерживает поля, предназначенные для чтения и записи (изменяемые), а также поля, предназначенные только для чтения (неизменяемые). Большинство полей изменяемые. Это значит, что во время исполнения кода значение таких полей может многократно меняться. Данные же в неизменяемые поля можно записывать только при исполнении конструктора (который вызывается лишь раз — при создании объекта).
    // Теперь при исполнении метода Main этого приложения CLR загружает DLL-сборку (так как она требуется во время выполнения) и извлекает значение поля MaxEntriesInList из динамической памяти, выделенной для его хранения. Естественно, это значение равно 50.
    // Внимание
    // Неизменность поля ссылочного типа означает неизменность ссылки, которую этот
    // тип содержит, а вовсе не объекта, на которую указывает ссылка.
    

    // !!!
}