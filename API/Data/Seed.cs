using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager) //Aici era DataContext context
        {
            if (await userManager.Users.AnyAsync()) return; //verificam daca tabelul Users contine vreun User

            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
            if (users == null) return;

            var roles = new List<AppRole>
            {
                new AppRole{Name = "Member"},
                new AppRole{Name = "Admin"},
                new AppRole{Name = "Moderator"},
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }


            foreach (var user in users)
            {
                user.UserName = user.UserName.ToLower();

                //using var hmac = new HMACSHA512();
                // user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
                // user.PasswordSalt = hmac.Key;        <-- Asta e inainte sa folosim .NET Identity

                await userManager.CreateAsync(user, "Pa$$w0rd");       //context.Users.Add(user);
                await userManager.AddToRoleAsync(user, "Member");
            }

            //context.SaveChangesAsync();   UserManager are grija sa salveze modificarile prin CreateAsync()

            var admin = new AppUser
            {
                UserName = "admin"
            };

            await userManager.CreateAsync(admin, "Pa$$w0rd");
            await userManager.AddToRolesAsync(admin, new[] {"Admin", "Moderator"});
        }
    }
}