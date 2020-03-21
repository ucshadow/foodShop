using FoodStore.Controllers;
using FoodStore.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace FoodStore.HtmlHelpers
{
    public static class FakeDataGenerators
    {
        public static Random Rnd = new Random();

        public static void GenerateRandomRatings(this ProductController controller)
        {

            foreach (var p in controller.Repository.GetClone())
            {
                p.NumberOfVotes = controller.Rnd.Next(3, 20);
                p.Rating = controller.Rnd.Next(100, 500) / 100;
                controller.Repository.SaveProduct(p);
                Debug.WriteLine(p.Name);

            }
        }

        public static void GenerateRandomUser(this AccountController controller)
        {

            var user = new ApplicationUser { UserName = RandomString(), 
                Email = $"{RandomString()}@gmail.com" };
            controller.UserManager.CreateAsync(user, RandomString());
        }

        private static string RandomString(int length = 5)
        {
            var res = "";
            for(var i = 0; i < length; i++)
            {
                res += Char.ConvertFromUtf32(Rnd.Next(97, 123));
            }
            return res;
        }

    }
}