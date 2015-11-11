using MVCBase;
using MVCBase.Enums;
using MVCModel.Models;

namespace MVCData.Helpers.SqlProgrammability.SalesTasks
{
    public class Quotation
    {
        private readonly TotalBikePortalsEntities totalBikePortalsEntities;

        public Quotation(TotalBikePortalsEntities totalBikePortalsEntities)
        {
            this.totalBikePortalsEntities = totalBikePortalsEntities;
        }

        public void RestoreProcedure()
        {
            this.GetQuotationViewDetails();
            this.QuotationSaveRelative();
            this.QuotationPostSaveValidate();

            this.QuotationEditable();

            this.QuotationInitReference();
        }




        //Van de warehouse id, code, name; can phai xem xet lai!LEMINHHIEP
        private void GetQuotationViewDetails()
        {
            string queryString;
            SqlProgrammability.StockTasks.Inventories inventories = new StockTasks.Inventories(this.totalBikePortalsEntities);
            

            queryString = " @QuotationID Int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       DECLARE     @EntryDate DateTime       DECLARE @LocationID varchar(35)      DECLARE @WarehouseIDList varchar(35)         DECLARE @CommodityIDList varchar(3999) " + "\r\n";
            queryString = queryString + "       SELECT      @EntryDate = EntryDate, @LocationID = LocationID FROM Quotations WHERE QuotationID = @QuotationID " + "\r\n";
            queryString = queryString + "       IF          @EntryDate IS NULL          SET @EntryDate = CONVERT(Datetime, '31/12/2000', 103)" + "\r\n";
            queryString = queryString + "       SELECT      @WarehouseIDList = STUFF((SELECT ',' + CAST(WarehouseID as varchar)  FROM Warehouses WHERE LocationID = @LocationID FOR XML PATH('')) ,1,1,'') " + "\r\n";
            queryString = queryString + "       SELECT      @CommodityIDList = STUFF((SELECT ',' + CAST(CommodityID as varchar)  FROM QuotationDetails WHERE QuotationID = @QuotationID FOR XML PATH('')) ,1,1,'') " + "\r\n";

            queryString = queryString + "       " + inventories.GET_WarehouseJournal_BUILD_SQL("@WarehouseJournalTable", "@EntryDate", "@EntryDate", "@WarehouseIDList", "@CommodityIDList", "0", "0") + "\r\n";

            queryString = queryString + "       SELECT      QuotationDetails.QuotationDetailID, QuotationDetails.QuotationID, Commodities.CommodityID, Commodities.Code AS CommodityCode, Commodities.Name AS CommodityName, Commodities.CommodityTypeID, " + "\r\n";
            queryString = queryString + "                   CommoditiesAvailable.WarehouseID, CommoditiesAvailable.WarehouseCode, ISNULL(CommoditiesAvailable.QuantityAvailable, 0) AS QuantityAvailable, QuotationDetails.Quantity, QuotationDetails.QuantityInvoice, QuotationDetails.ListedPrice, QuotationDetails.DiscountPercent, QuotationDetails.UnitPrice, QuotationDetails.VATPercent, QuotationDetails.GrossPrice, QuotationDetails.Amount, QuotationDetails.VATAmount, QuotationDetails.GrossAmount, QuotationDetails.IsBonus, QuotationDetails.IsWarrantyClaim, QuotationDetails.Remarks " + "\r\n";
            queryString = queryString + "       FROM        QuotationDetails INNER JOIN" + "\r\n";
            queryString = queryString + "                   Commodities ON QuotationDetails.QuotationID = @QuotationID AND QuotationDetails.CommodityID = Commodities.CommodityID LEFT JOIN" + "\r\n";
            queryString = queryString + "                  (SELECT CommodityID, MIN(WarehouseID) AS WarehouseID, MIN(WarehouseCode) AS WarehouseCode, SUM(QuantityEndREC) AS QuantityAvailable FROM @WarehouseJournalTable GROUP BY CommodityID) CommoditiesAvailable ON QuotationDetails.CommodityID = CommoditiesAvailable.CommodityID " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalBikePortalsEntities.CreateStoredProcedure("GetQuotationViewDetails", queryString);
        }

        private void QuotationSaveRelative()
        {
            string queryString = " @EntityID int, @SaveRelativeOption int " + "\r\n"; //SaveRelativeOption: 1: Update, -1:Undo
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            

            this.totalBikePortalsEntities.CreateStoredProcedure("QuotationSaveRelative", queryString);
        }

        private void QuotationPostSaveValidate()
        {
            //string[] queryArray = new string[2];

            //queryArray[0] = " SELECT TOP 1 @FoundEntity = GoodsReceipts.GoodsReceiptID FROM PurchaseOrders INNER JOIN PurchaseOrderDetails ON PurchaseOrders.PurchaseOrderID = PurchaseOrderDetails.PurchaseOrderID INNER JOIN GoodsReceiptDetails ON PurchaseOrderDetails.PurchaseOrderDetailID = GoodsReceiptDetails.PurchaseOrderDetailID INNER JOIN GoodsReceipts ON GoodsReceiptDetails.GoodsReceiptID = GoodsReceipts.GoodsReceiptID AND GoodsReceipts.EntryDate < PurchaseOrders.EntryDate ";
            //queryArray[1] = " SELECT TOP 1 @FoundEntity = PurchaseOrderID FROM PurchaseOrderDetail WHERE (ROUND(Quantity - QuantityInvoice, 0) < 0) ";

            string[] queryArray = new string[0];
            this.totalBikePortalsEntities.CreateProcedureToCheckExisting("QuotationPostSaveValidate", queryArray);
        }



        private void QuotationEditable()
        {
            string[] queryArray = new string[2];

            //queryArray[0] = " SELECT TOP 1 @FoundEntity = QuotationDetails.QuotationID FROM ServiceContracts INNER JOIN QuotationDetails ON ServiceContracts.QuotationDetailID = QuotationDetails.QuotationDetailID WHERE QuotationDetails.QuotationID = @EntityID ";
            //queryArray[1] = " SELECT TOP 1 @FoundEntity = QuotationID FROM Quotations WHERE ServiceInvoiceID = @EntityID ";

            this.totalBikePortalsEntities.CreateProcedureToCheckExisting("QuotationEditable");
        }



        private void QuotationInitReference()
        {
            SimpleInitReference simpleInitReference = new SimpleInitReference("Quotations", "QuotationID", "Reference", ModelSettingManager.ReferenceLength, ModelSettingManager.ReferencePrefix(GlobalEnums.NmvnTaskID.Quotation));
            this.totalBikePortalsEntities.CreateTrigger("QuotationInitReference", simpleInitReference.CreateQuery());
        }

    }
}
