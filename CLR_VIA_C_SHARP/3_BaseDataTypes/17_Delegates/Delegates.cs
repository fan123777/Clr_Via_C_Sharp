using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CLR_VIA_C_SHARP._3_BaseDataTypes._17_Delegates
{
    class Delegates
    {
        public static void main()
        {
            Delegates d = new Delegates();
            d.testDelegate();
        }

        private void testDelegate()
        {
            StaticDelegateDemo();
            InstanceDelegateDemo();
            ChainDelegateDemo1(new Delegates());
            ChainDelegateDemo2(new Delegates());
        }

        // Объявление делегата; экземпляр ссылается на метод
        // с параметром типа Int32, возвращающий значение void
        internal delegate void Feedback(Int32 value);

        private static void StaticDelegateDemo()
        {
            Console.WriteLine("----- Static Delegate Demo -----");
            Counter(1, 3, null);
            Counter(1, 3, new Feedback(Delegates.FeedbackToConsole));
            Counter(1, 3, new Feedback(FeedbackToMsgBox)); // Префикс "Program."
            // не обязателен
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
            fbChain = (Feedback)
            Delegate.Remove(fbChain, new Feedback(FeedbackToMsgBox));
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
    }

    // В этой главе рассказывается о чрезвычайно полезном механизме, который используется уже много лет и называется функциями обратного вызова.
    // В Microsoft .NET Framework этот механизм поддерживается при помощи делегатов (delegates).
    
    // Знакомство с делегатами
    // В .NET Framework методы обратного вызова также имеют многочисленные применения. К примеру, можно зарегистрировать такой метод для получения различных уведомлений: о необработанных исключениях, изменении состояния окон, выборе пунктов меню, изменениях файловой системы и завершении асинхронных операций.
    // разобрать пример
    // 436
}
