using System;
using System.Collections.Generic;
using System.Text;

namespace MyModBus
{
    public interface ILog
    {
        void Write(string log);
    }
}