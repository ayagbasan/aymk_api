﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace aymk_database.Database
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AYMKEntities : DbContext
    {
        public AYMKEntities()
            : base("name=AYMKEntities")
        {
    		this.Configuration.ProxyCreationEnabled = false;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Alert> Alert { get; set; }
        public virtual DbSet<CatalogError> CatalogError { get; set; }
        public virtual DbSet<CatalogMarket> CatalogMarket { get; set; }
        public virtual DbSet<Log> Log { get; set; }
        public virtual DbSet<Notification> Notification { get; set; }
        public virtual DbSet<CatalogExchange> CatalogExchange { get; set; }
        public virtual DbSet<Exchange> Exchange { get; set; }
        public virtual DbSet<Account> Account { get; set; }
    }
}
