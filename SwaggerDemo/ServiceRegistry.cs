using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerDemo
{
    public class ServiceRegistry : Registry
    {
        public ServiceRegistry()
        {
            For<IResultProvider>().Use<ResultProvider>();
        }
    }
}
