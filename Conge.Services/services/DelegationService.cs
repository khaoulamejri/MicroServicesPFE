using Conge.Data;
using Conge.Data.Infrastructure;
using Conge.Domain.Entities;
using Conge.Domain.Enum;
using Conge.Services.Iservices;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conge.Services.services
{
    public class DelegationService : IDelegationService
    {
        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;
        private readonly ApplicationDbContext Context;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public DelegationService(ApplicationDbContext ctx, IHttpContextAccessor httpContextAccessor)
        {
            Context = ctx;
            _httpContextAccessor = httpContextAccessor;
            dbFactory = new DatabaseFactory(ctx);
            utOfWork = new UnitOfWork(dbFactory);


        }

        public List<Delegation> GetAllDelegations()
        {
            // return utOfWork.DelegationRepository.GetAll().ToList();
            var session = _httpContextAccessor.HttpContext.User.Claims.ToList();
            int currentCompanyId = int.Parse(session[2].Value);
            return utOfWork.DelegationRepository.GetMany(d => d.companyID == currentCompanyId).ToList();
        }

        public Delegation GetDelegationById(int id)
        {
            return utOfWork.DelegationRepository.GetById(id);
        }

        public Delegation GetDelegationByTitre(int id)
        {
            return utOfWork.DelegationRepository.Get(a => a.TitreId == id);
        }

        public List<Delegation> GetDelegationByEmployeeId(int id)
        {
            return utOfWork.DelegationRepository.GetMany(d => d.IdEmployee == id).ToList();
        }

        public List<Delegation> GetDelegationByRemplaçantId(int id)
        {
            
         //   return utOfWork.DelegationRepository.GetMany(d => d.IdRemplacant == id && d.IdRemplacant != d.IdEmployee).ToList();
            return utOfWork.DelegationRepository.GetMany(d => d.IdRemplacant == id).ToList();
        }

        public bool hasDelegation(int idRemplacant)
        {
            DateTime date = DateTime.UtcNow;
            var listDelegation = utOfWork.DelegationRepository.GetMany(d => d.DateDebut <= date && date < d.DateFin && d.IdRemplacant == idRemplacant && d.Statut != StatusDelegation.annuler).ToList();
            return listDelegation.Any();
        }

        public int getSubstituter(int employee)
        {
            int idSub = 0;
            DateTime date = DateTime.UtcNow;
            var Delegation = utOfWork.DelegationRepository.Get(d => d.DateDebut <= date && date < d.DateFin && d.IdEmployee == employee && d.Statut != StatusDelegation.annuler);
            if (Delegation != null)
                idSub = Delegation.IdRemplacant;
            return idSub;
        }

        public Delegation getDelegation(int employee)
        {
            DateTime date = DateTime.UtcNow;
            var Delegation = utOfWork.DelegationRepository.Get(d => d.DateDebut <= date && date < d.DateFin && d.IdEmployee == employee && d.Statut != StatusDelegation.annuler);
            return Delegation;
        }

        public Delegation getDelegationByRemplacant(int idRemplacant)
        {
            DateTime date = DateTime.UtcNow;
            var Delegation = utOfWork.DelegationRepository.Get(d => d.DateDebut <= date && date < d.DateFin && d.IdRemplacant == idRemplacant && d.Statut != StatusDelegation.annuler);
            return Delegation;
        }

        public int getDelegator(int remplacant)
        {
            int idDelegator = 0;
            DateTime date = DateTime.UtcNow;
            var Delegation = utOfWork.DelegationRepository.Get(d => d.DateDebut <= date && date < d.DateFin && d.IdRemplacant == remplacant && d.Statut != StatusDelegation.annuler);
            if (Delegation != null)
                idDelegator = Delegation.IdEmployee;
            return idDelegator;
        }

        public Delegation Create(Delegation delegation)
        {
            try
            {
                utOfWork.DelegationRepository.Add(delegation);
                utOfWork.Commit();
                return delegation;

            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Delegation Edit(Delegation delegation)
        {

            try
            {
                utOfWork.DelegationRepository.Update(delegation);
                utOfWork.Commit();
                return delegation;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
