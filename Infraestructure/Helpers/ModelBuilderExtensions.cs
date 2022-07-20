using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Helpers
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasData
                (
                    new Usuario() { Id = 13, Nombre = "VALESKA VILLAGR", Username = "16782703-9", Email = "vvillagran@dl.cl", Suspended = false, Rut= "16782703-9" },
                    new Usuario() { Id = 15, Nombre = "Lilian Carolina Gonzalez Silva", Username = "17768779-0", Email = "17768779-0@pilotoapp.com", Suspended = false, Rut = "" },
                    new Usuario() { Id = 19, Nombre = "Alejandro Sarmiento Millares", Username = "25338108-6", Email = "25338108-6@pilotoapp.com", Suspended = false, Rut = "" },
                    new Usuario() { Id = 20, Nombre = "ANA GABRIELA DUR", Username = "26402139-1", Email = "26402139-1@pilotoapp.com", Suspended = false, Rut = "26402139-1" },
                    new Usuario() { Id = 81 , Nombre = "Pamela de los Angeles Lira Morales", Username = "17050718-5", Email = "17050718-5@pilotoapp.com", Suspended = false, Rut = "17050718-5" }
                );
        }
    }
}
