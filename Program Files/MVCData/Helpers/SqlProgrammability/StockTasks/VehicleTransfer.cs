using MVCBase;
using MVCBase.Enums;
using MVCModel.Models;

namespace MVCData.Helpers.SqlProgrammability.StockTasks
{
    public class VehicleTransfer
    {
        private readonly TotalBikePortalsEntities totalBikePortalsEntities;

        public VehicleTransfer(TotalBikePortalsEntities totalBikePortalsEntities)
        {
            this.totalBikePortalsEntities = totalBikePortalsEntities;
        }

        public void RestoreProcedure()
        {
            this.GetVehicleTransferViewDetails();
            this.GetPendingVehicleTransferOrders();

            this.StockTransferUpdateTransferOrder();

            this.VehicleTransferSaveRelative();
            this.VehicleTransferPostSaveValidate();

            this.StockTransferEditable();

            this.StockTransferInitReference();
        }

       
            

        private void GetVehicleTransferViewDetails()
        {
            string queryString;

            queryString = " @StockTransferID Int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      StockTransferDetails.StockTransferDetailID, StockTransferDetails.StockTransferID, StockTransferDetails.TransferOrderDetailID, StockTransferDetails.GoodsReceiptDetailID, StockTransferDetails.SupplierID, Commodities.CommodityID, Commodities.Code AS CommodityCode, Commodities.Name AS CommodityName, Commodities.CommodityTypeID, Warehouses.WarehouseID, Warehouses.Code AS WarehouseCode, GoodsReceiptDetails.ChassisCode, GoodsReceiptDetails.EngineCode, GoodsReceiptDetails.ColorCode, " + "\r\n";
            queryString = queryString + "                   ROUND(GoodsReceiptDetails.Quantity - GoodsReceiptDetails.QuantityIssue + StockTransferDetails.Quantity, 0) AS QuantityAvailable, StockTransferDetails.Quantity, StockTransferDetails.Remarks" + "\r\n";
            queryString = queryString + "       FROM        StockTransferDetails INNER JOIN" + "\r\n";
            queryString = queryString + "                   GoodsReceiptDetails ON StockTransferDetails.StockTransferID = @StockTransferID AND StockTransferDetails.GoodsReceiptDetailID = GoodsReceiptDetails.GoodsReceiptDetailID INNER JOIN" + "\r\n";
            queryString = queryString + "                   Commodities ON GoodsReceiptDetails.CommodityID = Commodities.CommodityID INNER JOIN" + "\r\n";
            queryString = queryString + "                   Warehouses ON GoodsReceiptDetails.WarehouseID = Warehouses.WarehouseID" + "\r\n";
            queryString = queryString + "       " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalBikePortalsEntities.CreateStoredProcedure("GetVehicleTransferViewDetails", queryString);
        }


        private void GetPendingVehicleTransferOrders()
        {
            string queryString;

            queryString = " @LocationID Int, @TransferOrderID Int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      TransferOrderDetails.TransferOrderDetailID, GoodsReceiptDetails.GoodsReceiptDetailID, GoodsReceiptDetails.SupplierID, Commodities.CommodityID, Commodities.Code AS CommodityCode, Commodities.Name AS CommodityName, Commodities.CommodityTypeID, Warehouses.WarehouseID, Warehouses.Code AS WarehouseCode, GoodsReceiptDetails.ChassisCode, GoodsReceiptDetails.EngineCode, GoodsReceiptDetails.ColorCode, " + "\r\n";
            queryString = queryString + "                   ROUND(TransferOrderDetails.Quantity - TransferOrderDetails.QuantityTransfer, " + GlobalEnums.rndQuantity + ") AS QuantityOrderPending, ROUND(ISNULL(GoodsReceiptDetails.Quantity - GoodsReceiptDetails.QuantityIssue, 0), " + GlobalEnums.rndQuantity + ") AS QuantityAvailable, TransferOrderDetails.Remarks, CAST(IIF(TransferOrderDetails.Remarks = GoodsReceiptDetails.ChassisCode + '#' + GoodsReceiptDetails.EngineCode AND ROUND(GoodsReceiptDetails.Quantity - GoodsReceiptDetails.QuantityIssue, " + GlobalEnums.rndQuantity + ") > 0, 1, 0) AS bit) AS IsSelected" + "\r\n";
            queryString = queryString + "       FROM        TransferOrderDetails INNER JOIN" + "\r\n";
            queryString = queryString + "                   Warehouses ON TransferOrderDetails.TransferOrderID = @TransferOrderID AND TransferOrderDetails.CommodityTypeID = " + (int)GlobalEnums.CommodityTypeID.Vehicles + " AND ROUND(TransferOrderDetails.Quantity - TransferOrderDetails.QuantityTransfer, " + GlobalEnums.rndQuantity + ") > 0 AND TransferOrderDetails.WarehouseID = Warehouses.WarehouseID AND Warehouses.LocationID = @LocationID INNER JOIN" + "\r\n";
            queryString = queryString + "                   Commodities ON TransferOrderDetails.CommodityID = Commodities.CommodityID LEFT JOIN" + "\r\n";
            queryString = queryString + "                   GoodsReceiptDetails ON ROUND(GoodsReceiptDetails.Quantity - GoodsReceiptDetails.QuantityIssue, " + GlobalEnums.rndQuantity + ") > 0 AND TransferOrderDetails.WarehouseID = GoodsReceiptDetails.WarehouseID AND TransferOrderDetails.CommodityID = GoodsReceiptDetails.CommodityID " + "\r\n";
            
            queryString = queryString + "       " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalBikePortalsEntities.CreateStoredProcedure("GetPendingVehicleTransferOrders", queryString);
        }

        private void StockTransferUpdateTransferOrder()
        {
            string queryString = " @EntityID int, @SaveRelativeOption int " + "\r\n"; //SaveRelativeOption: 1: Update, -1:Undo
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       UPDATE      TransferOrderDetails" + "\r\n";
            queryString = queryString + "       SET         QuantityTransfer = ROUND(TransferOrderDetails.QuantityTransfer + StockTransferDetails.Quantity * @SaveRelativeOption, 0)" + "\r\n";
            queryString = queryString + "       FROM       (SELECT TransferOrderDetailID, SUM(Quantity) AS Quantity FROM StockTransferDetails WHERE StockTransferID = @EntityID AND TransferOrderDetailID IS NOT NULL GROUP BY TransferOrderDetailID) StockTransferDetails INNER JOIN" + "\r\n";
            queryString = queryString + "                   TransferOrderDetails ON StockTransferDetails.TransferOrderDetailID = TransferOrderDetails.TransferOrderDetailID" + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalBikePortalsEntities.CreateStoredProcedure("StockTransferUpdateTransferOrder", queryString);
        }

        private void VehicleTransferSaveRelative()
        {
            string queryString = " @EntityID int, @SaveRelativeOption int " + "\r\n"; //SaveRelativeOption: 1: Update, -1:Undo
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       EXEC        StockTransferUpdateTransferOrder @EntityID, @SaveRelativeOption " + "\r\n";

            queryString = queryString + "       UPDATE      GoodsReceiptDetails" + "\r\n";
            queryString = queryString + "       SET         QuantityIssue = ROUND(GoodsReceiptDetails.QuantityIssue + StockTransferDetails.Quantity * @SaveRelativeOption, 0)" + "\r\n";
            queryString = queryString + "       FROM       (SELECT GoodsReceiptDetailID, SUM(Quantity) AS Quantity FROM StockTransferDetails WHERE StockTransferID = @EntityID GROUP BY GoodsReceiptDetailID) StockTransferDetails INNER JOIN" + "\r\n";
            queryString = queryString + "                   GoodsReceiptDetails ON StockTransferDetails.GoodsReceiptDetailID = GoodsReceiptDetails.GoodsReceiptDetailID" + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalBikePortalsEntities.CreateStoredProcedure("VehicleTransferSaveRelative", queryString);

        }

        private void VehicleTransferPostSaveValidate()
        {
            string[] queryArray = new string[3];

            queryArray[0] = " SELECT TOP 1 @FoundEntity = 'Warehouse Date: ' + CAST(GoodsReceiptDetails.EntryDate AS nvarchar) FROM StockTransferDetails INNER JOIN GoodsReceiptDetails ON StockTransferDetails.StockTransferID = @EntityID AND StockTransferDetails.GoodsReceiptDetailID = GoodsReceiptDetails.GoodsReceiptDetailID AND StockTransferDetails.EntryDate < GoodsReceiptDetails.EntryDate ";
            queryArray[1] = " SELECT TOP 1 @FoundEntity = 'Transfer Order Date: ' + CAST(TransferOrders.EntryDate AS nvarchar) FROM StockTransfers INNER JOIN TransferOrders ON StockTransfers.StockTransferID = @EntityID AND StockTransfers.TransferOrderID = TransferOrders.TransferOrderID AND StockTransfers.EntryDate < TransferOrders.EntryDate ";
            queryArray[2] = " SELECT TOP 1 @FoundEntity = 'Over Quantity: ' + CAST(ROUND(Quantity - QuantityIssue, 0) AS nvarchar) FROM GoodsReceiptDetails WHERE (ROUND(Quantity - QuantityIssue, 0) < 0) ";
            
            this.totalBikePortalsEntities.CreateProcedureToCheckExisting("VehicleTransferPostSaveValidate", queryArray);
        }


        private void StockTransferEditable()
        {
            string[] queryArray = new string[1];

            queryArray[0] = " SELECT TOP 1 @FoundEntity = VoucherID FROM GoodsReceipts WHERE GoodsReceiptTypeID = " + (int)GlobalEnums.GoodsReceiptTypeID.StockTransfer + " AND VoucherID = @EntityID ";

            this.totalBikePortalsEntities.CreateProcedureToCheckExisting("StockTransferEditable", queryArray);
        }


        private void StockTransferInitReference()
        {
            StockTransferInitReference simpleInitReference = new StockTransferInitReference("StockTransfers", "StockTransferID", "Reference", ModelSettingManager.ReferenceLength, ModelSettingManager.ReferencePrefix(GlobalEnums.NmvnTaskID.StockTransfer));
            this.totalBikePortalsEntities.CreateTrigger("StockTransferInitReference", simpleInitReference.CreateQuery());
        }

    }
}
