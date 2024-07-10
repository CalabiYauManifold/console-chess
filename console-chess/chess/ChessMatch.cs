using board;

namespace chess
{
    internal class ChessMatch
    {
        public Board Board { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool Finished { get; private set; }
        private HashSet<Piece> Pieces;
        private HashSet<Piece> Captured;
        public bool Check { get; private set; }

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            Check = false;
            Pieces = new HashSet<Piece>();
            Captured = new HashSet<Piece>();
            SetPieces();
        }

        public Piece ExecuteMove(Position origin, Position destination)
        {
            Piece p = Board.RemovePiece(origin);
            p.IncreaseMoveQuantity();
            Piece capturedPiece = Board.RemovePiece(destination);
            Board.SetPiece(p, destination);

            if (capturedPiece != null)
            {
                Captured.Add(capturedPiece);
            }

            return capturedPiece;
        }

        public void UndoMove(Position origin, Position destination, Piece capturedPiece)
        {
            Piece p = Board.RemovePiece(destination);
            p.DecreaseMoveQuantity();
            if (capturedPiece != null)
            {
                Board.SetPiece(capturedPiece, destination);
                Captured.Remove(capturedPiece);
            }
            Board.SetPiece(p, origin);
        }

        public void MakePlay(Position origin, Position destination)
        {
            Piece capturedPiece = ExecuteMove(origin, destination);

            if (OnCheck(CurrentPlayer))
            {
                UndoMove(origin, destination, capturedPiece);

                throw new BoardException("You can't put yourself in check");
            }

            if (OnCheck(Opponent(CurrentPlayer)))
            {
                Check = true;
            }
            else
            {
                Check = false;
            }

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

        public HashSet<Piece> CapturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece p in Captured)
            {
                if (p.Color == color)
                {
                    aux.Add(p);
                }
            }

            return aux;
        }

        public HashSet<Piece> OnGamePieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece p in Pieces)
            {
                if (p.Color == color)
                {
                    aux.Add(p);
                }
            }
            aux.ExceptWith(CapturedPieces(color));

            return aux;
        }

        private Color Opponent(Color color)
        {
            if (color == Color.White)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }

        private Piece King(Color color)
        {
            foreach (Piece p in OnGamePieces(color))
            {
                if (p is King)
                {
                    return p;
                }
            }
            return null;
        }

        public bool OnCheck(Color color)
        {
            Piece K = King(color);
            if (K == null)
            {
                throw new BoardException("There is no " + color + " king on the board");
            }

            foreach (Piece p in OnGamePieces(Opponent(color)))
            {
                bool[,] mat = p.PossibleMoves();
                if (mat[K.Position.Line, K.Position.Column])
                {
                    return true;
                }
            }

            return false;
        }

        public void SetNewPiece(char column, int line, Piece piece)
        {
            Board.SetPiece(piece, new ChessPosition(column, line).ToPosition());
            Pieces.Add(piece);
        }

        private void SetPieces()
        {
            SetNewPiece('c', 1, new Rook(Color.White, Board));
            SetNewPiece('c', 2, new Rook(Color.White, Board));
            SetNewPiece('d', 2, new Rook(Color.White, Board));
            SetNewPiece('e', 2, new Rook(Color.White, Board));
            SetNewPiece('e', 1, new Rook(Color.White, Board));
            SetNewPiece('d', 1, new King(Color.White, Board));

            SetNewPiece('c', 8, new Rook(Color.Black, Board));
            SetNewPiece('c', 7, new Rook(Color.Black, Board));
            SetNewPiece('d', 7, new Rook(Color.Black, Board));
            SetNewPiece('e', 7, new Rook(Color.Black, Board));
            SetNewPiece('e', 8, new Rook(Color.Black, Board));
            SetNewPiece('d', 8, new King(Color.Black, Board));
        }
    }
}
