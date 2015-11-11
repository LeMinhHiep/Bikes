using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using MVCModel;
using MVCBase.Enums;

namespace MVCDTO.SalesTasks
{
    public class QuotationPrimitiveDTO : BaseDTO, IPrimitiveEntity, IPrimitiveDTO
    {
        public GlobalEnums.NmvnTaskID NMVNTaskID { get { return GlobalEnums.NmvnTaskID.Quotation; } }

        public int GetID() { return this.QuotationID; }
        public void SetID(int id) { this.QuotationID = id; }

        public int QuotationID { get; set; }

        public int CustomerID { get; set; }
        [Display(Name = "Khách hàng")]
        public string CustomerName { get; set; }
        [Display(Name = "Ngày sinh")]
        public Nullable<System.DateTime> CustomerBirthday { get; set; }
        [Display(Name = "Điện thoại")]
        public string CustomerTelephone { get; set; }
        [Display(Name = "Địa chỉ")]
        public string CustomerAddressNo { get; set; }
        [Display(Name = "Khu vực")]
        public string CustomerEntireTerritoryEntireName { get; set; }

        [Display(Name = "Phương thức thanh toán")]
        public int PaymentTermID { get; set; }



        public Nullable<int> ServiceContractID { get; set; }
        public string ServiceContractReference { get; set; }

        public string ServiceContractCommodityID { get; set; }
        [Display(Name = "Mã hàng")]
        public string ServiceContractCommodityCode { get; set; }
        [Display(Name = "Tên hàng")]
        public string ServiceContractCommodityName { get; set; }

        [Display(Name = "Ngày mua")]
        public Nullable<System.DateTime> ServiceContractPurchaseDate { get; set; }
        [Display(Name = "Biển số xe")]
        public string ServiceContractLicensePlate { get; set; }
        [Display(Name = "Số khung")]
        public string ServiceContractChassisCode { get; set; }
        [Display(Name = "Số máy")]
        public string ServiceContractEngineCode { get; set; }
        [Display(Name = "Mã màu")]
        public string ServiceContractColorCode { get; set; }
        [Display(Name = "Tên đại lý")]
        public string ServiceContractAgentName { get; set; }


        [Display(Name = "Người phụ trách")]
        public int PersonInChargeID { get; set; }
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
        [Display(Name = "Bình quân CK")]
        public decimal AverageDiscountPercent { get; set; }

        public string Damages { get; set; }
        public string Causes { get; set; }
        public string Solutions { get; set; }

        [Display(Name = "Diễn giải")]
        public string Description { get; set; }
        [Display(Name = "Ghi chú")]
        public string Remarks { get; set; }

        public bool IsFinished { get; set; }
    }


    public class QuotationDTO : QuotationPrimitiveDTO, IBaseDetailEntity<QuotationDetailDTO>
    {
        public QuotationDTO()
        {
            this.QuotationViewDetails = new List<QuotationDetailDTO>();
        }


        public List<QuotationDetailDTO> QuotationViewDetails { get; set; }
        public List<QuotationDetailDTO> ViewDetails { get { return this.QuotationViewDetails; } set { this.QuotationViewDetails = value; } }

        public ICollection<QuotationDetailDTO> GetDetails() { return this.QuotationViewDetails; }

        public override void PerformPresaveRule()
        {
            base.PerformPresaveRule();
            this.GetDetails().ToList().ForEach(e => { e.EntryDate = this.EntryDate; e.LocationID = this.LocationID; });
        }
    }


}
