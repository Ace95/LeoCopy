using System.Windows.Media;

using FirstFloor.ModernUI.Presentation;

namespace GestioneCopie.WPF.App.ContentViewModels
{
    /// <summary>
    /// A simple view model for configuring theme, font and accent colors.
    /// </summary>
    public class HomeContentMenuViewModel
        : NotifyPropertyChanged
    {
        private ImageSource _imageSource;

        private string _author = "Marco Acerbis";
        private string _classe = "Classe V° N - 2013/2014";

        public HomeContentMenuViewModel()
        {
        }

        #region Public Properties
        public string Author
        {
            get { return this._author; }
            set
            {
                if (this._author == value) return;

                this._author = value;
                OnPropertyChanged("Author");
            }
        }

        public string Classe
        {
            get { return _classe; }
            set
            {
                if (this._classe == value) return;

                this._classe = value;
                OnPropertyChanged("Classe");
            }
        }

        public ImageSource ImageSource
        {
            get { return this._imageSource; }
            set
            {
                if (this._imageSource == value) return;

                this._imageSource = value;
                OnPropertyChanged("ImageSource");
            }
        }
        #endregion
    }
}
