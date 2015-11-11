using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

using MVCModel.Models;
using MVCCore.Repositories.CommonTasks;

namespace MVCData.Repositories.CommonTasks
{
    public class CommodityRepository : GenericRepository<Commodity>, ICommodityRepository
    {
        public CommodityRepository(TotalBikePortalsEntities totalBikePortalsEntities)
            : base(totalBikePortalsEntities)
        {
        }



        public IList<Commodity> SearchCommoditiesByName(string searchText, string commodityTypeIDList)
        {
            this.TotalBikePortalsEntities.Configuration.ProxyCreationEnabled = false;

            var queryable = this.TotalBikePortalsEntities.Commodities.Where(w => w.Code.Contains(searchText) || w.Name.Contains(searchText)).Include(i => i.CommodityCategory);
            if (commodityTypeIDList != null)
            {
                List<int> listCommodityTypeID = commodityTypeIDList.Split(',').Select(n => int.Parse(n)).ToList();
                queryable = queryable.Where(w => listCommodityTypeID.Contains(w.CommodityTypeID));
            }

            List<Commodity> commodities = queryable.ToList();

            this.TotalBikePortalsEntities.Configuration.ProxyCreationEnabled = true;

            return commodities;
        }

        public IList<Commodity> SearchCommoditiesByIndex(int commodityCategoryID, int commodityTypeID)
        {
            this.TotalBikePortalsEntities.Configuration.ProxyCreationEnabled = false;
            List<Commodity> commodities = this.TotalBikePortalsEntities.Commodities.Where(w => w.CommodityCategoryID == commodityCategoryID || w.CommodityTypeID == commodityTypeID).ToList();
            this.TotalBikePortalsEntities.Configuration.ProxyCreationEnabled = true;

            return commodities;
        }

        public IList<CommoditiesInGoodsReceipt> GetCommoditiesInGoodsReceipts(int? locationID, string searchText, int? salesInvoiceID, int? stockTransferID, int? stockAdjustID)
        {
            List<CommoditiesInGoodsReceipt> commoditiesInGoodsReceipts = this.TotalBikePortalsEntities.GetCommoditiesInGoodsReceipts(locationID, searchText, salesInvoiceID, stockTransferID, stockAdjustID).ToList();

            return commoditiesInGoodsReceipts;
        }

        public IList<CommoditiesInWarehouse> GetCommoditiesInWarehouses(int? locationID, DateTime? entryDate, string searchText, int? salesInvoiceID, int? stockTransferID, int? stockAdjustID)
        {
            List<CommoditiesInWarehouse> commoditiesInWarehouses = this.TotalBikePortalsEntities.GetCommoditiesInWarehouses(locationID, entryDate, searchText, salesInvoiceID, stockTransferID, stockAdjustID).ToList();

            return commoditiesInWarehouses;
        }

        public IList<CommoditiesAvailable> GetCommoditiesAvailables(int? locationID, DateTime? entryDate, string searchText)
        {
            List<CommoditiesAvailable> commoditiesAvailables = this.TotalBikePortalsEntities.GetCommoditiesAvailables(locationID, entryDate, searchText).ToList();

            return commoditiesAvailables;
        }

        public IList<VehicleAvailable> GetVehicleAvailables(int? locationID, DateTime? entryDate, string searchText)
        {
            List<VehicleAvailable> vehicleAvailables = this.TotalBikePortalsEntities.GetVehicleAvailables(locationID, entryDate, searchText).ToList();

            return vehicleAvailables;
        }

        public IList<PartAvailable> GetPartAvailables(int? locationID, DateTime? entryDate, string searchText)
        {
            List<PartAvailable> partAvailables = this.TotalBikePortalsEntities.GetPartAvailables(locationID, entryDate, searchText).ToList();

            return partAvailables;
        }


    }
}
