using DAL.Interface.DTO;
using DAL.Interface.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using ORM.Entities;
using DAL.Mappers;

namespace DAL.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly DbContext context;

        public OwnerRepository(DbContext context)
        {
            this.context = context;
        }

        public void Create(AccountOwnerDto accountOwnerDto)
        {
            context.Set<AccountOwner>()
                .Add(accountOwnerDto.ToOrmOwner());
        }

        public void Update(AccountOwnerDto accountOwnerDto)
        {
            
        }

        public AccountOwnerDto GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AccountOwnerDto> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
