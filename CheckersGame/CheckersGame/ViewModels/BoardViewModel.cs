using CheckerGame.Data;
using CheckersGame.Commands;
using CheckersGame.Models;
using CheckersGame.Constants;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.IO;

namespace CheckersGame.ViewModels
{
    public class BoardViewModel : INotifyPropertyChanged
    {
        #region Fields
        BoardData boardData;
        Player currentPlayer;
        int selectedX, selectedY;

        private enum Player
        {
            BLACK,
            WHITE
        }
        #endregion Fields

        #region Properties

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public BoardViewModel(bool newGame)
        {
            if (newGame)
            {
                boardData = new BoardData();
                currentPlayer = Player.BLACK;
            }
            else
            {
                Load();
            }
            PaintBoard();
            CellClickedCommand = new AppCommands(CellClicked);
            Surrender = new AppCommands(GameOver);
            SaveGame = new AppCommands(Save);
        }

        private string gameInfo;

        public string GameInfo
        {

            get { return gameInfo; }
            set { gameInfo = value; OnPropertyChanged("GameInfo"); }
        }

        private ObservableCollection<Board> board;

        public ObservableCollection<Board> Board
        {

            get { return board; }
            set { board= value; OnPropertyChanged("Board"); }
        }  

        public AppCommands CellClickedCommand { get; set; }

        public AppCommands Surrender { get; set; }

        public AppCommands SaveGame { get; set; }

        #endregion Properties

        #region Methods
        private void GameOver(object param)
        {
            if (currentPlayer == Player.BLACK)
            {
                StatisticsData statisticsData = new StatisticsData();
                statisticsData.WriteStatistics(false, false, false, true);
                GameInfo = ConstantsData.GameOver("WHITE");
            }
            else
            {
                StatisticsData statisticsData = new StatisticsData();
                statisticsData.WriteStatistics(false, true, false, false);
                GameInfo = ConstantsData.GameOver("BLACK");
            }
            foreach(var board in Board)
            {
                board.IsHitTestVisible = false;
            }
        }
       
        private void Save(object param)
        {
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    string value = boardData.GetBoardDataString();
                    if (currentPlayer == Player.BLACK)
                        value += "White";
                    else value += "Black";
                    myStream.Write(Encoding.Default.GetBytes(value), 0, value.Length);
                    myStream.Close();
                }
            }
        }

        private void Load()
        {
            try
            {
                string pathToFile = "";
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    pathToFile = fileDialog.FileName;
                }

                if (File.Exists(pathToFile))
                {
                    string text = "";
                    using (StreamReader sr = new StreamReader(pathToFile))
                    {
                        text = sr.ReadToEnd();
                        boardData = new BoardData();
                        string value = boardData.RestoreBoardDataFromSave(text);
                        string white = @"White";
                        if (value.Contains(white))
                        {
                            currentPlayer = Player.BLACK;
                        }
                        else
                        {
                            currentPlayer = Player.WHITE;
                        }
                        }
                }

               
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("A aparut o eroare la deschiderea acestui fisier", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CellClicked(object param)
        {
            Board board = param as Board;
            selectedX = (int)board.CellPosition.X;
            selectedY = (int)board.CellPosition.Y;
            
            boardData.ButtonClicked(new Point(board.CellPosition.X, board.CellPosition.Y));
            if (!boardData.IsFirstTimeClicked())
            {
                if (currentPlayer == Player.BLACK)
                    currentPlayer = Player.WHITE;
                else currentPlayer = Player.BLACK;
            }
            PaintBoard();
        }
        private void PaintBoard()
        {
            Board = new ObservableCollection<Board>();
            for (int index_i = 0; index_i < 8; index_i++)
                for (int index_j = 0; index_j < 8; index_j++)
                {
                    switch (boardData.GetBoardData()[index_i, index_j])
                    {
                        case BoardData.BoardCodes.DWP:
                            if (index_i == 7)
                            {
                                board.Add(new Board { CellColor = CellColor.DarkWithWhiteKingPiece, CellPosition = new Point(index_i, index_j) });
                                boardData.SetKingPiece(false, index_i, index_j);
                            }
                            else
                                board.Add(new Board { CellColor = CellColor.DarkWithWhitePawnPiece, CellPosition = new Point(index_i, index_j) });
                            break;
                        case BoardData.BoardCodes.DBP:
                            if (index_i == 0)
                            {
                                board.Add(new Board { CellColor = CellColor.DarkWithBlackKingPiece, CellPosition = new Point(index_i, index_j) });
                                boardData.SetKingPiece(true, index_i, index_j);
                            }
                            else
                                board.Add(new Board { CellColor = CellColor.DarkWithBlackPawnPiece, CellPosition = new Point(index_i, index_j) });
                            break;
                        case BoardData.BoardCodes.W:
                            board.Add(new Board { CellColor = CellColor.Light, CellPosition = new Point(index_i, index_j) });
                            break;
                        case BoardData.BoardCodes.D:
                            board.Add(new Board { CellColor = CellColor.Dark, CellPosition = new Point(index_i, index_j) });
                            break;
                        case BoardData.BoardCodes.DWK:
                            board.Add(new Board { CellColor = CellColor.DarkWithWhiteKingPiece, CellPosition = new Point(index_i, index_j) });
                            break;
                        case BoardData.BoardCodes.DBK:
                            board.Add(new Board { CellColor = CellColor.DarkWithBlackKingPiece, CellPosition = new Point(index_i, index_j) });
                            break;
                        default:
                            throw new Exception("This should not be reached!");
                    }
                    board[index_i * 8 + index_j].IsHitTestVisible = false;                    
                }
            if (!boardData.IsFirstTimeClicked())
            {
                int numberOfMoves = 0;
                for (int index_i = 0; index_i < 8; index_i++)
                    for (int index_j = 0; index_j < 8; index_j++)
                    {
                        Moves moves = new Moves(boardData, index_j, index_i);
                        if (currentPlayer == Player.BLACK)
                        {
                            if (boardData.GetBoardData()[index_i, index_j] == BoardData.BoardCodes.DBP || boardData.GetBoardData()[index_i, index_j] == BoardData.BoardCodes.DBK)
                            {
                                if (moves.GetMovesForCurrentPiece() > 0) numberOfMoves += 1;
                                board[index_i * 8 + index_j].IsHitTestVisible = (moves.GetMovesForCurrentPiece() > 0) ? true : false;
                            }
                        }
                        else
                        {
                            if (boardData.GetBoardData()[index_i, index_j] == BoardData.BoardCodes.DWP || boardData.GetBoardData()[index_i, index_j] == BoardData.BoardCodes.DWK)
                            {
                                if (moves.GetMovesForCurrentPiece() > 0) numberOfMoves += 1;
                                board[index_i * 8 + index_j].IsHitTestVisible = (moves.GetMovesForCurrentPiece() > 0) ? true : false;
                            }
                        }
                    }
                GameInfo = ConstantsData.GameInfo(currentPlayer.ToString(), numberOfMoves, false);
                if(numberOfMoves==0)
                {
                    if (currentPlayer == Player.BLACK)
                    {
                        StatisticsData data = new StatisticsData();
                        data.WriteStatistics(false, false, true, false);
                        GameInfo = ConstantsData.GameOver("WHITE");
                    }
                    else
                    {

                        StatisticsData data = new StatisticsData();
                        data.WriteStatistics(true, false, false, false);
                        GameInfo = ConstantsData.GameOver("BLACK");
                    }
                }
            }
            else
            {
                Moves moves = new Moves(boardData, selectedY, selectedX);
                GameInfo = ConstantsData.GameInfo(currentPlayer.ToString(), moves.GetMovesForCurrentPiece(), true);
                foreach (int move in moves.GetMovesList())
                {
                    board[move].IsHitTestVisible = true;
                }
            }
        }
        #endregion Methods
    }
}
