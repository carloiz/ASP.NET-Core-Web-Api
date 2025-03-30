namespace ASP.NetCoreWebApi.common.IService
{
    public interface IReadService<TEntity, TId, TDto>
        where TEntity : class
        where TDto : class
    {
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<TDto?> GetByIdAsync(TId id);
        Task<(IEnumerable<TDto> items, int totalCount)> GetPagedAsync(int pageNumber, int pageSize);
    }
}
