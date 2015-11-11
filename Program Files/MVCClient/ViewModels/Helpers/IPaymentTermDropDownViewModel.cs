using System.Web.Mvc;
using System.Collections.Generic;

namespace MVCClient.ViewModels.Helpers
{
    public interface IPaymentTermDropDownViewModel
    {
        int PaymentTermID { get; set; }
        IEnumerable<SelectListItem> PaymentTermDropDown { get; set; }
    }
}

