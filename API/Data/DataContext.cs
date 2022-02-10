using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : IdentityDbContext<AppUser, AppRole, int,
             IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>,
                IdentityRoleClaim<int>, IdentityUserToken<int>>       //DbContext      <--Asta e inainte sa folosim .NET Identity
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        //public DbSet<AppUser> Users { get; set; } //Acesta este un tabel cu Users     <-- Asta e inainte sa folosim .NET Identity
        public DbSet<UserLike> Likes { get; set; }  //Acesta este un tabel intermediar cu LikedBy and Liked
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder buiilder)  //Aici facem override la DbContext. Specificam ce fel de tabel si ce fel de relatie vrem (Many to Many)
                                                                        //OBS: de la .NET 5 EntityFramework ofera automat many to many
        {
            base.OnModelCreating(buiilder);

            buiilder.Entity<AppUser>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
                                                                        //Astea 2 sunt din .NET Identity
            buiilder.Entity<AppRole>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            buiilder.Entity<UserLike>()
                .HasKey(k => new { k.SourceUserId, k.LikedUserId });

            buiilder.Entity<UserLike>()
                .HasOne(s => s.SourceUser)
                .WithMany(l => l.LikedUsers)
                .HasForeignKey(s => s.SourceUserId)
                .OnDelete(DeleteBehavior.Cascade);

            buiilder.Entity<UserLike>()
                .HasOne(s => s.LikedUser)
                .WithMany(l => l.LikedByUsers)
                .HasForeignKey(s => s.LikedUserId)
                .OnDelete(DeleteBehavior.Cascade);

            buiilder.Entity<Message>()
                .HasOne(u => u.Recipient)
                .WithMany(m => m.MessagesRecieved)
                .OnDelete(DeleteBehavior.Restrict);

            buiilder.Entity<Message>()
                .HasOne(u => u.Sender)
                .WithMany(m => m.MessagesSent)
                .OnDelete(DeleteBehavior.Restrict);
        }


    }
}