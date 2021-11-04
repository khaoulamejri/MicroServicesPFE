using NoteDeFrais.Data;
using NoteDeFrais.Data.Infrastructure;
using NoteDeFrais.Domain.Entities;
using NoteDeFrais.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDeFrais.Services.Services
{
    public class FileNServices : IFileNServices
    {
        DatabaseFactory dbFactory = null;
        IUnitOfWork utOfWork = null;
        private readonly ApplicationDbContext Context;

        public FileNServices(ApplicationDbContext context)
        {
            Context = context;
            dbFactory = new DatabaseFactory(context);
            utOfWork = new UnitOfWork(dbFactory);
        }

      
        public List<DocumentsDepenses> GetDepenseDocumentsListByDepenseId(int depenseId)
        {
            return utOfWork.DocumentsDepensesRepository.GetMany(d => d.DepensesId == depenseId).ToList();
        }

        public DocumentsDepenses GetDepenseDocumentById(int id)
        {
            return utOfWork.DocumentsDepensesRepository.Get(a => a.Id == id);
        }

        public DocumentsDepenses DeleteDepenseDocument(DocumentsDepenses document)
        {
            try
            {
                utOfWork.DocumentsDepensesRepository.Delete(document);
                utOfWork.Commit();
                return document;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public DocumentsNoteFrais CreateDocumentsNoteFrais(DocumentsNoteFrais doc)
        {
            try
            {
                utOfWork.DocumentsNoteFraisRepository.Add(doc);
                utOfWork.Commit();
                return doc;
            }

            catch (Exception e)
            {
                return null;
            }
        }

        public DocumentsDepenses CreateDocumentsDepenses(DocumentsDepenses doc)
        {
            try
            {
                utOfWork.DocumentsDepensesRepository.Add(doc);
                utOfWork.Commit();
                return doc;
            }

            catch (Exception e)
            {
                return null;
            }
        }

    }
}
