using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

using FirstFloor.ModernUI.Presentation;

using GestioneCopie.Business;
using GestioneCopie.Common.Infrastructures;
using GestioneCopie.Data.Model;

namespace GestioneCopie.WPF.App.ContentViewModels
{
    public class SezioniViewModel : NotifyPropertyChanged
    {
        private readonly BusinessSezioni _businessSezioni = new BusinessSezioni();

        private ObservableCollection<SezioniModel> _elencoSezioni;

        private SezioniModel _selectedSezione;

        private readonly ICommand _createCommand;
        private readonly ICommand _updateCommand;
        private readonly ICommand _printCommand;

        #region Constructor
        public SezioniViewModel()
        {
            // Init Command della Toolbar Sezioni
            this._createCommand = new CommandModel(OnCreateCommand);
            this._updateCommand = new CommandModel(OnUpdateCommand);
            this._printCommand = new CommandModel(OnPrintCommand);

            // Acquisizione Elenco Classi da Repository
            this.ReadElencoSezioni();
        }
        #endregion

        #region Public Properties
        public ObservableCollection<SezioniModel> ElencoSezioni
        {
            get { return this._elencoSezioni; }
            set
            {
                if (this._elencoSezioni == value) return;

                this._elencoSezioni = value;
                OnPropertyChanged("ElencoSezioni");
            }
        }

        public SezioniModel SelectedSezione
        {
            get { return this._selectedSezione; }
            set
            {
                if (this._selectedSezione == value) return;

                this._selectedSezione = value;
                OnPropertyChanged("SelectedSezione");
            }
        }
        #endregion

        #region Methods
        private bool ChkSezione()
        {
            return this.SelectedSezione != null;
        }

        private void ReadElencoSezioni()
        {
            this.ElencoSezioni = this._businessSezioni.ReadElencoSezioni();

            this.SelectedSezione = this.ElencoSezioni.FirstOrDefault();
        }

        private void CreaNuovaSezione()
        {
            this.SelectedSezione = this._businessSezioni.CreateSezione();
            this.ElencoSezioni.Add(this.SelectedSezione);
        }

        private void SaveSezione()
        {
            if (!this.ChkSezione())
                return;

            if (this._businessSezioni.SaveSezione(this.SelectedSezione)> 0)
                this.ReadElencoSezioni();
        }
        #endregion

        #region Command
        private void OnCreateCommand(object parameter)
        {
            this.CreaNuovaSezione();
        }

        private void OnUpdateCommand(object parameter)
        {
            this.SaveSezione();
        }

        private void OnPrintCommand(object parameter)
        {
            
        }

        public ICommand CreateCommand
        {
            get { return this._createCommand; }
        }

        public ICommand UpdateCommand
        {
            get { return this._updateCommand; }
        }

        public ICommand PrintCommand
        {
            get { return this._printCommand; }
        }
        #endregion
    }
}
