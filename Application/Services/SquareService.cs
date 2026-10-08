using ClaimTheSquare.Application.Interfaces;
using ClaimTheSquare.Domain;

namespace ClaimTheSquare.Application.Services
{
    public class SquareService
    {
        private readonly ISquareRepository _repo;

        public SquareService(ISquareRepository repo)
        {
            this._repo = repo;
        }


        public Task<IReadOnlyList<Square>> GetAllAsync()
        {
            return _repo.GetAllAsync();
        }

        public async Task<ClaimResult> ClaimAsync(Square squareToClaim)
        {
            if (squareToClaim.Index < 0 || squareToClaim.Index > 63) return ClaimResult.Invalid;
            if (String.IsNullOrWhiteSpace(squareToClaim.Text)) return ClaimResult.Invalid;
            if (String.IsNullOrWhiteSpace(squareToClaim.ForeColor)) return ClaimResult.Invalid;
            if (String.IsNullOrWhiteSpace(squareToClaim.BackColor)) return ClaimResult.Invalid;

            bool claimSuccessful = await _repo.TryAddAsync(squareToClaim);
            if (claimSuccessful)
            { 
                return ClaimResult.Success; 
            }
            else
            {
                return ClaimResult.AlreadyTaken;
            } 
        }
    }
}
