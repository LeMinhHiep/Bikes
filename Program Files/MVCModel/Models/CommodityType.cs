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
    
    public partial class CommodityType
    {
        public CommodityType()
        {
            this.CommodityTypes1 = new HashSet<CommodityType>();
            this.Commodities = new HashSet<Commodity>();
        }
    
        public int CommodityTypeID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<int> AncestorID { get; set; }
        public string Remarks { get; set; }
    
        public virtual ICollection<CommodityType> CommodityTypes1 { get; set; }
        public virtual CommodityType CommodityType1 { get; set; }
        public virtual ICollection<Commodity> Commodities { get; set; }
    }
}
