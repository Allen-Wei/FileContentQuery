using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using FileContentQuery.Core;
using FileContentQuery.Models;

namespace FileContentQuery
{
    class Program
    {
        static void Main(string[] args)
        {
            var handler = new ParameterHandler();
            var parameters = handler.ToModel<QueryParameters>(args);
            DiskIoOperation operate = new DiskIoOperation(parameters);
            operate.Print();
        }

    }
}
