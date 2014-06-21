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
    public class FotocopieViewModel : NotifyPropertyChanged
    {
        #region Private Properties
        private readonly BusinessDocenti _businessDocenti = new BusinessDocenti();
        private readonly BusinessClassi _businessClassi = new BusinessClassi();
        private readonly BusinessSezioni _businessSezioni = new BusinessSezioni();
        private readonly BusinessFotocopie _businessFotocopie = new BusinessFotocopie();

        private ObservableCollection<DocentiModel> _elencoDocenti;
        private ObservableCollection<ClassiModel> _elencoClassi;
        private ObservableCollection<SezioniModel> _elencoSezioni; 
        private ObservableCollection<FotocopieModel> _fotocopieDocente;

        private DocentiModel _selectedDocente;
        private ClassiModel _selectedClasse;
        private SezioniModel _selectedSezione;
        private FotocopieModel _currentFotocopia;
        private FotocopieModel _selectedFotocopia;

        private bool _nuovaRegistrazione;
        private int _totaleFotocopieDocente = 0;

        private readonly ICommand _createCommand;
        private readonly ICommand _updateCommand;
        private readonly ICommand _deleteCommand;
        private readonly ICommand _deleteAllCommand;
        #endregion

        #region Constructor
        public FotocopieViewModel()
        {
            // Init Command della Toolbar Docenti
            this._createCommand = new CommandModel(OnCreateCommand);
            this._updateCommand = new CommandModel(OnUpdateCommand);
            this._deleteCommand = new CommandModel(OnDeleteCommand);
            this._deleteAllCommand = new CommandModel(OnDeleteAllCommand);

            // Carico gli elenchi
            this.ReadElencoDocenti();
            this.ReadElencoClassi();
            this.ReadElencoSezioni();
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

        public ObservableCollection<ClassiModel> ElencoClassi
        {
            get { return this._elencoClassi; }
            set
            {
                if (this._elencoClassi == value) return;

                this._elencoClassi = value;
                OnPropertyChanged("ElencoClassi");
            }
        }

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

        public ClassiModel SelectedClasse
        {
            get { return this._selectedClasse; }
            set
            {
                if (this._selectedClasse == value) return;

                this._selectedClasse = value;
                OnPropertyChanged("SelectedClasse");
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

        //public FotocopieModel CurrentFotocopia
        //{
        //    get { return this._currentFotocopia; }
        //    set
        //    {
        //        if (this._currentFotocopia == value) return;

        //        this._currentFotocopia = value;
        //        OnPropertyChanged("CurrentFotocopia");
        //    }
        //}

        public FotocopieModel SelectedFotocopia
        {
            get { return this._selectedFotocopia; }
            set
            {
                if (this._selectedFotocopia == value) return;

                this._selectedFotocopia = value;
                OnPropertyChanged("SelectedFotocopia");
            }
        }

        public bool NuovaRegistrazione
        {
            get { return this._nuovaRegistrazione; }
            set
            {
                if (this._nuovaRegistrazione == value) return;

                this._nuovaRegistrazione = value;
                OnPropertyChanged("NuovaRegistrazione");
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
        private bool ChkCondizioniSalvataggio()
        {
            return (this.ChkDocente() && this.ChkClasse() && this.ChkSezione() && this.ChkFotocopia());
        }

        private bool ChkDocente()
        {
            return this.SelectedDocente != null;
        }

        private bool ChkClasse()
        {
            return this.SelectedClasse != null;
        }

        private bool ChkSezione()
        {
            return this.SelectedSezione != null;
        }

        private bool ChkFotocopia()
        {
            return this.SelectedFotocopia != null;
        }

        private void ReadElencoDocenti()
        {
            this.ElencoDocenti = this._businessDocenti.ReadElencoDocenti();
            this.SelectedDocente = this.ElencoDocenti.FirstOrDefault();
        }

        private void ReadElencoClassi()
        {
            this.ElencoClassi = this._businessClassi.ReadElencoClassi();
            this.SelectedClasse = this.ElencoClassi.FirstOrDefault();
        }

        private void ReadElencoSezioni()
        {
            this.ElencoSezioni = this._businessSezioni.ReadElencoSezioni();
            this.SelectedSezione = this.ElencoSezioni.FirstOrDefault();
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

        private void CreaNuovaRegistrazione()
        {
            this.SelectedFotocopia = null;
            this.SelectedFotocopia = this._businessFotocopie.NuovaRegistrazione();

            this.NuovaRegistrazione = true;
        }

        private void SaveRegistrazione()
        {
            if (!this.ChkCondizioniSalvataggio())
                return;

            this.SelectedFotocopia.IdDocente = this.SelectedDocente.IdDocente;
            this.SelectedFotocopia.IdClasse = this.SelectedClasse.IdClasse;
            this.SelectedFotocopia.Sezione = this.SelectedSezione.CodiceSezione;

            if (this._businessFotocopie.SalvaRegistrazione(this.SelectedFotocopia) <= 0) return;

            // Mostro il riepilogo fotocopie per il docente selezionato
            this.ReadElencoFotocopieDocente();
            this.NuovaRegistrazione = false;
        }

        private void DeleteRegistrazione()
        {
            if (this.SelectedFotocopia == null)
                return;

            if (ModernDialog.ShowMessage("Cancellazione Definitiva della Riga Selezionata. Confermi?", GestioneCopieStrings.DialogTitle, 
                MessageBoxButton.YesNo) == MessageBoxResult.No) return;

            if (this._businessFotocopie.EliminaRegistrazione(this.SelectedFotocopia) > 0)
                this.ReadElencoFotocopieDocente();
            else
                ModernDialog.ShowMessage("Errore Cancellazione Riga Selezionata", GestioneCopieStrings.DialogTitle,
                    MessageBoxButton.OK);
        }

        private void DeleteAllRegistrazini()
        {
            if (ModernDialog.ShowMessage("Cancellazione Definitiva di Tutte le Registrazioni. Confermi?", GestioneCopieStrings.DialogTitle,
                MessageBoxButton.YesNo) == MessageBoxResult.No) return;

            if (ModernDialog.ShowMessage("Operazione Non Reversibile. Confermi?", GestioneCopieStrings.DialogTitle,
                MessageBoxButton.YesNo) == MessageBoxResult.No) return;

            if (this._businessFotocopie.EliminaTutteLeRegistrazioni() > 0)
                this.ReadElencoFotocopieDocente();
            else
                ModernDialog.ShowMessage("Errore Cancellazione Registrazioni", GestioneCopieStrings.DialogTitle,
                    MessageBoxButton.OK);
        }
        #endregion

        #region Command
        private void OnCreateCommand(object parameter)
        {
            this.CreaNuovaRegistrazione();
        }

        private void OnUpdateCommand(object parameter)
        {
            this.SaveRegistrazione();
        }

        private void OnDeleteCommand(object parameter)
        {
            this.DeleteRegistrazione();
        }

        private void OnDeleteAllCommand(object parameter)
        {
            this.DeleteAllRegistrazini();
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

        public ICommand DeleteAllCommand
        {
            get { return this._deleteAllCommand; }
        }
        #endregion
    }
}
