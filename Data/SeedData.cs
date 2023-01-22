using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MoviesApp.Models;

namespace MoviesApp.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            Console.WriteLine("Seeding Database");
            using (var context = new MoviesContext(
                       serviceProvider.GetRequiredService<
                           DbContextOptions<MoviesContext>>()))
            {
                //movies.
                if (!context.Movies.Any())
                {
                    context.Movies.AddRange(
                        new Movie
                        {
                            Title = "When Harry Met Sally",
                            ReleaseDate = DateTime.Parse("1989-2-12"),
                            Genre = "Romantic Comedy",
                            Price = 7.99M
                        },
                        new Movie
                        {
                            Title = "Ghostbusters ",
                            ReleaseDate = DateTime.Parse("1984-3-13"),
                            Genre = "Comedy",
                            Price = 8.99M
                        },
                        new Movie
                        {
                            Title = "Ghostbusters 2",
                            ReleaseDate = DateTime.Parse("1986-2-23"),
                            Genre = "Comedy",
                            Price = 9.99M
                        },
                        new Movie
                        {
                            Title = "Rio Bravo",
                            ReleaseDate = DateTime.Parse("1959-4-15"),
                            Genre = "Western",
                            Price = 3.99M
                        }
                    );

                    context.SaveChanges();
                }
                
                if (!context.Actors.Any())
                {
                    context.Actors.AddRange(
                        new Actor
                        {
                            Name = "Keanu",
                            Surname = "Reeves",
                            DateOfBirth = new DateTime(1965, 9, 2)
                        },
                        new Actor
                        {
                            Name = "Johnny",
                            Surname = "Depp",
                            DateOfBirth = new DateTime(1963, 6, 9)
                        },
                        new Actor
                        {
                            Name = "Leonardo",
                            Surname = "DiCaprio",
                            DateOfBirth = new DateTime(1984, 11, 22)
                        },
                        new Actor
                        {
                            Name = "Will",
                            Surname = "Smith",
                            DateOfBirth = new DateTime(1968, 9, 25)
                        }
                    );

                    context.SaveChanges();
                }

                var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
                var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

                if (!roleManager.RoleExistsAsync("Admin").Result)
                {
                    roleManager.CreateAsync(new IdentityRole { Name = "Admin" }).Wait();
                }

                if (userManager.FindByEmailAsync("admin@example.com").Result == null)
                {
                    var user = new ApplicationUser
                    {
                        UserName = "admin@example.com",
                        Email = "admin@example.com",
                        FirstName = "Super",
                        LastName = "Admin"
                    };

                    IdentityResult result = userManager.CreateAsync(user, "P@ssw0rd").Result;

                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "Admin").Wait();
                    }
                }
            }
        }
    }
}