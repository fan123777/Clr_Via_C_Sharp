﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CLR_VIA_C_SHARP._2_Type_Design._11_Events
{
    class Events
    {
        public static void main()
        {

        }

        // Этап 1. Определение типа для хранения информации,
        // которая передается получателям уведомления о событии
        internal class NewMailEventArgs : EventArgs
        {
            private readonly String m_from, m_to, m_subject;
            public NewMailEventArgs(String from, String to, String subject)
            {
                m_from = from; m_to = to; m_subject = subject;
            }
            public String From { get { return m_from; } }
            public String To { get { return m_to; } }
            public String Subject { get { return m_subject; } }
        }

        internal class MailManager
        {
            // Этап 2. Определение члена-события
            public event EventHandler<NewMailEventArgs> NewMail;

            // Этап 3. Определение метода, ответственного за уведомление
            // зарегистрированных объектов о событии
            // Если этот класс изолированный, нужно сделать метод закрытым
            // или невиртуальным
            protected virtual void OnNewMail(NewMailEventArgs e)
            {
                // Сохранить ссылку на делегата во временной переменной
                // для обеспечения безопасности потоков
                EventHandler<NewMailEventArgs> temp = Volatile.Read(ref NewMail);
                // Если есть объекты, зарегистрированные для получения
                // уведомления о событии, уведомляем их
                if (temp != null) temp(this, e);
            }

            protected virtual void OnNewMail1(NewMailEventArgs e)
            {
                e.Raise(this, ref NewMail);
            }
        }
    }

    public static class EventArgExtensions
    {
        public static void Raise<TEventArgs>(this TEventArgs e, Object sender, ref EventHandler<TEventArgs> eventDelegate)
        {
            // Копирование ссылки на поле делегата во временное поле
            // для безопасности в отношении потоков
            EventHandler<TEventArgs> temp = Volatile.Read(ref eventDelegate);
            // Если зарегистрированный метод заинтересован в событии, уведомите его
            if (temp != null) temp(sender, e);
        }
    }

    // Тип, в котором определено событие (или экземпляры этого типа), может уведомлять другие объекты о некоторых особых ситуациях, которые могут случиться.
    // Например, если в классе Button (кнопка) определить событие Click (щелчок), то в приложение можно использовать объекты, которые будут получать уведомление о щелчке объекта Button, а получив такое уведомление — исполнять некоторые действия. События — это члены типа, обеспечивающие такого рода взаимодействие.
    // А именно определения события в типе означает, что тип поддерживает следующие возможности:
    // - регистрация своей заинтересованности в событии;
    // - отмена регистрации своей заинтересованности в событии;
    // - оповещение зарегистрированных методов о произошедшем событии.
    // Типы могут предоставлять эту функциональность при определении событий, так как они поддерживают список зарегистрированных методов. Когда событие происходит, тип уведомляет об этом все зарегистрированные методы.
    // Модель событий CLR основана на делегатах (delegate). Делегаты обеспечивают реализацию механизма обратного вызова, безопасную по отношению к типам. Методы обратного вызова (callback methods) позволяют объекту получать уведомления, на которые он подписался. В этой главе мы будем постоянно пользоваться делегатами.

    // Разработка типа, поддерживающего событие
    // Этап 1. Определение типа для хранения всей дополнительной информации, передаваемой получателям уведомления о событии.
    // При возникновении события объект, в котором оно возникло, должен передать дополнительную информацию объектам-получателям уведомления о событии. Для предоставления получателям эту информацию нужно инкапсулировать в собственный класс, содержащий набор закрытых полей и набор открытых неизменяемых (только для чтения) свойств.
    // В соответствии с соглашением, классы, содержащие информацию о событиях, передаваемую обработчику события, должны наследовать от типа System.EventArgs, а имя типа должно заканчиваться словом EventArgs. В этом примере у типа NewMailEventArgs есть поля, идентифицирующие отправителя сообщения (m_from), его получателя (m_to) и тему (m_subject).
    // ПримечАние
    // Тип EventArgs определяется в библиотеке классов . NET Framework Class Library (FCL) и выглядит примерно следующим образом:
    // [ComVisible(true), Serializable]
    // public class EventArgs
    // {
    //     public static readonly EventArgs Empty = new EventArgs();
    //     public EventArgs() { }
    // }
    // Как видите, в этом классе нет ничего особенного. Он просто служит базовым типом, от которого можно порождать другие типы. С большинством событий не передается дополнительной информации. Например, в случае уведомления объектом Button о щелчке на кнопке, само обращение к методу обратного вызова — и есть вся нужная информация. 
    // Определяя событие, не передающее дополнительные данные, можно не создавать новый объект Event-Args, достаточно просто воспользоваться свойством EventArgs.Empty.
    // Этап 2. Определение члена-события
    // В C# событие объявляется с ключевым словом event. Каждому члену-событию назначаются область действия (практически всегда он открытый, поэтому доступен из любого кода), тип делегата, указывающий на прототип вызываемого метода (или методов), и имя (любой допустимый идентификатор).
    // Здесь NewMail — имя события, а типом события является EventHandler <NewMailEventArgs>. Это означает, что получатели уведомления о событии должны предоставлять метод обратного вызова, прототип которого соответствует типу делегату EventHandler<NewMailEventArgs>. Так как обобщенный делегат System.EventHandler определен следующим образом:
    // public delegate void EventHandler<TEventArgs>
    // (Object sender, TEventArgs e) where TEventArgs : EventArgs;
    // Поэтому прототип метода должен выглядеть так:
    // void MethodName(Object sender, NewMailEventArgs e);
    // Примечание
    // Многих удивляет, почему механизм событий требует, чтобы параметр sender имел тип Object. Вообще-то, поскольку MailManager — единственный тип, реализующий события с объектом NewMailEventArgs, было бы разумнее использовать следующий прототип метода обратного вызова:
    // void MethodName(MailManager sender, NewMailEventArgs e);
    // Причиной того, что параметр sender имеет тип Object, является наследование. Что произойдет, если MailManager задействовать в качестве базового класса для создания класса SmtpMailManager? В методе обратного вызова придется в прототипе задать параметр sender как SmtpMailManager, а не MailManager, но этого делать нельзя, так как тип SmtpMailManager просто наследует событие NewMail. Поэтому код, ожидающий от SmtpMailManager информацию о событии, все равно будет вынужден приводить аргумент sender к типу SmtpMailManager. Иначе говоря, приведение все равно необходимо, поэтому параметр sender с таким же успехом можно объявить с типом Object.
    // Еще одна причина для объявления sender с типом Object — гибкость, поскольку делегат может применяться несколькими типами, которые поддерживают событие, передающее объект NewMailEventArgs. В частности, класс PopMailManager мог бы использовать делегат, даже если бы не наследовал от класса MailManager.
    // И еще одно: механизм событий требует, чтобы в имени делегата и методе обратного вызова параметр, производный от EventArgs, назывался «e». Такое требование устанавливается по единственной причине: для обеспечения единообразия, облегчающего и упрощающего изучение и реализацию событий разработчиками. Инструменты создания кода (например, такой как Microsoft Visual Studio) также «знают», что нужно вызывать параметр e.
    // И последнее: механизм событий требует, чтобы все обработчики возвращали void. Это обязательно, потому что при возникновении события могут выполняться несколько методов обратного вызова и невозможно получить у них все возвращаемое значение. Тип void просто запрещает методам возвращать какое бы то ни было значение. К сожалению, в библиотеке FCL есть обработчики событий, в частности ResolveEventHandler, в которых Microsoft не следует собственным правилам и возвращает объект типа Assembly.
    // Этап 3. Определение метода, ответственного за уведомление зарегистрированных объектов о событии
    // В соответствии с соглашением в классе должен быть виртуальный защищенный метод, вызываемый из кода класса и его потомков при возникновении события. Этот метод принимает один параметр, объект MailMsgEventArgs, содержащий дополнительные сведения о событии.
    // Реализация по умолчанию этого метода просто проверяет, есть ли объекты, зарегистрировавшиеся для получения уведомления о событии, и при положительном результате проверки сообщает зарегистрированным методам о возникновении события.
    // Уведомление о событии, безопасное в отношении потоков
    // В первом выпуске .NET Framework рекомендовалось уведомлять о событиях следующим образом:
    // // Версия 1
    // protected virtual void OnNewMail(NewMailEventArgs e)
    // {
    //    if (NewMail != null) NewMail(this, e);
    // }
    // Однако в методе OnNewMail кроется одна потенциальная проблема. Программный поток видит, что значение NewMail не равно null, однако перед вызовом NewMail другой поток может удалить делегата из цепочки, присвоив NewMail значение null. В результате будет выдано исключение NullReferenceException.
    // // Версия 2
    // protected void OnNewMail(NewMailEventArgs e)
    // {
    //     EventHandler<NewMailEventArgs> temp = NewMail;
    //     if (temp != null) temp(this, e);
    // }
    // Вспомните, что делегаты неизменяемы, поэтому теоретически этот способ работает. Однако многие разработчики не осознают, что компилятор может оптимизировать этот программный код, удалив переменную temp. В этом случае обе представленные версии кода окажутся идентичными, в результате опять-таки возможно исключение NullReferenceException.
    // Для реального решения этой проблемы необходимо переписать OnNewMail так:
    // // Версия 3
    // protected void OnNewMail(NewMailEventArgs e)
    // {
    //     EventHandler<NewMailEventArgs> temp = Thread.VolatileRead(ref NewMail);
    //     if (temp != null) temp(this, e);
    // }
    // Все JIT-компиляторы Microsoft соблюдают принцип отказа от лишних операций чтения из кучи, а следовательно, кэширование ссылки в локальной переменной гарантирует, что обращение по ссылке будет производиться всего один раз.
    // Такое поведение официально не документировано и теоретически может измениться, поэтому лучше все же использовать последнюю версию представленного программного кода.
    // Кроме того, события в основном используются в однопоточных сценариях (приложения Windows Presentation Foundation и Windows Store), так что безопасность потоков вообще не создает особых проблем.
    // Для удобства можно определить метод расширения, инкапсулирующий логику, безопасную в отношении потоков. Определите расширенный метод следующим образом:
    // Этап 4. Определение метода, преобразующего входную информацию в желаемое событие


    // !!!
    // 
}
