using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.Collections.Generic;

using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;


using MVCBase.Enums;
using MVCModel.Models;
using MVCDTO.StockTasks;
using MVCCore.Repositories.StockTasks;




using Microsoft.AspNet.Identity;




namespace MVCClient.Api.StockTasks
{
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class TransferOrdersApiController : Controller
    {
        private readonly ITransferOrderRepository transferOrderRepository;

        public TransferOrdersApiController(ITransferOrderRepository transferOrderRepository)
        {
            this.transferOrderRepository = transferOrderRepository;
        }

        protected IQueryable<TransferOrder> GetTransferOrders(GlobalEnums.NmvnTaskID nmvnTaskID, GlobalEnums.StockTransferTypeID stockTransferTypeID)
        {
            return this.transferOrderRepository.Loading(User.Identity.GetUserId(), nmvnTaskID).Where(ww => ww.StockTransferTypeID == (int)stockTransferTypeID).Include(w => w.Warehouse).Include(l => l.Location);
        }


        public JsonResult SearchTransferOrders([DataSourceRequest] DataSourceRequest dataSourceRequest, int locationID, string commodityTypeIDList, string searchText)
        {
            var result = transferOrderRepository.SearchTransferOrders(locationID, commodityTypeIDList, searchText).Select(s => new
            {
                s.TransferOrderID,
                TransferOrderReference = s.Reference,
                TransferOrderEntryDate = s.EntryDate,
                TransferOrderRequestedDate = s.RequestedDate,

                s.WarehouseID,
                WarehouseCode = s.Warehouse.Code,
                WarehouseName = s.Warehouse.Name,
                WarehouseLocationName = s.Warehouse.Location.Name,
                WarehouseLocationTelephone = s.Warehouse.Location.Telephone,
                WarehouseLocationFacsimile = s.Warehouse.Location.Facsimile,
                WarehouseLocationAddress = s.Warehouse.Location.Address
            });
            return Json(result.ToDataSourceResult(dataSourceRequest), JsonRequestBehavior.AllowGet);
        }


    }

    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class VehicleTransferOrdersApiController : TransferOrdersApiController
    {
        public VehicleTransferOrdersApiController(IVehicleTransferOrderRepository vehicleTransferOrderRepository)
            : base(vehicleTransferOrderRepository)
        {
        }

        public JsonResult GetVehicleTransferOrders([DataSourceRequest] DataSourceRequest request)
        {
            IQueryable<TransferOrder> transferOrders = this.GetTransferOrders(GlobalEnums.NmvnTaskID.VehicleTransferOrder, GlobalEnums.StockTransferTypeID.VehicleTransfer);

            DataSourceResult response = transferOrders.ToDataSourceResult(request, o => new VehicleTransferOrderPrimitiveDTO
            {
                TransferOrderID = o.TransferOrderID,
                EntryDate = o.EntryDate,
                RequestedDate = o.RequestedDate,
                Reference = o.Reference,
                LocationName = o.Location.Name,
                WarehouseName = o.Warehouse.Name,
                TotalQuantity = o.TotalQuantity,
                Description = o.Description,
                Remarks = o.Remarks
            });
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }

    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class PartTransferOrdersApiController : TransferOrdersApiController
    {
        public PartTransferOrdersApiController(IPartTransferOrderRepository partTransferOrderRepository)
            : base(partTransferOrderRepository)
        {
        }

        public JsonResult GetPartTransferOrders([DataSourceRequest] DataSourceRequest request)
        {
            IQueryable<TransferOrder> transferOrders = this.GetTransferOrders(GlobalEnums.NmvnTaskID.PartTransferOrder, GlobalEnums.StockTransferTypeID.PartTransfer);

            DataSourceResult response = transferOrders.ToDataSourceResult(request, o => new PartTransferOrderPrimitiveDTO
            {
                TransferOrderID = o.TransferOrderID,
                EntryDate = o.EntryDate,
                RequestedDate = o.RequestedDate,
                Reference = o.Reference,
                LocationName = o.Location.Name,
                WarehouseName = o.Warehouse.Name,
                TotalQuantity = o.TotalQuantity,
                Description = o.Description,
                Remarks = o.Remarks
            });
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}