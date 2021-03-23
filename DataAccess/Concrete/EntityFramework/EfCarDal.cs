using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, ReCapProjectContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails()
        {
            using(ReCapProjectContext context=new ReCapProjectContext())
            {
                var Result = from ca in context.Cars
                             join c in context.Colors
                             on ca.ColorId equals c.ColorId
                             join b in context.Brands
                             on ca.BrandId equals b.BrandId
                             select new CarDetailDto { BrandName = b.BrandName, CarName = ca.Description, ColorName = c.ColorName, DailyPrice = ca.DailyPrice };

                 return Result.ToList();
            }
           
        }
    }
}
