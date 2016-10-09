using System.Collections.Generic;

namespace Bridge_App.Interface
{
    public interface IAlgoritm
    {
        string MethodName { get; set; }
        List<Step> Stepping { get; set; }
        void Solv(List<People> peoples);
        void ProcessorSelect(string methodName);
    }
}