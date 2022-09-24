using System;

namespace DefaultNamespace
{
    public class MyEventArgs : EventArgs
    {
        public Type SenderType { get; }
        public string MyArg { get; }

        public MyEventArgs(string myArg, Type senderType)
        {
            MyArg = myArg;
            SenderType = senderType;
        }
    }
}