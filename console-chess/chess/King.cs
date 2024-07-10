using board;

namespace chess
{
    internal class King : Piece
    {
        public King(Color color, Board board) : base(color, board)
        {
        }

        private bool CanMove(Position pos)
        {
            Piece p = Board.Piece(pos);

            return p == null || p.Color != Color;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];

            Position pos = new Position(0, 0);

            // Top
            pos.SetValues(Position.Line - 1, Position.Column);
            if (Board.LegalPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }
            // Top right
            pos.SetValues(Position.Line - 1, Position.Column + 1);
            if (Board.LegalPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }
            // Right
            pos.SetValues(Position.Line, Position.Column + 1);
            if (Board.LegalPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }
            // Bottom right
            pos.SetValues(Position.Line + 1, Position.Column + 1);
            if (Board.LegalPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }
            // Bottom 
            pos.SetValues(Position.Line + 1, Position.Column);
            if (Board.LegalPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }
            // Bottom left
            pos.SetValues(Position.Line + 1, Position.Column - 1);
            if (Board.LegalPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }
            // Left
            pos.SetValues(Position.Line, Position.Column - 1);
            if (Board.LegalPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }
            // Top left
            pos.SetValues(Position.Line - 1, Position.Column - 1);
            if (Board.LegalPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            return mat;
        }

        public override string ToString()
        {
            return "K";
        }
    }
}
