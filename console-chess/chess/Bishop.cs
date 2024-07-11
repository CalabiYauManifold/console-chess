using board;


namespace chess
{
    internal class Bishop : Piece
    {
        public Bishop(Color color, Board board) : base(color, board)
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

            // Top Left
            pos.SetValues(Position.Line - 1, Position.Column - 1);
            while (Board.LegalPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.SetValues(pos.Line - 1, pos.Column - 1);
            }

            // Top Right
            pos.SetValues(Position.Line - 1, Position.Column + 1);
            while (Board.LegalPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.SetValues(pos.Line - 1, pos.Column + 1); ;
            }

            // Bottom Right
            pos.SetValues(Position.Line + 1, Position.Column + 1);
            while (Board.LegalPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.SetValues(pos.Line + 1, pos.Column + 1);
            }

            // Bottom Left
            pos.SetValues(Position.Line + 1, Position.Column - 1);
            while (Board.LegalPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.SetValues(pos.Line + 1, pos.Column - 1);
            }

            return mat;
        }

        public override string ToString()
        {
            return "B";
        }
    }
}
