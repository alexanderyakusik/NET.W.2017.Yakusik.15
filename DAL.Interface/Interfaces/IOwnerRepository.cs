using DAL.Interface.DTO;
using System.Collections.Generic;

namespace DAL.Interface.Interfaces
{
    public interface IOwnerRepository
    {
        void Create(AccountOwnerDto accountOwnerDto);

        void Update(AccountOwnerDto accountOwnerDto);

        AccountOwnerDto GetById(int id);

        IEnumerable<AccountOwnerDto> GetAll();
    }
}
