using System;
using System.ComponentModel;

namespace GestioneCopie.Data.Model
{
    public class FotocopieModel : INotifyPropertyChanged
    {
        private int _idDocente;
        private string _idClasse;
        private string _sezione;
        private DateTime _dataFotocopie;
        private int _numeroFotocopie;
        private bool _nuovaRegistrazione;

        public int IdDocente
        {
            get { return this._idDocente; }
            set 
            { 
                if (this._idDocente == value) return;

                this._idDocente = value;
                InvokePropertyChanged("IdDocente");
            }
        }

        public string NominativoDocente { get; set; }

        public string IdClasse
        {
            get { return this._idClasse; }
            set
            {
                if (this._idClasse == value) return;

                this._idClasse = value;
                this.InvokePropertyChanged("IdClasse");
            }
        }

        public string DescrizioneClasse { get; set; }

        public string Sezione
        {
            get { return this._sezione; }
            set
            {
                if (this._sezione == value) return;

                this._sezione = value;
                this.InvokePropertyChanged("Sezione");
            }
        }

        public DateTime DataFotocopie
        {
            get { return this._dataFotocopie; }
            set
            {
                if (this._dataFotocopie == value) return;

                this._dataFotocopie = value;
                this.InvokePropertyChanged("DataFotocopie");
            }
        }

        public int NumeroFotocopie
        {
            get { return this._numeroFotocopie; }
            set
            {
                if (this._numeroFotocopie == value) return;

                this._numeroFotocopie = value;
                this.InvokePropertyChanged("NumeroFotocopie");
            }
        }

        public bool NuovaRegistrazione
        {
            get { return this._nuovaRegistrazione; }
            set
            {
                if (this._nuovaRegistrazione == value) return;

                this._nuovaRegistrazione = value;
                this.InvokePropertyChanged("NuovaRegistrazione");
            }
        }

        public FotocopieModel()
        {
            this.DataFotocopie = DateTime.Now;
            this.NumeroFotocopie = 0;
            this.NuovaRegistrazione = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void InvokePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
