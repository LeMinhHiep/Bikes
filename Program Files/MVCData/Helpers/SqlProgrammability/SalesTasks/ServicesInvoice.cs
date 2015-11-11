using MVCModel.Models;

namespace MVCData.Helpers.SqlProgrammability.SalesTasks
{
    public class ServicesInvoice
    {
        private readonly TotalBikePortalsEntities totalBikePortalsEntities;

        public ServicesInvoice(TotalBikePortalsEntities totalBikePortalsEntities)
        {
            this.totalBikePortalsEntities = totalBikePortalsEntities;
        }

        public void RestoreProcedure()
        {
            this.GetRelatedPartsInvoiceValue();

            this.ServicesInvoicePostSaveValidate();

            this.ServicesInvoiceEditable();
            this.ServicesInvoiceDeletable();

            this.SalesInvoicePrint();
        }

        /// <summary>
        /// Get QuantityAvailable (Remaining) Commodities BY EVERY GoodsReceiptDetailID (THIS MEANS: BY EVERY GoodsReceiptDetails LINE)
        /// </summary>
        private void GetRelatedPartsInvoiceValue()
        {
            string queryString = " @ServiceInvoiceID int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            queryString = queryString + "       SELECT      COUNT(SalesInvoiceID) AS NoInvoice, SUM(TotalGrossAmount ) AS TotalGrossAmount " + "\r\n";
            queryString = queryString + "       FROM        SalesInvoices " + "\r\n";
            queryString = queryString + "       WHERE       ServiceInvoiceID = @ServiceInvoiceID " + "\r\n";

            this.totalBikePortalsEntities.CreateStoredProcedure("GetRelatedPartsInvoiceValue", queryString);
        }


        private void ServicesInvoicePostSaveValidate()
        {
            string[] queryArray = new string[2];

            queryArray[0] = " SELECT TOP 1 @FoundEntity = 'Contract Date: ' + CAST(ServiceContracts.EntryDate AS nvarchar) FROM SalesInvoices INNER JOIN ServiceContracts ON SalesInvoices.SalesInvoiceID = @EntityID AND SalesInvoices.ServiceContractID = ServiceContracts.ServiceContractID AND SalesInvoices.EntryDate < ServiceContracts.EntryDate ";
            queryArray[1] = " SELECT TOP 1 @FoundEntity = 'Part Date: ' + CAST(SalesInvoices.EntryDate AS nvarchar) FROM SalesInvoices INNER JOIN SalesInvoices AS ServiceInvoices ON ServiceInvoices.SalesInvoiceID = @EntityID AND SalesInvoices.ServiceInvoiceID = ServiceInvoices.SalesInvoiceID AND SalesInvoices.EntryDate < ServiceInvoices.EntryDate ";

            this.totalBikePortalsEntities.CreateProcedureToCheckExisting("ServicesInvoicePostSaveValidate", queryArray);
        }






        private void ServicesInvoiceEditable()
        {
            string[] queryArray = new string[1];

            queryArray[0] = " SELECT TOP 1 @FoundEntity = SalesInvoiceID FROM SalesInvoices WHERE SalesInvoiceID = @EntityID AND IsFinished = 1 ";

            this.totalBikePortalsEntities.CreateProcedureToCheckExisting("ServicesInvoiceEditable", queryArray);
        }

        private void ServicesInvoiceDeletable()
        {
            string[] queryArray = new string[1];

            queryArray[0] = " SELECT TOP 1 @FoundEntity = SalesInvoiceID FROM SalesInvoices WHERE ServiceInvoiceID = @EntityID ";

            this.totalBikePortalsEntities.CreateProcedureToCheckExisting("ServicesInvoiceDeletable", queryArray);
        }









        private void SalesInvoicePrint()
        {
            string queryString = " @SalesInvoiceID int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT          SalesInvoices.SalesInvoiceID, SalesInvoices.EntryDate, SalesInvoices.Reference, SalesInvoices.VATInvoiceNo, SalesInvoices.VATInvoiceDate, SalesInvoices.VATInvoiceSeries, " + "\r\n";
            queryString = queryString + "                       SalesInvoices.SalesInvoiceTypeID, SalesInvoiceTypes.Description AS SalesInvoiceTypeDescription, Customers.Name AS CustomerName, Customers.AddressNo, EntireTerritories.EntireName AS EntireTerritoryEntireName, SalesInvoices.Damages, SalesInvoices.Causes, SalesInvoices.Solutions, " + "\r\n";
            queryString = queryString + "                       ServiceContracts.EntryDate AS ServiceContractEntryDate, Vehicles.Name AS VehicleName, ServiceContracts.ChassisCode, ServiceContracts.EngineCode, ServiceContracts.LicensePlate, ServiceContracts.ColorCode, ServiceContracts.AgentName, SalesInvoices.CurrentMeters, ServiceContracts.EndingMeters, ServiceContracts.EndingDate, " + "\r\n";
            queryString = queryString + "                       SalesInvoiceDetails.CommodityID, Commodities.Code AS CommodityCode, Commodities.Name AS CommodityName, SalesInvoiceDetails.Quantity, SalesInvoiceDetails.ListedPrice, SalesInvoiceDetails.DiscountPercent, SalesInvoiceDetails.UnitPrice, SalesInvoiceDetails.VATPercent, SalesInvoiceDetails.GrossPrice, SalesInvoiceDetails.Amount, SalesInvoiceDetails.VATAmount, SalesInvoiceDetails.GrossAmount " + "\r\n";
            queryString = queryString + "       FROM            SalesInvoices INNER JOIN " + "\r\n";
            queryString = queryString + "                       SalesInvoiceTypes ON SalesInvoices.SalesInvoiceTypeID = SalesInvoiceTypes.SalesInvoiceTypeID AND (SalesInvoices.SalesInvoiceID = @SalesInvoiceID OR SalesInvoices.ServiceInvoiceID = @SalesInvoiceID) INNER JOIN " + "\r\n";
            queryString = queryString + "                       Customers ON SalesInvoices.CustomerID = Customers.CustomerID INNER JOIN " + "\r\n";
            queryString = queryString + "                       EntireTerritories ON Customers.TerritoryID = EntireTerritories.TerritoryID LEFT JOIN " + "\r\n";
            queryString = queryString + "                       ServiceContracts ON SalesInvoices.ServiceContractID = ServiceContracts.ServiceContractID LEFT JOIN " + "\r\n";
            queryString = queryString + "                       Commodities AS Vehicles ON ServiceContracts.CommodityID = Vehicles.CommodityID LEFT JOIN " + "\r\n";
            queryString = queryString + "                       SalesInvoiceDetails ON SalesInvoices.SalesInvoiceID = SalesInvoiceDetails.SalesInvoiceID LEFT JOIN " + "\r\n";
            queryString = queryString + "                       Commodities ON SalesInvoiceDetails.CommodityID = Commodities.CommodityID " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalBikePortalsEntities.CreateStoredProcedure("SalesInvoicePrint", queryString);

        }


    }
}

