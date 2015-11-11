using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

using MVCBase.Enums;
using MVCModel.Models;
using MVCCore.Repositories.SalesTasks;


namespace MVCData.Repositories.SalesTasks
{
    public class QuotationRepository : GenericWithDetailRepository<Quotation, QuotationDetail>, IQuotationRepository
    {
        public QuotationRepository(TotalBikePortalsEntities totalBikePortalsEntities)
            : base(totalBikePortalsEntities, "QuotationEditable", "QuotationDeletable", "QuotationApprovable")
        {
        }

        public IList<Quotation> GetActiveQuotations(int? quotationID, string searchQuotation, int isFinished)
        {
            this.TotalBikePortalsEntities.Configuration.ProxyCreationEnabled = false;
            List<Quotation> Quotations = this.TotalBikePortalsEntities.Quotations.Include(c => c.Customer).Include(t => t.Customer.EntireTerritory).Include(sc => sc.ServiceContract.Commodity).Where(w => w.InActive == false && (w.QuotationID == quotationID || (isFinished == -1 || (isFinished == 0 && !w.IsFinished) || (isFinished == 1 && w.IsFinished))) && (searchQuotation == "" || w.ServiceContract.LicensePlate.Contains(searchQuotation) || w.ServiceContract.ChassisCode.Contains(searchQuotation) || w.ServiceContract.EngineCode.Contains(searchQuotation))).ToList();
            this.TotalBikePortalsEntities.Configuration.ProxyCreationEnabled = true;

            return Quotations;
        }

    }
}
