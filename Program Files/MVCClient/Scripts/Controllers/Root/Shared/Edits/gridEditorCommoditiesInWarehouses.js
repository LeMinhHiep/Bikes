﻿define(["superBase", "gridEditorTemplate"], (function (superBase, gridEditorTemplate) {

    var definedExemplar = function (kenGridName) {
        definedExemplar._super.constructor.call(this, kenGridName);
    }

    var superBaseHelper = new superBase();
    superBaseHelper.inherits(definedExemplar, gridEditorTemplate);



    //The commodity here is AutoComplete Widget
    definedExemplar.prototype.handleSelect = function (e) {
        var currentDataSourceRow = this._getCurrentDataSourceRow();

        if (currentDataSourceRow != undefined) {
            var dataItem = e.sender.dataItem(e.item.index());

            currentDataSourceRow.set("CommodityID", dataItem.CommodityID);
            currentDataSourceRow.set("CommodityCode", dataItem.CommodityCode);
            currentDataSourceRow.set("CommodityName", dataItem.CommodityName);
            currentDataSourceRow.set("CommodityTypeID", dataItem.CommodityTypeID);
            currentDataSourceRow.set("WarehouseID", dataItem.WarehouseID);
            currentDataSourceRow.set("WarehouseCode", dataItem.WarehouseCode);
            currentDataSourceRow.set("QuantityAvailable", dataItem.QuantityAvailable);
            currentDataSourceRow.set("Quantity", 1);
            currentDataSourceRow.set("VATPercent", dataItem.VATPercent);
            currentDataSourceRow.set("GrossPrice", dataItem.GrossPrice);

            if (currentDataSourceRow.ListedPrice != undefined)
                currentDataSourceRow.set("ListedPrice", currentDataSourceRow.UnitPrice);
        }

        window.commodityNameBeforeChange = dataItem.CommodityName;
    };


    definedExemplar.prototype.handleChange = function (e) {
        this._setEditorValue("CommodityName", window.commodityNameBeforeChange);
    };



    return definedExemplar;
}));