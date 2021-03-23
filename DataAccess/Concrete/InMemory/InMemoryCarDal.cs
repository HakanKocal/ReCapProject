using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal : ICarDal
    {
        List<Car> _car;
        public InMemoryCarDal()
        {
            _car = new List<Car> {
            new Car{ Id = 1, BrandId = 1, ColorId = 1,DailyPrice=10000,ModelYear=2020,Description="Audi" },
            new Car { Id = 2, BrandId = 2, ColorId = 1, ModelYear = 2021, DailyPrice = 200000, Description = "Mercedes" },
            new Car { Id = 3, BrandId = 2, ColorId = 2, ModelYear = 2021, DailyPrice = 300000, Description = "Mercedes" },
            new Car { Id = 4, BrandId = 3, ColorId = 3, ModelYear = 2021, DailyPrice = 500000, Description = "BMW" }
            };
        }

        public void Add(Car car)
        {
            _car.Add(car);
        }

        public void Delete(Car car)
        {
            Car CarDelete = _car.SingleOrDefault(p=>p.Id==car.Id);
            _car.Remove(CarDelete);
        }

        public Car Get(Expression<Func<Car, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetAll()
        {
            return _car;
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetById(int Id)
        {
            return _car.Where(p => p.Id == Id).ToList();
            

        }

        public List<CarDetailDto> GetCarDetails()
        {
            throw new NotImplementedException();
        }

        public void Update(Car car)
        {
            Car CarUpdate = _car.SingleOrDefault(p => p.Id == car.Id);
            CarUpdate.BrandId = car.BrandId;
            CarUpdate.ColorId = car.ColorId;
            CarUpdate.DailyPrice = car.DailyPrice;
            CarUpdate.Description = car.Description;
            CarUpdate.ModelYear = car.ModelYear;

        }
    }
}
