using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Data.Entity;

using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;


using MVCModel.Models;
using MVCDTO.PurchaseTasks;
using MVCCore.Repositories.PurchaseTasks;



using Microsoft.AspNet.Identity;



namespace MVCClient.Api.PurchaseTasks
{
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class PurchaseInvoicesApiController : Controller
    {
        private readonly IPurchaseInvoiceRepository purchaseInvoiceRepository;

        public PurchaseInvoicesApiController(IPurchaseInvoiceRepository purchaseInvoiceRepository)
        {
            this.purchaseInvoiceRepository = purchaseInvoiceRepository;
        }

        public JsonResult GetPurchaseInvoices([DataSourceRequest] DataSourceRequest request)
        {
            IQueryable<PurchaseInvoice> purchaseInvoices = this.purchaseInvoiceRepository.Loading(User.Identity.GetUserId(), MVCBase.Enums.GlobalEnums.NmvnTaskID.PurchaseInvoice).Include(c => c.Customer).Include(e => e.Customer.EntireTerritory);
            
            DataSourceResult response = purchaseInvoices.ToDataSourceResult(request, o => new PurchaseInvoicePrimitiveDTO
            {
                PurchaseInvoiceID = o.PurchaseInvoiceID,
                EntryDate = o.EntryDate,
                Reference = o.Reference,
                CustomerName = o.Customer.Name,
                CustomerAttentionName = o.Customer.AttentionName,
                CustomerTelephone = o.Customer.Telephone,
                CustomerAddressNo = o.Customer.AddressNo,
                CustomerEntireTerritoryEntireName = o.Customer.EntireTerritory.EntireName,
                TotalQuantity = o.TotalQuantity,
                TotalAmount = o.TotalAmount,
                TotalGrossAmount = o.TotalGrossAmount,                
                Description = o.Description,
                Remarks = o.Remarks
            });
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPurchaseOrders([DataSourceRequest] DataSourceRequest dataSourceRequest, int locationID, int? purchaseInvoiceID, string purchaseOrderReference)
        {
            ICollection<PurchaseInvoiceGetPurchaseOrder> PurchaseInvoiceGetPurchaseOrders = this.purchaseInvoiceRepository.GetPurchaseOrders(locationID, purchaseInvoiceID, purchaseOrderReference);
            return Json(PurchaseInvoiceGetPurchaseOrders.ToDataSourceResult(dataSourceRequest), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSuppliers([DataSourceRequest] DataSourceRequest dataSourceRequest, int locationID, int? purchaseInvoiceID, string supplierName)
        {
            ICollection<PurchaseInvoiceGetSupplier> PurchaseInvoiceGetSuppliers = this.purchaseInvoiceRepository.GetSuppliers(locationID, purchaseInvoiceID, supplierName);
            return Json(PurchaseInvoiceGetSuppliers.ToDataSourceResult(dataSourceRequest), JsonRequestBehavior.AllowGet);
        }

    }
}