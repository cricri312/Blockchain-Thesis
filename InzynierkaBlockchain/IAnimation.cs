using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
//this interface is only to remind me to implement a function in the Animation class
namespace InzynierkaBlockchain
{
    interface IAnimation
    {
        void Init();
        void SleepAndClear();
        void CompleteMenuOffilne();
        void CompleteMenuOnlineOffline();
    }
}
