using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersGame.ViewModels
{
    public class AboutViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public AboutViewModel()
        {
            Developer = "Developer Name: Petre Mihai Bogdan";
            Email = "Developer Email: mihai.petre@student.unitbv.ro";
            Group = "Developer Group: 10LF293";
            Rules= "GAME RULES\n\n TYPES OF PIECE MOVEMENT:\n"+
" - Simple move -the piece moves a box, diagonally, in front.If the pawn\n"+
"managed to reach the opposite end of the game board, then it will turn into\n"+
"The 'king' and his move will also be with a house, diagonally, but he will be able to move\n"+
"both front and back.\n"+
" - Jumping over the opponent -when a piece has an opposing piece on one of them\n"+
"its moving positions, then that piece will jump over the other side, causing it to\n"+
"shoot from the game board.A player can only jump over the opponent's pieces\n"+
"to capture them, not over pieces of his.\n" +
" - Multiple jumps - if a player jumped over an opponent and immediately\n" +
"His neighborhood will find another enemy piece that can be captured, will do\n" +
"another jump, and so on until he can no longer capture side pieces.\n\n" +
"Players will only be able to perform these jumps if they choose to\n" +
"the beginning of the game for performing multiple jumps.\n\n" +
"End of game - the game ends when one of the players is gone\n" +
"pieces on the game board.The opposing player will be the winner in this case.";
        }

        private string developer;
        public string Developer
        {
            get { return developer; }
            set { developer = value; OnPropertyChanged("Developer"); }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged("Email"); }
        }

        private string group;
        public string Group
        {
            get { return group; }
            set { group = value; OnPropertyChanged("Group"); }
        }
        private string rules;

        public string Rules
        {
            get { return rules; }
            set { rules = value; OnPropertyChanged("Rules"); }
        }

    }
}
