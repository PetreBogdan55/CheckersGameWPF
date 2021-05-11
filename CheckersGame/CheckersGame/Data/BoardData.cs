using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CheckerGame.Data
{
    public class BoardData
    {
        public enum BoardCodes
        {
            D, // dark
            W, // white
            DBP, // dark with black pawn
            DWP, // dark with white pawn
            DBK, // dark with black king
            DWK, // dark with white king
        }

        BoardCodes[,] board = new BoardCodes[8, 8];
        Point click1;
        Point click2;
        int clickIndex;

        public BoardData()
        {
            click1 = new Point();
            click2 = new Point();
            clickIndex = -1;
            SetOrResetBoard();
        }

        private void SetOrResetBoard()
        {
            for(int index_i=0; index_i < 8; index_i++)
            {
                for(int index_j=0; index_j < 8; index_j++)
                {
                    if (index_i < 3 && (index_i + index_j) % 2 == 1)
                    {
                        board[index_i, index_j] =  BoardCodes.DWP; // dark with white pwan;
                    }
                    else if (index_i > 4 && (index_i + index_j) % 2 == 1)
                    {
                        board[index_i, index_j] = BoardCodes.DBP; // dark with dark pwan;
                    }
                    else if ((index_i + index_j) % 2 == 0)
                    {
                        board[index_i, index_j] = BoardCodes.W; // white
                    }
                    else
                    {
                        board[index_i, index_j] = BoardCodes.D; // dark
                    }
                }
            }
        }

        public bool IsFirstTimeClicked()
        {
            if (clickIndex % 2 == 0) return true;
            return false;
        }
        public BoardCodes[,] GetBoardData()
        {
            return board;
        }

        public void AddDarkSpot(int x, int y)
        {
            board[x,y] = BoardCodes.D;
        }

        public string RestoreBoardDataFromSave(string value)
        {
            string[] separators = new string[] { ",", ".", "!", "\'", " ", "\'s" };
            List<string> keys = new List<string>();
            foreach (string word in value.Split(separators, StringSplitOptions.RemoveEmptyEntries))
                keys.Add(word);
            int index = 0;
            for (int index_i = 0; index_i < 8; index_i++)
            {
                for (int index_j = 0; index_j < 8; index_j++)
                {
                    switch (keys[index])
                    {
                        case "D":
                            board[index_i, index_j] = BoardCodes.D;
                            break;
                        case "W":
                            board[index_i, index_j] = BoardCodes.W;
                            break;
                        case "DBP":
                            board[index_i, index_j] = BoardCodes.DBP;
                            break;
                        case "DWP":
                            board[index_i, index_j] = BoardCodes.DWP;
                            break;
                        case "DBK":
                            board[index_i, index_j] = BoardCodes.DBK;
                            break;
                        case "DWK":
                            board[index_i, index_j] = BoardCodes.DWK;
                            break;
                        default:
                            break;
                    }
                    index++;
                }
            }
            Console.WriteLine(keys[8]+" " + board[1, 0].ToString());
            return keys[index];
        }

        public string GetBoardDataString()
        {
            string output = String.Empty;
            for (int index_i = 0; index_i < 8; index_i++)
            {
                for (int index_j = 0; index_j < 8; index_j++)
                {
                    output += board[index_i, index_j] + " ";
                }
                output += "\n";
            }
            return output;
        }

        public void ButtonClicked(Point point)
        {
            clickIndex++;
            if(clickIndex%2==1)
            {
                Moves moves = new Moves(this, (int)click1.X, (int)click1.Y);
                click2 = point;
                moves.RemoveCapturedPieces((int)click1.X, (int)click1.Y, (int)click2.X, (int)click2.Y);
                BoardCodes aux = board[(int)click1.X, (int)click1.Y];
                board[(int)click1.X, (int)click1.Y] = board[(int)click2.X, (int)click2.Y];
                board[(int)click2.X, (int)click2.Y] = aux;
                click1 = new Point();
                click2 = new Point();
            }
            else
            {
                click1 = point;
                click2 = new Point();
            }
        }   
        
        public void SetKingPiece(bool black, int x, int y)
        {
            if (black)
                board[x, y] = BoardCodes.DBK;
            else board[x, y] = BoardCodes.DWK;
        }
    }
}
