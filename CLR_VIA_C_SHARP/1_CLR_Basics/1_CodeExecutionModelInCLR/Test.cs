using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLR_VIA_C_SHARP.CLR_Basics.Test
{
    internal sealed class Test
    {
        // Конструктор
        public Test() { }
        // Финализатор
        ~Test() { }
        // Перегрузка оператора
        public static Boolean operator ==(Test t1, Test t2)
        {
            return true;
        }
        public static Boolean operator !=(Test t1, Test t2)
        {
            return false;
        }
        // Перегрузка оператора
        public static Test operator +(Test t1, Test t2) { return null; }
        // Свойство
        public String AProperty
        {
            get { return null; }
            set { }
        }
        // Индексатор
        public String this[Int32 x]
        {
            get { return null; }
            set { }
        }
        // Событие
        public event EventHandler AnEvent;
    }
}
