//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVCModel.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Location
    {
        public Location()
        {
            this.Warehouses = new HashSet<Warehouse>();
            this.OrganizationalUnits = new HashSet<OrganizationalUnit>();
            this.SalesInvoices = new HashSet<SalesInvoice>();
            this.ServiceContracts = new HashSet<ServiceContract>();
            this.TransferOrders = new HashSet<TransferOrder>();
        }
    
        public int LocationID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string Facsimile { get; set; }
        public string Remarks { get; set; }
        public string Code { get; set; }
    
        public virtual ICollection<Warehouse> Warehouses { get; set; }
        public virtual ICollection<OrganizationalUnit> OrganizationalUnits { get; set; }
        public virtual ICollection<SalesInvoice> SalesInvoices { get; set; }
        public virtual ICollection<ServiceContract> ServiceContracts { get; set; }
        public virtual ICollection<TransferOrder> TransferOrders { get; set; }
    }
}
