﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IPos.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class IPosEntities : DbContext
    {
        public IPosEntities()
            : base("name=IPosEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Partner> Partner { get; set; }
        public virtual DbSet<Permission> Permission { get; set; }
        public virtual DbSet<Product_Categories> Product_Categories { get; set; }
        public virtual DbSet<Product_Image> Product_Image { get; set; }
        public virtual DbSet<Product_Unit> Product_Unit { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Role_Permission> Role_Permission { get; set; }
        public virtual DbSet<Shop> Shop { get; set; }
        public virtual DbSet<Transaction> Transaction { get; set; }
        public virtual DbSet<Transaction_Detail> Transaction_Detail { get; set; }
        public virtual DbSet<Transaction_Payment> Transaction_Payment { get; set; }
        public virtual DbSet<Transaction_Type_Group> Transaction_Type_Group { get; set; }
        public virtual DbSet<User_Permission> User_Permission { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}
