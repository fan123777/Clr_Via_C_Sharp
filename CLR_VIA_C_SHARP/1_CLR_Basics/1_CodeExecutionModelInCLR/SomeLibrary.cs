using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Приказываем компилятору проверять код
// на совместимость с CLS
[assembly: CLSCompliant(true)]

namespace CLR_VIA_C_SHARP.CLR_Basics.SomeLibrary
{
    // Предупреждения выводятся, потому что класс является открытым
    public sealed class SomeLibraryType
    {
        // Предупреждение: возвращаемый тип 'SomeLibrary.SomeLibraryType.Abc()'
        // не является CLS-совместимым
        public UInt32 Abc() { return 0; }

        // Предупреждение: идентификаторы 'SomeLibrary.SomeLibraryType.abc()',
        // отличающиеся только регистром символов, не являются
        // CLS-совместимыми
        public void abc() { }

        // Предупреждения нет: закрытый метод
        private UInt32 ABC() { return 0; }

        // Если удалить ключевое слово public перед sealed class SomeLibraryType и перекомпилировать код, оба предупреждения пропадают.
    }
}
