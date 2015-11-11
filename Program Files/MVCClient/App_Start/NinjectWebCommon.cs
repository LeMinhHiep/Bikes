[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(MVCClient.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(MVCClient.App_Start.NinjectWebCommon), "Stop")]

namespace MVCClient.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using MVCModel.Models;
    using MVCModel.Helpers;
    using MVCCore.Repositories.CommonTasks;
    using MVCCore.Repositories.PurchaseTasks;
    using MVCCore.Repositories.SalesTasks;
    using MVCCore.Services.CommonTasks;
    using MVCCore.Services.PurchaseTasks;
    using MVCCore.Services.SalesTasks;
    using MVCData.Repositories.CommonTasks;
    using MVCData.Repositories.PurchaseTasks;
    using MVCData.Repositories.SalesTasks;
    using MVCClient.Builders;
    using MVCClient.Builders.PurchaseTasks;
    using MVCClient.Builders.SalesTasks;
    using MVCClient.Builders.CommonTasks;
    using MVCClient.Converters;
    using MVCClient.ViewModels.PurchaseTasks;
    using MVCClient.ViewModels.SalesTasks;
    using MVCCore.Helpers;
    using MVCData.Helpers;
    using MVCClient.ViewModels.Menus;
    using MVCService.CommonTasks; 
    using MVCService.PurchaseTasks;
    using MVCService.SalesTasks;
    using MVCDTO.PurchaseTasks;
    using MVCDTO.SalesTasks;
    using MVCCore.Services.StockTasks;
    using MVCService.StockTasks;
    using MVCCore.Repositories.StockTasks;
    using MVCData.Repositories.StockTasks;
    using MVCClient.Builders.StockTasks;
    using MVCCore.Services.Helpers;
    using MVCService.Helpers;
       

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                kernel.Bind<TotalBikePortalsEntities>().ToSelf().InRequestScope();
                kernel.Bind<CommonTableEntities>().ToSelf().InRequestScope();

                kernel.Bind<IPurchaseOrderService>().To<PurchaseOrderService>();
                kernel.Bind<IPurchaseOrderRepository>().To<PurchaseOrderRepository>();

                kernel.Bind<IPurchaseInvoiceService>().To<PurchaseInvoiceService>();                
                kernel.Bind<IPurchaseInvoiceRepository>().To<PurchaseInvoiceRepository>();

                kernel.Bind<IGoodsReceiptService>().To<GoodsReceiptService>();
                kernel.Bind<IGoodsReceiptRepository>().To<GoodsReceiptRepository>();
                kernel.Bind<IGoodsReceiptHelperService>().To<GoodsReceiptHelperService>();

                kernel.Bind<IVehiclesInvoiceService>().To<VehiclesInvoiceService>();
                kernel.Bind<IVehiclesInvoiceRepository>().To<VehiclesInvoiceRepository>();

                kernel.Bind<IPartsInvoiceService>().To<PartsInvoiceService>();
                kernel.Bind<IPartsInvoiceRepository>().To<PartsInvoiceRepository>();
                kernel.Bind<IPartsInvoiceHelperService>().To<PartsInvoiceHelperService>();

                kernel.Bind<IServicesInvoiceService>().To<ServicesInvoiceService>();
                kernel.Bind<IServicesInvoiceRepository>().To<ServicesInvoiceRepository>();

                kernel.Bind<IQuotationService>().To<QuotationService>();
                kernel.Bind<IQuotationRepository>().To<QuotationRepository>();

                kernel.Bind<ICustomerService>().To<CustomerService>();
                kernel.Bind<ICustomerRepository>().To<CustomerRepository>();

                kernel.Bind<ICommodityService>().To<CommodityService>();
                kernel.Bind<ICommodityRepository>().To<CommodityRepository>(); 

                kernel.Bind<IServiceContractService>().To<ServiceContractService>();
                kernel.Bind<IServiceContractRepository>().To<ServiceContractRepository>();

                kernel.Bind<IVehicleTransferOrderService>().To<VehicleTransferOrderService>();
                kernel.Bind<ITransferOrderRepository>().To<TransferOrderRepository>();

                kernel.Bind<IVehicleTransferService>().To<VehicleTransferService>();
                kernel.Bind<IVehicleTransferRepository>().To<VehicleTransferRepository>();

                kernel.Bind<IPartTransferService>().To<PartTransferService>();
                kernel.Bind<IPartTransferRepository>().To<PartTransferRepository>();

                kernel.Bind<IPartTransferHelperService>().To<PartTransferHelperService>(); 

                kernel.Bind<IPriceTermRepository>().To<PriceTermRepository>();
                kernel.Bind<IPaymentTermRepository>().To<PaymentTermRepository>();

                kernel.Bind<IEntireTerritoryRepository>().To<EntireTerritoryRepository>();
                kernel.Bind<IInventoryRepository>().To<InventoryRepository>();
                kernel.Bind<ILocationRepository>().To<LocationRepository>();
                kernel.Bind<IWarehouseRepository>().To<WarehouseRepository>();
                kernel.Bind<ICommodityCategoryRepository>().To<CommodityCategoryRepository>();
                kernel.Bind<ICommodityTypeRepository>().To<CommodityTypeRepository>();
                kernel.Bind<ICustomerCategoryRepository>().To<CustomerCategoryRepository>();
                kernel.Bind<ICustomerTypeRepository>().To<CustomerTypeRepository>();
                kernel.Bind<ITerritoryRepository>().To<TerritoryRepository>();
                kernel.Bind<IAspNetUserRepository>().To<AspNetUserRepository>();
                kernel.Bind<IServiceContractTypeRepository>().To<ServiceContractTypeRepository>();
                kernel.Bind<IModuleRepository>().To<ModuleRepository>();
                kernel.Bind<IModuleDetailRepository>().To<ModuleDetailRepository>();

                kernel.Bind<IPurchaseOrderViewModelSelectListBuilder>().To<PurchaseOrderViewModelSelectListBuilder>();
                kernel.Bind<IPurchaseInvoiceViewModelSelectListBuilder>().To<PurchaseInvoiceViewModelSelectListBuilder>();
                kernel.Bind<IGoodsReceiptViewModelSelectListBuilder>().To<GoodsReceiptViewModelSelectListBuilder>();
                kernel.Bind<IVehiclesInvoiceViewModelSelectListBuilder>().To<VehiclesInvoiceViewModelSelectListBuilder>();
                kernel.Bind<IPartsInvoiceViewModelSelectListBuilder>().To<PartsInvoiceViewModelSelectListBuilder>();
                kernel.Bind<IServicesInvoiceViewModelSelectListBuilder>().To<ServicesInvoiceViewModelSelectListBuilder>();
                kernel.Bind<IQuotationViewModelSelectListBuilder>().To<QuotationViewModelSelectListBuilder>();

                kernel.Bind<ICommodityViewModelSelectListBuilder>().To<CommodityViewModelSelectListBuilder>();
                kernel.Bind<ICommodityCategorySelectListBuilder>().To<CommodityCategorySelectListBuilder>();
                kernel.Bind<ICommodityTypeSelectListBuilder>().To<CommodityTypeSelectListBuilder>();

                kernel.Bind<IVehicleTransferOrderViewModelSelectListBuilder>().To<VehicleTransferOrderViewModelSelectListBuilder>();
                kernel.Bind<IVehicleTransferViewModelSelectListBuilder>().To<VehicleTransferViewModelSelectListBuilder>();
                kernel.Bind<IPartTransferViewModelSelectListBuilder>().To<PartTransferViewModelSelectListBuilder>();

                kernel.Bind<IPriceTermSelectListBuilder>().To<PriceTermSelectListBuilder>();
                kernel.Bind<IPaymentTermSelectListBuilder>().To<PaymentTermSelectListBuilder>();
                kernel.Bind<IAspNetUserSelectListBuilder>().To<AspNetUserSelectListBuilder>();

                kernel.Bind<ICustomerViewModelSelectListBuilder>().To<CustomerViewModelSelectListBuilder>();
                kernel.Bind<ICustomerCategorySelectListBuilder>().To<CustomerCategorySelectListBuilder>();
                kernel.Bind<ICustomerTypeSelectListBuilder>().To<CustomerTypeSelectListBuilder>();

                kernel.Bind<IServiceContractViewModelSelectListBuilder>().To<ServiceContractViewModelSelectListBuilder>();
                kernel.Bind<IServiceContractTypeSelectListBuilder>().To<ServiceContractTypeSelectListBuilder>();
                kernel.Bind<IWarehouseSelectListBuilder>().To<WarehouseSelectListBuilder>();
                
                kernel.Bind<IConverter<ModuleViewModel, Module>>().To<ModuleViewModelConverter>();
                kernel.Bind<IConverter<ModuleDetailViewModel, ModuleDetail>>().To<ModuleDetailViewModelConverter>();

                
                

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
        }        
    }
}
