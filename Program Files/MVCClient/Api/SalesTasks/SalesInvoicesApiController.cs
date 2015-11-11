using System;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.Web.UI;

using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;


using MVCBase.Enums;
using MVCModel.Models;

using MVCDTO.SalesTasks;

using MVCCore.Repositories.SalesTasks;
using MVCClient.ViewModels.SalesTasks;
using System.Collections.Generic;





using Microsoft.AspNet.Identity;





namespace MVCClient.Api.SalesTasks
{
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class VehiclesInvoiceApiController : Controller
    {
        private readonly IVehiclesInvoiceRepository vehiclesInvoiceRepository;

        public VehiclesInvoiceApiController(IVehiclesInvoiceRepository vehiclesInvoiceRepository)
        {
            this.vehiclesInvoiceRepository = vehiclesInvoiceRepository;
        }

        public JsonResult GetVehiclesInvoices([DataSourceRequest] DataSourceRequest request)
        {
            IQueryable<SalesInvoiceDetail> salesInvoiceDetails = this.vehiclesInvoiceRepository.DetailLoading(User.Identity.GetUserId(), MVCBase.Enums.GlobalEnums.NmvnTaskID.VehiclesInvoice);

            DataSourceResult response = salesInvoiceDetails.ToDataSourceResult(request, o => new VehiclesInvoicePrimitiveDTO
            {
                Remarks = o.SalesInvoice.Location.Code,
                SalesInvoiceID = o.SalesInvoiceID,
                EntryDate = o.EntryDate,
                VATInvoiceNo = o.SalesInvoice.VATInvoiceNo,
                CustomerName = o.SalesInvoice.Customer.Name + ",    " + o.SalesInvoice.Customer.AddressNo,
                Description = o.Commodity.Name,
                TotalGrossAmount = o.GrossAmount
            });

            return Json(response, JsonRequestBehavior.AllowGet);
        }
       
    }



    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class PartsInvoiceApiController : Controller
    {
        private readonly IPartsInvoiceRepository partsInvoiceRepository;

        public PartsInvoiceApiController(IPartsInvoiceRepository partsInvoiceRepository)
        {
            this.partsInvoiceRepository = partsInvoiceRepository;
        }

        public JsonResult GetPartsInvoices([DataSourceRequest] DataSourceRequest request)
        {
            IQueryable<SalesInvoice> salesInvoices = this.partsInvoiceRepository.Loading(User.Identity.GetUserId(), MVCBase.Enums.GlobalEnums.NmvnTaskID.PartsInvoice).Where(t => t.SalesInvoiceTypeID == (int)GlobalEnums.SalesInvoiceTypeID.PartsInvoice).Include(c => c.Customer).Include(e => e.Customer.EntireTerritory).Include(s => s.ServiceContract.Commodity).Include(i => i.SalesInvoice1);

            DataSourceResult response = salesInvoices.ToDataSourceResult(request, o => new PartsInvoicePrimitiveDTO
            {
                SalesInvoiceID = o.SalesInvoiceID,
                EntryDate = o.EntryDate,
                Reference = o.Reference,
                CustomerName = o.Customer.Name,
                CustomerBirthday = o.Customer.Birthday,
                CustomerTelephone = o.Customer.Telephone,
                CustomerAddressNo = o.Customer.AddressNo,
                CustomerEntireTerritoryEntireName = o.Customer.EntireTerritory.EntireName,
                ServiceContractCommodityCode = o.ServiceContract != null ? o.ServiceContract.Commodity.Code : "",
                ServiceContractCommodityName = o.ServiceContract != null ? o.ServiceContract.Commodity.Name : "",
                ServiceContractLicensePlate = o.ServiceContract != null ? o.ServiceContract.LicensePlate : "",
                ServiceContractChassisCode = o.ServiceContract != null ? o.ServiceContract.ChassisCode : "",
                ServiceContractEngineCode = o.ServiceContract != null ? o.ServiceContract.EngineCode : "",
                ServiceInvoiceEntryDate = o.SalesInvoice1 != null ? o.SalesInvoice1.EntryDate : (DateTime?)null,
                TotalGrossAmount = o.TotalGrossAmount,
                Description = o.Description,
                Remarks = o.Remarks
            });
            return Json(response, JsonRequestBehavior.AllowGet);
        }

    }


    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class ServicesInvoiceApiController : Controller
    {
        private readonly IServicesInvoiceRepository servicesInvoiceRepository;

        public ServicesInvoiceApiController(IServicesInvoiceRepository servicesInvoiceRepository)
        {
            this.servicesInvoiceRepository = servicesInvoiceRepository;
        }

        public JsonResult GetServicesInvoices([DataSourceRequest] DataSourceRequest request)
        {
            IQueryable<SalesInvoice> salesInvoices = this.servicesInvoiceRepository.Loading(User.Identity.GetUserId(), MVCBase.Enums.GlobalEnums.NmvnTaskID.ServicesInvoice).Where(t => t.SalesInvoiceTypeID == (int)GlobalEnums.SalesInvoiceTypeID.ServicesInvoice).Include(c => c.Customer).Include(e => e.Customer.EntireTerritory).Include(s => s.ServiceContract.Commodity);

            DataSourceResult response = salesInvoices.ToDataSourceResult(request, o => new ServicesInvoicePrimitiveDTO
            {
                SalesInvoiceID = o.SalesInvoiceID,
                EntryDate = o.EntryDate,
                Reference = o.Reference,
                CustomerName = o.Customer.Name,
                CustomerBirthday = o.Customer.Birthday,
                CustomerTelephone = o.Customer.Telephone,
                CustomerAddressNo = o.Customer.AddressNo,
                CustomerEntireTerritoryEntireName = o.Customer.EntireTerritory.EntireName,
                ServiceContractCommodityName = o.ServiceContract.Commodity.Name,
                ServiceContractLicensePlate = o.ServiceContract.LicensePlate,
                ServiceContractChassisCode = o.ServiceContract.ChassisCode,
                ServiceContractEngineCode = o.ServiceContract.EngineCode,
                ServiceContractColorCode = o.ServiceContract.ColorCode,
                TotalGrossAmount = o.TotalGrossAmount,
                Description = o.Description,
                Remarks = o.Remarks
            });
            return Json(response, JsonRequestBehavior.AllowGet);
        }



        public JsonResult GetActiveServiceInvoices([DataSourceRequest] DataSourceRequest dataSourceRequest, int locationID, int? serviceInvoiceID, string licensePlate, int isFinished)
        {
            var result = servicesInvoiceRepository.GetActiveServiceInvoices(locationID, serviceInvoiceID, licensePlate, isFinished).Select(s => new
            {
                s.SalesInvoiceID,
                s.Reference,
                s.EntryDate,

                s.QuotationID,
                QuotationReference = s.Quotation != null ? s.Quotation.Reference : null,
                QuotationEntryDate = s.Quotation != null ? s.Quotation.EntryDate : (DateTime?)null,

                s.CustomerID,
                CustomerName = s.Customer.Name,
                CustomerBirthday = s.Customer.Birthday,
                CustomerTelephone = s.Customer.Telephone,
                CustomerAddressNo = s.Customer.AddressNo,
                CustomerEntireTerritoryEntireName = s.Customer.EntireTerritory.Name,

                s.ServiceContractID,
                ServiceContractReference = s.ServiceContract.Reference,
                ServiceContractCommodityID = s.ServiceContract.CommodityID,
                ServiceContractCommodityCode = s.ServiceContract.Commodity.Code,
                ServiceContractCommodityName = s.ServiceContract.Commodity.Name,
                ServiceContractLicensePlate = s.ServiceContract.LicensePlate,
                ServiceContractColorCode = s.ServiceContract.ColorCode,
                ServiceContractChassisCode = s.ServiceContract.ChassisCode,
                ServiceContractEngineCode = s.ServiceContract.EngineCode,
                ServiceContractPurchaseDate = s.ServiceContract.PurchaseDate,
                ServiceContractAgentName = s.ServiceContract.AgentName
            });
            return Json(result.ToDataSourceResult(dataSourceRequest), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [OutputCache(NoStore = true, Location = OutputCacheLocation.Client, Duration = 0)]
        public JsonResult GetRelatedPartsInvoiceValue(int serviceInvoiceID)
        {
            try
            {
                var relatedPartsInvoiceValue = this.servicesInvoiceRepository.GetRelatedPartsInvoiceValue(serviceInvoiceID);
                return Json(new
                {
                    NoInvoice = relatedPartsInvoiceValue[0].NoInvoice,
                    TotalPartsAmount = relatedPartsInvoiceValue[0].TotalGrossAmount
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

    }
}