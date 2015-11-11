using System;
using System.ComponentModel.DataAnnotations;

using MVCModel;
using MVCModel.Helpers;

namespace MVCDTO.StockTasks
{
    public abstract class StockTransferDetailDTO : BaseModel, IPrimitiveEntity
    {
        public int GetID() { return this.StockTransferDetailID; }

        public int StockTransferDetailID { get; set; }
        public int StockTransferID { get; set; }

        public Nullable<int> TransferOrderDetailID { get; set; }

        public int CommodityID { get; set; }
        [Display(Name = "Mã hàng")]
        [UIHint("StringReadonly")]
        public string CommodityCode { get; set; }
        [Display(Name = "Hàng hóa")]
        [Required(ErrorMessage = "Vui lòng chọn hàng hóa")]
        public virtual string CommodityName { get; set; }

        public int CommodityTypeID { get; set; }

        public int WarehouseID { get; set; }
        [Display(Name = "Kho xuất")]
        [UIHint("StringReadonly")]
        public string WarehouseCode { get; set; }
        [Display(Name = "Kho xuất")]
        [UIHint("StringReadonly")]
        public string WarehouseName { get; set; }

        [Display(Name = "Tồn kho")]
        [UIHint("DecimalReadonly")]
        public decimal QuantityAvailable { get; set; }

        [Display(Name = "SL")]
        [UIHint("Decimal")]
        [Range(0, 99999999999, ErrorMessage = "Số lượng không hợp lệ")]
        [GenericCompare(CompareToPropertyName = "QuantityAvailable", OperatorName = GenericCompareOperator.LessThanOrEqual, ErrorMessage = "Số lượng không được lớn hơn số lượng còn lại")]
        public decimal Quantity { get; set; }
        [Display(Name = "SL nhập")]
        public decimal QuantityReceipt { get; set; }
        [Display(Name = "Ghi chú")]
        public string Remarks { get; set; }
    }

    public class VehicleTransferDetailDTO : StockTransferDetailDTO
    {
        public int GoodsReceiptDetailID { get; set; }
        public int SupplierID { get; set; }

        [Display(Name = "Số khung")]
        [UIHint("StringReadonly")]
        public string ChassisCode { get; set; }
        [UIHint("StringReadonly")]
        [Display(Name = "Số động cơ")]
        public string EngineCode { get; set; }
        [UIHint("StringReadonly")]
        [Display(Name = "Mã màu")]
        public string ColorCode { get; set; }

        [UIHint("NMVN/CommoditiesInGoodsReceiptsAutoComplete")]
        public override string CommodityName { get; set; }
    }




    public class PartTransferDetailDTO : StockTransferDetailDTO, IHelperWarehouseID, IHelperCommodityID, IHelperCommodityTypeID
    {
        public int GetWarehouseID() { return this.WarehouseID; } //Purpose: for IHelperWarehouseID only

        [UIHint("NMVN/CommoditiesInWarehousesAutoComplete")]
        public override string CommodityName { get; set; }
    }
}
