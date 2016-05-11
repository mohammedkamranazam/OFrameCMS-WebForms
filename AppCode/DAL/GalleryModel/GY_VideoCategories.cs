//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjectJKL.AppCode.DAL.GalleryModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class GY_VideoCategories
    {
        public GY_VideoCategories()
        {
            this.GY_ChildVideoCategories = new HashSet<GY_VideoCategories>();
            this.GY_Videos = new HashSet<GY_Videos>();
            this.GY_VideoSet = new HashSet<GY_VideoSet>();
        }
    
        public int VideoCategoryID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Hide { get; set; }
        public Nullable<int> ImageID { get; set; }
        public string Locale { get; set; }
        public Nullable<int> ParentVideoCategoryID { get; set; }
    
        public virtual ICollection<GY_VideoCategories> GY_ChildVideoCategories { get; set; }
        public virtual GY_VideoCategories GY_ParentVideoCategory { get; set; }
        public virtual ICollection<GY_Videos> GY_Videos { get; set; }
        public virtual ICollection<GY_VideoSet> GY_VideoSet { get; set; }
    }
}