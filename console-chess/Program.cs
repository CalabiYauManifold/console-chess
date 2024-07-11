using board;
using chess;

namespace console_chess
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ChessMatch match = new ChessMatch();

                while (!match.Finished)
                {
                    try
                    {
                        Console.Clear();
                        Screen.PrintMatch(match);

                        Console.Write("Origin: ");
                        Position origin = Screen.ReadChessPosition().ToPosition();
                        match.ValidateOriginPosition(origin);

                        bool[,] possibleMoves = match.Board.Piece(origin).PossibleMoves();

                        Console.Clear();
                        Screen.PrintBoard(match.Board, possibleMoves);

                        Console.WriteLine();
                        Console.Write("Destination: ");
                        Position destination = Screen.ReadChessPosition().ToPosition();
                        match.ValidateDestinationOrigin(origin, destination);

                        match.MakePlay(origin, destination);
                    }
                    catch (BoardException ex) 
                    {
                        Console.WriteLine(ex.Message);
                        Console.ReadLine();
                    }
                }

                Console.Clear();
                Screen.PrintMatch(match);
            }
            catch (BoardException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}