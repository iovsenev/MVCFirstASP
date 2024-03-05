namespace MVCFirstASP.Models.DB.Repositories
{
    public interface IRequestRepository
    {
        Task Add(Request request);
        Task<Request[]> GetAll();
    }
}
