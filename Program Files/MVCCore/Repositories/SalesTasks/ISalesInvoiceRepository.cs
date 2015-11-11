using System.Linq;
using System.Collections.Generic;

using MVCModel.Models;
using MVCBase.Enums;


namespace MVCCore.Repositories.SalesTasks
{
    public interface ISalesInvoiceRepository : IGenericWithDetailRepository<SalesInvoice, SalesInvoiceDetail>
    {
    }

    public interface IVehiclesInvoiceRepository : ISalesInvoiceRepository
    {
        IQueryable<SalesInvoiceDetail> DetailLoading(string aspUserID, GlobalEnums.NmvnTaskID nmvnTaskID);//for Loading (09/07/2015) - let review and optimize Loading laster
    }

    public interface IPartsInvoiceRepository : ISalesInvoiceRepository
    {
    }

    public interface IServicesInvoiceRepository : ISalesInvoiceRepository
    {
        IList<SalesInvoice> GetActiveServiceInvoices(int locationID, int? serviceInvoiceID, string licensePlate, int isFinished);
        IList<RelatedPartsInvoiceValue> GetRelatedPartsInvoiceValue(int serviceInvoiceID);
    }

}
