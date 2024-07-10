using board;
using Microsoft.Win32.SafeHandles;

namespace chess
{
    internal class ChessMatch
    {
        public Board Board { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
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

        public void MakePlay(Position origin, Position destination)
        {
            ExecuteMove(origin, destination);
            Turn++;
            ChangePlayer();
        }

        public void ValidateOriginPosition(Position pos)
        {
            if (Board.Piece(pos) == null)
            {
                throw new BoardException("There is no piece on the chosen origin position!");
            }
            if (CurrentPlayer != Board.Piece(pos).Color)
            {
                throw new BoardException("The chosen origin piece isn't yours!");
            }
            if (!Board.Piece(pos).ExistsPossibleMoves())
            {
                throw new BoardException("There is no possible moves for the chosen origin piece!");
            }
        }

        public void ValidateDestinationOrigin(Position origin, Position destination)
        {
            if (!Board.Piece(origin).CanMoveTo(destination))
            {
                throw new BoardException("Invalid destination position!");
            }
        }

        private void ChangePlayer()
        {
            if (CurrentPlayer == Color.White)
            {
                CurrentPlayer = Color.Black;
            }
            else
            {
                CurrentPlayer = Color.White;
            }
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
