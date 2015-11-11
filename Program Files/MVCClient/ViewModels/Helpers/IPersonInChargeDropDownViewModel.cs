using System.Web.Mvc;
using System.Collections.Generic;

namespace MVCClient.ViewModels.Helpers
{
    public interface IPersonInChargeDropDownViewModel
    {
        int PersonInChargeID { get; set; }
        IEnumerable<SelectListItem> PersonInChargeDropDown { get; set; }
    }
}