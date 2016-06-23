using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Threading;

namespace CLR_VIA_C_SHARP._3_BaseDataTypes._17_Delegates
{
    // Несколько разных определений делегатов
    internal delegate Object TwoInt32s(Int32 n1, Int32 n2);
    internal delegate Object OneString(String s1);

    class Delegates
    {
        public static void main()
        {
            Delegates d = new Delegates();
            d.testDelegate();
            d.testDelegateChain();
            d.testLambda();
        }

        private void testDelegate()
        {
            StaticDelegateDemo();
            InstanceDelegateDemo();
            ChainDelegateDemo1(new Delegates());
            ChainDelegateDemo2(new Delegates());
        }

        private void testDelegateChain()
        {
            // Объявление пустой цепочки делегатов
            GetStatus getStatus = null;
            // Создание трех компонентов и добавление в цепочку
            // методов проверки их состояния
            getStatus += new GetStatus(new Light().SwitchPosition);
            getStatus += new GetStatus(new Fan().Speed);

            getStatus += new GetStatus(new Speaker().Volume);
            // Сводный отчет о состоянии трех компонентов
            Console.WriteLine(GetComponentStatusReport(getStatus));
        }

        private void testLambda()
        {
            // Если делегат не содержит аргументов, используйте круглые скобки
            Func<String> f = () => "Jeff";

            // Для делегатов с одним и более аргументами можно в явном виде указать типы
            Func<Int32, String> f2 = (Int32 n) => n.ToString();
            Func<Int32, Int32, String> f3 = (Int32 n1, Int32 n2) => (n1 + n2).ToString();

            // Компилятор может самостоятельно определить типы для делегатов с одним и более аргументами
            Func<Int32, String> f4 = (n) => n.ToString();
            Func<Int32, Int32, String> f5 = (n1, n2) => (n1 + n2).ToString();

            // Если аргумент у делегата всего один, круглые скобки можно опустить
            Func<Int32, String> f6 = n => n.ToString();

            // Для аргументов ref/out нужно в явном виде указывать ref/out и тип
            Bar b = (out Int32 n) => n = 5;

            // Чтобы вставить в тело функции несколько инструкций, заключите их в фигурные скобки.
            // Если делегат ожидает получить возвращаемое значение, не забудьте инструкцию return, как показано в следующем примере:
            Func<Int32, Int32, String> f7 = (n1, n2) =>
            {
                Int32 sum = n1 + n2;
                return sum.ToString();
            };

            // Создание и инициализация массива String
            String[] names = { "Jeff", "Kristin", "Aidan", "Grant" };
            // Извлечение имен со строчной буквой 'a'
            Char charToFind = 'a';
            names = Array.FindAll(names, name => name.IndexOf(charToFind) >= 0);
            // Преобразование всех символов строки в верхний регистр
            names = Array.ConvertAll(names, name => name.ToUpper());
            // Вывод результатов
            Array.ForEach(names, Console.WriteLine);
        }

        // Объявление делегата; экземпляр ссылается на метод
        // с параметром типа Int32, возвращающий значение void
        internal delegate void Feedback(Int32 value);

        private static void StaticDelegateDemo()
        {
            Console.WriteLine("----- Static Delegate Demo -----");
            Counter(1, 3, null);
            Counter(1, 3, new Feedback(Delegates.FeedbackToConsole));
            Counter(1, 3, new Feedback(FeedbackToMsgBox)); // Префикс "Program. не обязателен
            Console.WriteLine();
        }

        private static void InstanceDelegateDemo()
        {
            Console.WriteLine("----- Instance Delegate Demo -----");
            Delegates p = new Delegates();
            Counter(1, 3, new Feedback(p.FeedbackToFile));
            Console.WriteLine();
        }

        private static void ChainDelegateDemo1(Delegates p)
        {
            Console.WriteLine("----- Chain Delegate Demo 1 -----");
            Feedback fb1 = new Feedback(FeedbackToConsole);
            Feedback fb2 = new Feedback(FeedbackToMsgBox);
            Feedback fb3 = new Feedback(p.FeedbackToFile);
            Feedback fbChain = null;
            fbChain = (Feedback)Delegate.Combine(fbChain, fb1);
            fbChain = (Feedback)Delegate.Combine(fbChain, fb2);
            fbChain = (Feedback)Delegate.Combine(fbChain, fb3);
            Counter(1, 2, fbChain);
            Console.WriteLine();
            fbChain = (Feedback)Delegate.Remove(fbChain, new Feedback(FeedbackToMsgBox));
            Counter(1, 2, fbChain);
        }

        private static void ChainDelegateDemo2(Delegates p)
        {
            Console.WriteLine("----- Chain Delegate Demo 2 -----");
            Feedback fb1 = new Feedback(FeedbackToConsole);
            Feedback fb2 = new Feedback(FeedbackToMsgBox);
            Feedback fb3 = new Feedback(p.FeedbackToFile);
            Feedback fbChain = null;
            fbChain += fb1;
            fbChain += fb2;
            fbChain += fb3;
            Counter(1, 2, fbChain);
            Console.WriteLine();
            fbChain -= new Feedback(FeedbackToMsgBox);
            Counter(1, 2, fbChain);
        }

        private static void Counter(Int32 from, Int32 to, Feedback fb)
        {
            for (Int32 val = from; val <= to; val++)
            {
                // Если указаны методы обратного вызова, вызываем их
                if (fb != null)
                    fb(val);
            }
        }

        private static void FeedbackToConsole(Int32 value)
        {
            Console.WriteLine("Item=" + value);
        }

        private static void FeedbackToMsgBox(Int32 value)
        {
            Console.WriteLine("Item=" + value);
        }

        private void FeedbackToFile(Int32 value)
        {
            using (StreamWriter sw = new StreamWriter("Status", true))
            {
                sw.WriteLine("Item=" + value);
            }
        }

        // Определение делегатов, позволяющих запрашивать состояние компонентов
        private delegate String GetStatus();

        // Метод запрашивает состояние компонентов и возвращает информацию
        private static String GetComponentStatusReport(GetStatus status)
        {
            // Если цепочка пуста, ничего делать не нужно
            if (status == null) return null;
            // Построение отчета о состоянии
            StringBuilder report = new StringBuilder();
            // Создание массива из делегатов цепочки
            Delegate[] arrayOfDelegates = status.GetInvocationList();
            // Циклическая обработка делегатов массива
            foreach (GetStatus getStatus in arrayOfDelegates)
            {
                try
                {
                    // Получение строки состояния компонента и добавление ее в отчет
                    report.AppendFormat("{0}{1}{1}", getStatus(), Environment.NewLine);
                }
                catch (InvalidOperationException e)
                {
                    // В отчете генерируется запись об ошибке для этого компонента
                    Object component = getStatus.Target;
                    report.AppendFormat(
                    "Failed to get status from {1}{2}{0} Error: {3}{0}{0}",
                    Environment.NewLine,
                    ((component == null) ? "" : component.GetType() + "."),
                    getStatus.Method.Name,
                    e.Message);
                }
            }
            // Возвращение сводного отчета вызывающему коду
            return report.ToString();
        }

        delegate void Bar(out Int32 z);
    }

    // Определение компонента Light
    internal sealed class Light
    {
        // Метод возвращает состояние объекта Light
        public String SwitchPosition()
        {
            return "The light is off";
        }
    }

    // Определение компонента Fan
    internal sealed class Fan
    {
        // Метод возвращает состояние объекта Fan
        public String Speed()
        {
            throw new InvalidOperationException("The fan broke due to overheating");
        }
    }

    // Определение компонента Speaker
    internal sealed class Speaker
    {
        // Метод возвращает состояние объекта Speaker
        public String Volume()
        {
            return "The volume is loud";
        }
    }

    internal sealed class AClass
    {
        public static void UsingLocalVariablesInTheCallbackCode(Int32 numToDo)
        {
            // Локальные переменные
            Int32[] squares = new Int32[numToDo];
            AutoResetEvent done = new AutoResetEvent(false);
            // Выполнение задач в других потоках
            for (Int32 n = 0; n < squares.Length; n++)
            {
                ThreadPool.QueueUserWorkItem(
                obj =>
                {
                    Int32 num = (Int32)obj;
                    // Обычно решение этой задачи требует больше времени
                    squares[num] = num * num;
                    // Если это последняя задача, продолжаем выполнять главный поток
                    if (Interlocked.Decrement(ref numToDo) == 0)
                        done.Set();
                },
                n);
            }
            // Ожидаем завершения остальных потоков
            done.WaitOne();
            // Вывод результатов
            for (Int32 n = 0; n < squares.Length; n++)
                Console.WriteLine("Index {0}, Square={1}", n, squares[n]);
        }
    }

    // В этой главе рассказывается о чрезвычайно полезном механизме, который используется уже много лет и называется функциями обратного вызова.
    // В Microsoft .NET Framework этот механизм поддерживается при помощи делегатов (delegates).
    // Они обеспечивают возможность последовательного вызова нескольких методов, а также вызова как статических, так и экземплярных методов.

    // Знакомство с делегатами
    // В .NET Framework методы обратного вызова также имеют многочисленные применения. К примеру, можно зарегистрировать такой метод для получения различных уведомлений: о необработанных исключениях, изменении состояния окон, выборе пунктов меню, изменениях файловой системы и завершении асинхронных операций.

    // Обратный вызов статических методов
    // ПримечАние
    // Метод FeedbackToConsole определен в типе Program как закрытый, но при этом может быть вызван методом Counter.
    // Так как оба метода определены в пределах одного типа, проблем с безопасностью не возникает.
    // Но даже если бы метод Counter был определен в другом типе, это не сказалось бы на работе коде.
    // Другими словами, если код одного типа вызывает посредством делегата закрытый член другого типа, проблем с безопасностью или уровнем доступа не возникает, если делегат создан в коде, имеющем нужный уровень доступа.
    // Как C#, так и CLR поддерживают ковариантность и контравариантность ссылочных типов при привязке метода к делегату.
    // Ковариантность (covariance) означает, что метод может возвратить тип, производный от типа, возвращаемого делегатом.
    // Контравариантность (contra-variance) означает, что метод может принимать параметр, который является базовым для типа параметра делегата.
    // delegate Object MyCallback(FileStream s);
    // можно получить экземпляр этого делегата, связанный с методом, прототип которого выглядит примерно так
    // String SomeMethod(Stream s);
    // ковариантность и контравариантность поддерживаются только для ссылочных типов, но не для значимых типов или значения void.

    // Обратный вызов экземплярных методов

    // Тонкости использования делегатов
    // internal delegate void Feedback(Int32 value);
    // Заставляет компилятор создать полное определение класса.
    // internal class Feedback : System.MulticastDelegate
    // {
    //     // Конструктор
    //     public Feedback(Object object, IntPtr method);
    //     // Метод, прототип которого задан в исходном тексте
    //     public virtual void Invoke(Int32 value);
    //     // Методы, обеспечивающие асинхронный обратный вызов
    //     public virtual IAsyncResult BeginInvoke(Int32 value, AsyncCallback callback, Object object);
    //     public virtual void EndInvoke(IAsyncResult result);
    // }
    // Внимание
    // Класс System.MulticastDelegate является производным от класса System.Delegate, который, в свою очередь, наследует от класса System.Object.
    // Два класса делегатов появились исторически, в то время как в FCL предполагался только один.
    // Вам следует помнить об обоих классах, так как даже если выбрать в качестве базового класс MulticastDelegate, все равно иногда приходится работать с делегатами, использующими методы класса Delegate.
    // Скажем, именно этому классу принадлежат статические методы Combine и Remove (о том, зачем они нужны, мы поговорим чуть позже).
    // Сигнатуры этих методов указывают, что они принимают параметры класса Delegate.
    // Так как тип вашего делегата является производным от класса MulticastDelegate, для которого базовым является класс Delegate, методам можно передавать экземпляры типа делегата.
    // Важнейшие закрытые поля класса MulticastDelegate
    // Поле - Тип - Описание
    // _target - System.Object - Если делегат является оболочкой статического метода, это поле содержит значение null. Если делегат является оболочкой экземплярного метода, поле ссылается на объект, с которым будет работать метод обратного вызова. Другими словами, поле указывает на значение, которое следует передать параметру this экземплярного метода
    // _methodPtr - System.IntPtr - Внутреннее целочисленное значение, используемое CLR для идентификации метода обратного вызова
    // _invocationList - System.Object - Это поле обычно имеет значение null. Оно может ссылаться на массив делегатов при построении из них цепочки (об этом мы поговорим чуть позже)
    // Ссылка на объект передается в параметре object конструктора.
    // Специальное значение IntPtr (получаемое из маркеров метаданных MethodDef или MemberRef), идентифицирующее метод, передается в параметре method.
    // В случае статических методов параметр object передает значение null.
    // Внутри конструктора значения этих двух аргументов сохранятся в закрытых полях _target и _methodPtr соответственно.
    // Кроме того, конструктор присваивает значение null полю _invocationList.
    // Инструкция if сначала проверяет, не содержит ли переменная fb значения null.
    // Если проверка пройдена, обращаемся к методу обратного вызова.
    // Такая проверка необходима потому, что fb — это всего лишь переменная, ссылающаяся на делегат Feedback; она может иметь, в том числе, значение null.
    // fb(val);
    // компилятор генерирует такой же код, как и для строки:
    // fb.Invoke(val);
    // Обратите внимание, что сигнатура метода Invoke совпадает с сигнатурой делегата, ведь и делегат Feedback, и метод Invoke принимают один параметр типа Int32 и возвращают значение void.

    // Обратный вызов нескольких методов (цепочки делегатов)
    // Цепочкой (chaining) называется коллекция делегатов, дающая возможность вызывать все методы, представленные этими делегатами.
    // Открытый статический метод Combine класса Delegate добавляет в цепочку делегатов:
    // fbChain = (Feedback) Delegate.Combine(fbChain, fb1);
    // Когда метод Invoke вызывается для делегата, ссылающегося на переменную fbChain, этот делегат обнаруживает, что значение поля _invocationList отлично от null.
    // Это приводит к выполнению цикла, перебирающего все элементы массива, вызывая для них метод, оболочкой которого служит указанный делегат.
    // Реализация метода Invoke класса Feedback выглядит примерно так:
    // public void Invoke(Int32 value) {
    // Delegate[] delegateSet = _invocationList as Delegate[];
    // if (delegateSet != null) {
    // Этот массив указывает на делегаты, которые следует вызвать
    // foreach (Feedback d in delegateSet)
    // d(value); // Вызов каждого делегата
    // } else {
    // Этот делегат определяет используемый метод обратного вызова.
    // Этот метод вызывается для указанного объекта.
    // _methodPtr.Invoke(_target, value);
    // Строка выше — имитация реального кода.
    // То, что происходит в действительности, не выражается средствами C#.
    // }
    // }
    // Для удаления делегатов из цепочки применяется статический метод Remove объекта Delegate.
    // Эта процедура демонстрируется в конце кода метода ChainDelegateDemo1:
    // fbChain = (Feedback) Delegate.Remove(fbChain, new Feedback(FeedbackToMsgBox));
    
    // Поддержка цепочек делегатов в C#
    // Чтобы упростить задачу разработчиков, компилятор C# автоматически предоставляет перегруженные версии операторов += и -= для экземпляров делегатов.
    // Эти операторы вызывают методы Delegate.Combine и Delegate.Remove соответственно.
    // Они упрощают построение цепочек делегатов.
    
    // Дополнительные средства управления цепочками делегатов
    // В качестве альтернативы можно воспользоваться экземплярным методом GetInvocationList класса MulticastDelegate. Этот метод позволяет в явном виде вызвать любой из делегатов в цепочке:
    
    // Обобщенные делегаты
    // В .NET Framework имеются 17 делегатов Action, от не имеющих аргументов вообще до имеющих 16 аргументов.
    // Чтобы вызвать метод с большим количеством аргументов, придется определить собственного делегата, но это уже маловероятно.
    // public delegate void Action(); // Этот делегат не обобщенный
    // public delegate void Action<T>(T obj);
    // public delegate void Action<T1, T2>(T1 arg1, T2 arg2);
    // Кроме делегатов Action в .NET Framework имеется 17 делегатов Func, которые позволяют методу обратного вызова вернуть значение:
    // public delegate TResult Func<TResult>();
    // public delegate TResult Func<T, TResult>(T arg);
    // public delegate TResult Func<T1, T2, TResult>(T1 arg1, T2 arg2);
    // Вместо определения собственных типов делегатов рекомендуется по мере возможности использовать обобщенных делегатов; ведь это уменьшает количество типов в системе и упрощает код.
    // Однако, если нужно передать аргумент по ссылке, используя ключевые слова ref или out, может потребоваться определение собственного делегата:
    // delegate void Bar(ref Int32 z);
    
    // Упрощенный синтаксис работы с делегатами
    // Многие программисты не любят делегатов из-за сложного синтаксиса. К примеру, рассмотрим строку:
    // button1.Click += new EventHandler(button1_Click);
    // Здесь button1_Click — метод, который выглядит примерно так:
    // void button1_Click(Object sender, EventArgs e)
    // {
    // // Действия после щелчка на кнопке...
    // }

    // Тем не менее программисты не хотят вникать во все эти детали и предпочли бы записать код следующим образом:
    // button1.Click += button1_Click;
    
    // Упрощение 1: не создаем объект делегата
    // internal sealed class AClass {
    // public static void CallbackWithoutNewingADelegateObject() {
    // ThreadPool.QueueUserWorkItem(SomeAsyncTask, 5);
    // }
    // private static void SomeAsyncTask(Object o) {
    // Console.WriteLine(o);
    // }
    // }

    // Упрощение 2: не определяем метод обратного вызова
    // В приведенном фрагменте кода метод обратного вызова SomeAsyncTask передается методу QueueUserWorkItem класса ThreadPool.
    // C# позволяет подставить реализацию метода обратного вызова непосредственно в код, а не в отдельный метод.
    // Скажем, наш код можно записать так:
    // internal sealed class AClass
    // {
    //     public static void CallbackWithoutNewingADelegateObject()
    //     {
    //         ThreadPool.QueueUserWorkItem(obj => Console.WriteLine(obj), 5);
    //     }
    // }
    // Лямбда-выражения используются в тех местах, где компилятор ожидает присутствия делегата.
    // Обнаружив лямбда-выражение, компилятор автоматически определяет в классе новый закрытый метод (в нашем примере - AClass).
    // Этот метод называется анонимной функцией (anonymous function), так как вы обычно не знаете его имени, которое автоматически создается компилятором.
    // ПримечАние
    // При написании лямбда-выражений невозможно применить к сгенерированному компилятором методу пользовательские атрибуты или модификаторы (например, unsafe).
    // Впрочем, обычно это не является проблемой, так как созданные компилятором анонимные методы всегда закрыты.
    // Каждый такой метод является статическим или нестатическим в зависимости от того, будет ли он иметь доступ к каким-либо экземплярным членам.
    // Соответственно, применять к этим методам модификаторы public, protected, internal, virtual, sealed, override или abstract просто не требуется.
    // Написанный код компилятор C# переписывает примерно таким образом:
    // internal sealed class AClass
    // {
    // Это закрытое поле создано для кэширования делегата
    // Преимущество: CallbackWithoutNewingADelegateObject не будет
    // создавать новый объект при каждом вызове
    // Недостатки: кэшированные объекты недоступны для сборщика мусора
    // [CompilerGenerated]
    // private static WaitCallback <>9__CachedAnonymousMethodDelegate1;
    // public static void CallbackWithoutNewingADelegateObject() {
    // if (<>9__CachedAnonymousMethodDelegate1 == null) {
    // // При первом вызове делегат создается и кэшируется
    // <>9__CachedAnonymousMethodDelegate1 =
    // new WaitCallback(<CallbackWithoutNewingADelegateObject>b__0);
    // }
    // ThreadPool.QueueUserWorkItem(<>9__CachedAnonymousMethodDelegate1, 5);
    // }
    // [CompilerGenerated]
    // private static void <CallbackWithoutNewingADelegateObject>b__0(
    // Object obj) {
    // Console.WriteLine(obj);
    // }
    // }
    // Впрочем, код может обращаться к любым определенным в классе статическим полям или методам.
    // internal sealed class AClass
    // {
    //     private static String sm_name; // Статическое поле
    //     public static void CallbackWithoutNewingADelegateObject()
    //     {
    //         ThreadPool.QueueUserWorkItem(
    //             // Код обратного вызова может обращаться к статическим членам
    //         obj => Console.WriteLine(sm_name + ": " + obj),
    //         5);
    //     }
    // }
    // Если же в коде анонимного метода наличествуют ссылки на члены экземпляра, компилятор создает нестатический анонимный метод:
    // internal sealed class AClass {
    // private String m_name; // Поле экземпляра
    // Метод экземпляра
    // public void CallbackWithoutNewingADelegateObject() {
    // ThreadPool.QueueUserWorkItem(
    // Код обратного вызова может ссылаться на члены экземпляра
    // obj => Console.WriteLine(m_name+ ": " + obj),
    // 5);
    // }
    // }
    // Имена аргументов, которые следует передать лямбда-выражению, указываются слева от оператора =>.
    // При этом следует придерживаться правил, которые мы рассмотрим на примерах:
    // Внимание
    // Хотя это и не кажется очевидным, основная выгода от использования лямбда-выражений состоит в том, что они снижают уровень неопределенности вашего кода.
    // Обычно приходится писать отдельный метод, присваивать ему имя и передавать это имя методу, в котором требуется делегат.
    // Именно имя позволяет ссылаться на фрагмент кода. 
    // И если ссылка на один и тот же фрагмент требуется в различных местах программы, создание метода — это самое правильное решение.
    // Если же обращение к фрагменту кода предполагается всего одно, на помощь приходят лямбда-выражения.
    // Именно они позволяют встраивать фрагменты кода в нужное место, избавляя от необходимости их именования и повышая тем самым продуктивность работы программиста.
    // ПримечАние
    // Механизм анонимных методов впервые появился в C# 2.0.
    // Подобно лямбда-выражениям (появившимся в C# 3.0), анонимные методы описывают синтаксис создания анонимных функций.
    // Рекомендуется использовать лямбда-выражения вместо анонимных методов, так как их синтаксис более компактен, что упрощает чтение кода.
    // Разумеется, компилятор до сих пор поддерживает анонимные функции, так что необходимости вносить исправления в код, написанный на C# 2.0, нет.
    // Тем не менее в этой книге рассматривается только синтаксис лямбда-выражений.

    // Упрощение 3: не создаем обертку для локальных переменных для передачи их методу обратного вызова(AClass)
    // ПримечАние
    // Когда лямбда-выражение заставляет компилятор генерировать класс с превращенными в поля параметрами/локальными переменными, увеличивается время жизни объекта, на который ссылаются эти переменные. Обычно параметры/локальные переменные уничтожаются после завершения метода, в котором они используются. В данном же случае они остаются, пока не будет уничтожен объект, содержащий поле.
    // В большинстве приложений это не имеет особого значения, тем не менее этот факт следует знать.
    // Внимание
    // Без сомнения, у любого программиста возникает соблазн использовать лямбда-выражения там, где это уместно и не уместно.
    // Лично я привык к ним не сразу.
    // Ведь код, который вы пишете внутри метода, на самом деле этому методу не принадлежит, что затрудняет отладку и пошаговое выполнение.
    // Хотя я был откровенно поражен тем, что отладчик Visual Studio позволял выполнять лямбда-выражения в моем исходном коде в пошаговом режиме.
    // Я установил для себя правило: если в методе обратного вызова предполагается более трех строк кода, не использовать лямбда-выражения.
    // Вместо этого я пишу метод вручную и присваиваю ему имя.
    // Впрочем, при разумном подходе лямбда-выражения способны серьезно повысить продуктивность работы программиста и упростить поддержку кода.
    // В следующем примере кода лямбда-выражения смотрятся естественно, и без них написание, чтение и редактирование кода было бы намного сложнее:

    // Делегаты и отражение
    // К счастью, в классе System.Reflection.MethodInfo имеется метод CreateDelegate, позволяющий создавать и вызывать делегаты даже при отсутствии сведений о них на момент компиляции.
    // Вот как выглядят перегруженные версии этого метода:
    // public abstract class MethodInfo : MethodBase
    // {
    //     // Создание делегата, служащего оберткой статического метода.
    //     public virtual Delegate CreateDelegate(Type delegateType);
    //     // Создание делегата, служащего оберткой экземплярного метода;
    //     // target ссылается на аргумент 'this'.
    //     public virtual Delegate CreateDelegate(Type delegateType, Object target);
    // }
    // После того как делегат будет создан, его можно вызвать методом DynamicInvoke класса Delegate, который выглядит примерно так:
    // public abstract class Delegate
    // {
    //     // Вызов делегата с передачей параметров
    //     public Object DynamicInvoke(params Object[] args);
    // }
    // При использовании API отражения необходимо сначала получить объект MethodInfo для метода, для которого требуется создать делегата.
    // Затем вызов метода CreateDelegate создает новый объект типа, производного от Delegate и определяемого первым параметром delegateType.
    // Если делегат представляет экземплярный метод, также следует передать CreateDelegate параметр target, обозначающий объект, который должен передаваться экземплярному методу как параметр this.
    // Метод DynamicInvoke класса System.Delegate позволяет задействовать метод обратного вызова делегата, передавая набор параметров, определяемых во время выполнения.
    // При вызове метода DynamicInvoke проверяется совместимость переданных параметров с параметрами, ожидаемыми методом обратного вызова.


    // !!!
    // - lambda
    // - delegate reflection
}