using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using MVCBase.Enums;
using MVCModel;

namespace MVCDTO.StockTasks
{
    public abstract class StockTransferPrimitiveDTO : BaseDTO, IPrimitiveEntity, IPrimitiveDTO
    {
        public virtual GlobalEnums.NmvnTaskID NMVNTaskID { get { return GlobalEnums.NmvnTaskID.StockTransfer; } }

        public int GetID() { return this.StockTransferID; }
        public void SetID(int id) { this.StockTransferID = id; }

        public int StockTransferID { get; set; }

        public Nullable<int> TransferOrderID { get; set; }
        [Display(Name = "Lệnh điều hàng")]
        public string TransferOrderReference { get; set; }
        [Display(Name = "Ngày lập lệnh")]
        public Nullable<System.DateTime> TransferOrderEntryDate { get; set; }
        [Display(Name = "Ngày yêu cầu hàng về")]
        public Nullable<System.DateTime> TransferOrderRequestedDate { get; set; }


        [Display(Name = "Loại chuyển kho")]
        public virtual int StockTransferTypeID { get; set; }

        public int WarehouseID { get; set; }
        [Display(Name = "Kho nhập")]
        [Required]
        public string WarehouseName { get; set; }
        [Display(Name = "Kho nhập")]
        public string WarehouseCode { get; set; }
        public string WarehouseLocationTelephone { get; set; }
        public string WarehouseLocationFacsimile { get; set; }
        public string WarehouseLocationName { get; set; }
        public string WarehouseLocationAddress { get; set; }

        [Display(Name = "Người duyệt")]
        public int ApproverID { get; set; }

        [Display(Name = "Tổng SL")]
        public decimal TotalQuantity { get; set; }
        [Display(Name = "Diễn giải")]
        public string Description { get; set; }
        [Display(Name = "Ghi chú")]
        public string Remarks { get; set; }
    }


    public class VehicleTransferPrimitiveDTO : StockTransferPrimitiveDTO
    {
        public override GlobalEnums.NmvnTaskID NMVNTaskID { get { return GlobalEnums.NmvnTaskID.VehicleTransfer; } }
        public override int StockTransferTypeID { get { return (int)GlobalEnums.StockTransferTypeID.VehicleTransfer; } }
    }

    public class VehicleTransferDTO : VehicleTransferPrimitiveDTO, IBaseDetailEntity<VehicleTransferDetailDTO>
    {
        public VehicleTransferDTO()
        {
            this.VehicleTransferViewDetails = new List<VehicleTransferDetailDTO>();
        }

        public List<VehicleTransferDetailDTO> VehicleTransferViewDetails { get; set; }
        public List<VehicleTransferDetailDTO> ViewDetails { get { return this.VehicleTransferViewDetails; } set { this.VehicleTransferViewDetails = value; } }

        public ICollection<VehicleTransferDetailDTO> GetDetails() { return this.VehicleTransferViewDetails; }

        public override void PerformPresaveRule()
        {
            base.PerformPresaveRule();
            this.GetDetails().ToList().ForEach(e => { e.EntryDate = this.EntryDate; e.LocationID = this.LocationID; });
        }
    }






    public class PartTransferPrimitiveDTO : StockTransferPrimitiveDTO
    {
        public override GlobalEnums.NmvnTaskID NMVNTaskID { get { return GlobalEnums.NmvnTaskID.PartTransfer; } }
        public override int StockTransferTypeID { get { return (int)GlobalEnums.StockTransferTypeID.PartTransfer; } }
    }

    public class PartTransferDTO : PartTransferPrimitiveDTO, IBaseDetailEntity<PartTransferDetailDTO>
    {
        public PartTransferDTO()
        {
            this.PartTransferViewDetails = new List<PartTransferDetailDTO>();
        }

        public List<PartTransferDetailDTO> PartTransferViewDetails { get; set; }
        public List<PartTransferDetailDTO> ViewDetails { get { return this.PartTransferViewDetails; } set { this.PartTransferViewDetails = value; } }

        public ICollection<PartTransferDetailDTO> GetDetails() { return this.PartTransferViewDetails; }

        public override void PerformPresaveRule()
        {
            base.PerformPresaveRule();
            this.GetDetails().ToList().ForEach(e => { e.EntryDate = this.EntryDate; e.LocationID = this.LocationID; });
        }
    }
}
