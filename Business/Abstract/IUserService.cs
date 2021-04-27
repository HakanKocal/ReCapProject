using Core.Entites.Concrete;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<List<User>> GetAll();
        List<OperationClaim> GetClaims(User user);
        IResult Add(User user);
        User GetByEmail(string email);
        IResult Delete(User user);
        IResult Update(User user);
        IDataResult<List<User>> Get(int id);
    }
}
