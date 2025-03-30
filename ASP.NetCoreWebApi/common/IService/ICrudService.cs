namespace ASP.NetCoreWebApi.common.IService
{
    public interface ICrudService<TEntity, TId, TDto, TCreateDto, TUpdateDto> :
        IReadService<TEntity, TId, TDto>,
        IWriteService<TEntity, TId, TCreateDto, TUpdateDto>
        where TEntity : class
        where TDto : class
        where TCreateDto : class
        where TUpdateDto : class
    {
    }
}
