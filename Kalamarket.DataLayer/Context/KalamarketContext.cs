using Kalamarket.DataLayer.Entities;
using Kalamarket.DataLayer.Entities.Address;
using Kalamarket.DataLayer.Entities.DisCount;
using Kalamarket.DataLayer.Entities.Entitieproduct;
using Kalamarket.DataLayer.Entities.Entitieproduct.FaQ;
using Kalamarket.DataLayer.Entities.Role;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kalamarket.DataLayer.Context
{
    public class KalamarketContext : DbContext
    {
        public KalamarketContext(DbContextOptions<KalamarketContext> options) : base(options)
        {

        }
        public DbSet<MainSlider> mainSliders { get; set; }
        public DbSet<user> users { get; set; }

        #region FaQ
        public DbSet<Answer> answers { get; set; }
        public DbSet<comment> comments { get; set; }
        public DbSet<question> questions { get; set; }
        #endregion

        #region Product

        public DbSet<brand> brands { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<product> products { get; set; }
        public DbSet<productColor> ProductColors { get; set; }
        public DbSet<ProductGallery> ProductGalleries { get; set; }
        public DbSet<ProductGurantee> productGurantees { get; set; }
        public DbSet<PropertyName> propertyNames { get; set; }
        public DbSet<Propertyname_Category> propertyname_Categories { get; set; }
        public DbSet<PropertyValue> propertyValues { get; set; }
        public DbSet<Review> reviews { get; set; }
        public DbSet<ProductPrice> ProductPrices { get; set; }
        public DbSet<Faviorate> Faviorates { get; set; }
        public DbSet<ProductReating> ProductReatings { get; set; }

        #endregion

        #region Address
        public DbSet<Province> provinces { get; set; }
        public DbSet<city> cities { get; set; }
        public DbSet<useraddress> useraddresses { get; set; }
        #endregion
        #region Cart

        public DbSet<Cart> cart { get; set; }
        public DbSet<CartDetail> CartDetail { get; set; }

        #endregion

        #region Discount
        public DbSet<discount> discounts { get; set; }
        public DbSet<UserDiscount> UserDiscounts { get; set; }
        #endregion

        #region RolePermission

        public DbSet<role> roles { get; set; }
        public DbSet<permission> permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }


        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            base.OnModelCreating(modelBuilder);
        }

    }
}
