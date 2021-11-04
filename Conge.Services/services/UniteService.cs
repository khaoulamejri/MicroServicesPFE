using Conge.Data;
using Conge.Data.Infrastructure;
using Conge.Domain.Entities;
using Conge.Services.Iservices;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conge.Services.services
{
    public class UniteService : IUniteService
    {
        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;
        private readonly ApplicationDbContext Context;

        public UniteService(ApplicationDbContext ctx)
        {
            Context = ctx;
            dbFactory = new DatabaseFactory(ctx);
            utOfWork = new UnitOfWork(dbFactory);
        }

        public List<Unite> GetAllUnite()
        {
            return utOfWork.UniteRepository.GetAll().ToList();
        }

        public Unite GetUniteByID(int id)
        {
            return utOfWork.UniteRepository.Get(a => a.Id == id);
        }

        public Unite Create(Unite Unite)
        {
            try
            {
                utOfWork.UniteRepository.Add(Unite);
                utOfWork.Commit();
                return Unite;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Unite Edit(Unite Unite)
        {
            try
            {
                utOfWork.UniteRepository.Update(Unite);
                utOfWork.Commit();
                return Unite;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Unite Delete(int UniteId)
        {
            try
            {
                var Unite = GetUniteByID(UniteId);
                if (Unite != null)
                {
                    utOfWork.UniteRepository.Delete(Unite);
                    utOfWork.Commit();
                    return Unite;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<SelectListItem> SelectListUnite()
        {
            var AllUniteList = GetAllUnite();
            if (AllUniteList != null)
            {
                var UniteList = new List<SelectListItem>();
                UniteList.Add(new SelectListItem()
                {
                    Text = "--- TOUS---",
                    Value = "0"
                });
                Parallel.ForEach(AllUniteList, item =>
                {
                    UniteList.Add(new SelectListItem()
                    {
                        Text = item.Display,
                        Value = item.Id.ToString()
                    });
                });
                return UniteList;
            }
            else
                return null;
        }

        public bool CheckUnicityUnite(string code)
        {
            return !utOfWork.UniteRepository.GetMany(a => a.Code == code).Any();
        }

        public bool CheckUnicityUniteByID(string code, int id)
        {
            return !utOfWork.UniteRepository.GetMany(a => a.Code == code && a.Id != id).Any();
        }
    }
}