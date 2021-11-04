using NoteDeFrais.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDeFrais.Services.IServices
{
 public   interface IFileNServices
    {
       

        List<DocumentsDepenses> GetDepenseDocumentsListByDepenseId(int depenseId);
        DocumentsDepenses GetDepenseDocumentById(int id);
        DocumentsDepenses DeleteDepenseDocument(DocumentsDepenses document);
        DocumentsNoteFrais CreateDocumentsNoteFrais(DocumentsNoteFrais doc);
        DocumentsDepenses CreateDocumentsDepenses(DocumentsDepenses doc);
       
    }
}
