using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BusinessLogic.BusinessExceptions;
using BusinessLogic.ModelsDto;
using BusinessLogic.ServiceInterfaces;
using BusinessLogic.Mapping;
using DataAccess.Models;
using DataAccess.Repos.Base;
using FluentValidation;

namespace BusinessLogic
{
    public abstract class BaseService<TDbEntity, UDtoEntity> : IBaseService<UDtoEntity>
        where TDbEntity : BaseEntity, new()
        where UDtoEntity : BaseDto, new()
    {
        protected IRepo<TDbEntity> _dataRepo;
        protected static IMapper _mapper = MapperDto.Service;
        protected IValidator<UDtoEntity> _entityValidator;

        public BaseService(IRepo<TDbEntity> dataRepo, IValidator<UDtoEntity> entityValidator)
        {
            _dataRepo = dataRepo;
            _entityValidator = entityValidator;
        }

        public virtual UDtoEntity FindItem(int id)
        {
            var item = GetDbEntityItemFromRepoAndThrow(id);
            return _mapper.Map<UDtoEntity>(item);
        }

        public virtual IEnumerable<UDtoEntity> GetAllItems()
        {
            var items = _dataRepo.GetAll();
            var dtoItems = items.Select(_mapper.Map<UDtoEntity>);
            return dtoItems;
        }

        public virtual void CreateItem(UDtoEntity dtoItem)
        {
            dtoItem.Id = 0;
            _entityValidator.ValidateAndThrow(dtoItem);
            _dataRepo.Add(_mapper.Map<TDbEntity>(dtoItem));
        }

        public virtual void UpdateItem(UDtoEntity dtoItem)
        {
            _entityValidator.ValidateAndThrow(dtoItem);
            var targetDbEntityItem = GetDbEntityItemFromRepoAndThrow(dtoItem.Id);
            _mapper.Map(dtoItem, targetDbEntityItem);
            _dataRepo.Update(targetDbEntityItem);
        }

        public virtual void DeleteItem(int id)
        {
            GetDbEntityItemFromRepoAndThrow(id);
            _dataRepo.Delete(id);
        }

        private TDbEntity GetDbEntityItemFromRepoAndThrow(int id)
        {
            var dbItem = _dataRepo.Find(id) ??
                throw new ItemNotFoundException($"Item {typeof(TDbEntity).Name} with ID = {id} not found");
            return dbItem;
        }
    }
}