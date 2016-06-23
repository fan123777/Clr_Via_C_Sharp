using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Reflection;

namespace CLR_VIA_C_SHARP._3_BaseDataTypes._18_CustomAttributes
{
    [assembly: CLSCompliant(true)]

        [Serializable]
        [DefaultMemberAttribute("Main")]
        [DebuggerDisplayAttribute("Richter", Name = "Jeff", Target = typeof(Program))]
    class CustomAttributes
    {
        [Conditional("Debug")]
        [Conditional("Release")]
        public void DoSomething()
        {

        }

        public CustomAttributes()
        {

        }

        [CLSCompliant(true)]
        [STAThread]
        public static void main()
        {
            CustomAttributes c = new CustomAttributes();

            // Вывод набора атрибутов, примененных к типу
            ShowAttributes(typeof(Program));
            // Получение и задание методов, связанных с типом
            var members =
            from m in typeof(Program).GetTypeInfo().DeclaredMembers.OfType<MethodBase>()
            where m.IsPublic
            select m;
            foreach (MemberInfo member in members)
            {
                // Вывод набора атрибутов, примененных к члену
                ShowAttributes(member);
            }

            Program.MyMain();
        }

        private static void ShowAttributes(MemberInfo attributeTarget)
        {
            var attributes = attributeTarget.GetCustomAttributes<Attribute>();
            Console.WriteLine("Attributes applied to {0}: {1}",
            attributeTarget.Name, (attributes.Count() == 0 ? "None" : String.Empty));
            foreach (Attribute attribute in attributes)
            {
                // Вывод типа всех примененных атрибутов
                Console.WriteLine(" {0}", attribute.GetType().ToString());
                if (attribute is DefaultMemberAttribute)
                    Console.WriteLine(" MemberName={0}",
                    ((DefaultMemberAttribute)attribute).MemberName);
                if (attribute is ConditionalAttribute)
                    Console.WriteLine(" ConditionString={0}",
                ((ConditionalAttribute)attribute).ConditionString);
                if (attribute is CLSCompliantAttribute)
                    Console.WriteLine(" IsCompliant={0}",
                    ((CLSCompliantAttribute)attribute).IsCompliant);
                DebuggerDisplayAttribute dda = attribute as DebuggerDisplayAttribute;
                if (dda != null)
                {
                    Console.WriteLine(" Value={0}, Name={1}, Target={2}",
                    dda.Value, dda.Name, dda.Target);
                }
            }
            Console.WriteLine();
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal sealed class OSVERSIONINFO
    {
        public OSVERSIONINFO()
        {
            OSVersionInfoSize = (UInt32)Marshal.SizeOf(this);
        }
        public UInt32 OSVersionInfoSize = 0;
        public UInt32 MajorVersion = 0;
        public UInt32 MinorVersion = 0;
        public UInt32 BuildNumber = 0;
        public UInt32 PlatformId = 0;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public String CSDVersion = null;
    }

    internal sealed class MyClass
    {
        [DllImport("Kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern Boolean GetVersionEx([In, Out] OSVERSIONINFO ver);
    }

    [AttributeUsage(AttributeTargets.Enum, Inherited = false)]
    public class FlagsAttribute : System.Attribute
    {
        public FlagsAttribute()
        {

        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true)]
    internal class TastyAttribute : Attribute
    {
    }

    [Tasty]
    [Serializable]
    internal class BaseType
    {
        [Tasty]
        protected virtual void DoSomething() { }
    }

    internal class DerivedType : BaseType
    {
        protected override void DoSomething() { }
    }

    internal enum Color
    {
        Red
    }

    [AttributeUsage(AttributeTargets.All)]
    internal sealed class SomeAttribute : Attribute
    {
        public SomeAttribute(String name, Object o, Type[] types)
        {
            // 'name' ссылается на String
            // 'o' ссылается на один из легальных типов (упаковка при необходимости)
            // 'types' ссылается на одномерный массив Types
            // с нулевой нижней границей
        }
    }

    [Some("Jeff", Color.Red, new Type[] { typeof(Math), typeof(Console) })]
    internal sealed class SomeType
    {

    }

    [Flags]
    internal enum Accounts
    {
        Savings = 0x0001,
        Checking = 0x0002,
        Brokerage = 0x0004
    }

    [AttributeUsage(AttributeTargets.Class)]
    internal sealed class AccountsAttribute : Attribute
    {
        private Accounts m_accounts;

        public AccountsAttribute(Accounts accounts)
        {
            m_accounts = accounts;
        }

        public override Boolean Match(Object obj)
        {
            // Если в базовом классе реализован метод Match и это не
            // класс Attribute, раскомментируйте следующую строку
            // if (!base.Match(obj)) return false;
            // Так как 'this' не равен null, если obj равен null,
            // объекты не совпадают
            // ПРИМЕЧАНИЕ. Эту строку можно удалить, если вы считаете,
            // что базовый тип корректно реализует метод Match
            if (obj == null) return false;
            // Объекты разных типов не могут быть равны
            // ПРИМЕЧАНИЕ. Эту строку можно удалить, если вы считаете,
            // что базовый тип корректно реализует метод Match
            if (this.GetType() != obj.GetType()) return false;
            // Приведение obj к нашему типу для доступа к полям
            // ПРИМЕЧАНИЕ. Это приведение всегда работает,
            // так как объекты принадлежат к одному типу
            AccountsAttribute other = (AccountsAttribute)obj;
            // Сравнение полей
            // Проверка, является ли accounts 'this' подмножеством
            // accounts объекта others
            if ((other.m_accounts & m_accounts) != m_accounts)
                return false;
            return true; // Объекты совпадают
        }

        public override Boolean Equals(Object obj)
        {
            // Если в базовом классе реализован метод Equals и это
            // не класс Object, раскомментируйте следующую строку
            // if (!base.Equals(obj)) return false;
            // Так как 'this' не равен null, при obj равном null
            // объекты не совпадают
            // ПРИМЕЧАНИЕ. Эту строку можно удалить, если вы считаете,
            // что базовый тип корректно реализует метод Equals
            if (obj == null) return false;
            // Объекты разных типов не могут совпасть
            // ПРИМЕЧАНИЕ. Эту строку можно удалить, если вы считаете,
            // что базовый тип корректно реализует метод Equals
            if (this.GetType() != obj.GetType()) return false;
            // Приведение obj к нашему типу для получения доступа к полям
            // ПРИМЕЧАНИЕ. Это приведение работает всегда,
            // так как объекты принадлежат к одному типу
            AccountsAttribute other = (AccountsAttribute)obj;
            // Сравнение значений полей 'this' и other
            if (other.m_accounts != m_accounts)
                return false;
            return true; // Объекты совпадают
        }

        // Переопределяем GetHashCode, так как Equals уже переопределен
        public override Int32 GetHashCode()
        {
            return (Int32)m_accounts;
        }
    }

    [Accounts(Accounts.Savings)]
    internal sealed class ChildAccount { }
    [Accounts(Accounts.Savings | Accounts.Checking | Accounts.Brokerage)]
    internal sealed class AdultAccount { }

    public sealed class Program
    {
        public static void MyMain()
        {
            CanWriteCheck(new ChildAccount());
            CanWriteCheck(new AdultAccount());
            // Просто для демонстрации корректности работы метода для
            // типа, к которому не был применен атрибут AccountsAttribute
            CanWriteCheck(new Program());
        }

        private static void CanWriteCheck(Object obj)
        {
            // Создание и инициализация экземпляра типа атрибута
            Attribute checking = new AccountsAttribute(Accounts.Checking);
            // Создание экземпляра атрибута, примененного к типу
            Attribute validAccounts =
            obj.GetType().GetCustomAttribute<AccountsAttribute>(false);
            // Если атрибут применен к типу и указывает на счет "Checking",
            // значит, тип может выписывать чеки
            if ((validAccounts != null) && checking.Match(validAccounts))
            {
                Console.WriteLine("{0} types can write checks.", obj.GetType());
            }
            else
            {
                Console.WriteLine("{0} types can NOT write checks.", obj.GetType());
            }
        }
    }

    // В этой главе описывается один из самых новаторских механизмов Microsoft .NET Framework — механизм настраиваемых атрибутов (custom attributes).
    // Именно настраиваемые атрибуты позволяют снабжать кодовые конструкции декларативными аннотациями, наделяя код особыми возможностями.
    
    // Сфера применения настраиваемых атрибутов
    // Атрибуты public, private, static и им подобные применяются как к типам, так и к членам типов.
    // Никто не станет спорить с тем, что атрибуты полезны — но как насчет возможности задания собственных атрибутов?
    // Определять и задействовать настраиваемые атрибуты может кто угодно, а все CLR-совместимые компиляторы должны их распознавать и генерировать соответствующие метаданные.
    // Библиотека классов .NET Framework (FCL) включает определения сотен настраиваемых атрибутов, которые вы можете использовать в своем коде.
    // Вот несколько примеров:
    // - Атрибут DllImport при применении к методу информирует CLR о том, что метод реализован в неуправляемом коде указанной DLL-библиотеки.
    // - Атрибут Serializable при применении к типу информирует механизмы сериализации о том, что экземплярные поля доступны для сериализации и десериализации.
    // - Атрибут AssemblyVersion при применении к сборке задает версию сборки.
    // - Атрибут Flags при применении к перечислимому типу превращает перечислимый тип в набор битовых флагов.
    // CLR позволяет применять атрибуты ко всему, что может быть представлено метаданными.
    // Чаще всего они применяются к записям в следующих таблицах определений: TypeDef (классы, структуры, перечисления, интерфейсы и делегаты), MethodDef (конструкторы), ParamDef, FieldDef, PropertyDef, EventDef, AssemblyDef и ModuleDef.
    // В частности, C# позволяет применять настраиваемые атрибуты только к исходному коду, определяющему такие элементы, как сборки, модули, типы (класс, структура, перечисление, интерфейс, делегат), поля, методы (в том числе конструкторы), параметры методов, возвращаемые значения методов, свойства, события, параметры обобщенного типа.
    // Вы можете задать префикс, указывающий, к чему будет применен атрибут.
    // [assembly: SomeAttr] // Применяется к сборке
    // [module: SomeAttr] // Применяется к модулю
    // [type: SomeAttr] // Применяется к типу
    // internal sealed class SomeType<[typevar: SomeAttr] T>
    // { // Применяется
    //     // к переменной обобщенного типа
    //     [field: SomeAttr] // Применяется к полю
    //     public Int32 SomeField = 0;
    //     [return: SomeAttr] // Применяется к возвращаемому значению
    //     [method: SomeAttr] // Применяется к методу
    //     public Int32 SomeMethod(
    //     [param: SomeAttr] // Применяется к параметру
    // Int32 SomeParam) { return SomeParam; }
    //     [property: SomeAttr] // Применяется к свойству
    //     public String SomeProp
    //     {
    //         [method: SomeAttr] // Применяется к механизму доступа get
    //         get { return null; }
    //     }
    //     [event: SomeAttr] // Применяется к событиям
    //     [field: SomeAttr] // Применяется к полям, созданным компилятором
    //     [method: SomeAttr] // Применяется к созданным
    //     // компилятором методам add и remove
    //     public event EventHandler SomeEvent;
    // }
    // Теперь, когда вы знаете, как применять настраиваемые атрибуты, давайте разберемся, что они собой представляют.
    // Настраиваемый атрибут — это всего лишь экземпляр типа.
    // Для соответствия общеязыковой спецификации (CLS) он должен прямо или косвенно наследовать от абстрактного класса System.Attribute.
    // В C# допустимы только CLS-совместимые атрибуты.
    // ПримечАние
    // При определении атрибута компилятор позволяет опускать суффикс Attribute, что упрощает ввод кода и делает его более читабельным.
    // Я активно использую эту возможность в приводимых в книге примерах — например, пишу [DllImport(...)] вместо [DllImportAttribute(...)].
    // Как уже упоминалось, атрибуты являются экземплярами класса.
    // И этот класс должен иметь открытый конструктор для создания экземпляров.
    // А значит, синтаксис применения атрибутов аналогичен вызову конструктора.
    // [DllImport("Kernel32", CharSet = CharSet.Auto, SetLastError = true)]
    // А что с еще двумя «параметрами»?
    // Показанный особый синтаксис позволяет задавать любые открытые поля или свойства объекта DllImportAttribute после его создания.
    // В рассматриваемом примере при создании этого объекта его конструктору передается строка "Kernel32", а открытым экземплярным полям CharSet и SetLastError присваиваются значения CharSet.Auto и true соответственно.
    // Показанные далее строки приводят к одному и тому же результату и демонстрируют все возможные способы применения набора атрибутов:
    // [Serializable][Flags]
    // [Serializable, Flags]
    // [FlagsAttribute, SerializableAttribute]
    // [FlagsAttribute()][Serializable()]

    // Определение класса атрибутов
    // Обратите внимание, что класс FlagsAttribute наследует от класса Attribute; именно это делает его CLS-совместимым.
    // Вдобавок в имени класса присутствует суффикс Attribute.
    // Это соответствует стандарту именования, хотя и не является обязательным.
    // Наконец, все неабстрактные атрибуты должны содержать хотя бы один открытый конструктор.
    // Простейший конструктор FlagsAttribute не имеет параметров и не выполняет никаких действий.
    // Внимание
    // Атрибут следует рассматривать как логический контейнер состояния.
    // Иначе говоря, хотя атрибут и является классом, этот класс должен быть крайне простым.
    // Он должен содержать всего один открытый конструктор, принимающий обязательную (или позиционную) информацию о состоянии атрибута.
    // Также класс может содержать открытые поля/свойства, принимающие дополнительную (или именованную) информацию о состоянии атрибута.
    // В классе не должно быть открытых методов, событий или других членов.
    // В общем случае я не одобряю использование открытых полей. Атрибутов это тоже касается.
    // Лучше воспользоваться свойствами, так как они обеспечивают большую гибкость в случаях, когда требуется внести изменения в реализацию класса атрибутов.
    // Чтобы указать компилятору область действия атрибута, применим к классу атрибута экземпляр класса System.AttributeUsageAttribute:
    // Перечислимый тип System.AttributeTargets определяется в FCL так:
    // [Flags, Serializable]
    // public enum AttributeTargets
    // {
    //     Assembly = 0x0001,
    //     Module = 0x0002,
    //     Class = 0x0004,
    //     Struct = 0x0008,
    //     Enum = 0x0010,
    //     Constructor = 0x0020,
    //     Method = 0x0040,
    //     Property = 0x0080,
    //     Field = 0x0100,
    //     Event = 0x0200,
    //     Interface = 0x0400,
    //     Parameter = 0x0800,
    //     Delegate = 0x1000,
    //     ReturnValue = 0x2000,
    //     GenericParameter = 0x4000,
    //     All = Assembly | Module | Class | Struct | Enum |
    //     Constructor | Method | Property | Field | Event |
    //     Interface | Parameter | Delegate | ReturnValue |
    //     GenericParameter
    // }
    // Однако есть и атрибуты, многократное применение которых оправдано — в FCL это класс атрибутов ConditionalAttribute.
    // Для этого параметру AllowMultiple должно быть присвоено значение true.
    // В противном случае многократное применение атрибута невозможно.
    // Свойство Inherited класса AttributeUsageAttribute указывает, будет ли атрибут, применяемый к базовому классу, применяться также к производным классам и переопределенным методам.
    // Следует помнить, что в .NET Framework наследование атрибутов допустимо только для классов, методов, свойств, событий, полей, возвращаемых значений и параметров.
    // ПримечАние
    // Если при определении собственного класса атрибутов вы забудете применить атрибут AttributeUsage, компилятор и CLR будут рассматривать полученный результат как применимый к любым элементам, но только один раз.
    // Кроме того, он будет наследуемым. Именно такие значения по умолчанию имеют поля класса AttributeUsageAttribute.

    // Конструктор атрибута и типы данных полей и свойств
    // Определяя конструктор экземпляров класса атрибутов, а также поля и свойства, следует ограничиться небольшим набором типов данных.
    // Допустимы типы: Boolean, Char, Byte, SByte, Int16, UInt16, Int32, UInt32, Int64, UInt64, Single, Double, String, Type, Object и перечислимые типы.
    // Внимание
    // Настраиваемый атрибут лучше всего представлять себе как экземпляр класса, сериализованный в байтовый поток, находящийся в метаданных.
    // В период выполнения байты из метаданных десериализуются для конструирования экземпляра класса.
    // На самом деле компилятор генерирует информацию, необходимую для создания экземпляра класса атрибутов, и размещает ее в метаданных.
    // Каждый параметр конструктора записывается с однобайтным идентификатором, за которым следует его значение.
    // Завершив «сериализацию» параметров, компилятор генерирует значения для каждого указанного поля и свойства, записывая его имя, за которым следует однобайтный идентификатор типа и значение.
    // Для массивов сначала указывается количество элементов.

    // Выявление настраиваемых атрибутов
    // Код может анализироваться на наличие атрибутов при помощи технологии, называемой отражением (reflection).
    // public override String ToString() {
    // // Применяется ли к перечислимому типу экземпляр типа FlagsAttribute?
    // if (this.GetType().IsDefined(typeof(FlagsAttribute), false)) {
    // // Да; выполняем код, интерпретирующий значение как
    // // перечислимый тип с битовыми флагами
    // ...
    // } else {
    // // Нет; выполняем код, интерпретирующий значение как
    // // обычный перечислимый тип
    // ...
    // }
    // ...
    // }
    // То есть после определения собственных классов атрибутов нужно также написать код, проверяющий, существует ли экземпляр класса атрибута (для указанных элементов), и в зависимости от результата меняющий порядок выполнения программы.
    // Только в этом случае настраиваемый атрибут принесет пользу!
    // Проверить наличие атрибута в FCL можно разными способами.
    // Для объектов класса System.Type можно использовать метод IsDefined, как показано ранее.
    // Остановимся на методах класса System.Reflection.CustomAttributeExtensions. Именно он является базовым для CLS-совместимых атрибутов.
    // Методы класса System.Reflection.CustomAttributeExtensions, определяющие наличие в метаданных CLS-совместимых настраиваемых атрибутов.
    // Метод - Описание
    // IsDefined - Возвращает значение true при наличии хотя бы одного экземпляра указанного класса, производного от Attribute, связанного с целью. Работает быстро, так как не создает (не десериализует) никаких экземпляров класса атрибута
    // GetCustomAttributes - Возвращает массив, каждый элемент которого является экземпляром указанного класса атрибута. Каждый экземпляр создается (десериализуется) с использованием указанных при компиляции параметров, полей и свойств. Если цель не имеет экземпляров указанного класса атрибута, метод возвращает пустую коллекцию. Обычно метод используется с атрибутами, параметр AllowMultiple которых имеет значение true, или со списком всех примененных атрибутов
    // GetCustomAttribute - Возвращает экземпляр указанного класса атрибута. Каждый экземпляр создается (десериализуется) с использованием указанных при компиляции параметров, полей и свойств. Если цель не имеет экземпляров указанного класса атрибута, метод возвращает null. При наличии более чем одного экземпляра генерируется исключение System.Reflection.AmbiguousMatchException. Обычно метод работает с атрибутами, параметр AllowMultiple которых имеет значение false
    // В пространстве имен System.Reflection находятся классы, позволяющие анализировать содержимое метаданных модуля: Assembly, Module, ParameterInfo, MemberInfo, Type, MethodInfo, ConstructorInfo, FieldInfo, EventInfo, PropertyInfo и соответствующие им классы *Builder.
    // Все эти классы содержат методы IsDefined и GetCustomAttributes.
    // ПримечАние
    // Имейте в виду, что методы отражения, поддерживающие логический параметр inherit, реализуют только классы Attribute, Type и MethodInfo.
    // Все прочие методы отражения этот параметр игнорируют и иерархию наследования не проверяют.
    // Для проверки наличия унаследованного атрибута в событиях, свойствах, полях, конструкторах или параметрах используйте один из методов класса Attribute.
    
    // Сравнение экземпляров атрибута
    // Класс System.Attribute переопределяет метод Equals класса Object, заставляя его сравнивать типы объектов.
    // Если они не совпадают, метод возвращает значение false.
    // В случае же совпадения метод Equals использует отражения для сравнения полей двух атрибутов (вызывая метод Equals для каждого поля).
    
    // Выявление настраиваемых атрибутов без создания объектов, производных от Attribute
    // Для обнаружения атрибутов без выполнения кода класса атрибута применяется класс System.Reflection.CustomAttributeData.
    // В нем определен единственный статический метод GetCustomAttributes, позволяющий получить информацию о примененных атрибутах.
    // Этот метод имеет четыре перегруженные версии: одна принимает параметр типа Assembly, другая — Module, третья — ParameterInfo, последняя — MemberInfo.
    // Метод GetCustomAttributes класса CustomAttributeData работает как метод-фабрика: он возвращает набор объектов CustomAttributeData в объекте типа IList<CustomAttributeData>.
    // Каждому элементу этой коллекции соответствует один настраиваемый атрибут.
    // Для каждого объекта класса CustomAttributeData можно запросить предназначенные только для чтения свойства, определив в результате, каким способом мог бы быть сконструирован и инициализирован объект.
    
    // Условные атрибуты
    // Класс атрибута, к которому применен атрибут System.Diagnostics.ConditionalAttribute, называется классом условного атрибута (conditional attribute).
}