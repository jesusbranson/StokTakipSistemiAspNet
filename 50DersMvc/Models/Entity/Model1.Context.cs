﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace _50DersMvc.Models.Entity
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<tbl_Kategoriler> tbl_Kategoriler { get; set; }
        public virtual DbSet<tbl_Müsteriler> tbl_Müsteriler { get; set; }
        public virtual DbSet<tbl_Satislar> tbl_Satislar { get; set; }
        public virtual DbSet<tbl_Ürünler> tbl_Ürünler { get; set; }
    }
}
