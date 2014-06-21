using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;

using GestioneCopie.Business;
using GestioneCopie.Common.Infrastructures;
using GestioneCopie.Data.Model;
using GestioneCopie.WPF.App.Resources;

namespace GestioneCopie.WPF.App.ContentViewModels
{
    public class DocentiViewModel : NotifyPropertyChanged
    {
        private readonly BusinessDocenti _businessDocenti = new BusinessDocenti();
        private readonly BusinessFotocopie _businessFotocopie = new BusinessFotocopie();

        private ObservableCollection<DocentiModel> _elencoDocenti;
        private ObservableCollection<FotocopieModel> _fotocopieDocente;
        
        private DocentiModel _selectedDocente;
        private int _totaleFotocopieDocente = 0;

        private readonly ICommand _createCommand;
        private readonly ICommand _updateCommand;
        private readonly ICommand _deleteCommand;
        private readonly ICommand _printCommand;

        #region Constructor
        public DocentiViewModel()
        {
            // Init Command della Toolbar Docenti
            this._createCommand = new CommandModel(OnCreateCommand);
            this._updateCommand = new CommandModel(OnUpdateCommand);
            this._deleteCommand = new CommandModel(OnDeleteCommand);
            this._printCommand = new CommandModel(OnPrintCommand);

            // Carico l'elenco dei Docenti
            this.ReadElencoDocenti();
        }
        #endregion

        #region Public Properties
        public ObservableCollection<DocentiModel> ElencoDocenti
        {
            get { return this._elencoDocenti; }
            set
            {
                if (this._elencoDocenti == value) return;

                this._elencoDocenti = value;
                OnPropertyChanged("ElencoDocenti");
            }
        }

        public ObservableCollection<FotocopieModel> FotocopieDocente
        {
            get { return this._fotocopieDocente; }
            set
            {
                if (this._fotocopieDocente == value) return;

                this._fotocopieDocente = value;
                OnPropertyChanged("FotocopieDocente");
            }
        }

        public DocentiModel SelectedDocente
        {
            get { return this._selectedDocente; }
            set
            {
                if (this._selectedDocente == value) return;

                this._selectedDocente = value;
                this.ReadElencoFotocopieDocente();
                OnPropertyChanged("SelectedDocente");
            }
        }

        public int TotaleFotocopieDocente
        {
            get { return this._totaleFotocopieDocente; }
            set
            {
                if (this._totaleFotocopieDocente == value) return;

                this._totaleFotocopieDocente = value;
                OnPropertyChanged("TotaleFotocopieDocente");
            }
        }
        #endregion

        #region Methods
        private bool ChkDocente()
        {
            return this.SelectedDocente != null;
        }

        private void ReadElencoDocenti()
        {
            this.ElencoDocenti = this._businessDocenti.ReadElencoDocenti();

            this.SelectedDocente = this.ElencoDocenti.FirstOrDefault();
        }

        private void ReadElencoFotocopieDocente()
        {
            if (!this.ChkDocente())
                return;

            this.FotocopieDocente =
                this._businessFotocopie.ReadArchivioFotocopiPerDocente(this.SelectedDocente);

            this.SelectedDocente.TotaleFotocopie = 0;
            foreach (var fotocopia in FotocopieDocente)
            {
                this.SelectedDocente.TotaleDocente += fotocopia.NumeroFotocopie;
            }
            this.SelectedDocente.TotaleFotocopie = this._businessFotocopie.GetTotaleFotocopie();
        }

        private void CreaNuovoDocente()
        {
            this.SelectedDocente = this._businessDocenti.CreaDocente();
            this.ElencoDocenti.Add(this.SelectedDocente);
        }

        private void SaveDocente()
        {
            if (!this.ChkDocente())
                return;

            var idDocente = this.SelectedDocente.IdDocente;
            this._businessDocenti.SaveDocente(this.SelectedDocente);

            this.ReadElencoDocenti();

            if (idDocente != 0)
                this.SelectedDocente = this.ElencoDocenti.First(d => d.IdDocente == idDocente);
        }

        private void DeleteDocente()
        {
            if (!this.ChkDocente()) return;

            if (ModernDialog.ShowMessage("Cancellazione Definitiva del Docente Selezionato. Confermi?", GestioneCopieStrings.DialogTitle, 
                MessageBoxButton.YesNo) == MessageBoxResult.No) return;

            if (this._businessDocenti.DeleteDocente(this.SelectedDocente) > 0)
                this.ReadElencoDocenti();
            else
                ModernDialog.ShowMessage("Errore Cancellazione Docente", GestioneCopieStrings.DialogTitle,
                    MessageBoxButton.OK);
        }

        private void StampaElencoFotocopie()
        {
            if (!this.ChkDocente())
                return;

            this._businessFotocopie.StampaElencoFotocopiePerDocente(this.SelectedDocente);
        }
        #endregion

        #region Command
        private void OnCreateCommand(object parameter)
        {
            this.CreaNuovoDocente();
        }

        private void OnUpdateCommand(object parameter)
        {
            this.SaveDocente();
        }

        private void OnDeleteCommand(object parameter)
        {
            this.DeleteDocente();
        }

        private void OnPrintCommand(object parameter)
        {
            this.StampaElencoFotocopie();
        }

        public ICommand CreateCommand
        {
            get { return this._createCommand; }
        }

        public ICommand UpdateCommand
        {
            get { return this._updateCommand; }
        }

        public ICommand DeleteCommand
        {
            get { return this._deleteCommand; }
        }

        public ICommand PrintCommand
        {
            get { return this._printCommand; }
        }
        #endregion
    }
}
