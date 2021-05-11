using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersGame.Constants
{
    public static class ConstantsData
    {
        private static int defaultStartMoves = 4;
        public static string GameInfo(string playerName, int playerPossibleMoves, bool isPieceSelected)
        {
            if (playerPossibleMoves < 0) playerPossibleMoves = defaultStartMoves;

            string message = "Current Player: " + playerName;
            if(!isPieceSelected)
                message+="\nPossible pieces to select: "+playerPossibleMoves;
            else message += "\nPossible moves for this piece: " + playerPossibleMoves;
            return message;
        }

        public static string GameOver(string opponentName)
        {
            return opponentName + " win! \nPress 'New Game' to play again!";
        }
    }
}
