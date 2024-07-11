using board;

namespace chess
{
    internal class King : Piece
    {
        private ChessMatch Match;

        public King(Color color, Board board, ChessMatch match) : base(color, board)
        {
            Match = match;
        }

        private bool CanMove(Position pos)
        {
            Piece p = Board.Piece(pos);

            return p == null || p.Color != Color;
        }

        private bool RookTestForCastle(Position pos)
        {
            Piece p = Board.Piece(pos);

            return p != null && p is Rook && p.Color == Color && p.MoveQuantity == 0;
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

            // Special move: Castling
            if (MoveQuantity == 0 && !Match.Check)
            {
                // Special move: short castle
                Position rookPos1 = new Position(Position.Line, Position.Column + 3);
                if (RookTestForCastle(rookPos1))
                {
                    Position pos1 = new Position(Position.Line, Position.Column + 1);
                    Position pos2 = new Position(Position.Line, Position.Column + 2);

                    if (Board.Piece(pos1) == null &&  Board.Piece(pos2) == null)
                    {
                        mat[Position.Line, Position.Column + 2] = true;
                    }
                }
                // Special move: long castle
                Position rookPos2 = new Position(Position.Line, Position.Column - 4);
                if (RookTestForCastle(rookPos2))
                {
                    Position pos1 = new Position(Position.Line, Position.Column - 1);
                    Position pos2 = new Position(Position.Line, Position.Column - 2);
                    Position pos3 = new Position(Position.Line, Position.Column - 3);

                    if (Board.Piece(pos1) == null && Board.Piece(pos2) == null && Board.Piece(pos3) == null)
                    {
                        mat[Position.Line, Position.Column - 2] = true;
                    }
                }
            }

            return mat;
        }

        public override string ToString()
        {
            return "K";
        }
    }
}
