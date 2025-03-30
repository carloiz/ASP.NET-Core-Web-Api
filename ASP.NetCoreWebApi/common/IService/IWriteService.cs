namespace ASP.NetCoreWebApi.common.IService
{
    public interface IWriteService<TEntity, TId, TCreateDto, TUpdateDto>
        where TEntity : class
        where TCreateDto : class
        where TUpdateDto : class
    {
        Task<TEntity> CreateAsync(TCreateDto createDto);
        Task<bool> UpdateAsync(TId id, TUpdateDto updateDto);
        Task<bool> DeleteAsync(TId id);
    }
}
