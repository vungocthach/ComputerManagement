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
    
    public partial class DETAIL_REPORT_REVENUE : Helper.BaseViewModel
    {
        public int reportId { get; set; }
        public byte day { get; set; }
        public long money { get; set; }
        public int amount { get; set; }
    
        public virtual REPORT REPORT { get; set; }
    }
}
