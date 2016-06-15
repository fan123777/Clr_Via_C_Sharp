using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLR_VIA_C_SHARP._3_BaseDataTypes._14_Strings
{
    class Testing
    {
        public void main()
        {
            Char c;
            Int32 n;
            c = (Char)65;
            n = (Int32)c;
            c = unchecked((Char)(65536 + 65));
            c = Convert.ToChar(65);
            n = Convert.ToInt32(c);
            try
            {
                c = Convert.ToChar(70000); // Слишком много для 16 разрядов
                Console.WriteLine(c); // Этот вызов выполняться НЕ будет
            }
            catch (OverflowException)
            {
                Console.WriteLine("Can't convert 70000 to a Char.");
            }

            String s = "Hello world";
            String s1 = "Hello" + " " + "World";

            // Задание пути к приложению
            String file = "C:\\Windows\\System32\\Notepad.exe";

            // Задание пути к приложению с помощью буквальной строки
            String myFile = @"C:\Windows\System32\Notepad.exe";

            StringBuilder sb = new StringBuilder();
            sb.Append("Hello");
        }
    }
}
