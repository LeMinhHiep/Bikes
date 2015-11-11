using System;
using System.ComponentModel.DataAnnotations;

using MVCModel;
using MVCModel.Helpers;

namespace MVCDTO.StockTasks
{
    public class GoodsReceiptDetailDTO : BaseModel, IPrimitiveEntity, IHelperWarehouseID, IHelperCommodityID, IHelperCommodityTypeID
    {
        public int GetID() { return this.GoodsReceiptDetailID; }

        public int GoodsReceiptDetailID { get; set; }
        public int GoodsReceiptID { get; set; }

        public int GoodsReceiptTypeID { get; set; }

        public int VoucherDetailID { get; set; }
        public int VoucherID { get; set; }

        public Nullable<int> SupplierID { get; set; }
        public int CommodityID { get; set; }
        [Display(Name = "Hàng hóa")]
        [UIHint("StringReadonly")]
        public string CommodityName { get; set; }
        [UIHint("StringReadonly")]
        public string CommodityCode { get; set; }

        public int CommodityTypeID { get; set; }

        //[Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn kho")]        
        public int WarehouseID { get; set; }
        public int GetWarehouseID() { return this.WarehouseID; } //Purpose: for IHelperWarehouseID only

        [Display(Name = "Kho")]
        [Required(ErrorMessage = "Vui lòng chọn kho")]
        [UIHint("NMVN/WarehouseAutoComplete")]
        public string WarehouseCode { get; set; }

        [Display(Name = "Xuất xứ")]
        public string Origin { get; set; }
        [Display(Name = "Đóng gói")]
        public string Packing { get; set; }

        [Display(Name = "SL còn")]
        [UIHint("DecimalReadonly")]
        public decimal QuantityRemains { get; set; }
        [Display(Name = "SL")]
        [UIHint("Decimal")]
        [GenericCompare(CompareToPropertyName = "QuantityRemains", OperatorName = GenericCompareOperator.LessThanOrEqual, ErrorMessage = "Số lượng không được lớn hơn số lượng còn lại")]
        public decimal Quantity { get; set; }

        [Display(Name = "SL phát hành")]
        [UIHint("Decimal")]
        public decimal QuantityIssue { get; set; }

        [Display(Name = "Đơn giá")]
        [UIHint("Decimal")]
        [Range(0, 99999999999, ErrorMessage = "Đơn giá không hợp lệ")]
        public decimal UnitPrice { get; set; }
        [Display(Name = "VAT")]
        [UIHint("Decimal")]
        public decimal VATPercent { get; set; }
        [Display(Name = "Giá sau thuế")]
        [UIHint("Decimal")]
        public decimal GrossPrice { get; set; }
        [Display(Name = "Thành tiền")]
        [UIHint("DecimalReadonly")]
        public decimal Amount { get; set; }
        [Display(Name = "Thuế VAT")]
        [UIHint("DecimalReadonly")]
        public decimal VATAmount { get; set; }
        [Display(Name = "Tổng cộng")]
        [UIHint("DecimalReadonly")]
        public decimal GrossAmount { get; set; }


        [Display(Name = "Số khung")]
        public string ChassisCode { get; set; }
        [Display(Name = "Số động cơ")]
        public string EngineCode { get; set; }
        [Display(Name = "Mã màu")]
        public string ColorCode { get; set; }

        [Display(Name = "Ghi chú")]
        public string Remarks { get; set; }
    }
}
