using System;
using System.ComponentModel.DataAnnotations;

using MVCModel;

namespace MVCDTO.PurchaseTasks
{
    public class PurchaseOrderDetailDTO : BaseModel, IPrimitiveEntity
    {
        public int GetID() { return this.PurchaseOrderDetailID; }

        public int PurchaseOrderDetailID { get; set; }
        public int PurchaseOrderID { get; set; }
        public int SupplierID { get; set; }
        [Required(ErrorMessage = "Please select commodityid")]
        public int CommodityID { get; set; }

        [Display(Name = "Hàng hóa")]
        [UIHint("NMVN/CommodityAutoComplete")]
        [Required(ErrorMessage = "Vui lòng chọn hàng hóa")]
        public string CommodityName { get; set; }
        [Display(Name = "Mã hàng")]
        [UIHint("StringReadonly")]
        public string CommodityCode { get; set; }

        public int CommodityTypeID { get; set; }

        [Display(Name = "Xuất xứ")]
        public string Origin { get; set; }
        [Display(Name = "Đóng gói")]
        public string Packing { get; set; }

        [Display(Name = "SL")]
        [UIHint("Decimal")]
        [Range(0, 99999999999, ErrorMessage = "Số lượng không hợp lệ")]
        [Required(ErrorMessage = "Vui lòng nhập số lượng")]
        public decimal Quantity { get; set; }
        [Display(Name = "SL hóa đơn")]
        public decimal QuantityInvoice { get; set; }
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
