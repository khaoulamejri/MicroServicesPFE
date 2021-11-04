using ParamGeneral.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParamGeneral.Services.Iservices
{
 public   interface IWFDocumentServices
    {
        WFDocument Create(WFDocument wFDocument);
        WFDocument Edit(WFDocument wFDocument);
        WFDocument Delete(WFDocument wFDocument);
        bool checkUnicity(WFDocument wFDocument);
        List<WFDocument> GetDocumentsToBeValidatedByEmployee(int employeeId, string documentType);
    }
}
