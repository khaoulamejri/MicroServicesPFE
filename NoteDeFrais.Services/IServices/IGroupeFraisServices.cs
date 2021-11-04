using NoteDeFrais.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDeFrais.Services.IServices
{
    public interface IGroupeFraisServices
    {
        GroupeFrais GetGroupeFraisByID(int id);
        List<GroupeFrais> GetAllGroupeFrais();
        GroupeFrais Create(GroupeFrais groupeFrais);
        GroupeFrais Edit(GroupeFrais groupeFrais);
        GroupeFrais Delete(int groupeFraisId);
        bool checkUnicity(GroupeFrais groupeFrais);

    }
}
