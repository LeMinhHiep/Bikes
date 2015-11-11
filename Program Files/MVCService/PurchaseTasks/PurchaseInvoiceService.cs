using System.Collections.Generic;
using System.Data.Entity.Core.Objects;

using MVCModel.Models;
using MVCDTO.PurchaseTasks;
using MVCCore.Repositories.PurchaseTasks;
using MVCCore.Services.PurchaseTasks;


namespace MVCService.PurchaseTasks
{
    public class PurchaseInvoiceService : GenericWithViewDetailService<PurchaseInvoice, PurchaseInvoiceDetail, PurchaseInvoiceViewDetail, PurchaseInvoiceDTO, PurchaseInvoicePrimitiveDTO, PurchaseInvoiceDetailDTO>, IPurchaseInvoiceService
    {
        public PurchaseInvoiceService(IPurchaseInvoiceRepository purchaseInvoiceRepository)
            : base(purchaseInvoiceRepository, "PurchaseInvoicePostSaveValidate", "PurchaseInvoiceSaveRelative", "GetPurchaseInvoiceViewDetails")
        {
        }

        public override ICollection<PurchaseInvoiceViewDetail> GetViewDetails(int purchaseInvoiceID)
        {
            throw new System.ArgumentException("Invalid call GetViewDetails(id). Use GetPurchaseInvoiceViewDetails instead.", "Purchase Invoice Service");
        }

        public ICollection<PurchaseInvoiceViewDetail> GetPurchaseInvoiceViewDetails(int purchaseInvoiceID, int purchaseOrderID, int supplierID, bool isReadOnly)
        {
            ObjectParameter[] parameters = new ObjectParameter[] { new ObjectParameter("PurchaseInvoiceID", purchaseInvoiceID), new ObjectParameter("PurchaseOrderID", purchaseOrderID), new ObjectParameter("SupplierID", supplierID), new ObjectParameter("IsReadOnly", isReadOnly) };
            return this.GetViewDetails(parameters);
        }

        public override bool Save(PurchaseInvoiceDTO purchaseInvoiceDTO)
        {
            purchaseInvoiceDTO.PurchaseInvoiceViewDetails.RemoveAll(x => x.Quantity == 0);
            return base.Save(purchaseInvoiceDTO);
        }
    }
}
