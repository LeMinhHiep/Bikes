using System.Linq;
using System.Web.Mvc;

using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;


using MVCModel.Models;

using MVCDTO.PurchaseTasks;

using MVCCore.Repositories.PurchaseTasks;
using MVCClient.ViewModels.PurchaseTasks;
using System.Collections.Generic;
using AutoMapper;

//using MVCClient.Controllers;




using Microsoft.AspNet.Identity;



namespace MVCClient.Api.PurchaseTasks
{
    //[GenericSimpleAuthorizeAttribute]
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class PurchaseOrdersApiController : Controller
    {
        private readonly IPurchaseOrderRepository purchaseOrderRepository;

        public PurchaseOrdersApiController(IPurchaseOrderRepository purchaseOrderRepository)
        {
            this.purchaseOrderRepository = purchaseOrderRepository;
        }

        public JsonResult GetPurchaseOrders([DataSourceRequest] DataSourceRequest request)
        {
            IQueryable<PurchaseOrder> purchaseOrders = this.purchaseOrderRepository.Loading(User.Identity.GetUserId(), MVCBase.Enums.GlobalEnums.NmvnTaskID.PurchaseOrder);
            //DataSourceResult response = purchaseOrders.ToDataSourceResult<PurchaseOrder, PurchaseOrderPrimitiveDTO>(request, domain => Mapper.Map<PurchaseOrder, PurchaseOrderPrimitiveDTO>(domain));

            DataSourceResult response = purchaseOrders.ToDataSourceResult(request, o => new PurchaseOrderPrimitiveDTO
            {
                PurchaseOrderID = o.PurchaseOrderID,
                EntryDate = o.EntryDate,
                Reference = o.Reference,
                TotalQuantity = o.TotalQuantity,
                TotalGrossAmount = o.TotalGrossAmount,
                TotalAmount = o.TotalAmount,
                Description = o.Description,
                Remarks = o.Remarks
            });


            return Json(response, JsonRequestBehavior.AllowGet);
        }

    }
}