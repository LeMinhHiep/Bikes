using MVCModel.Models;
using MVCDTO.CommonTasks;
using MVCCore.Repositories.CommonTasks;
using MVCCore.Services.CommonTasks;


namespace MVCService.CommonTasks
{
    public class CommodityService : GenericService<Commodity, CommodityDTO, CommodityPrimitiveDTO>, ICommodityService
    {
        private readonly ICommodityRepository CommodityRepository;

        public CommodityService(ICommodityRepository CommodityRepository)
            : base(CommodityRepository)
        {
            this.CommodityRepository = CommodityRepository;
        }

    }
}

