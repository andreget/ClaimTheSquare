namespace ClaimTheSquare.Domain
{
    /*
     * textObjects: [{ index: 17, text: 'Terje', backColor: 'green', foreColor: 'white' }]
     */
    public class Square
    {
        public required int Index { get; init; }
        public required string Text { get; init; }
        public required string BackColor { get; init; }
        public required string ForeColor { get; init; }       
    }
}
