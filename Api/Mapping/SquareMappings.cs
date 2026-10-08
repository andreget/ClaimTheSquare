using ClaimTheSquare.Api.DTOs;
using ClaimTheSquare.Domain;

namespace ClaimTheSquare.Api.Mapping
{
    public static class SquareMappings
    {

        public static SquareDTO ToDTO(this Square square)
        {
            if (square is null) throw new System.ArgumentNullException(nameof(square));

            return new SquareDTO
            {
                Index = square.Index,
                Text = square.Text,
                BackColor = square.BackColor,
                ForeColor = square.ForeColor
            };
        }

        public static Square ToDomain(this SquareDTO squareDTO)
        {
            if (squareDTO is null) throw new System.ArgumentNullException(nameof(squareDTO));

            return new Square
            {
                Index = squareDTO.Index,
                Text = squareDTO.Text,
                BackColor = squareDTO.BackColor,
                ForeColor = squareDTO.ForeColor
            };
        }

    }
}
