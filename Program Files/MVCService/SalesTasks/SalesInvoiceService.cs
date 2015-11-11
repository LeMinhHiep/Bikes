using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;

using AutoMapper;

using MVCBase.Enums;
using MVCModel.Models;
using MVCDTO.SalesTasks;
using MVCCore.Repositories.SalesTasks;
using MVCCore.Repositories.CommonTasks;
using MVCCore.Services.SalesTasks;
using MVCCore.Services.Helpers;
using MVCService.Helpers;

namespace MVCService.SalesTasks
{
    public class VehiclesInvoiceService : GenericWithViewDetailService<SalesInvoice, SalesInvoiceDetail, VehiclesInvoiceViewDetail, VehiclesInvoiceDTO, VehiclesInvoicePrimitiveDTO, VehiclesInvoiceDetailDTO>, IVehiclesInvoiceService
    {
        private readonly IServiceContractRepository serviceContractRepository;
        private readonly IServiceContractService serviceContractService;

        public VehiclesInvoiceService(IVehiclesInvoiceRepository vehiclesInvoiceRepository,
                                      IServiceContractRepository serviceContractRepository,
                                      IServiceContractService serviceContractService)
            : base(vehiclesInvoiceRepository, "VehiclesInvoicePostSaveValidate", "VehiclesInvoiceSaveRelative", "GetVehiclesInvoiceViewDetails")
        {
            this.serviceContractRepository = serviceContractRepository;
            this.serviceContractService = serviceContractService;
        }

        public override ICollection<VehiclesInvoiceViewDetail> GetViewDetails(int salesInvoiceID)
        {
            ObjectParameter[] parameters = new ObjectParameter[] { new ObjectParameter("SalesInvoiceID", salesInvoiceID) };
            return this.GetViewDetails(parameters);
        }


        public override bool Save(VehiclesInvoiceDTO vehiclesInvoiceDTO)
        {
            vehiclesInvoiceDTO.VehiclesInvoiceViewDetails.RemoveAll(x => x.Quantity == 0);
            return base.Save(vehiclesInvoiceDTO);
        }

        protected override SalesInvoice SaveMe(VehiclesInvoiceDTO vehiclesInvoiceDTO)
        {
            SalesInvoice salesInvoice = base.SaveMe(vehiclesInvoiceDTO);

            ICollection<ServiceContractGetVehiclesInvoice> serviceContractGetVehiclesInvoices = this.serviceContractRepository.ServiceContractGetVehiclesInvoice(salesInvoice.LocationID, "", salesInvoice.SalesInvoiceID, null);
            foreach (ServiceContractGetVehiclesInvoice serviceContractGetVehiclesInvoice in serviceContractGetVehiclesInvoices)
            {

                ServiceContractDTO serviceContract = new ServiceContractDTO();

                serviceContract.EntryDate = serviceContractGetVehiclesInvoice.EntryDate;
                serviceContract.SalesInvoiceDetailID = serviceContractGetVehiclesInvoice.SalesInvoiceDetailID;
                serviceContract.PurchaseDate = serviceContractGetVehiclesInvoice.EntryDate;

                serviceContract.ServiceContractTypeID = (int)GlobalEnums.ServiceContractTypeID.Warranty;

                serviceContract.CustomerID = serviceContractGetVehiclesInvoice.CustomerID;
                serviceContract.CommodityID = serviceContractGetVehiclesInvoice.CommodityID;
                serviceContract.ChassisCode = serviceContractGetVehiclesInvoice.ChassisCode;
                serviceContract.EngineCode = serviceContractGetVehiclesInvoice.EngineCode;
                serviceContract.ColorCode = serviceContractGetVehiclesInvoice.ColorCode;

                serviceContract.BeginningDate = serviceContractGetVehiclesInvoice.BeginningDate;
                serviceContract.EndingDate = serviceContractGetVehiclesInvoice.EndingDate;

                serviceContract.BeginningMeters = 0;
                serviceContract.EndingMeters = serviceContractGetVehiclesInvoice.LimitedKilometreWarranty;

                serviceContract.PreparedPersonID = vehiclesInvoiceDTO.PreparedPersonID;
                serviceContract.ApproverID = vehiclesInvoiceDTO.ApproverID;

                this.serviceContractService.UserID = this.UserID;
                this.serviceContractService.Save(serviceContract, true);
            }

            return salesInvoice;
        }
    }












    public class PartsInvoiceService : GenericWithViewDetailService<SalesInvoice, SalesInvoiceDetail, PartsInvoiceViewDetail, PartsInvoiceDTO, PartsInvoicePrimitiveDTO, PartsInvoiceDetailDTO>, IPartsInvoiceService
    {
        private DateTime? checkedDate; //For check over stock
        private string warehouseIDList = "";
        private string commodityIDList = "";

        private readonly IInventoryRepository inventoryRepository;
        private readonly IPartsInvoiceHelperService partsInvoiceHelperService;
        

        public PartsInvoiceService(IPartsInvoiceRepository partsInvoiceRepository, IInventoryRepository inventoryRepository, IPartsInvoiceHelperService partsInvoiceHelperService)
            : base(partsInvoiceRepository, "PartsInvoicePostSaveValidate", "PartsInvoiceSaveRelative", "GetPartsInvoiceViewDetails")
        {
            this.inventoryRepository = inventoryRepository;
            this.partsInvoiceHelperService = partsInvoiceHelperService;
        }

        public override ICollection<PartsInvoiceViewDetail> GetViewDetails(int salesInvoiceID)
        {
            ObjectParameter[] parameters = new ObjectParameter[] { new ObjectParameter("SalesInvoiceID", salesInvoiceID) };
            return this.GetViewDetails(parameters);
        }

        public override bool Save(PartsInvoiceDTO partsInvoiceDTO)
        {
            partsInvoiceDTO.PartsInvoiceViewDetails.RemoveAll(x => x.Quantity == 0);
            return base.Save(partsInvoiceDTO);
        }

        protected override void UpdateDetail(PartsInvoiceDTO dto, SalesInvoice entity)
        {
            this.partsInvoiceHelperService.GetWCParameters(dto, null, ref this.checkedDate, ref this.warehouseIDList, ref this.commodityIDList);

            base.UpdateDetail(dto, entity);
        }

        protected override void UndoDetail(PartsInvoiceDTO dto, SalesInvoice entity, bool isDelete)
        {
            this.partsInvoiceHelperService.GetWCParameters(null, entity, ref this.checkedDate, ref this.warehouseIDList, ref this.commodityIDList);

            base.UndoDetail(dto, entity, isDelete);
        }



        protected override void PostSaveValidate(SalesInvoice entity)
        {
            this.inventoryRepository.CheckOverStock(this.checkedDate, this.warehouseIDList, this.commodityIDList);
            base.PostSaveValidate(entity);
        }


        public override bool Editable(PartsInvoiceDTO dto)
        {
            bool editable = base.Editable(dto);
            if (editable && dto.GetID() <= 0 && dto.ServiceInvoiceID != null) //RE CHECK EDITABLE IF THIS IS NEW PartsInvoice for AN EXISTING ServiceInvoice (MEANS: dto.GetID() <= 0 AND dto.ServiceInvoiceID != null)
                return !base.GenericWithDetailRepository.CheckExisting((int)dto.ServiceInvoiceID, "ServicesInvoiceEditable");
            else
                return editable;
        }


    }












    public class ServicesInvoiceService : GenericWithDetailService<SalesInvoice, SalesInvoiceDetail, ServicesInvoiceDTO, ServicesInvoicePrimitiveDTO, ServicesInvoiceDetailDTO>, IServicesInvoiceService
    {
        public ServicesInvoiceService(IServicesInvoiceRepository ServicesInvoiceRepository)
            : base(ServicesInvoiceRepository, "ServicesInvoicePostSaveValidate")
        {
        }

        public override bool Save(ServicesInvoiceDTO servicesInvoiceDTO)
        {
            servicesInvoiceDTO.SalesInvoiceDetails.RemoveAll(x => x.Quantity == 0);
            return base.Save(servicesInvoiceDTO);
        }
    }





    public class PartsInvoiceHelperService : HelperService<SalesInvoice, SalesInvoiceDetail, PartsInvoiceDTO, PartsInvoiceDetailDTO>, IPartsInvoiceHelperService
    {
    }


}
