using ClaimTheSquare.Domain;

namespace ClaimTheSquare.Application.Interfaces
{
    public interface ISquareRepository
    {
        // Henter alle ruter som er tatt
        Task<IReadOnlyList<Square>> GetAllAsync();

        // Returnerer false hvis ruten (Index) allerede er tatt
        Task<bool> TryAddAsync(Square square);
    }
}
