﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ComputerManagementEntities : DbContext
    {
        public ComputerManagementEntities()
            : base("name=ComputerManagementEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BILL> BILLs { get; set; }
        public virtual DbSet<CATEGORY> CATEGORies { get; set; }
        public virtual DbSet<CUSTOMER> CUSTOMERs { get; set; }
        public virtual DbSet<ITEM_BILL> ITEM_BILL { get; set; }
        public virtual DbSet<PRODUCT> PRODUCTs { get; set; }
        public virtual DbSet<REGULATION> REGULATIONs { get; set; }
        public virtual DbSet<SPECIFICATION> SPECIFICATIONs { get; set; }
        public virtual DbSet<SPECIFICATION_TYPE> SPECIFICATION_TYPE { get; set; }
        public virtual DbSet<ITEM_BILL_SERI> ITEM_BILL_SERI { get; set; }
        public virtual DbSet<DETAIL_REPORT_CATEGORY> DETAIL_REPORT_CATEGORY { get; set; }
        public virtual DbSet<DETAIL_REPORT_PRODUCT> DETAIL_REPORT_PRODUCT { get; set; }
        public virtual DbSet<DETAIL_REPORT_REVENUE> DETAIL_REPORT_REVENUE { get; set; }
        public virtual DbSet<REPORT> REPORTs { get; set; }
        public virtual DbSet<BILL_REPAIR> BILL_REPAIR { get; set; }
    }
}
