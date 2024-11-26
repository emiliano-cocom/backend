using Backend.DTOs;

namespace Backend.Services
{
    public interface ICommonService<T, TI, TU>
    {
        public List<string> Errors { get; }
        public Task<IEnumerable<T>> Get();
        public Task<T> GetById(int id);
        public Task<T> Add(TI beerInsertDto);
        public Task<T> Update(int id, TU beerUpdateDto);
        public Task<T> Delete(int id);
        bool Validate(TI dto);
        bool Validate(TU dto);
    }
}
