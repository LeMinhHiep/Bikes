using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.Collections.Generic;

using AutoMapper;

using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;


using MVCModel.Models;

using MVCDTO.SalesTasks;

using MVCCore.Repositories.SalesTasks;
using MVCCore.Services.SalesTasks;
using System.Text;
using MVCBase.Enums;



namespace MVCClient.Api.SalesTasks
{
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class QuotationsApiController : Controller
    {
        private readonly IQuotationRepository quotationRepository;
        private readonly IQuotationService quotationService;

        public QuotationsApiController(IQuotationRepository quotationRepository, IQuotationService quotationService)
        {
            this.quotationRepository = quotationRepository;
            this.quotationService = quotationService;
        }

        public JsonResult GetQuotations([DataSourceRequest] DataSourceRequest request)
        {
            IQueryable<Quotation> quotations = this.quotationRepository.GetAll().Include(c => c.Customer).Include(e => e.Customer.EntireTerritory).Include(s => s.ServiceContract.Commodity);

            DataSourceResult response = quotations.ToDataSourceResult(request, o => new QuotationPrimitiveDTO
            {
                QuotationID = o.QuotationID,
                EntryDate = o.EntryDate,
                Reference = o.Reference,
                CustomerName = o.Customer.Name,
                CustomerBirthday = o.Customer.Birthday,
                CustomerTelephone = o.Customer.Telephone,
                CustomerAddressNo = o.Customer.AddressNo,
                CustomerEntireTerritoryEntireName = o.Customer.EntireTerritory.EntireName,
                ServiceContractCommodityCode = o.ServiceContract.Commodity.Code,
                ServiceContractCommodityName = o.ServiceContract.Commodity.Name,
                ServiceContractLicensePlate = o.ServiceContract.LicensePlate,
                ServiceContractChassisCode = o.ServiceContract.ChassisCode,
                ServiceContractEngineCode = o.ServiceContract.EngineCode,
                TotalGrossAmount = o.TotalGrossAmount,
                Description = o.Description,
                Remarks = o.Remarks
            });
            return Json(response, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetActiveQuotations([DataSourceRequest] DataSourceRequest dataSourceRequest, int? quotationID, string searchQuotation, int isFinished)
        {
            var result = quotationRepository.GetActiveQuotations(quotationID, searchQuotation, isFinished).Select(s => new
            {
                s.QuotationID,
                QuotationReference = s.Reference,
                QuotationEntryDate = s.EntryDate,

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


        public JsonResult GetQuotationDetails([DataSourceRequest] DataSourceRequest dataSourceRequest, int quotationID, string commodityTypeIDList)
        {
            IEnumerable<QuotationDetailPopupDTO> QuotationDetailPopupDTOs;
            IEnumerable<QuotationViewDetail> entityViewDetails = this.quotationService.GetViewDetails(quotationID);

            if (commodityTypeIDList != null)
            {
                List<int> listCommodityTypeID = commodityTypeIDList.Split(',').Select(n => int.Parse(n)).ToList();
                entityViewDetails = entityViewDetails.Where(w => listCommodityTypeID.Contains(w.CommodityTypeID));
            }

            QuotationDetailPopupDTOs = Mapper.Map<IEnumerable<QuotationViewDetail>, IEnumerable<QuotationDetailPopupDTO>>(entityViewDetails);
            return Json(QuotationDetailPopupDTOs.ToDataSourceResult(dataSourceRequest), JsonRequestBehavior.AllowGet);
        }

    }


}