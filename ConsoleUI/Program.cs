using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
                CarManager carManager = new CarManager(new EfCarDal());
                foreach (var car in carManager.GetCarDetails())
                {
                    Console.WriteLine(car.BrandName+" "+car.CarName+" "+car.ColorName+" "+car.DailyPrice);
                }
                // carManager.Add(new Car { Id = 9, BrandId = 9, ColorId = 3, DailyPrice = 100, ModelYear = 2000, Description = "volkwagen" });
                // carManager.Delete(new Car { Id = 9 ,DailyPrice=100});
                //foreach (var car in carManager.GetAll())
                //{
                //    Console.WriteLine(car.Description);
                //}



                //Console.WriteLine("----------------------------");
                //ColorManager colorManager = new ColorManager(new EfColorDal());
                //colorManager.Add(new Color { ColorId = 1, ColorName = "mavi" });
                //foreach (var color in colorManager.GetAll())
                //{
                //    Console.WriteLine(color.ColorId+"  "+color.ColorName);
                //}


                //Console.WriteLine("---------------------------------");
                //BrandManager brandManager = new BrandManager(new EfBrand());
                //brandManager.Add(new Brand { BrandId = 1, BrandName = "Audi" });
                //foreach (var brand in brandManager.GetAll())
                //{
                //    Console.WriteLine(brand.BrandId+" "+brand.BrandName);
                //}




            }
    }
}
