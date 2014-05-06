﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DokiTCGTest.Server.Messages
{
    interface IMessage
    {
        protected string command;
        protected string prefix;

        IMessage(string prefix, string command);
    }
}