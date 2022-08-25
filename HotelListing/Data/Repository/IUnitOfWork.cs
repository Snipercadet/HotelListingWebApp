using HotelListing.Models;

namespace HotelListing.Data.Repository
{
    public interface IUnitOfWork: IDisposable
    {
        IGenericRepository<Country> Countries { get; }
        IGenericRepository<Hotel> Hotels { get; }
        void Save();
    }
}
