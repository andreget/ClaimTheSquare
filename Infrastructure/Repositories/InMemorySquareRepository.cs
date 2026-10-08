using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using ClaimTheSquare.Application.Interfaces;
using ClaimTheSquare.Domain;

namespace ClaimTheSquare.Infrastructure.Repositories
{
    public class InMemorySquareRepository : ISquareRepository
    {
        private readonly ConcurrentDictionary<int, Square> squares = new();

        public InMemorySquareRepository()
        {
            var sq = new Square { 
                Index = 17, 
                BackColor = "green", 
                ForeColor = "white", 
                Text = "Terje" 
            };
            squares.TryAdd(17, sq);
        }

        public Task<IReadOnlyList<Square>> GetAllAsync()
        {
            // les alle ut sortert
            IReadOnlyList<Square> squareSorted = squares.Values
                .OrderBy(s => s.Index)
                .ToList();
                
            return Task.FromResult(squareSorted);
        }

        public Task<bool> TryAddAsync(Square square)
        {
            return Task.FromResult(squares.TryAdd(square.Index, square));
        }
    }
}
