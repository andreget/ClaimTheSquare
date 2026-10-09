using ClaimTheSquare.Application.Interfaces;
using ClaimTheSquare.Domain;
using System.Text.Json;

namespace ClaimTheSquare.Infrastructure.Repositories
{
    public class FileSquareRepository : ISquareRepository
    {
        private readonly string _filePath;
        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        private static readonly JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };

        private async Task<IReadOnlyList<Square>> ReadSquaresFromFile()
        {
            string fileText = File.Exists(_filePath) ? await File.ReadAllTextAsync(_filePath) : "";
            if (String.IsNullOrWhiteSpace(fileText)) return new List<Square>();
            
            return JsonSerializer.Deserialize<List<Square>>(fileText, options) ?? new List<Square>();
        }

        private async Task<bool> WriteSquaresToFile(List<Square> squares)
        {
            
            Directory.CreateDirectory(Path.GetDirectoryName(_filePath));

            string listAsString = JsonSerializer.Serialize<List<Square>>(squares, options);
            await File.WriteAllTextAsync(_filePath, listAsString);

            return true;
        }

        public FileSquareRepository(string filePath)
        {
            this._filePath = filePath;          
        }

        public async Task<IReadOnlyList<Square>> GetAllAsync()
        {
            await _semaphore.WaitAsync();
            try
            {
                var squaresFromFile = await ReadSquaresFromFile();
                return squaresFromFile.OrderBy(s => s.Index).ToList();
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<bool> TryAddAsync(Square square)
        {
            bool squareAdded = false;
            await _semaphore.WaitAsync();
            try
            {
                var squaresFromFile = await ReadSquaresFromFile();
                if( !squaresFromFile.Any(s => s.Index == square.Index) )
                {
                    // Index er ikke allerede i bruk
                    var squaresToWrite = squaresFromFile.Append(square);
                    squareAdded = await WriteSquaresToFile(squaresToWrite.ToList());
                }
            }
            finally
            {
                _semaphore.Release();
            }

            return squareAdded;
        }
    }
}
