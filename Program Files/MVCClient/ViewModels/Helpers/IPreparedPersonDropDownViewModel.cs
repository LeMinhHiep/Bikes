using System.Web.Mvc;
using System.Collections.Generic;

namespace MVCClient.ViewModels.Helpers
{
    public interface IPreparedPersonDropDownViewModel
    {
        int PreparedPersonID { get; set; }
        IEnumerable<SelectListItem> PreparedPersonDropDown { get; set; }
    }
}
