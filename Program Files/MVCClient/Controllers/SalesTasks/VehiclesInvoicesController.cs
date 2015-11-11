using MVCModel.Models;

using MVCCore.Services.SalesTasks;

using MVCDTO.SalesTasks;

using MVCClient.ViewModels.SalesTasks;
using MVCClient.Builders.SalesTasks;


namespace MVCClient.Controllers.SalesTasks
{
    public class VehiclesInvoicesController : GenericViewDetailController<SalesInvoice, SalesInvoiceDetail, VehiclesInvoiceViewDetail, VehiclesInvoiceDTO, VehiclesInvoicePrimitiveDTO, VehiclesInvoiceDetailDTO, VehiclesInvoiceViewModel>
    {
        public VehiclesInvoicesController(IVehiclesInvoiceService vehiclesInvoiceService, IVehiclesInvoiceViewModelSelectListBuilder vehiclesInvoiceViewModelSelectListBuilder)
            : base(vehiclesInvoiceService, vehiclesInvoiceViewModelSelectListBuilder)
        {
        }
    }  
}