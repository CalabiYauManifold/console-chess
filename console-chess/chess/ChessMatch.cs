using board;

namespace chess
{
    internal class ChessMatch
    {
        public Board Board { get; private set; }
        private int Turn;
        private Color CurrentPlayer;
        public bool Finished { get; private set; }

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            SetPieces();
        }

        public void ExecuteMove(Position origin, Position destination)
        {
            Piece p = Board.RemovePiece(origin);
            p.IncreaseMoveQuantity();
            Piece capturedPiece = Board.RemovePiece(destination);
            Board.SetPiece(p, destination);
        }

        private void SetPieces()
        {
            Board.SetPiece(new Rook(Color.White, Board), new ChessPosition('c', 1).ToPosition());
            Board.SetPiece(new Rook(Color.White, Board), new ChessPosition('c', 2).ToPosition());
            Board.SetPiece(new Rook(Color.White, Board), new ChessPosition('d', 2).ToPosition());
            Board.SetPiece(new Rook(Color.White, Board), new ChessPosition('e', 2).ToPosition());
            Board.SetPiece(new Rook(Color.White, Board), new ChessPosition('e', 1).ToPosition());
            Board.SetPiece(new King(Color.White, Board), new ChessPosition('d', 1).ToPosition());

            Board.SetPiece(new Rook(Color.Black, Board), new ChessPosition('c', 8).ToPosition());
            Board.SetPiece(new Rook(Color.Black, Board), new ChessPosition('c', 7).ToPosition());
            Board.SetPiece(new Rook(Color.Black, Board), new ChessPosition('d', 7).ToPosition());
            Board.SetPiece(new Rook(Color.Black, Board), new ChessPosition('e', 7).ToPosition());
            Board.SetPiece(new Rook(Color.Black, Board), new ChessPosition('e', 8).ToPosition());
            Board.SetPiece(new King(Color.Black, Board), new ChessPosition('d', 8).ToPosition());
        }
    }
}
