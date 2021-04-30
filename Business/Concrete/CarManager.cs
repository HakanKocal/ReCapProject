using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constans;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;
        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        [ValidationAspect(typeof(CarValidation))]
        [SecuredOperation("product.add,admin")]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Car car)
        {
            if(car.Description.Length>=2&&car.DailyPrice>0)
            {
                _carDal.Add(car);
                return new SuccesResult(Messages.CarAdded);
            }
            else
            {
                return new ErrorResult(Messages.CarNameInvalid);
            }
        }

        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccesResult(Messages.CarDeleted);

        }
        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<List<Car>> GetAll()
        {
            return new SuccesDataResult<List<Car>>(_carDal.GetAll(),Messages.CarListed);
        }
        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            return new SuccesDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(),Messages.CarDetailsListed);
        }
        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<List<Car>> GetCarsByBrandId(int brandId)
        {
            return new SuccesDataResult<List<Car>>(_carDal.GetAll(p=>p.BrandId==brandId));
        }
        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<List<Car>> GetCarsByColorId(int colorId)
        {
            return new SuccesDataResult<List<Car>>(_carDal.GetAll(p => p.ColorId == colorId));
        }

        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccesResult(Messages.CarUpdated);
        }
        [TransactionScopeAspect]
        public IResult AddTransactionalTest(Car car)
        {
            _carDal.Update(car);
            _carDal.Add(car);
            return new SuccesResult();
        }

    }
}
