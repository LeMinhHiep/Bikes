using System;
using System.ComponentModel.DataAnnotations;

using MVCModel;

namespace MVCDTO.SalesTasks
{
    public class QuotationDetailDTO : BaseModel, IPrimitiveEntity
    {
        public int GetID() { return this.QuotationDetailID; }

        public int QuotationDetailID { get; set; }
        public int QuotationID { get; set; }

        public int CommodityID { get; set; }
        [Display(Name = "Hàng hóa")]
        [UIHint("NMVN/CommodityAutoComplete")]
        [Required(ErrorMessage = "Vui lòng chọn hàng hóa")]
        public string CommodityName { get; set; }
        [Display(Name = "Mã hàng")]
        [UIHint("StringReadonly")]
        public string CommodityCode { get; set; }

        public int CommodityTypeID { get; set; }

        [Display(Name = "Tồn kho")]
        [UIHint("DecimalReadonly")]
        public decimal QuantityAvailable { get; set; }


        [Display(Name = "SL")]
        [UIHint("Decimal")]
        [Range(0, 99999999999, ErrorMessage = "Số lượng không hợp lệ")]
        public decimal Quantity { get; set; }
        [Display(Name = "Đơn giá")]
        [UIHint("Decimal")]
        public decimal ListedPrice { get; set; }
        [Display(Name = "CK")]
        [UIHint("Decimal")]
        public decimal DiscountPercent { get; set; }
        [Display(Name = "Giá bán")]
        [UIHint("Decimal")]
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
        [Display(Name = "Ghi chú")]
        public string Remarks { get; set; }
        public Nullable<bool> IsBonus { get; set; }
        public Nullable<bool> IsWarrantyClaim { get; set; }
    }

    /// <summary>
    /// This class is used as model of kendo grid to show in the popup windows with a selected checkbox column: IsSelected
    /// </summary>
    public class QuotationDetailPopupDTO : QuotationDetailDTO
    {
        public int WarehouseID { get; set; }
        [Display(Name = "Kho")]
        [UIHint("StringReadonly")]
        public string WarehouseCode { get; set; }


        public Nullable<bool> IsSelected { get; set; }
    }
}
