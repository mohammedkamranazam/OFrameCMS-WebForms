//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjectJKL.AppCode.DAL.ShoppingCartModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class SC_ProductTypes
    {
        public SC_ProductTypes()
        {
            this.SC_Products = new HashSet<SC_Products>();
        }
    
        public int ProductTypeID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Hide { get; set; }
        public int SectionID { get; set; }
        public int CategoryID { get; set; }
        public int SubCategoryID { get; set; }
    
        public virtual SC_Categories SC_Categories { get; set; }
        public virtual ICollection<SC_Products> SC_Products { get; set; }
        public virtual SC_Sections SC_Sections { get; set; }
        public virtual SC_SubCategories SC_SubCategories { get; set; }
    }
}