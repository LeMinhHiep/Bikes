using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MVCBase.Enums;

namespace MVCBase
{
    public static class ModelSettingManager
    {

        public static int ReferenceLength = 6;
        public static string ReferencePrefix(GlobalEnums.NmvnTaskID nmvnTaskID)
        {
            switch (nmvnTaskID)
            {
                case GlobalEnums.NmvnTaskID.PurchaseOrder:
                    return "D";
                case GlobalEnums.NmvnTaskID.PurchaseInvoice:
                    return "H";

                case GlobalEnums.NmvnTaskID.Quotation:
                    return "B";

                case GlobalEnums.NmvnTaskID.SalesInvoice:
                    return @"CASE WHEN @SalesInvoiceTypeID = 
                                    " + (int)GlobalEnums.SalesInvoiceTypeID.VehiclesInvoice + @" THEN 'X' ELSE 
                             CASE WHEN @SalesInvoiceTypeID = 
                                    " + (int)GlobalEnums.SalesInvoiceTypeID.PartsInvoice + @" THEN 'P' ELSE 
                             CASE WHEN @SalesInvoiceTypeID = 
                                    " + (int)GlobalEnums.SalesInvoiceTypeID.ServicesInvoice + @" THEN 'S' ELSE '#' END
                             END END";

                case GlobalEnums.NmvnTaskID.GoodsReceipt:
                    return "N";

                case GlobalEnums.NmvnTaskID.ServiceContract:
                    return "H";

                case GlobalEnums.NmvnTaskID.TransferOrder:
                    return "LD";

                case GlobalEnums.NmvnTaskID.StockTransfer:
                    return @"CASE WHEN @StockTransferTypeID = 
                                    " + (int)GlobalEnums.StockTransferTypeID.VehicleTransfer + @" THEN 'DX' ELSE 
                             CASE WHEN @StockTransferTypeID = 
                                    " + (int)GlobalEnums.StockTransferTypeID.PartTransfer + @" THEN 'DP' ELSE '#' END 
                             END";
                default:
                    return "";
            }


        }
    }
}
