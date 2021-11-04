using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class HierarchyPositionServices : IHierarchyPositionServices
    {
        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;
        private readonly ApplicationDbContext Context;
        private readonly ITypeHierarchyPositionsServices typeHierarchyPositionsServices;
        private readonly IPositionServices positionServices;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HierarchyPositionServices(ApplicationDbContext ctx, IHttpContextAccessor httpContextAccessor, ITypeHierarchyPositionsServices ith, IPositionServices pos)
        {
            Context = ctx;
            _httpContextAccessor = httpContextAccessor;
            typeHierarchyPositionsServices = ith;
            positionServices = pos;
            dbFactory = new DatabaseFactory(ctx);
            utOfWork = new UnitOfWork(dbFactory);
        }

        public List<HierarchyPosition> GetAllHierarchyPosition()
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            return utOfWork.HierarchyPositionRepository.GetMany(d => d.companyID == currentCompanyId).Include(x => x.PositionSupID).Include(p => p.PositionID).Include(y => y.TypeHierarchyPosition).ToList();
        }

        public HierarchyPosition GetHierarchyPositionByID(int id)
        {
            return utOfWork.HierarchyPositionRepository.GetMany(d => d.Id == id).Include(y => y.TypeHierarchyPosition).FirstOrDefault();
        }

        public List<HierarchyPosition> GetHierarchyPositionByPositionId(int id)
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            var position = positionServices.GetPositionByID(id);
            if (position != null)
            {
                if (currentCompanyId == position.companyID)
                {
                    return utOfWork.HierarchyPositionRepository.GetMany(d => d.PositionID == id).Include(x => x.Position).Include(y => y.TypeHierarchyPosition).ToList();
                }
                else
                {
                    return utOfWork.HierarchyPositionRepository.GetMany(d => d.PositionID == id).Include(x => x.Position).Include(y => y.TypeHierarchyPosition).ToList();
                }
            }
            else
                return null;
        }

        public HierarchyPosition Create(HierarchyPosition HierarchyPosition)
        {
            try
            {
                utOfWork.HierarchyPositionRepository.Add(HierarchyPosition);
                utOfWork.Commit();
                return HierarchyPosition;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public string Edit(HierarchyPosition HierarchyPosition)
        {
            try
            {
                if (!utOfWork.HierarchyPositionRepository.GetMany(d => d.PositionID == HierarchyPosition.PositionID && d.TypeHierarchyPosition.Id == HierarchyPosition.TypeHierarchyPositionID && d.Id != HierarchyPosition.Id).Any())
                {
                    utOfWork.HierarchyPositionRepository.Update(HierarchyPosition);
                    utOfWork.Commit();
                    return "ok";
                }
                else
                {
                    return "no";
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public HierarchyPosition Delete(int HierarchyPositionId)
        {
            try
            {
                HierarchyPosition HierarchyPosition = utOfWork.HierarchyPositionRepository.Get(a => a.Id == HierarchyPositionId);
                if (HierarchyPosition != null)
                {
                    utOfWork.HierarchyPositionRepository.Delete(HierarchyPosition);
                    utOfWork.Commit();
                    return HierarchyPosition;
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

        public List<TypeHierarchyPosition> SelectListItemTypeHierarchyPositions()
        {
            return typeHierarchyPositionsServices.GetAllTypeHierarchyPosition();
        }

        public List<Position> SelectListItemPositions()
        {
            return positionServices.GetAllPosition();
        }

        //public List<SelectListItem> SelectListItemPositionsByCompany(int companyID)
        //{
        //    var listPos = positionServices.GetAllPositionByCompanyId(companyID);
        //    if (listPos != null)
        //    {
        //        var PosByCompList = new List<SelectListItem>();
        //        Parallel.ForEach(listPos, item =>
        //        {
        //            PosByCompList.Add(new SelectListItem()
        //            {
        //                Text = item.Display,
        //                Value = item.Id.ToString()
        //            });
        //        });
        //        return PosByCompList;
        //    }
        //    else
        //        return null;
        //}

        public List<int> GetListPosInf(int PositionID, int type, List<int> test, bool SupInclut = true)
        {
            if (test.Count == 0 && SupInclut) test.Add(PositionID);
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            var IntermList = utOfWork.HierarchyPositionRepository.GetMany(d => d.PositionSupID == PositionID && d.TypeHierarchyPositionID == type && d.companyID == currentCompanyId).Include(x => x.Position).Include(y => y.TypeHierarchyPosition).Include(z => z.PositionSup).ToList();
            if (IntermList != null)
            {
                foreach (var pos in IntermList)
                {
                    test.Add(pos.PositionID);
                    test = GetListPosInf(pos.PositionID, type, test, SupInclut);
                }
            }
            return test;
        }

        public List<int> GetListPosInfByCMPSelected(int PositionID, int type, List<int> test, bool SupInclut = true)
        {
            if (test.Count == 0 && SupInclut) test.Add(PositionID);
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            var IntermList = utOfWork.HierarchyPositionRepository.GetMany(d => d.PositionSupID == PositionID && d.TypeHierarchyPositionID == type && d.companyID == currentCompanyId).Include(x => x.Position).Include(y => y.TypeHierarchyPosition).Include(z => z.PositionSup).ToList();

            foreach (var pos in IntermList)
            {
                test.Add(pos.PositionID);
                test = GetListPosInfByCMPSelected(pos.PositionID, type, test, SupInclut);
            }
            return test;
        }

        public List<int> GetListPosInfWithoutCMP(int PositionID, int type, List<int> test, bool SupInclut = true)
        {
            if (test.Count == 0 && SupInclut) test.Add(PositionID);
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            var IntermList = utOfWork.HierarchyPositionRepository.GetMany(d => d.PositionSupID == PositionID).Include(x => x.Position).Include(y => y.TypeHierarchyPosition).Include(z => z.PositionSup).ToList();
            if (IntermList != null)
            {
                foreach (var pos in IntermList)
                {
                    test.Add(pos.PositionID);
                    test = GetListPosInf(pos.PositionID, type, test, SupInclut);
                }
            }
            return test;
        }

        public List<Position> GetListPositionInferieur(int PositionID, int type, bool SupInclut = true)
        {
            var sonList = new List<int>();
            var posNodeList = GetListPosInf(PositionID, type, sonList, SupInclut);
            var posList = new List<Position>();
            if (posNodeList != null)
            {
                foreach (int pos in posNodeList)
                {
                    posList.Add(utOfWork.PositionRepository.Get(a => a.Id == pos));
                }
            }
            return posList;
        }

        public List<Position> GetListPositionInferieurByCMPSelected(int PositionID, int type, bool SupInclut = true)
        {
            var sonList = new List<int>();
            var posNodeList = GetListPosInfByCMPSelected(PositionID, type, sonList, SupInclut);
            var posList = new List<Position>();
            if (posNodeList != null)
            {
                foreach (int pos in posNodeList)
                {
                    posList.Add(utOfWork.PositionRepository.Get(a => a.Id == pos));
                }
            }
            return posList;
        }

        public List<Position> GetListPositionInferieurWithoutCMP(int PositionID, int type, bool SupInclut = true)
        {
            var sonList = new List<int>();
            var posNodeList = GetListPosInfWithoutCMP(PositionID, type, sonList, SupInclut);
            var posList = new List<Position>();
            if (posNodeList != null)
            {
                foreach (int pos in posNodeList)
                {
                    posList.Add(utOfWork.PositionRepository.Get(a => a.Id == pos));
                }
            }
            return posList;
        }

        public List<SelectListItem> SelectListItemPositionsByCompany(int companyID)
        {
            throw new NotImplementedException();
        }
    }
}