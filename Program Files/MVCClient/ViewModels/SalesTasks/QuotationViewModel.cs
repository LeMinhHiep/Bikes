using System.Web.Mvc;
using System.Collections.Generic;

using MVCBase.Enums;
using MVCDTO.SalesTasks;
using MVCClient.ViewModels.Helpers;


namespace MVCClient.ViewModels.SalesTasks
{
    public class QuotationViewModel : QuotationDTO, IViewDetailViewModel<QuotationDetailDTO>, IContractibleInvoiceViewModel, ICustomerAutoCompleteViewModel, IPersonInChargeDropDownViewModel, IPreparedPersonDropDownViewModel, IApproverDropDownViewModel, IPaymentTermDropDownViewModel
    {
        public IEnumerable<SelectListItem> PaymentTermDropDown { get; set; }
        public IEnumerable<SelectListItem> PersonInChargeDropDown { get; set; }
        public IEnumerable<SelectListItem> PreparedPersonDropDown { get; set; }
        public IEnumerable<SelectListItem> ApproverDropDown { get; set; }
    }

}