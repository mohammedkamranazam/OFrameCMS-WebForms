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
    
    public partial class SC_ProductDownloads
    {
        public long ProductDownloadID { get; set; }
        public int ProductID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Hide { get; set; }
        public int DisableDownloadAfterDays { get; set; }
        public long MaxDownloadsAllowed { get; set; }
        public string DownloadURL { get; set; }
        public string WebDownloadURL { get; set; }
        public bool UseWebDownloadURL { get; set; }
    
        public virtual SC_Products SC_Products { get; set; }
    }
}