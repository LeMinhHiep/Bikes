using System;
using System.Web.UI;
using System.Web.Mvc;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

using MVCBase.Enums;
using MVCModel.Models;

using MVCDTO.SalesTasks;

using MVCCore.Repositories.SalesTasks;
using MVCClient.ViewModels.SalesTasks;
using MVCCore.Repositories.StockTasks;
using MVCDTO.StockTasks;





using Microsoft.AspNet.Identity;




namespace MVCClient.Api.StockTasks
{
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class VehicleTransferApiController : Controller
    {
        private readonly IVehicleTransferRepository vehicleTransferRepository;

        public VehicleTransferApiController(IVehicleTransferRepository vehicleTransferRepository)
        {
            this.vehicleTransferRepository = vehicleTransferRepository;
        }

        public JsonResult GetVehicleTransfers([DataSourceRequest] DataSourceRequest request)
        {
            IQueryable<StockTransfer> saleTransfers = this.vehicleTransferRepository.Loading(User.Identity.GetUserId(), MVCBase.Enums.GlobalEnums.NmvnTaskID.VehicleTransfer).Where(t => t.StockTransferTypeID == (int)GlobalEnums.StockTransferTypeID.VehicleTransfer).Include(t => t.TransferOrder);

            DataSourceResult response = saleTransfers.ToDataSourceResult(request, o => new VehicleTransferPrimitiveDTO
            {
                StockTransferID = o.StockTransferID,
                EntryDate = o.EntryDate,
                TransferOrderEntryDate = o.TransferOrder != null ? o.TransferOrder.EntryDate : (DateTime?)null,
                TransferOrderReference = o.TransferOrder != null ? o.TransferOrder.Reference : null,
                TransferOrderRequestedDate = o.TransferOrder != null ? o.TransferOrder.RequestedDate : (DateTime?)null,
                Reference = o.Reference,
                WarehouseID = o.WarehouseID,
                TotalQuantity = o.TotalQuantity,
                Description = o.Description,
                Remarks = o.Remarks
            });
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPendingVehicleTransferOrders([DataSourceRequest] DataSourceRequest dataSourceRequest, int locationID, int transferOrderID)
        {
            var result = this.vehicleTransferRepository.GetPendingVehicleTransferOrders(locationID, transferOrderID);
            return Json(result.ToDataSourceResult(dataSourceRequest), JsonRequestBehavior.AllowGet);
        }

    }

    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class PartTransferApiController : Controller
    {
        private readonly IPartTransferRepository partTransferRepository;

        public PartTransferApiController(IPartTransferRepository partTransferRepository)
        {
            this.partTransferRepository = partTransferRepository;
        }

        public JsonResult GetPartTransfers([DataSourceRequest] DataSourceRequest request)
        {
            IQueryable<StockTransfer> saleTransfers = this.partTransferRepository.Loading(User.Identity.GetUserId(), MVCBase.Enums.GlobalEnums.NmvnTaskID.PartTransfer).Where(t => t.StockTransferTypeID == (int)GlobalEnums.StockTransferTypeID.PartTransfer).Include(t => t.TransferOrder);

            DataSourceResult response = saleTransfers.ToDataSourceResult(request, o => new PartTransferPrimitiveDTO
            {
                StockTransferID = o.StockTransferID,
                EntryDate = o.EntryDate,
                TransferOrderEntryDate = o.TransferOrder != null ? o.TransferOrder.EntryDate : (DateTime?)null,
                TransferOrderReference = o.TransferOrder != null ? o.TransferOrder.Reference : null,
                TransferOrderRequestedDate = o.TransferOrder != null ? o.TransferOrder.RequestedDate : (DateTime?)null,
                Reference = o.Reference,
                WarehouseID = o.WarehouseID,
                TotalQuantity = o.TotalQuantity,
                Description = o.Description,
                Remarks = o.Remarks
            });
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPendingPartTransferOrders([DataSourceRequest] DataSourceRequest dataSourceRequest, int locationID, int transferOrderID)
        {
            var result = this.partTransferRepository.GetPendingPartTransferOrders(locationID, transferOrderID);
            return Json(result.ToDataSourceResult(dataSourceRequest), JsonRequestBehavior.AllowGet);
        }

    }
  
}