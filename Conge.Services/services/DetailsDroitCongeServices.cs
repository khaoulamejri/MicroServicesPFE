using Conge.Data;
using Conge.Data.Common;
using Conge.Data.Infrastructure;
using Conge.Domain.Entities;
using Conge.Domain.ViewsModels;
using Conge.Services.Iservices;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static Conge.Data.Common.DTOS;

namespace Conge.Services.services
{
    public class DetailsDroitCongeServices : IDetailsDroitCongeServices
    {
        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;
        private readonly ApplicationDbContext Context;
          private readonly IHttpContextAccessor _httpContextAccessor;
   //     private readonly IEmployeeServices _employeeServices;
        public DetailsDroitCongeServices(ApplicationDbContext ctx, IHttpContextAccessor httpContextAccessor)
        {
            Context = ctx;
            _httpContextAccessor = httpContextAccessor;
            dbFactory = new DatabaseFactory(ctx);
            utOfWork = new UnitOfWork(dbFactory);
        }

        public List<Details_DroitConge> GetDetailsDroitCongeByDroitCongeId(int id)
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);

            return utOfWork.DetailsDroitCongeRepository.GetMany(d => d.companyID == currentCompanyId && d.DroitCongeId == id).ToList();
        }

        public Details_DroitConge Create(Details_DroitConge DetDroitConge)
        {
            try
            {
                utOfWork.DetailsDroitCongeRepository.Add(DetDroitConge);
                utOfWork.Commit();
                return DetDroitConge;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Details_DroitConge Edit(Details_DroitConge DetDroitConge)
        {

            try
            {
                utOfWork.DetailsDroitCongeRepository.Update(DetDroitConge);
                utOfWork.Commit();
                return DetDroitConge;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Details_DroitConge Delete(int DetDroitCongeId)
        {
            try
            {
                Details_DroitConge DetDroitConge = GetDetailsDroitCongeByID(DetDroitCongeId);
                if (DetDroitConge != null)
                {
                    utOfWork.DetailsDroitCongeRepository.Delete(DetDroitConge);
                    utOfWork.Commit();
                    return DetDroitConge;
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

        public Details_DroitConge GetDetailsDroitCongeByID(int id)
        {
            return utOfWork.DetailsDroitCongeRepository.Get(a => a.Id == id);
        }

        public bool deleteAllDetailsDroitConge(int id)
        {
            bool result = true;
            List<Details_DroitConge> listDetDroit = GetDetailsDroitCongeByDroitCongeId(id);
            using (var scope = new TransactionScope(TransactionScopeOption.Required,
            new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                try
                {
                    foreach (var item in listDetDroit)
                    {
                        Details_DroitConge obj = Delete(item.Id);
                        if (obj == null)
                            result = false;
                    }
                    scope.Complete();


                }
                finally
                {
                    scope.Dispose();
                }
            }
            return result;
        }

        public DetailsDroitCongeViewModel GetDetailsDroitVMCongeByDroitCongeId(int id)
        {
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            var listeDeroitConge = utOfWork.DetailsDroitCongeRepository.Get(d => d.companyID == currentCompanyId && d.DroitCongeId == id);
           var employe = utOfWork.EmployeeRepository.Get(a => a.Id == listeDeroitConge.IdEmployee);
          var DetailsDroitCongeViewModel = listeDeroitConge.AssDto(employe.Nom, employe.Prenom);

            return DetailsDroitCongeViewModel;
        }
        public List<DetailsDroitCongeViewModel> GetDetailsDroitVMSCongeByDroitCongeIdtest(int id)
        {
            List<DetailsDroitCongeViewModel> listDetails = new List<DetailsDroitCongeViewModel>();

        DetailsDroitCongeViewModel mvtlist = GetDetailsDroitVMCongeByDroitCongeId(id);
            listDetails.Add(mvtlist);
            return listDetails;

        }
        public List<DetailsDroitCongeViewModell> GetDetailsDroitVMSCongeByDroitCongeId(int id)
        {

            //var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            //int currentCompanyId = int.Parse(session[2].Value);
            //var listeDeroitConges = utOfWork.DetailsDroitCongeRepository.GetMany(d => d.companyID == currentCompanyId && d.DroitCongeId == id);
            //var listeDeroitConge = utOfWork.DetailsDroitCongeRepository.Get(d => d.companyID == currentCompanyId && d.DroitCongeId == id);
            //var employes = utOfWork.EmployeeRepository.GetMany(a => a.Id == listeDeroitConge.IdEmployee);
            ////var employe = employes.Select(a => a.Id == listeDeroitConge.IdEmployee);

            //var employe = utOfWork.EmployeeRepository.Get(a => a.Id == listeDeroitConge.IdEmployee);
            ////  List<DetailsDroitCongeViewModel> DetailsDroitCongeViewModel = listeDeroitConges.AssDto(employe.Nom, employe.Prenom).toList();
            //var DetailsDroitCongeViewModel = listeDeroitConge.AssDto(employe.Nom, employe.Prenom);
            //   var Listt = new List<DetailsDroitCongeViewModel>();
            //  List<DetailsDroitCongeViewModel> listDetails.Add(DetailsDroitCongeViewModel).ToList();
            // List<DetailsDroitCongeViewModel> listDetails = new List<DetailsDroitCongeViewModel>();
            //  listDetails.Add(new DetailsDroitCongeViewModel(int Id, DateTime ? DateModif, DateTime DateCreat, string UserCreat, string UserModif, string Commentaire,  int companyID,  float Droit, int DroitCongeId, float DroitMisAJour, int IdEmployee, string Nom, string Prenom).ToList());
            // List<DetailsDroitCongeViewModel> listDetails = DetailsDroitCongeViewModel.
            //var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            //int currentCompanyId = int.Parse(session[2].Value);
            //var listeDeroitConges = utOfWork.DetailsDroitCongeRepository.GetMany(d => d.companyID == currentCompanyId && d.DroitCongeId == id);
            //var itemIds = listeDeroitConges.Select(item => item.IdEmployee);
            //var catalogItemEntities = utOfWork.EmployeeRepository.GetMany(item => itemIds.Contains(item.Id));
            //var inventoryItemDtos = listeDeroitConges.Select(inventoryItem =>
            //{
            //    var catalogItem = catalogItemEntities.Single(catalogItem => catalogItem.Id == inventoryItem.IdEmployee);
            //    //return inventoryItem.AssDto(catalogItem.Nom, catalogItem.Prenom);
            //}).ToList(); ;
            //return (List<DetailsDroitCongeViewModel>)inventoryItemDtos;
            /***********/
            //var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            //int currentCompanyId = int.Parse(session[2].Value);
            //var listeDeroitConge = utOfWork.DetailsDroitCongeRepository.Get(d => d.companyID == currentCompanyId && d.DroitCongeId == id);
            //var employe = utOfWork.EmployeeRepository.Get(a => a.Id == listeDeroitConge.IdEmployee);
            //var DetailsDroitCon = listeDeroitConge.AssDto(employe.Nom, employe.Prenom);
            //List < DetailsDroitCongeViewModel > listDetails = new List<DetailsDroitCongeViewModel>();
            //listDetails.Add(DetailsDroitCon);
            //return listDetails;
            /************/
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            var listeDeroitConge = utOfWork.DetailsDroitCongeRepository.Get(d => d.companyID == currentCompanyId && d.DroitCongeId == id);
            var employe = utOfWork.EmployeeRepository.Get(a => a.Id == listeDeroitConge.IdEmployee);
            List<DetailsDroitCongeViewModell> listDetails = new List<DetailsDroitCongeViewModell>();
                listDetails.Select( item => new DetailsDroitCongeViewModell
                {
              Id = listeDeroitConge.Id,
                DateModif = listeDeroitConge.DateModif,
                DateCreat = listeDeroitConge.DateCreat,
                UserCreat = listeDeroitConge.UserCreat,
                UserModif = listeDeroitConge.UserModif,
                Commentaire = listeDeroitConge.Commentaire,
                companyID = listeDeroitConge.companyID,
                Droit = listeDeroitConge.Droit,
                DroitCongeId = listeDeroitConge.DroitCongeId,
                DroitMisAJour = listeDeroitConge.DroitMisAJour,
                IdEmployee = listeDeroitConge.IdEmployee,
                Nom = employe.Nom,
                Prenom = employe.Prenom



            }).ToList();
            return listDetails;

        }
    }
}
