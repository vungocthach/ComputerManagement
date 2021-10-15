//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ComputerProject
{
    using System;
    using System.Collections.Generic;
    
    public partial class PRODUCT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRODUCT()
        {
            this.DETAIL_REPORT_PRODUCT = new HashSet<DETAIL_REPORT_PRODUCT>();
            this.ITEM_BILL = new HashSet<ITEM_BILL>();
            this.ITEM_BILL_SERI = new HashSet<ITEM_BILL_SERI>();
            this.SPECIFICATIONs = new HashSet<SPECIFICATION>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public int priceOrigin { get; set; }
        public int priceSales { get; set; }
        public byte[] image { get; set; }
        public string description { get; set; }
        public string producer { get; set; }
        public int quantity { get; set; }
        public Nullable<int> warrantyTime { get; set; }
        public bool isStopSelling { get; set; }
        public int categoryId { get; set; }
    
        public virtual CATEGORY CATEGORY { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DETAIL_REPORT_PRODUCT> DETAIL_REPORT_PRODUCT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ITEM_BILL> ITEM_BILL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ITEM_BILL_SERI> ITEM_BILL_SERI { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SPECIFICATION> SPECIFICATIONs { get; set; }
    }
}
