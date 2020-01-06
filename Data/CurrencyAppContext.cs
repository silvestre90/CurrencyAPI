using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebAPITest.Models;

namespace WebAPITest.Data
{
    public class CurrencyAppContext: DbContext
    {
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestType> RequestTypes { get; set; }

        public CurrencyAppContext(DbContextOptions<CurrencyAppContext> options) : base(options)
        {

        }

        public CurrencyAppContext()
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
        }


    }

    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>().HasData(
                new Currency
                {
                    Id = 1,
                    CurrencyCode = "USD",
                },

                new Currency
                {
                    Id = 2,
                    CurrencyCode = "CHF",
                },

                new Currency
                {
                    Id = 3,
                    CurrencyCode = "GBP",
                },

                new Currency
                {
                    Id = 4,
                    CurrencyCode = "EUR",
                },

                new Currency
                {
                    Id = 5,
                    CurrencyCode = "JPY",
                }
                
                );

            modelBuilder.Entity<RequestType>().HasData(
                new RequestType
                {
                    Id = 1,
                    Type = "Internal",
                },

                new RequestType
                {
                    Id = 2,
                    Type = "External",
                }

                
            );
        }
    }
}
