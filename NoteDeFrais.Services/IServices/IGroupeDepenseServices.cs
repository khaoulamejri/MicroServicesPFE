using NoteDeFrais.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDeFrais.Services.IServices
{
    public interface IGroupeDepenseServices
    {
        GroupeFraisDepense GetGroupeFraisDepenseByID(int id);
        List<GroupeFraisDepense> GetAllGroupeFraisDepense();
        GroupeFraisDepense Create(GroupeFraisDepense groupeFraisDepense);
        GroupeFraisDepense Edit(GroupeFraisDepense groupeFraisDepense);
        GroupeFraisDepense Delete(int groupeFraisDepenseId);
        List<TypeDepense> GetAllTypeDepenseByGroupeId(int id);
        List<GroupeFraisDepense> GetAllGroupeDepenseByGroupeId(int id);
        GroupeFraisDepense GetGroupefraisByTypeDepense(int idTypeDepense, int groupeFraisID);
        bool checkUnicity(GroupeFraisDepense groupeFraisDepense);
    }
}
