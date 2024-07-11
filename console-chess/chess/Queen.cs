using board;

namespace console_chess.chess
{
    internal class Queen : Piece
    {
        public Queen(Color color, Board board) : base(color, board)
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
            while (Board.LegalPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.Line--;
            }

            // Bottom
            pos.SetValues(Position.Line + 1, Position.Column);
            while (Board.LegalPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.Line++;
            }

            // Right
            pos.SetValues(Position.Line, Position.Column + 1);
            while (Board.LegalPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.Column++;
            }

            // Left
            pos.SetValues(Position.Line, Position.Column - 1);
            while (Board.LegalPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (Board.Piece(pos) != null && Board.Piece(pos).Color != Color)
                {
                    break;
                }
                pos.Column--;
            }

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
            return "Q";
        }
    }
}
