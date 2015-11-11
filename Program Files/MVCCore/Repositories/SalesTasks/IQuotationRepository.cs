using System.Collections.Generic;

using MVCModel.Models;


namespace MVCCore.Repositories.SalesTasks
{
    public interface IQuotationRepository : IGenericWithDetailRepository<Quotation, QuotationDetail>
    {
        IList<Quotation> GetActiveQuotations(int? quotationID, string searchQuotation, int isFinished);
    }
}
