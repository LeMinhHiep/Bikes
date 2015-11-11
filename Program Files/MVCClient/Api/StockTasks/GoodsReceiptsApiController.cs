﻿using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;

using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;


using MVCModel.Models;
using MVCDTO.StockTasks;
using MVCCore.Repositories.StockTasks;


using Microsoft.AspNet.Identity;


namespace MVCClient.Api.StockTasks
{
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class GoodsReceiptsApiController : Controller
    {
        private readonly IGoodsReceiptRepository goodsReceiptRepository;       

        public GoodsReceiptsApiController(IGoodsReceiptRepository goodsReceiptRepository)
        {
            this.goodsReceiptRepository = goodsReceiptRepository;
        }

        public JsonResult GetGoodsReceipts([DataSourceRequest] DataSourceRequest request)
        {
            IQueryable<GoodsReceipt> goodsReceipts = this.goodsReceiptRepository.Loading(User.Identity.GetUserId(), MVCBase.Enums.GlobalEnums.NmvnTaskID.GoodsReceipt);

            DataSourceResult response = goodsReceipts.ToDataSourceResult(request, o => new GoodsReceiptPrimitiveDTO
            {
                GoodsReceiptID = o.GoodsReceiptID,
                EntryDate = o.EntryDate,
                Reference = o.Reference,
                TotalQuantity = o.TotalQuantity,                
                Description = o.Description,
                Remarks = o.Remarks
            });
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPurchaseInvoices([DataSourceRequest] DataSourceRequest dataSourceRequest, int locationID, int? goodsReceiptID, string purchaseInvoiceReference)
        {
            ICollection<GoodsReceiptGetPurchaseInvoice> GoodsReceiptGetPurchaseInvoices = this.goodsReceiptRepository.GetPurchaseInvoices(locationID, goodsReceiptID, purchaseInvoiceReference);
            return Json(GoodsReceiptGetPurchaseInvoices.ToDataSourceResult(dataSourceRequest), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStockTransfers([DataSourceRequest] DataSourceRequest dataSourceRequest, int locationID, int? goodsReceiptID, string stockTransferReference)
        {
            ICollection<GoodsReceiptGetStockTransfer> GoodsReceiptGetStockTransfers = this.goodsReceiptRepository.GetStockTransfers(locationID, goodsReceiptID, stockTransferReference);
            return Json(GoodsReceiptGetStockTransfers.ToDataSourceResult(dataSourceRequest), JsonRequestBehavior.AllowGet);
        }      

    }
}