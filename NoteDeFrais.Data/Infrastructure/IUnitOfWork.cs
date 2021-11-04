using NoteDeFrais.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDeFrais.Data.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        IPaysRepository PaysRepository { get; }
        ICompteComptableRepository CompteComptableRepository { get; }
        IDepensesRepository DepensesRepository { get; }
        IDeviseRepository DeviseRepository { get; }
        IDocumentsDepensesRepository DocumentsDepensesRepository { get; }
        IDocumentsNoteFraisRepository DocumentsNoteFraisRepository { get; }
        IEmployeeGroupeRepository EmployeeGroupeRepository { get; }
        IEmployeeRepository EmployeeRepository { get; }
        IFraisKilometriquesRepository FraisKilometriquesRepository { get; }
        IGroupeFraisDepenseRepository GroupeFraisDepenseRepository { get; }
        IGroupeFraisRepository GroupeFraisRepository { get; }
        IMoyenPaiementRepository MoyenPaiementRepository { get; }
        INoteFraisRepository NoteFraisRepository { get; }
        IOrdreMissionRepository OrdreMissionRepository { get; }
        ITypeDepenseRepository TypeDepenseRepository { get; }
        ITypeOrdreMissionRepository TypeOrdreMissionRepository { get; }
        IWFDocumentRepository WFDocumentRepository { get; }



    }
}
