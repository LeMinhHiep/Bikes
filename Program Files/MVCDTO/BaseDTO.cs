using System;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using MVCModel;
using MVCBase.Enums;

namespace MVCDTO
{
    public abstract class BaseDTO : IAccessControlAttribute //: BaseModel
    {
        protected BaseDTO()
        {
            // apply any DefaultValueAttribute settings to their properties
            var propertyInfos = this.GetType().GetProperties();
            foreach (var propertyInfo in propertyInfos)
            {
                var attributes = propertyInfo.GetCustomAttributes(typeof(DefaultValueAttribute), true);
                if (attributes.Any())
                {
                    var attribute = (DefaultValueAttribute)attributes[0];
                    propertyInfo.SetValue(this, attribute.Value, null);
                }
            }
            this.EntryDate = DateTime.Now;
        }

        [Display(Name = "Ngày lập")]
        [Required]
        public DateTime? EntryDate { get; set; }
        [Display(Name = "Số phiếu")]
        public string Reference { get; set; }



        public int UserID { get; set; }
        [Required]
        [Display(Name = "Người lập")]
        public virtual int PreparedPersonID { get; set; }
        public int OrganizationalUnitID { get; set; }
        public int LocationID { get; set; }



        public bool Approved { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        public bool InActive { get; set; }
        public bool InActivePartial { get; set; }
        public Nullable<System.DateTime> InActiveDate { get; set; }


        public bool Editable { get; set; }
        public bool Deletable { get; set; }
        


        //These properties are used as an implementation preservation of ISimpleViewModel for these ________ViewModel class (Those class ________ViewModel which is BOTH inheritance from this BaseDTO AND implementation of ISimpleViewModel)
        public virtual bool PrintAfterClosedSubmit { get { return false; } }
        public GlobalEnums.SubmitTypeOption SubmitTypeOption { get; set; }




        public virtual void PerformPresaveRule() { }
    }
}

