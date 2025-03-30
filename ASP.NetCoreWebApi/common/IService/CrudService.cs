using ASP.NetCoreWebApi.EFCore;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ASP.NetCoreWebApi.common.IService
{
    public class CrudService<TEntity, TId, TDto, TCreateDto, TUpdateDto>
        : ICrudService<TEntity, TId, TDto, TCreateDto, TUpdateDto>
        where TEntity : class
        where TDto : class
        where TCreateDto : class
        where TUpdateDto : class
    {
        protected readonly EF_DataContext _context;
        protected readonly IMapper _mapper;
        protected readonly DbSet<TEntity> _dbSet;

        // Change from protected to public constructor
        public CrudService(EF_DataContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dbSet = _context.Set<TEntity>();
        }

        // Read operations
        public virtual async Task<IEnumerable<TDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<TDto>>(await _dbSet.AsNoTracking().ToListAsync());
        }

        public virtual async Task<TDto?> GetByIdAsync(TId id)
        {
            var entity = await _dbSet.FindAsync(id);
            return entity == null ? null : _mapper.Map<TDto>(entity);
        }

        public virtual async Task<(IEnumerable<TDto> items, int totalCount)> GetPagedAsync(int pageNumber, int pageSize)
        {
            var query = _dbSet.AsNoTracking();
            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (_mapper.Map<IEnumerable<TDto>>(items), totalCount);
        }

        // Write operations
        public virtual async Task<TEntity> CreateAsync(TCreateDto createDto)
        {
            var entity = _mapper.Map<TEntity>(createDto);
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<bool> UpdateAsync(TId id, TUpdateDto updateDto)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) return false;

            _mapper.Map(updateDto, entity);
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public virtual async Task<bool> DeleteAsync(TId id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) return false;

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        // Additional helper methods
        protected IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec)
        {
            return SpecificationEvaluator<TEntity>.GetQuery(_dbSet.AsQueryable(), spec);
        }
    }
}
