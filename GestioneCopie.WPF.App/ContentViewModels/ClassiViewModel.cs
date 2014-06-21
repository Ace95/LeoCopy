using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

using FirstFloor.ModernUI.Presentation;

using GestioneCopie.Business;
using GestioneCopie.Common.Infrastructures;
using GestioneCopie.Data.Model;

namespace GestioneCopie.WPF.App.ContentViewModels
{
    public class ClassiViewModel : NotifyPropertyChanged
    {
        private readonly BusinessClassi _businessClassi = new BusinessClassi();
        private readonly BusinessFotocopie _businessFotocopie = new BusinessFotocopie();

        private ObservableCollection<ClassiModel> _elencoClassi;
        private ObservableCollection<FotocopieModel> _fotocopieClasse;

        private ClassiModel _selectedClasse;

        private readonly ICommand _createCommand;
        private readonly ICommand _updateCommand;
        private readonly ICommand _deleteCommand;
        private readonly ICommand _printCommand;

        #region Constructor
        public ClassiViewModel()
        {
            // Init Command della Toolbar Classi
            this._createCommand = new CommandModel(OnCreateCommand);
            this._updateCommand = new CommandModel(OnUpdateCommand);
            this._deleteCommand = new CommandModel(OnDeleteCommand);
            this._printCommand = new CommandModel(OnPrintCommand);

            // Acquisizione Elenco Classi da Repository
            this.ReadElencoClassi();
        }
        #endregion

        #region Public Properties
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

        public ObservableCollection<FotocopieModel> FotocopieClasse
        {
            get { return this._fotocopieClasse; }
            set
            {
                if (this._fotocopieClasse == value) return;

                this._fotocopieClasse = value;
                OnPropertyChanged("FotocopieClasse");
            }
        }

        public ClassiModel SelectedClasse
        {
            get { return this._selectedClasse; }
            set
            {
                if (this._selectedClasse == value) return;

                this._selectedClasse = value;
                this.ReadElencoFotocopieClasse();
                OnPropertyChanged("SelectedClasse");
            }
        }
        #endregion

        #region Methods
        private bool ChkClasse()
        {
            return this.SelectedClasse != null;
        }

        private void ReadElencoClassi()
        {
            this.ElencoClassi = this._businessClassi.ReadElencoClassi();

            this.SelectedClasse = this.ElencoClassi.FirstOrDefault();
        }

        private void ReadElencoFotocopieClasse()
        {
            if (!this.ChkClasse())
                return;

            this.FotocopieClasse =
                this._businessFotocopie.ReadArchivioFotocopiPerClasse(this.SelectedClasse);

            this.SelectedClasse.TotaleFotocopie = 0;
            foreach (var fotocopia in FotocopieClasse)
            {
                this.SelectedClasse.TotaleClasse += fotocopia.NumeroFotocopie;
            }
            this.SelectedClasse.TotaleFotocopie = this._businessFotocopie.GetTotaleFotocopie();
        }

        private void CreaNuovaClasse()
        {
            this.SelectedClasse = this._businessClassi.CreateClasse();
            this.ElencoClassi.Add(this.SelectedClasse);
        }

        private void SaveClasse()
        {
            if (!this.ChkClasse())
                return;

            //this._businessClassi.SaveDocente(this.SelectedClasse);
        }

        private void DeleteClasse()
        { }

        private void StampaElencoFotocopie()
        {
            if (!this.ChkClasse())
                return;

            //this._businessFotocopie.StampaElencoFotocopiePerDocente(this.SelectedDocente);
        }
        #endregion

        #region Command
        private void OnCreateCommand(object parameter)
        {
            this.CreaNuovaClasse();
        }

        private void OnUpdateCommand(object parameter)
        {
            this.SaveClasse();
        }

        private void OnDeleteCommand(object parameter)
        {
            this.DeleteClasse();
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
