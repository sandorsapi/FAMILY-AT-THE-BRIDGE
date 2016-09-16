using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge_App.Interface
{
    public interface IPeopleRepository
    {
       void Save(List<People> peoplesValue);
    }
}
