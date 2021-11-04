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
    public class PositionServices : IPositionServices
    {
        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;
        private readonly ApplicationDbContext Context;
        private readonly IParamGenerauxServices paramGenerauxServices;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmploiServices emploiServices;

        public PositionServices(ApplicationDbContext ctx, IHttpContextAccessor httpContextAccessor, IParamGenerauxServices pgs, IEmploiServices emplServices)
        {
            Context = ctx;
            _httpContextAccessor = httpContextAccessor;
            paramGenerauxServices = pgs;
            emploiServices = emplServices;
            dbFactory = new DatabaseFactory(ctx);
            utOfWork = new UnitOfWork(dbFactory);
        }

        public Position GetFirstPositionByEmployeeId(int employeeId)
        {
            return utOfWork.PositionRepository.GetMany(t => t.AffectationEmployee.FirstOrDefault().EmployeeID == employeeId).Include(a => a.Unite).FirstOrDefault();
        }

        public List<Position> GetAllPosition()
        {
            var _db = Context;
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            //if (includeUnit)
            //{
                var listPosition = (from p in _db.position
                                    join d in _db.departements on p.DepartementID equals d.Id
                                    join e in _db.emploi on p.EmploiID equals e.Id
                                    into DetailsEmplois
                                     from e in DetailsEmplois.DefaultIfEmpty()
                                    join u in _db.unite on p.UniteID equals u.Id
                                    where (p.companyID == currentCompanyId)
                                    select new Position
                                    {
                                        Id = p.Id,
                                        companyID = currentCompanyId,
                                        DateCreat = p.DateCreat,
                                        DateModif = p.DateModif,
                                        UserCreat = p.UserCreat,
                                        UserModif = p.UserModif,
                                        AffectationEmployee = p.AffectationEmployee,
                                        Code = p.Code,
                                        Departement = d,
                                        Intitule = p.Intitule,
                                        Emploi = e,
                                        Unite = u
                                    }).ToList();
                return listPosition;
            //}
            //else
            //{
            //    var listPosition = (from p in _db.position
            //                        join d in _db.departements on p.DepartementID equals d.Id
            //                        join e in _db.emploi on p.EmploiID equals e.Id 
            //                        //into DetailsEmplois
            //                    //    from e in DetailsEmplois.DefaultIfEmpty()
            //                        where (p.companyID == currentCompanyId)
            //                        select new Position
            //                        {
            //                            Id = p.Id,
            //                            companyID = currentCompanyId,
            //                            DateCreat = p.DateCreat,
            //                            DateModif = p.DateModif,
            //                            UserCreat = p.UserCreat,
            //                            UserModif = p.UserModif,
            //                            AffectationEmployee = p.AffectationEmployee,
            //                            Code = p.Code,
            //                            Departement = d,
            //                            Intitule = p.Intitule,
            //                            Emploi = e
            //                        }).ToList();
             //   return listPosition;
           // }
        }

        public List<Position> GetAllPositionAllCompany()
        {
            return utOfWork.PositionRepository.GetAll().Include(D => D.Departement).Include(U => U.Unite).ToList();
        }

        public List<Position> GetAllPositionByCompanyId(int companyID)
        {
            return utOfWork.PositionRepository.GetMany(t => t.companyID == companyID).Include(D => D.Departement).Include(U => U.Unite).ToList();
        }

        public Position GetPositionByID(int id)
        {
            return utOfWork.PositionRepository.GetMany(d => d.Id == id).Include(D => D.Departement).Include(h => h.HierarchyPosition).Include(U => U.Unite).First();
        }

        public List<Position> GetPositionByDepartementID(int id)
        {
            return utOfWork.PositionRepository.GetMany(d => d.DepartementID == id).Include(D => D.Departement).Include(E => E.Emploi).Include(U => U.Unite).ToList();
        }

        public Position Creat(Position Position)
        {
            try
            {
                var emploi = emploiServices.GetEmploiById(Position.EmploiID.Value);
                if (emploi != null)
                {
                    var PositionsWithSameRef = GetAllPositionByReference(emploi.Reference);
                    var maxNumber = 1;
                    if (PositionsWithSameRef.Any())
                    {
                        maxNumber = PositionsWithSameRef.Max(p => int.Parse(new String(p.Code.Where(Char.IsDigit).ToArray()))) + 1;
                    }
                    if (!utOfWork.PositionRepository.GetMany(d => d.Intitule == Position.Intitule && d.DepartementID == Position.DepartementID).ToList().Any())
                    {
                        //var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
                        //int currentCompanyId = int.Parse(session[2].Value);
                        //Position.companyID = currentCompanyId;
                         Position.Code = emploi.Reference + maxNumber;
                      //  Position.Code = emploi.Reference;

                        utOfWork.PositionRepository.Add(Position);
                        utOfWork.Commit();
                        return Position;
                    }
                    else
                        return null;
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Position Edit(Position Position)
        {
            try
            {
                utOfWork.PositionRepository.Update(Position);
                utOfWork.Commit();
                return Position;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Position Delete(int PositionId)
        {
            try
            {
                var Position = utOfWork.PositionRepository.GetById(PositionId);
                if (Position != null)
                {
                    utOfWork.PositionRepository.Delete(Position);
                    utOfWork.Commit();
                    return Position;
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

        public List<Position> GetAllPositionByEmploiId(int emploiID)
        {
            return utOfWork.PositionRepository.GetMany(d => d.EmploiID == emploiID).Include(D => D.Departement).Include(U => U.Unite).ToList();
        }

        public List<Position> GetAllPositionByReference(string reference)
        {
            //var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            //int currentCompanyId = int.Parse(session[2].Value);
            //return utOfWork.PositionRepository.GetMany(p => p.Code.Contains(reference) && p.companyID == currentCompanyId).ToList();
            return utOfWork.PositionRepository.GetMany(p => p.Code.Contains(reference)).ToList();

        }

        public List<PositionViewModel> GetAllPositionByCompanyIdVM(int companyId)
        {
            var _db = Context;
            var postionList = (from p in _db.position
                               where (p.companyID == companyId)
                               select new PositionViewModel
                               {
                                   Id = p.Id,
                                   Display = p.Display
                               }).ToList();
            return postionList;
        }

        public bool CheckUnicityPositionById(string intitulePosition, int departementID, int id)
        {
            return !utOfWork.PositionRepository.GetMany(d => d.Intitule == intitulePosition && d.DepartementID == departementID && d.Id != id).Any();
        }

        public Position GetPositionIncludingEmploiById(int id)
        {
            return utOfWork.PositionRepository.GetMany(d => d.Id == id).Include(E => E.Emploi).FirstOrDefault();
        }

        public bool CheckAssignedPositionsJob(int EmploiID)
        {
            return utOfWork.PositionRepository.GetMany(a => a.EmploiID == EmploiID).Any();
        }

        public List<Position> GetAllExcludingSpecificPositions(List<int> PositionsIDList, int CompanyId)
        {
            return GetAllPositionByCompanyId(CompanyId).Where(a => !PositionsIDList.Contains(a.Id)).ToList();
        }
    }
}