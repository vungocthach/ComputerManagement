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
    
    public partial class ITEM_BILL : HelperService.BaseViewModel
    {
        public int billId { get; set; }
        public int productId { get; set; }
        public int unitPrice { get; set; }
        public int quantity { get; set; }
    
        public virtual BILL BILL { get; set; }
        public virtual PRODUCT PRODUCT { get; set; }
    }
}
