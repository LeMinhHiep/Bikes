using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;

using MVCCore.Repositories.SalesTasks;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using MVCDTO.SalesTasks;
using System.Collections.Generic;
using MVCModel.Models;



using Microsoft.AspNet.Identity;




namespace MVCClient.Api.SalesTasks
{
    //[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class ServiceContractsApiController : Controller
    {
        private readonly IServiceContractRepository serviceContractRepository;

        public ServiceContractsApiController(IServiceContractRepository serviceContractRepository)
        {
            this.serviceContractRepository = serviceContractRepository;
        }



        public JsonResult SearchAgentName(string agentName)
        {
            return Json(serviceContractRepository.SearchAgentName(agentName), JsonRequestBehavior.AllowGet);
        }


        public JsonResult SearchServiceContracts([DataSourceRequest] DataSourceRequest dataSourceRequest, string searchText)
        {
            var result = serviceContractRepository.SearchServiceContracts(searchText);
            return Json(result.ToDataSourceResult(dataSourceRequest), JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetServiceContracts([DataSourceRequest] DataSourceRequest request)
        {
            IQueryable<ServiceContract> serviceContracts = this.serviceContractRepository.Loading(User.Identity.GetUserId(), MVCBase.Enums.GlobalEnums.NmvnTaskID.ServiceContract).Include(c => c.Customer);

            DataSourceResult response = serviceContracts.ToDataSourceResult(request, o => new ServiceContractPrimitiveDTO
            {
                ServiceContractID = o.ServiceContractID,
                Reference = o.Reference,
                EntryDate = o.EntryDate,
                CustomerName = o.Customer.Name,
                CustomerBirthday = o.Customer.Birthday,
                CustomerTelephone = o.Customer.Telephone,
                LicensePlate = o.LicensePlate,
                ChassisCode = o.ChassisCode,
                EngineCode = o.EngineCode,
                Description = o.Description,
                Remarks = o.Remarks
            });
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ServiceContractGetVehiclesInvoice([DataSourceRequest] DataSourceRequest dataSourceRequest, int locationID, string searchText, int? salesInvoiceID, int? serviceContractID)
        {
            ICollection<ServiceContractGetVehiclesInvoice> serviceContractGetVehiclesInvoice = this.serviceContractRepository.ServiceContractGetVehiclesInvoice(locationID, searchText, salesInvoiceID, serviceContractID);
            return Json(serviceContractGetVehiclesInvoice.ToDataSourceResult(dataSourceRequest), JsonRequestBehavior.AllowGet);
        }
    }
}