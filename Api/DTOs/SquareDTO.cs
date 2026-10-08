namespace ClaimTheSquare.Api.DTOs
{
    public class SquareDTO
    {
        public required int Index { get; init; }
        public required string Text { get; init; }
        public required string BackColor { get; init; }
        public required string ForeColor { get; init; }
    }
}
