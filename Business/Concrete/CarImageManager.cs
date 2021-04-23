using Business.Abstract;
using Business.Constans;
using Core.Utilities.Business;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImagedal;
        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImagedal = carImageDal;
        }
        public IResult Add(IFormFile file, CarImage carImage)
        {
            IResult result = BusinessRules.Run(CheckIfImageLimitExceded(carImage.CarId));
            if (result != null)
            {
                return new ErrorResult(Messages.ImageLimitExceded);
            }
            carImage.ImagePath = FileHelper.Add(file);
            carImage.Date = DateTime.Now;
            _carImagedal.Add(carImage);
            return new SuccesResult(Messages.ImageAdded);
        }

        public IResult Delete(CarImage carImage)
        {
            FileHelper.Delete(carImage.ImagePath);
            _carImagedal.Delete(carImage);
            return new SuccesResult(Messages.ImageDeleted);
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccesDataResult<List<CarImage>>(_carImagedal.GetAll());
        }

        public IDataResult<CarImage> GetById(int Id)
        {
            IResult result = BusinessRules.Run(CheckIfCarImageImageCountNull(Id));
            if (result != null)
            {
                return new ErrorDataResult<CarImage>(result.Message);
            }
            return new SuccesDataResult<CarImage>(_carImagedal.Get(c => c.Id == Id));
        }

        public IResult Update(IFormFile file, CarImage carImage)
        {
            IResult result = BusinessRules.Run(CheckIfImageLimitExceded(carImage.CarId));
            if (result != null)
            {
                return result;
            }
            carImage.ImagePath = FileHelper.Update(_carImagedal.Get(c => c.Id == carImage.Id).ImagePath, file);
            carImage.Date = DateTime.Now;
            _carImagedal.Update(carImage);
            return new SuccesResult(Messages.ImageUpdate);
        }
        private IResult CheckIfImageLimitExceded(int CarId)
        {
            var result = _carImagedal.GetAll(c => c.CarId == CarId).Count;
            if (result >= 5)
            {
                return new ErrorResult(Messages.ImageLimitExceded);
            }
            return new SuccesResult();
        }
        private IDataResult<List<CarImage>> CheckIfCarImageImageCountNull(int carId)
        {
            try
            {
                string path = Environment.CurrentDirectory + @"\Images\DefaultImage.png";
                var result = _carImagedal.GetAll(c => c.CarId == carId).Any();

                if (!result)
                {
                    List<CarImage> carImage = new List<CarImage>();
                    carImage.Add(new CarImage { CarId = carId, ImagePath = path, Date = DateTime.Now });
                    return new SuccesDataResult<List<CarImage>>(carImage);
                }
            }
            catch (Exception exception)
            {
                return new ErrorDataResult<List<CarImage>>(exception.Message);
            }
            return new SuccesDataResult<List<CarImage>>(_carImagedal.GetAll(c => c.Id == carId).ToList());
        }
    }
}
