﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLR_VIA_C_SHARP._1_CLR_Basics._3_SharedAndStrongNameAssembly
{
    class SharedAndStrongNameAssembly
    {
        public static void main()
        {
            // почти все управляемые приложения используют типы, определенные Microsoft в библиотеке классов .NET Framework Class Library (FCL).

            // Два вида сборок — два вида развертывания
            // Среда CLR поддерживает два вида сборок: с нестрогими именами (weakly named assemblies) и со строгими именами (strongly named assemblies).
            // Сборки со строгими и нестрогими именами имеют идентичную структуру, то есть в них используется файловый формат PE (portable executable), заголовок PE32(+), CLR-заголовок, метаданные, таблицы манифеста, а также IL-код, рассмотренный в главах 1 и 2. Оба типа сборок компонуются при помощи одних и тех же инструментов, например компилятора C# или AL.exe. В действительности сборки со строгими и нестрогими именами отличаются тем, что первые подписаны при помощи пары ключей, уникально идентифицирующей издателя сборки.
            // Для сборки с нестрогим именем возможно лишь закрытое развертывание.

            // Назначение сборке строгого имени
            // Среда CLR должна поддерживать некий механизм, позволяющий уникально идентифицировать сборку. Именно для этого и служат строгие имена.
            // У сборки со строгим именем четыре атрибута, уникально ее идентифицирующих: имя файла (без расширения), номер версии, идентификатор регионального стандарта и открытый ключ.
            // место последнего атрибута используется небольшой хеш-код открытого ключа, который называют маркером открытого ключа (public key token).
            // 4 разные сборки:
            // - "MyTypes, Version=1.0.8123.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
            // - "MyTypes, Version=1.0.8123.0, Culture="en-US", PublicKeyToken=b77a5c561934e089"
            // - "MyTypes, Version=2.0.1234.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
            // - "MyTypes, Version=1.0.8123.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
            // должна получить пару ключей — открытый и закрытый, после чего открытый ключ можно будет связать со сборкой.
            // Кроме имени файла, у сборки со строгим именем есть номер версии и идентификатор региональных стандартов. Кроме того, она подписана при помощи закрытого ключа издателя.
            // Первый этап создания такой сборки — получение ключа при помощи утилиты Strong Name (SN.exe), поставляемой в составе .NET Framework SDK и Microsoft Visual Studio.
            // SN –k MyCompany.snk
            // SN –p MyCompany.keys MyCompany.PublicKey
            // SN –tp MyCompany.PublicKey
            // Чтобы облегчить жизнь разработчику (и конечному пользователю), были созданы маркеры открытого ключа. Маркер открытого ключа — это 64-разрядный хеш-код открытого ключа. Если вызвать утилиту SN.exe с параметром –tp, то после значения ключа она выводит соответствующий маркер открытого ключа.
            // При компиляции сборки необходимо задать компилятору параметр /keyfile:имя_файла:
            // csc /keyfile:MyCompany.snk Program.cs
            // Обнаружив в исходном тексте этот параметр, компилятор C# открывает заданный файл (MyCompany.snk), подписывает сборку закрытым ключом и встраивает открытый ключ в манифест сборки. Заметьте: подписывается лишь файл сборки, содержащий манифест, другие файлы сборки нельзя подписать явно.
            // В Visual Studio новая пара ключей создается в окне свойств проекта. Для этого перейдите на вкладку Signing, установите флажок Sign the assembly, а затем в поле со списком Choose a strong name key file выберите вариант <New…>.
            // По умолчанию хеш-код вычисляется по алгоритму SHA-1.
            // Значение хеш-кода подписывается закрытым ключом издателя, а полученная в результате цифровая подпись RSA заносится в зарезервированный раздел PE-файла (при хешировании PE-файла этот раздел исключается), и в CLR-заголовок PE-файла записывается адрес, по которому встроенная цифровая подпись находится в файле.
            // В задачу компилятора входит внедрение таблицы метаданных AssemblyRef в результирующий управляемый модуль. Каждая запись таблицы метаданных AssemblyRef описывает файл сборки, на которую ссылается данная сборка, и состоит из имени файла сборки (без расширения), номера версии, регионального стандарта и значения открытого ключа.
            // файла. Для экономии места в компании Microsoft хешируют открытый ключ и берут последние 8 байт полученного хеш-кода. В таблице AssemblyRef на самом деле хранятся именно такие усеченные значения открытого ключа — маркеры отрытого ключа. 
            // Если бы я создал файл с ключами при помощи утилиты SN.exe, а затем скомпилировал сборку с параметром /keyfile, то получилась бы подписанная сборка.

            // Глобальный кэш сборок
            // Место, где располагаются совместно используемые сборки, называют глобальным кэшем сборок (global assembly cache, GAC).
            // обычно GAC находится в каталоге %SystemRoot%\Microsoft.NET\Assembly
            // В период разработки и тестирования сборок со строгими именами для установки их в каталог GAC чаще всего применяют инструмент GACUtil.exe.
            // Вызвав утилиту GACUtil.exe с параметром /i, можно установить сборку в каталог GAC, а с параметром /u сборка будет удалена из GAC.
            // Параметр /r обеспечивает интеграцию сборки с механизмом установки и удаления программ Windows.
            // Глобальное развертывание сборки путем размещения ее в каталог GAC — это один из видов регистрации сборки в системе, хотя это никак не затрагивает реестр Windows. Установка сборок в GAC делает невозможными простые установку, копирование, восстановление, перенос и удаление приложения. По этой причине рекомендуется избегать глобального развертывания и использовать закрытое развертывание сборок всюду, где это только возможно.

            // Построение сборки, ссылающейся на сборку со строгим именем
            // Какую бы сборку вы ни строили, в результате всегда получается сборка, ссылающаяся на другие сборки со строгими именами. Это утверждение верно хотя бы потому, что класс System.Object определен в MSCorLib.dll, сборке со строгим именем.
            // Как отмечено в главе 2, если задано имя файла без указания пути, CSC.exe пытается найти нужную сборку в следующих каталогах (просматривая их в порядке перечисления).
            // 1. Рабочий каталог.
            // 2. Каталог, где находится файл CSC.exe. Этот каталог также содержит DLL-библиотеки CLR.
            // 3. Каталоги, заданные параметром /lib командной строки компилятора.
            // 4. Каталоги, указанные в переменной окружения LIB.
            // Таким образом, чтобы скомпоновать сборку, ссылающуюся на файл System.Drawing.dll разработки Microsoft, при вызове CSC.exe можно задать параметр /reference:System.Drawing.dll.
            // Дело в том, что во время установки .NET Framework все файлы сборок, созданных Microsoft, устанавливаются в двух экземплярах. Один набор файлов заносится в один каталог с CLR, а другой — в каталог GAC. Файлы в каталоге CLR облегчают построение пользовательских сборок, а их копии в GAC предназначены для загрузки во время выполнения.
            // Кроме того, сборки в каталоге CLR не привязаны к машине. Иначе говоря, эти сборки содержат только метаданные. Так как код IL не нужен на стадии построения, в этом каталоге не нужно хранить версии сборки для x86, x64 и ARM. Сборки в GAC содержат метаданные и IL, потому что код нужен только во время выполнения.
            
            // Устойчивость сборок со строгими именами к несанкционированной модификации
            // Когда приложению требуется привязка к сборке, на которую оно ссылается, CLR использует для поиска этой сборки в GAC ее свойства (имя, версию, региональные стандарты и открытый ключ). Если нужная сборка обнаруживается, возвращается путь к каталогу, в котором она находится, и загружается файл с ее манифестом.

            // Отложенное подписание
            // .NET Framework поддерживает отложенное подписание (delayed signing), также иногда называемое частичным (partial signing).
            // При построении сборки в результирующем PE-файле остается место для цифровой подписи RSA.
            // Полная последовательность действий по созданию сборки с отложенным подписанием выглядит следующим образом.
            // 1. Во время разработки сборки следует получить файл, содержащий лишь открытый ключ компании, и добавить в строку компиляции сборки параметры /keyfile и /delaysign:
            // csc /keyfile:MyCompany.PublicKey /delaysign MyAssembly.cs
            // 2. После построения сборки надо выполнить показанную далее команду, чтобы получить возможность тестирования этой сборки, установки ее в каталог GAC и компоновки других сборок, ссылающихся на нее. Эту команду достаточно исполнить лишь раз, не нужно делать это при каждой компоновке сборки.
            // SN.exe -Vr MyAssembly.dll



            // !!!
            // - SN.exe
            // - GACUtil.exe
        }
    }
}
