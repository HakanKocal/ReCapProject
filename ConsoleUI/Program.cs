using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;
using System.Collections.Generic;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //CarTest();
            // ColorTest();
            // BrandTest();
            //UserTest();
            RentalTest();


        }

        private static void RentalTest()
        {
            RentalManager rentalManager = new RentalManager(new EfRentalDal());
            var result = rentalManager.Add(new Rental { CarId = 1, CustomerId = 1, Id = 1, RentDate = (new DateTime(2021, 3, 29, 18, 59, 00)) });
            if (result.Succes)
            {
                Console.WriteLine(result.Message);
            }
            else
            {
                Console.WriteLine(result.Message);
            }
        }

        private static void UserTest()
        {
            UserManager userManager = new UserManager(new EfUserDal());
            User user = new User() { FirstName = "Hakan", LastName = "Koçal", Email = "kocalhakan53@gmail.com", Password = "12345" };
            userManager.Add(user);
            userManager.GetAll();
        }

        private static void BrandTest()
        {
            Console.WriteLine("---------------------------------");
            BrandManager brandManager = new BrandManager(new EfBrand());
            brandManager.Add(new Brand { BrandId = 1, BrandName = "Audi" });
            foreach (var brand in brandManager.GetAll().Data)
            {
                Console.WriteLine(brand.BrandId + " " + brand.BrandName);
            }
        }

        private static void ColorTest()
        {
            ColorManager colorManager = new ColorManager(new EfColorDal());
            colorManager.Add(new Color { ColorId = 1, ColorName = "mavi" });
            foreach (var color in colorManager.GetAll().Data)
            {
                Console.WriteLine(color.ColorId + "  " + color.ColorName);
            }
        }

        private static void CarTest()
        {
            CarManager carManager = new CarManager(new EfCarDal());
            var result = carManager.GetCarDetails();
            foreach (var car in result.Data)
            {
                Console.WriteLine(car.BrandName + " " + car.CarName + " " + car.ColorName + " " + car.DailyPrice);
            }
            carManager.Add(new Car { Id = 9, BrandId = 9, ColorId = 3, DailyPrice = 100, ModelYear = 2000, Description = "volkwagen" });
            carManager.Delete(new Car { Id = 9, DailyPrice = 100 });
            foreach (var car in carManager.GetAll().Data)
            {
                Console.WriteLine(car.Description);
            }
        }
    }
}
