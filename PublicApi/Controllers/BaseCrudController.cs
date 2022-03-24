using System.Linq;
using BusinessLogic.ModelsDto;
using BusinessLogic.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace PublicApi.Controllers
{
    [ApiController]
    public abstract class BaseCrudController<TDtoEntity> : ControllerBase
        where TDtoEntity : BaseDto, new()
    {
        protected readonly IBaseService<TDtoEntity> DtoEntityService;

        protected BaseCrudController(IBaseService<TDtoEntity> dtoEntityService)
        {
            DtoEntityService = dtoEntityService;
        }
        
        [HttpGet]
        public virtual ActionResult<ApiResponse<TDtoEntity[]>> GetAllItems()
        {
            var response = ApiResponse<TDtoEntity[]>.Success(DtoEntityService.GetAllItems().ToArray());
            return Ok(response);
        }

        [HttpGet("{id}")]
        public virtual ActionResult<TDtoEntity> GetItem(int id)
        {
            var response = ApiResponse<TDtoEntity>.Success(DtoEntityService.FindItem(id));
            return Ok(response);
        }

        [HttpPost]
        public virtual ActionResult PostItem(TDtoEntity item)
        {
            DtoEntityService.CreateItem(item);
            return Ok(GetEmptySuccessfulApiResponse());
        }

        [HttpPut]
        public virtual ActionResult UpdateItem(TDtoEntity item)
        {
            DtoEntityService.UpdateItem(item);
            return Ok(GetEmptySuccessfulApiResponse());
        }

        [HttpDelete("{id}")]
        public virtual ActionResult DeleteItem(int id)
        {
            DtoEntityService.DeleteItem(id);
            return Ok(GetEmptySuccessfulApiResponse());
        }

        private static ApiResponse<object> GetEmptySuccessfulApiResponse() =>
            ApiResponse<object>.Success(null);
    }
}