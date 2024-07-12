using board;

namespace chess
{
    internal class Pawn : Piece
    {
        private ChessMatch Match;

        public Pawn(Color color, Board board, ChessMatch match) : base(color, board)
        {
            Match = match;
        }

        private bool ExistsEnemy(Position pos)
        {
            Piece p = Board.Piece(pos);

            return p != null && p.Color != Color;
        }

        private bool Free(Position pos)
        {
            return Board.Piece(pos) == null;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];

            Position pos = new Position(0, 0);

            if (Color == Color.White)
            {
                pos.SetValues(Position.Line - 1, Position.Column);
                if (Board.LegalPosition(pos) && Free(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.SetValues(Position.Line - 2, Position.Column);
                Position pos2 = new Position(Position.Line - 1, Position.Column);
                if (Board.LegalPosition(pos2) && Free(pos2) && Board.LegalPosition(pos) && Free(pos) && MoveQuantity == 0)
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.SetValues(Position.Line - 1, Position.Column - 1);
                if (Board.LegalPosition(pos) && ExistsEnemy(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.SetValues(Position.Line - 1, Position.Column + 1);
                if (Board.LegalPosition(pos) && ExistsEnemy(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }

                // Special move: en passant
                if (Position.Line == 3)
                {
                    Position left = new Position(Position.Line, Position.Column - 1);
                    if (Board.LegalPosition(left) && ExistsEnemy(left) && Board.Piece(left) == Match.EnPassantVulnerable)
                    {
                        mat[left.Line - 1, left.Column] = true;
                    }
                    Position right = new Position(Position.Line, Position.Column + 1);
                    if (Board.LegalPosition(right) && ExistsEnemy(right) && Board.Piece(right) == Match.EnPassantVulnerable)
                    {
                        mat[right.Line - 1, right.Column] = true;
                    }
                }
            }
            else
            {
                pos.SetValues(Position.Line + 1, Position.Column);
                if (Board.LegalPosition(pos) && Free(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.SetValues(Position.Line + 2, Position.Column);
                Position pos2 = new Position(Position.Line + 1, Position.Column);
                if (Board.LegalPosition(pos2) && Free(pos2) && Board.LegalPosition(pos) && Free(pos) && MoveQuantity == 0)
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.SetValues(Position.Line + 1, Position.Column - 1);
                if (Board.LegalPosition(pos) && ExistsEnemy(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.SetValues(Position.Line + 1, Position.Column + 1);
                if (Board.LegalPosition(pos) && ExistsEnemy(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }

                // Special move: en passant
                if (Position.Line == 4)
                {
                    Position left = new Position(Position.Line, Position.Column - 1);
                    if (Board.LegalPosition(left) && ExistsEnemy(left) && Board.Piece(left) == Match.EnPassantVulnerable)
                    {
                        mat[left.Line + 1, left.Column] = true;
                    }
                    Position right = new Position(Position.Line, Position.Column + 1);
                    if (Board.LegalPosition(right) && ExistsEnemy(right) && Board.Piece(right) == Match.EnPassantVulnerable)
                    {
                        mat[right.Line + 1, right.Column] = true;
                    }
                }
            }

            return mat;
        }

        public override string ToString()
        {
            return "P";
        }
    }
}
