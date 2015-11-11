using System.Collections.Generic;

using MVCModel.Models;

namespace MVCCore.Repositories.CommonTasks
{
    public interface ILocationRepository
    {
        IList<Location> SearchLocationsByName(string searchText);
    }
}
