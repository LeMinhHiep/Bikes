using System.Collections.Generic;
using MVCModel.Models;

namespace MVCCore.Repositories.StockTasks
{
    public interface ITransferOrderRepository : IGenericWithDetailRepository<TransferOrder, TransferOrderDetail>
    {
        IList<TransferOrder> SearchTransferOrders(int locationID, string commodityTypeIDList, string searchText);
    }

    public interface IVehicleTransferOrderRepository : ITransferOrderRepository
    { }

    public interface IPartTransferOrderRepository : ITransferOrderRepository
    { }
}
