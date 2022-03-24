using System.Collections.Generic;

namespace BusinessLogic.ServiceInterfaces
{
    public interface IBaseService<TDtoEntity>
    {
        TDtoEntity FindItem(int id);
        IEnumerable<TDtoEntity> GetAllItems();
        void CreateItem(TDtoEntity item);
        void UpdateItem(TDtoEntity item);
        void DeleteItem(int id);
    }
}