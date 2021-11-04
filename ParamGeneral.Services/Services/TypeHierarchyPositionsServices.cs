using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
    public class TypeHierarchyPositionsServices : ITypeHierarchyPositionsServices
    {
        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;
        private readonly ApplicationDbContext Context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TypeHierarchyPositionsServices(ApplicationDbContext ctx, IHttpContextAccessor httpContextAccessor)
        {
            Context = ctx;
            _httpContextAccessor = httpContextAccessor;
            dbFactory = new DatabaseFactory(ctx);
            utOfWork = new UnitOfWork(dbFactory);
        }

        public List<TypeHierarchyPosition> GetAllTypeHierarchyPosition()
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            return utOfWork.TypeHierarchyPositionRepository.GetMany(t => t.companyID == currentCompanyId).Include(obj => obj.HierarchyPosition).Include(x => x.ParamGeneraux).ToList();
        }

        public TypeHierarchyPosition GetTypeHierarchyPositionByID(int id)
        {
            return utOfWork.TypeHierarchyPositionRepository.GetMany(d => d.Id == id).Include(obj => obj.HierarchyPosition).Include(x => x.ParamGeneraux).First();
        }
        public TypeHierarchyPosition GetTypeHierarchyPositioByID(int? id)
        {
            return utOfWork.TypeHierarchyPositionRepository.GetMany(d => d.Id == id).Include(obj => obj.HierarchyPosition).Include(x => x.ParamGeneraux).First();
        }

        public TypeHierarchyPosition Create(TypeHierarchyPosition TypeHierarchyPosition)
        {
            try
            {
                var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
                int currentCompanyId = int.Parse(session[2].Value);
                TypeHierarchyPosition.companyID = currentCompanyId;

                utOfWork.TypeHierarchyPositionRepository.Add(TypeHierarchyPosition);
                utOfWork.Commit();
                return TypeHierarchyPosition;
            }
            catch
            {
                return null;
            }
        }

        public TypeHierarchyPosition Edit(TypeHierarchyPosition TypeHierarchyPosition)
        {
            try
            {
                utOfWork.TypeHierarchyPositionRepository.Update(TypeHierarchyPosition);
                utOfWork.Commit();
                return TypeHierarchyPosition;
            }
            catch
            {
                return null;
            }
        }

        public TypeHierarchyPosition Delete(int TypeHierarchyPositionId)
        {
            try
            {
                var TypeHierarchyPosition = utOfWork.TypeHierarchyPositionRepository.Get(a => a.Id == TypeHierarchyPositionId);
                if (TypeHierarchyPosition != null)
                {
                    utOfWork.TypeHierarchyPositionRepository.Delete(TypeHierarchyPosition);
                    utOfWork.Commit();
                    return TypeHierarchyPosition;
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

        public bool CheckUnicityTypeHierarchyPosition(string code, int id)
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            return !utOfWork.TypeHierarchyPositionRepository.GetMany(d => d.Code == code && d.companyID == currentCompanyId && d.Id != id).Any();
        }
    }
}