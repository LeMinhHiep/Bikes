using System.Net;
using System.Web.Mvc;
using System.Text;

using RequireJsNet;

using MVCBase.Enums;

using MVCModel.Models;

using MVCCore.Services.SalesTasks;

using MVCDTO.SalesTasks;

using MVCClient.ViewModels.SalesTasks;
using MVCClient.Builders.SalesTasks;



namespace MVCClient.Controllers.SalesTasks
{
    public class PartsInvoicesController : GenericViewDetailController<SalesInvoice, SalesInvoiceDetail, PartsInvoiceViewDetail, PartsInvoiceDTO, PartsInvoicePrimitiveDTO, PartsInvoiceDetailDTO, PartsInvoiceViewModel>
    {
        public PartsInvoicesController(IPartsInvoiceService partsInvoiceService, IPartsInvoiceViewModelSelectListBuilder partsInvoiceViewModelSelectListBuilder)
            : base(partsInvoiceService, partsInvoiceViewModelSelectListBuilder, true)
        {
        }

        public override void AddRequireJsOptions()
        {
            base.AddRequireJsOptions();

            StringBuilder commodityTypeIDList = new StringBuilder();
            commodityTypeIDList.Append((int)GlobalEnums.CommodityTypeID.Parts);
            commodityTypeIDList.Append(","); commodityTypeIDList.Append((int)GlobalEnums.CommodityTypeID.Consumables);

            RequireJsOptions.Add("commodityTypeIDList", commodityTypeIDList.ToString(), RequireJsOptionsScope.Page);
        }

        
        public virtual ActionResult GetQuotationDetails()
        {
            this.AddRequireJsOptions();
            return View();
        }

    }

}