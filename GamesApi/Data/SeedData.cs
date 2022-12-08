using GamesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GamesApi.Data
{
    public class SeedData
    {
        public static void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Skill>().HasData(
            new Skill
            {
                Id = 1,
                Name = "Shooting",
                Damage = 3

            },
             new Skill
             {
                 Id = 2,
                 Name = "Fire",
                 Damage = 7

             },
              new Skill
              {
                  Id = 3,
                  Name = "WindStorm",
                  Damage = 9

              },
               new Skill
               {
                   Id = 4,
                   Name = "Hypnosis",
                   Damage = 10

               },
                new Skill
                {
                    Id = 5,
                    Name = "Sword storm",
                    Damage = 8

                },
                new Skill
                {
                    Id = 6,
                    Name = "Music",
                    Damage = 1

                });
        }
    }
}
