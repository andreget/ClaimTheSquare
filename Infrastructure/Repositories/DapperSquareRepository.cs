using ClaimTheSquare.Application.Interfaces;
using ClaimTheSquare.Domain;
using Microsoft.Data.SqlClient;
using Dapper;

namespace ClaimTheSquare.Infrastructure.Repositories
{
    public class DapperSquareRepository : ISquareRepository
    {
        private readonly string _connectionString;
        private const int DuplicateKeyUniqueIndex = 2601; // unik indeks
        private const int DuplicateKeyConstraint = 2627; // PRIMARY KEY / UNIQUE constraint

        public DapperSquareRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        

        public async Task<IReadOnlyList<Square>> GetAllAsync()
        {
            using ( SqlConnection conn = new SqlConnection(_connectionString))
            {
                var query = "SELECT * FROM Squares ORDER BY [Index]";
                return (await conn.QueryAsync<Square>(query)).ToList();
            }
        }

        public async Task<bool> TryAddAsync(Square square)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                var statement = "INSERT INTO Squares ([Index], [Text], [BackColor], [ForeColor]) VALUES (@Index, @Text, @BackColor, @ForeColor)";
                try
                {
                    var rowsAffected = await conn.ExecuteAsync(statement, square);
                    return rowsAffected == 1;
                }
                catch(SqlException se) when (se.Number == DuplicateKeyUniqueIndex || se.Number == DuplicateKeyConstraint)
                {
                    Console.WriteLine($"Exception when inserting {square.Index} into the repository.");
                    Console.WriteLine($"Exception message = {se.Message}");
                    return false;
                }
            }
        }
    }
}
