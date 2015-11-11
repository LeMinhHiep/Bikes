namespace MVCBase.Enums
{
    public static class GlobalEnums
    {
        public static int rndQuantity = 0;
        public static int rndAmount = 0;
        public static int rndDiscountPercent = 1;

        public enum SubmitTypeOption
        {
            Save = 0,
            Popup = 1,
            Closed = 9
        };

        public enum NmvnTaskID
        {
            Customer = 8001,
            Commodity = 8002,

            PurchaseOrder = 8021,
            PurchaseInvoice = 8022,



            Quotation = 8031,

            SalesInvoice = 8051,

            VehiclesInvoice = 8052,
            PartsInvoice = 8053,
            ServicesInvoice = 8055,

            ServiceContract = 8056,




            GoodsReceipt = 8077,
            StockAdjustment = 8078,
            VehicleAdjustment = 8078,
            PartAdjustment = 8078,


            TransferOrder = 8071,
            VehicleTransferOrder = 8071008,
            PartTransferOrder = 8071009,

            StockTransfer = 8073,
            VehicleTransfer = 8075,
            PartTransfer = 8076

        };

        public enum GoodsReceiptTypeID
        {
            PurchaseInvoice = 1,
            GoodsReturn = 2,
            StockTransfer = 3,
            InventoryAdjustment = 4
        };

        public enum SalesInvoiceTypeID
        {
            VehiclesInvoice = 10,
            PartsInvoice = 20,
            ServicesInvoice = 30
        };

        public enum StockTransferTypeID
        {
            VehicleTransfer = 10,
            PartTransfer = 20
        };

        public enum ServiceContractTypeID
        {
            Warranty = 1,
            Repair = 2,
            Maintenance = 3
        };

        public enum CommodityTypeID
        {
            Vehicles = 1,
            Parts = 2,
            Consumables = 3,
            Services = 6,
            Unknown = 99
        };


        public enum UpdateWarehouseBalanceOption
        {
            Add = 1,
            Minus = -1
        };


        public enum AccessLevel
        {
            Deny = 0,
            Readable = 1,
            Editable = 2
        };
    }
}
