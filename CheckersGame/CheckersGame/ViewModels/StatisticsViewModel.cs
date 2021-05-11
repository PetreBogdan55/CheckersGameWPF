using CheckerGame.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersGame.ViewModels
{
    public class StatisticsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public StatisticsViewModel()
        {
            StatisticsData statisticsData = new StatisticsData();
            string[] data = statisticsData.ReadStatistics();
            WinsBlack = data[0] + " total black wins";
            WinsBlackPlayerNormal =data[1] + " black wins without any white surrender";
            WinsBlackPlayerSurrender = data[2]+  " black wins with white surrender";
            WinsWhite = data[3] + " total white wins";
            WinsWhitePlayerNormal = data[4]+    " white wins without any black surrender";
            WinsWhitePlayerSurrender = data[5]+ " white wins with black surrender";
        }

        private string winsBlack;
        public string WinsBlack
        {
            get { return winsBlack; }
            set { winsBlack = value; OnPropertyChanged("WinsBlack"); }
        }

        private string winsWhite;
        public string WinsWhite
        {
            get { return winsWhite; }
            set { winsWhite = value; OnPropertyChanged("WinsWhite"); }
        }

        private string winsBlackPlayerNormal;
        public string WinsBlackPlayerNormal
        {
            get { return winsBlackPlayerNormal; }
            set { winsBlackPlayerNormal = value; OnPropertyChanged("WinsBlackPlayerNormal"); }
        }

        private string winsBlackPlayerSurrender;
        public string WinsBlackPlayerSurrender
        {
            get { return winsBlackPlayerSurrender; }
            set { winsBlackPlayerSurrender = value; OnPropertyChanged("WinsBlackPlayerSurrender"); }
        }

        private string winsWhitePlayerNormal;
        public string WinsWhitePlayerNormal
        {
            get { return winsWhitePlayerNormal; }
            set { winsWhitePlayerNormal = value; OnPropertyChanged("WinsWhitePlayerNormal"); }
        }
        private string winsWhitePlayerSurrender;

        public string WinsWhitePlayerSurrender
        {
            get { return winsWhitePlayerSurrender; }
            set { winsWhitePlayerSurrender = value; OnPropertyChanged("WinsWhitePlayerSurrender"); }
        }
    }
}
