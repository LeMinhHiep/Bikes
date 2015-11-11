using System.Linq;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;

using MVCModel.Models;
using MVCDTO.StockTasks;

using MVCBase.Enums;
using MVCCore.Services;
using MVCCore.Repositories.StockTasks;
using MVCCore.Services.StockTasks;


namespace MVCService.StockTasks
{
    public class VehicleTransferOrderService : GenericWithViewDetailService<TransferOrder, TransferOrderDetail, VehicleTransferOrderViewDetail, VehicleTransferOrderDTO, VehicleTransferOrderPrimitiveDTO, VehicleTransferOrderDetailDTO>, IVehicleTransferOrderService
    {
        public VehicleTransferOrderService(IVehicleTransferOrderRepository vehicleTransferOrderRepository)
            : base(vehicleTransferOrderRepository, null, null, "GetVehicleTransferOrderViewDetails")
        {
        }

        public override ICollection<VehicleTransferOrderViewDetail> GetViewDetails(int transferOrderID)
        {
            ObjectParameter[] parameters = new ObjectParameter[] { new ObjectParameter("TransferOrderID", transferOrderID) };
            return this.GetViewDetails(parameters);
        }

        public override bool Save(VehicleTransferOrderDTO TransferOrderDTO)
        {
            TransferOrderDTO.VehicleTransferOrderViewDetails.RemoveAll(x => x.Quantity == 0);
            return base.Save(TransferOrderDTO);
        }




        public bool InitWarehouseBalance15AUG()
        {
            //REMOVE ALL BEFORE RUN THIS, FOR 2 TABLES: DELETE FROM            WarehouseBalanceDetail, DELETE FROM            WarehouseBalancePrice 
            ICollection<GoodsReceipt> goodsReceipts = this.GenericWithDetailRepository.GetEntities<GoodsReceipt>().OrderBy(o => o.EntryDate).ToList();
            foreach (GoodsReceipt goodsReceipt in goodsReceipts)
            {
                this.CallUpdateWarehouseBalance15AUG(1, goodsReceipt.GoodsReceiptID, 0, 0);
            }

            ICollection<SalesInvoice> salesInvoices = this.GenericWithDetailRepository.GetEntities<SalesInvoice>().Where(w => w.SalesInvoiceTypeID == (int)GlobalEnums.SalesInvoiceTypeID.PartsInvoice).OrderBy(o => o.EntryDate).ToList();
            foreach (SalesInvoice salesInvoice in salesInvoices)
            {
                this.CallUpdateWarehouseBalance15AUG(-1, 0, salesInvoice.SalesInvoiceID, 0);
            }

            ICollection<StockTransfer> stockTransfers = this.GenericWithDetailRepository.GetEntities<StockTransfer>().Where(w => w.StockTransferTypeID == (int)GlobalEnums.StockTransferTypeID.PartTransfer).OrderBy(o => o.EntryDate).ToList();
            foreach (StockTransfer stockTransfer in stockTransfers)
            {
                this.CallUpdateWarehouseBalance15AUG(-1, 0, 0, stockTransfer.StockTransferID);
            }

            return true;
        }


        private void CallUpdateWarehouseBalance15AUG(int updateWarehouseBalanceOption, int goodsReceiptID, int salesInvoiceID, int stockTransferID)
        {
            using (var dbContextTransaction = this.GenericWithDetailRepository.BeginTransaction())
            {
                try
                {
                    ObjectParameter[] parameters = new ObjectParameter[] { new ObjectParameter("UpdateWarehouseBalanceOption", updateWarehouseBalanceOption), new ObjectParameter("GoodsReceiptID", goodsReceiptID), new ObjectParameter("SalesInvoiceID", salesInvoiceID), new ObjectParameter("StockTransferID", stockTransferID) };
                    this.GenericWithDetailRepository.ExecuteFunction("UpdateWarehouseBalance", parameters);

                    dbContextTransaction.Commit();
                }
                catch (System.Exception ex)
                {
                    dbContextTransaction.Rollback();
                    throw ex;
                }
            }
        }


    }
}
