using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Domain.Entities;

namespace UserApp.Services.IServices
{
    public interface IComposantService
    {
        Composant Create(Composant Composant);
        Composant Edit(Composant Composant);
        string Delete(int ComposantId);
        Composant GetComposantByID(int ComposantId);
        List<Composant> GetAllComposants();
        Composant GetComposantByName(string name);
        bool CheckCombinationExist(string type, string action, string module);
        bool CheckComponentExist(string name);
        bool CheckComponentExistByID(string name, int id);
        bool CheckCombinationExistByID(string type, string action, string module, int id);
    }
}
