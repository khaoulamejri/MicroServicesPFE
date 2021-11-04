using Microsoft.AspNetCore.Http;
using ParamGeneral.Data;
using ParamGeneral.Data.Infrastructure;
using ParamGeneral.Domain.Entities;
using ParamGeneral.Services.Iservices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParamGeneral.Services.Services
{
    public class WFDocumentServices : IWFDocumentServices
    {
        private readonly ApplicationDbContext Context;
        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;
        private readonly IHttpContextAccessor _httpContextAccessor;
  



        public WFDocumentServices(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
            
        {
           

            Context = context;
            dbFactory = new DatabaseFactory(context);
            utOfWork = new UnitOfWork(dbFactory);
            _httpContextAccessor = httpContextAccessor;
       

        }

        public WFDocument Create(WFDocument wFDocument)
        {
            try
            {
                utOfWork.WFDocumentRepository.Add(wFDocument);
                utOfWork.Commit();
                return wFDocument;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public WFDocument Edit(WFDocument wFDocument)
        {
            try
            {
                wFDocument.ActionDate = DateTime.Now;
                utOfWork.WFDocumentRepository.Update(wFDocument);
                utOfWork.Commit();
                return wFDocument;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public WFDocument Delete(WFDocument wFDocument)
        {
            try
            {
                if (wFDocument != null)
                {
                    utOfWork.WFDocumentRepository.Delete(wFDocument);
                    utOfWork.Commit();
                    return wFDocument;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<WFDocument> GetDocumentsToBeValidatedByEmployee(int employeeId, string documentType)
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int companyId = int.Parse(session[2].Value);

            var wfDocToBeValidated = new List<WFDocument>();
            wfDocToBeValidated = utOfWork.WFDocumentRepository.GetMany(wf => wf.Finished == false && wf.AffectedToId == employeeId && wf.TypeDocument == documentType && wf.companyID == companyId).ToList();
            return wfDocToBeValidated;
        }

        public bool checkUnicity(WFDocument wFDocument)
        {
            return !utOfWork.WFDocumentRepository.GetMany(d => d.Id != wFDocument.Id).Any();
        }
    }
}
