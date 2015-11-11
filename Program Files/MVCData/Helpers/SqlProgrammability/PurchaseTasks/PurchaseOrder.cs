using MVCBase;
using MVCBase.Enums;
using MVCModel.Models;
using MVCData.Helpers.SqlProgrammability;

namespace MVCData.Helpers.SqlProgrammability.PurchaseTasks
{
    public class PurchaseOrder
    {
        private readonly TotalBikePortalsEntities totalBikePortalsEntities;

        public PurchaseOrder(TotalBikePortalsEntities totalBikePortalsEntities)
        {
            this.totalBikePortalsEntities = totalBikePortalsEntities;
        }

        public void RestoreProcedure()
        {
            this.PurchaseOrderEditable();

            this.PurchaseOrderInitReference();
        }

        private void PurchaseOrderEditable()
        {
            string[] queryArray = new string[1];

            queryArray[0] = " SELECT TOP 1 @FoundEntity = PurchaseOrderID FROM PurchaseInvoiceDetails WHERE PurchaseOrderID = @EntityID ";

            this.totalBikePortalsEntities.CreateProcedureToCheckExisting("PurchaseOrderEditable", queryArray);
        }
        
        private void PurchaseOrderInitReference()
        {
            SimpleInitReference simpleInitReference = new SimpleInitReference("PurchaseOrders", "PurchaseOrderID", "Reference", ModelSettingManager.ReferenceLength, ModelSettingManager.ReferencePrefix(GlobalEnums.NmvnTaskID.PurchaseOrder));
            this.totalBikePortalsEntities.CreateTrigger("PurchaseOrderInitReference", simpleInitReference.CreateQuery());
        }

    }
}
