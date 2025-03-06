﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serialization
{
    internal interface ISerializer
    {
        void Serialize<T>(T data, string filePath);
        T Deserialize<T>(string filePath);
    }
}
