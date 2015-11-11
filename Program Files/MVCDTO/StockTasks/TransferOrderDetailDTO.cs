using System;
using System.ComponentModel.DataAnnotations;

using MVCModel;

namespace MVCDTO.StockTasks
{
    public abstract class TransferOrderDetailDTO : BaseModel, IPrimitiveEntity
    {
        public int GetID() { return this.TransferOrderDetailID; }

        public int TransferOrderDetailID { get; set; }
        public int TransferOrderID { get; set; }

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
        [Required(ErrorMessage = "Vui lòng chọn kho")]
        [UIHint("StringReadonly")]
        public string WarehouseCode { get; set; }

        [Display(Name = "Tồn kho")]
        [UIHint("DecimalReadonly")]
        public decimal QuantityAvailable { get; set; }

        [Display(Name = "SL")]
        [UIHint("Decimal")]
        [Range(0, 99999999999, ErrorMessage = "Số lượng không hợp lệ")]
        public decimal Quantity { get; set; }
        [Display(Name = "SL nhập")]
        public decimal QuantityTransfer { get; set; }

        [Display(Name = "Ghi chú")]
        public string Remarks { get; set; }

    }

    public class VehicleTransferOrderDetailDTO : TransferOrderDetailDTO
    {
        [UIHint("NMVN/VehicleAvailablesAutoComplete")]
        public override string CommodityName { get; set; }
    }

    public class PartTransferOrderDetailDTO : TransferOrderDetailDTO
    {
        [UIHint("NMVN/PartAvailablesAutoComplete")]
        public override string CommodityName { get; set; }
    }

}
