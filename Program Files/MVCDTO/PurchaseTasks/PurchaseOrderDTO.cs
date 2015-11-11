using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using MVCModel;
using MVCBase.Enums;

namespace MVCDTO.PurchaseTasks
{
    public abstract class PurchasePrimitiveDTO : BaseDTO
    {
        public int SupplierID { get; set; }
        [Display(Name = "Nhà cung cấp")]
        [Required(ErrorMessage = "vui lòng chọn nhà cung cấp")]
        public string CustomerName { get; set; }
        [Display(Name = "Người liên hệ")]
        public string CustomerAttentionName { get; set; }
        [Display(Name = "Địa chỉ")]
        public string CustomerAddressNo { get; set; }
        [Display(Name = "Khu vực")]
        public string CustomerEntireTerritoryEntireName { get; set; }
        [Display(Name = "Điện thoại")]
        public string CustomerTelephone { get; set; }

        [Display(Name = "Người liên hệ")]
        public string AttentionName { get; set; }
        [Display(Name = "Loại giá")]
        [Required]
        public int PriceTermID { get; set; }
        [Display(Name = "Phương thức thanh toán")]
        [Required]
        public int PaymentTermID { get; set; }
        [Display(Name = "Người duyệt")]
        public int ApproverID { get; set; }
        [Display(Name = "Tổng SL")]
        public decimal TotalQuantity { get; set; }
        [Display(Name = "Tổng tiền")]
        public decimal TotalAmount { get; set; }
        [Display(Name = "Tổng thuế")]
        public decimal TotalVATAmount { get; set; }
        [Display(Name = "Tổng cộng")]
        public decimal TotalGrossAmount { get; set; }
        [Display(Name = "Diễn giải")]
        public string Description { get; set; }
        [Display(Name = "Ghi chú")]
        public string Remarks { get; set; }
    }

    public class PurchaseOrderPrimitiveDTO : PurchasePrimitiveDTO, IPrimitiveEntity, IPrimitiveDTO
    {
        public GlobalEnums.NmvnTaskID NMVNTaskID { get { return GlobalEnums.NmvnTaskID.PurchaseOrder; } }

        public int GetID() { return this.PurchaseOrderID; }
        public void SetID(int id) { this.PurchaseOrderID = id; }

        public int PurchaseOrderID { get; set; }

        [Display(Name = "Ngày xác nhận")]
        public Nullable<System.DateTime> ConfirmDate { get; set; }

        [Display(Name = "Số phiếu xác nhận")]
        public string ConfirmReference { get; set; }

    }

    public class PurchaseOrderDTO : PurchaseOrderPrimitiveDTO, IBaseDetailEntity<PurchaseOrderDetailDTO>
    {
        public PurchaseOrderDTO()
        {
            this.PurchaseOrderDetails = new List<PurchaseOrderDetailDTO>();
        }


        public List<PurchaseOrderDetailDTO> PurchaseOrderDetails { get; set; }

        public ICollection<PurchaseOrderDetailDTO> GetDetails() { return this.PurchaseOrderDetails; }

        public override void PerformPresaveRule()
        {
            base.PerformPresaveRule();
            this.GetDetails().ToList().ForEach(e => { e.EntryDate = this.EntryDate; e.LocationID = this.LocationID; e.SupplierID = this.SupplierID; });
        }
    }
}
