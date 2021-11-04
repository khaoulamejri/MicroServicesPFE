using Conge.Domain.Entities;
using Conge.Domain.ViewsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Conge.Data.Common.DTOS;

namespace Conge.Services.Iservices
{
    public interface IDetailsDroitCongeServices
    {
        List<Details_DroitConge> GetDetailsDroitCongeByDroitCongeId(int id);
        DetailsDroitCongeViewModel GetDetailsDroitVMCongeByDroitCongeId(int id);
        List<DetailsDroitCongeViewModell> GetDetailsDroitVMSCongeByDroitCongeId(int id);

        Details_DroitConge Create(Details_DroitConge DetDroitConge);
        Details_DroitConge Edit(Details_DroitConge DetDroitConge);
        Details_DroitConge Delete(int DetDroitCongeId);
        Details_DroitConge GetDetailsDroitCongeByID(int id);
        bool deleteAllDetailsDroitConge(int id);

    }
}
