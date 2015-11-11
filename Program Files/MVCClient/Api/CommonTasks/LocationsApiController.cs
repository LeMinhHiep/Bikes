using System.Linq;
using System.Web.Mvc;

using MVCCore.Repositories.CommonTasks;

namespace MVCClient.Api.CommonTasks
{
    public class LocationsApiController : Controller
    {
        private readonly ILocationRepository locationRepository;

        public LocationsApiController(ILocationRepository locationRepository)
        {
            this.locationRepository = locationRepository;
        }


        public JsonResult SearchLocationsByName(string searchText)
        {
            var result = locationRepository.SearchLocationsByName(searchText).Select(s => new { s.LocationID, s.Code, s.Name});

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}