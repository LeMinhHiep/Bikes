using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;


using MVCBase.Enums;
using MVCModel.Models;
using MVCCore.Repositories.SalesTasks;



namespace MVCData.Repositories.SalesTasks
{
    public abstract class SalesInvoiceRepository : GenericWithDetailRepository<SalesInvoice, SalesInvoiceDetail>, ISalesInvoiceRepository
    {
        public SalesInvoiceRepository(TotalBikePortalsEntities totalBikePortalsEntities)
            : this(totalBikePortalsEntities, null) { }

        public SalesInvoiceRepository(TotalBikePortalsEntities totalBikePortalsEntities, string functionNameEditable)
            : this(totalBikePortalsEntities, functionNameEditable, null) { }

        public SalesInvoiceRepository(TotalBikePortalsEntities totalBikePortalsEntities, string functionNameEditable, string functionNameDeletable)
            : this(totalBikePortalsEntities, functionNameEditable, functionNameDeletable, null) { }

        public SalesInvoiceRepository(TotalBikePortalsEntities totalBikePortalsEntities, string functionNameEditable, string functionNameDeletable, string functionNameApprovable)
            : base(totalBikePortalsEntities, functionNameEditable, functionNameDeletable, functionNameApprovable)
        {
        }
        

    }







    public class VehiclesInvoiceRepository : SalesInvoiceRepository, IVehiclesInvoiceRepository
    {
        public VehiclesInvoiceRepository(TotalBikePortalsEntities totalBikePortalsEntities)
            : base(totalBikePortalsEntities, "VehiclesInvoiceEditable")
        {
            
            Helpers.SqlProgrammability.StockTasks.Inventories m = new Helpers.SqlProgrammability.StockTasks.Inventories(totalBikePortalsEntities);
            m.RestoreProcedure();

            
            Helpers.SqlProgrammability.PurchaseTasks.PurchaseOrder o = new Helpers.SqlProgrammability.PurchaseTasks.PurchaseOrder(totalBikePortalsEntities);
            o.RestoreProcedure();

            Helpers.SqlProgrammability.PurchaseTasks.PurchaseInvoice z = new Helpers.SqlProgrammability.PurchaseTasks.PurchaseInvoice(totalBikePortalsEntities);
            z.RestoreProcedure();



            Helpers.SqlProgrammability.SalesTasks.Quotation q = new Helpers.SqlProgrammability.SalesTasks.Quotation(totalBikePortalsEntities);
            q.RestoreProcedure();

            Helpers.SqlProgrammability.SalesTasks.VehiclesInvoice x = new Helpers.SqlProgrammability.SalesTasks.VehiclesInvoice(totalBikePortalsEntities);
            x.RestoreProcedure();

            Helpers.SqlProgrammability.SalesTasks.PartsInvoice y = new Helpers.SqlProgrammability.SalesTasks.PartsInvoice(totalBikePortalsEntities);
            y.RestoreProcedure();

            Helpers.SqlProgrammability.SalesTasks.ServicesInvoice t = new Helpers.SqlProgrammability.SalesTasks.ServicesInvoice(totalBikePortalsEntities);
            t.RestoreProcedure();

            Helpers.SqlProgrammability.SalesTasks.ServiceContracts n = new Helpers.SqlProgrammability.SalesTasks.ServiceContracts(totalBikePortalsEntities);
            n.RestoreProcedure();


            Helpers.SqlProgrammability.StockTasks.TransferOrder to = new Helpers.SqlProgrammability.StockTasks.TransferOrder(totalBikePortalsEntities);
            to.RestoreProcedure();

            Helpers.SqlProgrammability.StockTasks.GoodsReceipt a = new Helpers.SqlProgrammability.StockTasks.GoodsReceipt(totalBikePortalsEntities);
            a.RestoreProcedure();


            Helpers.SqlProgrammability.StockTasks.VehicleTransfer v = new Helpers.SqlProgrammability.StockTasks.VehicleTransfer(totalBikePortalsEntities);
            v.RestoreProcedure();

            Helpers.SqlProgrammability.StockTasks.PartTransfer b = new Helpers.SqlProgrammability.StockTasks.PartTransfer(totalBikePortalsEntities);
            b.RestoreProcedure();

            Helpers.SqlProgrammability.CommonTasks.AccessControl acl = new Helpers.SqlProgrammability.CommonTasks.AccessControl(totalBikePortalsEntities);
            acl.RestoreProcedure();

            Helpers.SqlProgrammability.CommonTasks.Commons cmm = new Helpers.SqlProgrammability.CommonTasks.Commons(totalBikePortalsEntities);
            cmm.RestoreProcedure();
            
        }

        public IQueryable<SalesInvoiceDetail> DetailLoading(string aspUserID, GlobalEnums.NmvnTaskID nmvnTaskID)//for Loading (09/07/2015) - let review and optimize Loading laster
        {
            int userID = this.TotalBikePortalsEntities.AspNetUsers.Where(w => w.Id == aspUserID).FirstOrDefault().UserID;
            return this.TotalBikePortalsEntities.SalesInvoiceDetails.Include(i => i.SalesInvoice).Where(w => w.SalesInvoice.SalesInvoiceTypeID == (int)GlobalEnums.SalesInvoiceTypeID.VehiclesInvoice && this.TotalBikePortalsEntities.AccessControls.Where(acl => acl.UserID == userID && acl.NMVNTaskID == (int)nmvnTaskID && acl.AccessLevel > 0).Select(s => s.OrganizationalUnitID).Contains(w.SalesInvoice.OrganizationalUnitID)).Include(ic => ic.Commodity).Include(cus => cus.SalesInvoice.Customer).Include(il => il.SalesInvoice.Location);
        }
    }








    public class PartsInvoiceRepository : SalesInvoiceRepository, IPartsInvoiceRepository
    {
        public PartsInvoiceRepository(TotalBikePortalsEntities totalBikePortalsEntities)
            : base(totalBikePortalsEntities, "PartsInvoiceEditable")
        {
        }
    }








    public class ServicesInvoiceRepository : SalesInvoiceRepository, IServicesInvoiceRepository
    {
        public ServicesInvoiceRepository(TotalBikePortalsEntities totalBikePortalsEntities)
            : base(totalBikePortalsEntities, "ServicesInvoiceEditable", "ServicesInvoiceDeletable")
        {
        }

        public IList<SalesInvoice> GetActiveServiceInvoices(int locationID, int? serviceInvoiceID, string searchText, int isFinished)
        {
            this.TotalBikePortalsEntities.Configuration.ProxyCreationEnabled = false;
            List<SalesInvoice> SalesInvoices = this.TotalBikePortalsEntities.SalesInvoices.Include(c => c.Customer).Include(t => t.Customer.EntireTerritory).Include(sc => sc.ServiceContract.Commodity).Include(q => q.Quotation).Where(w => w.LocationID == locationID && w.SalesInvoiceTypeID == (int)GlobalEnums.SalesInvoiceTypeID.ServicesInvoice && (w.SalesInvoiceID == serviceInvoiceID || (isFinished == -1 || (isFinished == 0 && !w.IsFinished) || (isFinished == 1 && w.IsFinished))) && (searchText == "" || w.ServiceContract.LicensePlate.Contains(searchText) || w.ServiceContract.ChassisCode.Contains(searchText) || w.ServiceContract.EngineCode.Contains(searchText))).ToList();
            this.TotalBikePortalsEntities.Configuration.ProxyCreationEnabled = true;

            return SalesInvoices;
        }

        public IList<RelatedPartsInvoiceValue> GetRelatedPartsInvoiceValue(int serviceInvoiceID)
        {
            return this.TotalBikePortalsEntities.GetRelatedPartsInvoiceValue(serviceInvoiceID).ToList();
        }

    }






}
