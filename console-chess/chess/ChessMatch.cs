﻿using board;
using console_chess.chess;

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

            if (CheckMateTest(Opponent(CurrentPlayer)))
            {
                Finished = true;
            }
            else
            {
                Turn++;
                ChangePlayer();
            }
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
            if (!Board.Piece(origin).PossibleMove(destination))
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

        public bool CheckMateTest(Color color)
        {
            if (!OnCheck(color))
            {
                return false;
            }

            foreach (Piece p in OnGamePieces(color))
            {
                bool[,] mat = p.PossibleMoves();
                for (int i = 0; i < Board.Lines; i++)
                {
                    for (int j = 0; j < Board.Columns; j++)
                    {
                        if (mat[i, j])
                        {
                            Position origin = p.Position;
                            Position destination = new Position(i, j);
                            Piece capturedPiece = ExecuteMove(origin, destination);
                            bool checkTest = OnCheck(color);
                            UndoMove(origin, destination, capturedPiece);

                            if (!checkTest)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }

        public void SetNewPiece(char column, int line, Piece piece)
        {
            Board.SetPiece(piece, new ChessPosition(column, line).ToPosition());
            Pieces.Add(piece);
        }

        private void SetPieces()
        {
            // White
            SetNewPiece('a', 1, new Rook(Color.White, Board));
            SetNewPiece('b', 1, new Knight(Color.White, Board));
            SetNewPiece('c', 1, new Bishop(Color.White, Board));
            SetNewPiece('d', 1, new Queen(Color.White, Board));
            SetNewPiece('e', 1, new King(Color.White, Board));
            SetNewPiece('f', 1, new Bishop(Color.White, Board));
            SetNewPiece('g', 1, new Knight(Color.White, Board));
            SetNewPiece('h', 1, new Rook(Color.White, Board));
            for (char ch = 'a'; ch <= 'h'; ch++)
            {
                SetNewPiece(ch, 2, new Pawn(Color.White, Board));
            }

            // Black
            SetNewPiece('a', 8, new Rook(Color.Black, Board));
            SetNewPiece('b', 8, new Knight(Color.Black, Board));
            SetNewPiece('c', 8, new Bishop(Color.Black, Board));
            SetNewPiece('d', 8, new Queen(Color.Black, Board));
            SetNewPiece('e', 8, new King(Color.Black, Board));
            SetNewPiece('f', 8, new Bishop(Color.Black, Board));
            SetNewPiece('g', 8, new Knight(Color.Black, Board));
            SetNewPiece('h', 8, new Rook(Color.Black, Board));
            for (char ch = 'a'; ch <= 'h'; ch++)
            {
                SetNewPiece(ch, 7, new Pawn(Color.Black, Board));
            }
        }
    }
}
