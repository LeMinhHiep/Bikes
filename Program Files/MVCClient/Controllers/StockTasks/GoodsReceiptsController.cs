using System.Net;
using System.Web.Mvc;
using System.Collections.Generic;

using MVCModel.Models;

using MVCCore.Services.StockTasks;

using MVCDTO.StockTasks;

using MVCClient.ViewModels.StockTasks;
using MVCClient.Builders.StockTasks;

namespace MVCClient.Controllers.StockTasks
{
    public class GoodsReceiptsController : GenericViewDetailController<GoodsReceipt, GoodsReceiptDetail, GoodsReceiptViewDetail, GoodsReceiptDTO, GoodsReceiptPrimitiveDTO, GoodsReceiptDetailDTO, GoodsReceiptViewModel>
    {
        private readonly IGoodsReceiptService goodsReceiptService;        
        IGoodsReceiptViewModelSelectListBuilder goodsReceiptViewModelSelectListBuilder;

        public GoodsReceiptsController(IGoodsReceiptService goodsReceiptService, IGoodsReceiptViewModelSelectListBuilder goodsReceiptViewModelSelectListBuilder)
            : base(goodsReceiptService, goodsReceiptViewModelSelectListBuilder, true)
        {
            this.goodsReceiptService = goodsReceiptService;
            this.goodsReceiptViewModelSelectListBuilder = goodsReceiptViewModelSelectListBuilder;            
        }


        protected override ICollection<GoodsReceiptViewDetail> GetEntityViewDetails(GoodsReceiptViewModel goodsReceiptViewModel)
        {
            ICollection<GoodsReceiptViewDetail> goodsReceiptViewDetails = this.goodsReceiptService.GetGoodsReceiptViewDetails(goodsReceiptViewModel.GoodsReceiptID, goodsReceiptViewModel.GoodsReceiptTypeID, goodsReceiptViewModel.VoucherID, false);

            return goodsReceiptViewDetails;
        }

    }
}

