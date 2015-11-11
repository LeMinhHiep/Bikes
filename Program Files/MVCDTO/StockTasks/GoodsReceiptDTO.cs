using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using MVCModel;
using MVCBase.Enums;

namespace MVCDTO.StockTasks
{
    public class GoodsReceiptPrimitiveDTO : BaseDTO, IPrimitiveEntity, IPrimitiveDTO
    {
        public GlobalEnums.NmvnTaskID NMVNTaskID { get { return GlobalEnums.NmvnTaskID.GoodsReceipt; } }

        public int GetID() { return this.GoodsReceiptID; }
        public void SetID(int id) { this.GoodsReceiptID = id; }

        public int GoodsReceiptID { get; set; }

        public int GoodsReceiptTypeID { get; set; }
        public int VoucherID { get; set; }

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

    public class GoodsReceiptDTO : GoodsReceiptPrimitiveDTO, IBaseDetailEntity<GoodsReceiptDetailDTO>
    {
        public GoodsReceiptDTO()
        {
            this.GoodsReceiptViewDetails = new List<GoodsReceiptDetailDTO>();
        }


        public List<GoodsReceiptDetailDTO> GoodsReceiptViewDetails { get; set; }
        public List<GoodsReceiptDetailDTO> ViewDetails { get { return this.GoodsReceiptViewDetails; } set { this.GoodsReceiptViewDetails = value; } }

        public ICollection<GoodsReceiptDetailDTO> GetDetails() { return this.GoodsReceiptViewDetails; }

        public override void PerformPresaveRule()
        {
            base.PerformPresaveRule();
            this.GetDetails().ToList().ForEach(e => { e.GoodsReceiptTypeID = this.GoodsReceiptTypeID; e.EntryDate = this.EntryDate; e.LocationID = this.LocationID; });
        }
    }
}
