using System.Web.Mvc;
using System.Collections.Generic;

namespace MVCClient.ViewModels.Helpers
{
    public interface IApproverDropDownViewModel
    {
        int ApproverID { get; set; }
        IEnumerable<SelectListItem> ApproverDropDown { get; set; }
    }
}
