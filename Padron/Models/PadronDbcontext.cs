using Padron.Models.PadronEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Padron.Models
{
    public class PadronDbcontext:DbContext
    {
        public PadronDbcontext():base("PadronConnectionString")
        {
            
        }

        public DbSet<Persona> Personas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Persona>().ToTable("View_Persona").HasKey(x=>x.Cedula);
            base.OnModelCreating(modelBuilder);
        }

    }
}