using System;
using System.Collections;
using System.Collections.Generic;
using DemoLab.Core;

namespace DemoLab.Examples
{
    public class Messenger : IMessenger
    {
        //отправляем сообщение
        public void SendMessage(string message)
        {
            throw new NotImplementedException();
        }

        //получаем сообщение в порядке обратном поступлению (из конца в начало)
        public string GetLastMessage()
        {
            throw new NotImplementedException();
        }

        //получаем сообщение в порядке поступления (с начала в конец)
        public string GetFirstMessage()
        {
            throw new NotImplementedException();
        }
    }

    public class TaskActivator : IRunnable
    {
        public object Run()
        {
            return new Messenger();
        }
    }
}