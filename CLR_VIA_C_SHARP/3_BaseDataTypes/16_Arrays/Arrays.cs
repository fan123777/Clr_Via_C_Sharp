﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CLR_VIA_C_SHARP._3_BaseDataTypes._16_Arrays
{
    class Arrays
    {
        public static void main()
        {
            Arrays a = new Arrays();
            a.testArray();
            a.testArrayInit();
            a.testCastArray();
            a.testCopyArray();
            a.testCovariance();
            a.testDecimalArray();
            a.testInternalArrayRealization();
            a.testAccessArray();
        }

        private void testArray()
        {
            Int32[] myIntegers; // Объявление ссылки на массив, myIntegers = null
            myIntegers = new Int32[100]; // Создание массива типа Int32 из 100 элементов, равных 0

            Employee[] myEmployees; // Объявление ссылки на массивб myEmployees = null
            myEmployees = new Employee[50]; // Создание массива из 50 ссылок на переменную Control, равных null

            // Создание двухмерного массива типа Doubles
            Double[,] myDoubles = new Double[10, 20];

            // Создание трехмерного массива ссылок на строки
            String[,,] myStrings = new String[5, 3, 10];

            // Создание одномерного массива из массивов типа Point
            Point[][] myPolygons = new Point[3][];

            // myPolygons[0] ссылается на массив из 10 экземпляров типа Point
            myPolygons[0] = new Point[10];

            // myPolygons[1] ссылается на массив из 20 экземпляров типа Point
            myPolygons[1] = new Point[20];

            // myPolygons[2] ссылается на массив из 30 экземпляров типа Point
            myPolygons[2] = new Point[30];

            // вывод точек первого многоугольника
            // for (Int32 x = 0; x < myPolygons[0].Length; x++)
            //    Console.WriteLine(myPolygons[0][x]);
        }

        private void testArrayInit()
        {
            String[] names = new String[] { "Aidan", "Grant" };

            // Использование локальной переменной неявного типа:
            var myNames = new String[] { "Aidan", "Grant" };

            // Задание типа массива с помощью локальной переменной неявного типа:
            var myNames1 = new[] { "Aidan", "Grant", null };

            String[] myNames2 = { "Aidan", "Grant" };

            // Применение переменных и массивов неявно заданного типа,
            // а также анонимного типа:
            var kids = new[] { new { Name = "Aidan" }, new { Name = "Grant" } };

            // Пример применения (с другой локальной переменной неявно заданного типа):
            foreach (var kid in kids)
                Console.WriteLine(kid.Name);
        }

        private void testCastArray()
        {
            // Создание двухмерного массива FileStream
            FileStream[,] fs2dim = new FileStream[5, 10];

            // Неявное приведение к массиву типа Object
            Object[,] o2dim = fs2dim;

            // Невозможно приведение двухмерного массива к одномерному
            // Ошибка компиляции CS0030: невозможно преобразовать тип 'object[*,*]'в 'System.IO.Stream[]'
            // Stream[] s1dim = (Stream[])o2dim;

            // Явное приведение к двухмерному массиву Stream
            Stream[,] s2dim = (Stream[,])o2dim;

            // Явное приведение к двухмерному массиву String
            // Компилируется, но во время выполнения
            // возникает исключение InvalidCastException
            // String[,] st2dim = (String[,])o2dim;

            // Создание одномерного массива Int32 (значимый тип)
            Int32[] i1dim = new Int32[5];

            // Невозможно приведение массива значимого типа
            // Ошибка компиляции CS0030: невозможно преобразовать тип 'int[]' в 'object[]'
            // Object[] o1dim = (Object[])i1dim;

            // Создание нового массива и приведение элементов к нужному типу
            // при помощи метода Array.Copy
            // Создаем массив ссылок на упакованные элементы типа Int32
            Object[] ob1dim = new Object[i1dim.Length];
            Array.Copy(i1dim, ob1dim, i1dim.Length); 
        }

        private void testCopyArray()
        {
            // Создание массива из 100 элементов значимого типа
            MyValueType[] src = new MyValueType[100];
            // Создание массива ссылок IComparable
            IComparable[] dest = new IComparable[src.Length];
            // Присваивание элементам массива IComparable ссылок на упакованные
            // версии элементов исходного массива
            Array.Copy(src, dest, src.Length);
        }

        private void testCovariance()
        {
            String[] sa = new String[100];
            Object[] oa = sa; // oa ссылается на массив элементов типа String
            oa[5] = "Jeff"; // CLR проверяет принадлежность oa к типу String; Проверка проходит успешно
            // oa[3] = 5; // CLR проверяет принадлежность oa к типу Int32; Генерируется исключение ArrayTypeMismatchException
        }

        private void testDecimalArray()
        {
            // Требуется двухмерный массив [2005..2009][1..4]
            Int32[] lowerBounds = { 2005, 1 };
            Int32[] lengths = { 5, 4 };
            Decimal[,] quarterlyRevenue = (Decimal[,])
            Array.CreateInstance(typeof(Decimal), lengths, lowerBounds);
            Console.WriteLine("{0,4} {1,9} {2,9} {3,9} {4,9}",
            "Year", "Q1", "Q2", "Q3", "Q4");
            Int32 firstYear = quarterlyRevenue.GetLowerBound(0);
            Int32 lastYear = quarterlyRevenue.GetUpperBound(0);
            Int32 firstQuarter = quarterlyRevenue.GetLowerBound(1);
            Int32 lastQuarter = quarterlyRevenue.GetUpperBound(1);
            for (Int32 year = firstYear; year <= lastYear; year++)
            {
                Console.Write(year + " ");
                for (Int32 quarter = firstQuarter;
                quarter <= lastQuarter; quarter++)
                {
                    Console.Write("{0,9:C} ", quarterlyRevenue[year, quarter]);
                }
                Console.WriteLine();
            }
        }

        private void testInternalArrayRealization()
        {
            Array a;

            // Создание одномерного массива с нулевым
            // начальным индексом и без элементов
            a = new String[0];
            Console.WriteLine(a.GetType()); // "System.String[]"

            // Создание одномерного массива с нулевым
            // начальным индексом и без элементов
            a = Array.CreateInstance(typeof(String),
            new Int32[] { 0 }, new Int32[] { 0 });
            Console.WriteLine(a.GetType()); // "System.String[]"

            // Создание одномерного массива с начальным индексом 1 и без элементов
            a = Array.CreateInstance(typeof(String),
            new Int32[] { 0 }, new Int32[] { 1 });
            Console.WriteLine(a.GetType()); // "System.String[*]" <-- ВНИМАНИЕ!
            Console.WriteLine();

            // Создание двухмерного массива с нулевым
            // начальным индексом и без элементов
            a = new String[0, 0];
            Console.WriteLine(a.GetType()); // "System.String[,]"

            // Создание двухмерного массива с нулевым
            // начальным индексом и без элементов
            a = Array.CreateInstance(typeof(String),
            new Int32[] { 0, 0 }, new Int32[] { 0, 0 });
            Console.WriteLine(a.GetType()); // "System.String[,]"

            // Создание двухмерного массива с начальным индексом 1 и без элементов
            a = Array.CreateInstance(typeof(String),
            new Int32[] { 0, 0 }, new Int32[] { 1, 1 });
            Console.WriteLine(a.GetType()); // "System.String[,]"

            Int32[] a1 = new Int32[5];
            for (Int32 index = 0; index < a1.Length; index++)
            {
                // Какие-то действия с элементом a[index]
            }
        }

        private void testAccessArray()
        {

        }
    }

    public class Employee
    {

    }

    public class Point
    {

    }

    // Определение значимого типа, реализующего интерфейс
    internal struct MyValueType : IComparable
    {
        public Int32 CompareTo(Object obj)
        {
            return 0;
        }
    }

    // Общеязыковая исполняющая среда Microsoft .NET (CLR) поддерживает одномерные (single-dimension), многомерные (multidimension) и нерегулярные (jagged) массивы.
    // Базовым для всех массивов является абстрактный класс System.Array, производный от System.Object.
    // Значит, массивы всегда относятся к ссылочному типу и размещаются в управляемой куче, а переменная в приложении содержит не элементы массива, а ссылку на массив.
    // Согласно общеязыковой спецификации (CLS), нумерация элементов в массиве должна начинаться с нуля.
    // На рисунке видно, что в массиве присутствует некая дополнительная информация.
    // Это сведения о размерности массива, нижних границах всех его измерений (почти всегда 0) и количестве элементов в каждом измерении.
    // Здесь же указывается тип элементов массива.
    // По возможности нужно ограничиваться одномерными массивами с нулевым начальным индексом, которые называют иногда SZ-массивами, или векторами.
    // Векторы обеспечивают наилучшую производительность, поскольку для операций с ними используются команды промежуточного языка (Intermediate Language, IL), например newarr, ldelem, ldelema, ldlen и stelem.
    // CLR поддерживает также нерегулярные (jagged) массивы — то есть «массивы массивов».
    // ПримечАние
    // CLR проверяет корректность индексов.
    // То есть если у вас имеется массив, состоящий из 100 элементов с индексами от 0 до 99, попытка обратиться к его элементу по индексу –5 или 100 породит исключение System.Index.OutOfRange.
    // Доступ к памяти за пределами массива нарушает безопасность типов и создает брешь в защите, недопустимую для верифицированного CLR-кода.
    // Проверка индекса обычно не влияет на производительность, так как компилятор выполняет ее всего один раз перед началом цикла, а не на каждой итерации. Впрочем, если вы считаете, что проверка индексов критична для скорости выполнения вашей программы, используйте для доступа к массиву небезопасный код.
    
    // Инициализация элементов массива
    // Набор разделенных запятой символов в фигурных скобках называется инициализатором массива (array initializer).
    
    // Приведение типов в массивах
    // В CLR для массивов с элементами ссылочного типа допустимо приведение.
    // В рамках решения этой задачи оба типа массивов должны иметь одинаковую размерность; кроме того, должно иметь место неявное или явное преобразование из типа элементов исходного массива в целевой тип. CLR не поддерживает преобразование массивов с элементами значимых типов в другие типы.
    // Метод Array.Copy не просто копирует элементы одного массива в другой.
    // Он действует как функция memmove языка C, но при этом правильно обрабатывает перекрывающиеся области памяти.
    // Он также способен при необходимости преобразовывать элементы массива в процессе их копирования.
    // Метод Copy выполняет следующие действия:
    // - Упаковка элементов значимого типа в элементы ссылочного типа, например копирование Int32[] в Object[].
    // - Распаковка элементов ссылочного типа в элементы значимого типа, например копирование Object[] в Int32[].
    // - Расширение (widening) примитивных значимых типов, например копирование Int32[] в Double[].
    // - Понижающее приведение в случаях, когда совместимость массивов невозможно определить по их типам.
    // Сюда относится, к примеру, приведение массива типа Object[] в массив типа IFormattable[].
    // Если все объекты в массиве Object[] реализуют интерфейс IFormattable[], приведение пройдет успешно.
    // Бывают ситуации, когда полезно изменить тип массива, то есть выполнить его ковариацию (array covariance).
    // Однако следует помнить, что эта операция сказывается на производительности. Допустим, вы написали такой код:
    // ПримечАние
    // Для простого копирования части элементов из одного массива в другой имеет смысл использовать метод BlockCopy класса System.Buffer, который работает быстрее метода Array.Copy.
    // К сожалению, этот метод поддерживает только примитивные типы и не имеет таких же широких возможностей приведения, как Array.Copy. 
    // Для надежного копирования набора элементов из одного массива в другой используйте метод ConstrainedCopy класса System.Array.
    // Он гарантирует, что в случае неудачного копирования будет выдано исключение, но данные в целевом массиве останутся неповрежденными. 

    // Базовый класс System.Array
    // FileStream[] fsArray;
    // Объявление переменной массива подобным образом приводит к автоматическому созданию типа FileStream[] для домена приложений.
    // Тип FileStream[] является производным от System.Array и соответственно наследует оттуда все методы и свойства.
    // Для их вызова служит переменная fsArray.
    // Это упрощает работу с массивами, ведь в классе System.Array есть множество полезных методов и свойств, в том числе Clone, CopyTo, GetLength, GetLongLength, GetLowerBound, GetUpperBound, Length и Rank.
    // Класс System.Array содержит также статические методы для работы с массивами, в том числе AsReadOnly, BinarySearch, Clear, ConstrainedCopy, ConvertAll, Copy, Exists, Find, FindAll, FindIndex, FindLast, FindLastIndex, ForEach, IndexOf, LastIndexOf, Resize, Reverse, Sort и TrueForAll.

    // Реализация интерфейсов IEnumerable, ICollection и IList
    // Многие методы работают с коллекциями, поскольку они объявлены с такими параметрами, как интерфейсы IEnumerable, ICollection и IList.
    // Им можно передавать и массивы, так как эти три необобщенных интерфейса реализованы в классе System.Array.
    // Команда разработчиков CLR решила, что не стоит осуществлять реализацию интерфейсов IEnumerable<T>, ICollection<T> и IList<T> классом System.Array, так как в этом случае возникают проблемы с многомерными массивами, а также с массивами, в которых нумерация не начинается с нуля.
    // Вместо этого разработчики пошли на хитрость: при создании одномерного массива с начинающейся с нуля индексацией CLR автоматически реализует интерфейсы IEnumerable<T>, ICollection<T> и IList<T> (здесь T — тип элементов массива), а также три интерфейса для всех базовых типов массива при условии, что эти типы являются ссылочными.
    // FileStream[] fsArray;
    // В этом случае при создании типа FileStream[] CLR автоматически реализует в нем интерфейсы IEnumerable<FileStream>, ICollection<FileStream> и IList<FileStream>.
    // Более того, тип FileStream[] будет реализовывать интерфейсы базовых классов IEnumerable<Stream>, IEnumerable<Object>, ICollection<Stream>, ICollection<Object>, IList<Stream> и IList<Object>.
    // ее можно передавать в методы с такими прототипами:
    // void M1(IList<FileStream> fsList) { ... }
    // void M2(ICollection<Stream> sCollection) { ... }
    // void M3(IEnumerable<Object> oEnumerable) { ... }
    // Обратите внимание, что если массив содержит элементы значимого типа, класс, которому он принадлежит, не будет реализовывать интерфейсы базовых классов элемента.
    // DateTime[] dtArray; // Массив элементов значимого типа
    // В данном случае тип DateTime[] будет реализовывать только интерфейсы IEnumerable<DateTime>, ICollection<DateTime> и IList<DateTime>;

    // Передача и возврат массивов
    // Передавая массив в метод в качестве аргумента, вы на самом деле передаете ссылку на него.
    // А значит, метод может модифицировать элементы массива.
    // Этого можно избежать, передав в качестве аргумента копию массива.
    // Имейте в виду, что метод Array.Copy выполняет поверхностное (shallow) копирование, и если элементы массива относятся к ссылочному типу, в новом массиве окажутся ссылки на существующие объекты.
    // Результатом вызова метода, возвращающего ссылку на массив, не содержащий элементов, является либо значение null, либо ссылка на массив с нулевым числом элементов.
    
    // Массивы с ненулевой нижней границей
    // Как уже упоминалось, массивы с ненулевой нижней границей вполне допустимы.
    // Создавать их можно при помощи статического метода CreateInstance типа Array.
    // Для доступа к элементам одномерных массивов пользуйтесь методами GetValue и SetValue класса Array.
    
    // Внутренняя реализация массивов
    // В CLR поддерживаются массивы двух типов:
    // Одномерные массивы с нулевым начальным индексом.
    // Иногда их называют SZ-массивами (от английского single-dimensional, zero-based), или векторами.
    // Одномерные и многомерные массивы с неизвестным начальным индексом.
    // Доступ к элементам одномерного массива с нулевой нижней границей осуществляется немного быстрее, чем доступ к элементам многомерных массивов или массивов с ненулевой нижней границей.
    // Обратите внимание на вызов свойства Length в проверочном выражении цикла for.
    // Фактически при этом вызывается метод, но JIT-компилятор «знает», что Length является свойством класса Array, поэтому создает код, в котором метод вызывается всего один раз, а полученный результат сохраняется в промежуточной переменной.
    // Кроме того, JIT-компилятор «знает», что цикл обращается к элементам массива с нулевой нижней границей, указывая Length - 1.
    // Поэтому он в процессе выполнения генерирует код, проверяющий, все ли элементы находятся в границах массива.
    // К сожалению, обращение к элементам многомерного массива или массива с ненулевой нижней границей происходит намного медленней.
    // Ведь в этих случаях код проверки индекса не выносится за пределы цикла и проверка осуществляется на каждой итерации.
    // Если вы серьезно озабочены проблемой производительности, имеет смысл использовать нерегулярные массивы (массивы массивов).
    // Кроме того, в C# и CLR возможен доступ к элементам массива при помощи небезопасного (неверифицируемого) кода.
    // В этом случае процедура проверки индексов массива просто отключается.
    // Данная техника применима только к массивам типа SByte, Byte, Int16, UInt16, Int32, UInt32, Int64, UInt64, Char, Single, Double, Decimal, Boolean, а также к массивам перечислимого типа или структуры значимого типа с полями одного из вышеуказанных типов.
    // 430




}
