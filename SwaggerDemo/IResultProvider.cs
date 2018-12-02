using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerDemo
{
    public interface IResultProvider
    {
        string[] ProvideResult();
    }

    public class ResultProvider : IResultProvider
    {
        public string[] ProvideResult()
        {
            return new String[] { "hello 1", "hello 2" };
        }
    }
}
