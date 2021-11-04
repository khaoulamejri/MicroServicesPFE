using ParamGeneral.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParamGeneral.Services.Iservices
{
    public interface IParamGenerauxServices
    {
        ParamGeneraux GetParamGeneraux();
        ParamGeneraux Create(ParamGeneraux paramGeneraux);
        ParamGeneraux Edit(ParamGeneraux paramGeneraux);
        ParamGeneraux GetParamGenerauxByID(int id);
        bool CheckUnicityParamGeneraux(int id);
    }
}
