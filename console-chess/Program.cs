using board;
using chess;

namespace console_chess
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);

            board.SetPiece(new Rook(Color.Black, board), new Position(0, 0));
            board.SetPiece(new Rook(Color.Black, board), new Position(1, 3));
            board.SetPiece(new King(Color.Black, board), new Position(2, 4));

            Screen.PrintBoard(board);
        }
    }
}