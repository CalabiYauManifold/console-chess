namespace board
{
    internal class Board
    {
        public int Lines { get; set; }
        public int Columns { get; set; }
        private Piece[,] Pieces;

        public Board(int lines, int columns)
        {
            Lines = lines;
            Columns = columns;
            Pieces = new Piece[Lines, Columns];
        }

        public Piece Piece(int line, int column)
        {
            return Pieces[line, column];
        }

        public Piece Piece(Position pos)
        {
            return Pieces[pos.Line, pos.Column];
        }

        public bool ExistsPiece(Position pos)
        {
            ValidatePosition(pos);

            return Piece(pos) != null;
        }

        public void SetPiece(Piece p, Position pos)
        {
            if (ExistsPiece(pos))
            {
                throw new BoardException("There is already a piece in that position!");
            }

            Pieces[pos.Line, pos.Column] = p;
            p.Position = pos;
        }

        public bool LegalPosition(Position pos)
        {
            if (pos.Line < 0 || pos.Line >= Lines || pos.Column < 0 || pos.Column >= Columns)
            {
                return false;
            }

            return true;
        }

        public void ValidatePosition(Position pos)
        {
            if (!LegalPosition(pos))
            {
                throw new BoardException("Invalid position!");
            }
        }
    }
}
