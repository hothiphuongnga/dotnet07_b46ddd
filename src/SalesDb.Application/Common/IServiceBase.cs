using AutoMapper;
using SalesDb.Domain.Common;
using SalesDb.Domain.Interfaces;

namespace SalesDb.Application.Common;

public interface IServiceBase<TEntity, TDto, TCreate, TKey>
{
    Task<ApiResponse<IEnumerable<TDto>>> GetAllAsync();
    Task<ApiResponse<TDto>> GetByIdAsync(TKey id);
    Task<ApiResponse<TDto>> CreateAsync(TCreate dto);
    Task<ApiResponse<TDto>> UpdateAsync(TKey id, TCreate dto);
    Task<ApiResponse<bool>> DeleteAsync(TKey id);
}

// thực thi IServiceBase 
public abstract class ServiceBase<TEntity, TDto, TCreate, TKey> : IServiceBase<TEntity, TDto, TCreate, TKey>
    where TEntity : BaseEntity<TKey>
{
    protected readonly IRepositoryBase<TEntity, TKey> Repo;
    protected readonly IUnitOfWork Uow;
    protected readonly IMapper Mapper;   // AutoMapper hoặc Mapster

    protected ServiceBase(IRepositoryBase<TEntity, TKey> repo, IUnitOfWork uow, IMapper mapper)
    {
        Repo = repo;
        Uow = uow;
        Mapper = mapper;
    }

    public virtual async Task<ApiResponse<IEnumerable<TDto>>> GetAllAsync()
    {
        return ApiResponse<IEnumerable<TDto>>.Ok(Mapper.Map<IEnumerable<TDto>>(await Repo.GetAllAsync()));

    }

    public virtual async Task<ApiResponse<TDto>> GetByIdAsync(TKey id)
    {
        var e = await Repo.GetByIdAsync(id);
        return e == null ? ApiResponse<TDto>.Fail("Not found", 404) : ApiResponse<TDto>.Ok(Mapper.Map<TDto>(e));
    }
    public virtual async Task<ApiResponse<TDto>> CreateAsync(TCreate dto)
    {
        var e = Mapper.Map<TEntity>(dto);
        await Repo.AddAsync(e); 
        await Uow.SaveChangesAsync();
        return ApiResponse<TDto>.Ok(Mapper.Map<TDto>(e), "Created");
    }
    public virtual async Task<ApiResponse<TDto>> UpdateAsync(TKey id, TCreate dto)
    {
        var e = await Repo.GetByIdAsync(id);
        if (e == null) return ApiResponse<TDto>.Fail("Not found", 404);
        Mapper.Map(dto, e); 
        Repo.Update(e); 
        await Uow.SaveChangesAsync();
        return ApiResponse<TDto>.Ok(Mapper.Map<TDto>(e));
    }
    public virtual async Task<ApiResponse<bool>> DeleteAsync(TKey id)
    {
        var e = await Repo.GetByIdAsync(id);
        if (e == null) return ApiResponse<bool>.Fail("Not found", 404);
        Repo.Remove(e); 
        await Uow.SaveChangesAsync();
        return ApiResponse<bool>.Ok(true);
    }
}