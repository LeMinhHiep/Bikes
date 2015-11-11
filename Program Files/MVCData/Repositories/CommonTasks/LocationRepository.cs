using System.Linq;
using System.Collections.Generic;

using MVCModel.Models;
using MVCCore.Repositories.CommonTasks;

namespace MVCData.Repositories.CommonTasks
{
    public class LocationRepository : ILocationRepository
    {
        private readonly TotalBikePortalsEntities totalBikePortalsEntities;

        public LocationRepository(TotalBikePortalsEntities totalBikePortalsEntities)
        {
            this.totalBikePortalsEntities = totalBikePortalsEntities;
        }

        public IList<Location> SearchLocationsByName(string searchText)
        {
            this.totalBikePortalsEntities.Configuration.ProxyCreationEnabled = false;

            List<Location> locations = this.totalBikePortalsEntities.Locations.Where(w => w.Code.Contains(searchText) || w.Name.Contains(searchText)).ToList();

            this.totalBikePortalsEntities.Configuration.ProxyCreationEnabled = true;

            return locations;
        }
    }
}
