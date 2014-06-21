//using System.Speech.Recognition;
using FirstFloor.ModernUI.Presentation;

namespace GestioneCopie.WPF.App.ContentViewModels
{
    /// <summary>
    /// A simple view model for configuring theme, font and accent colors.
    /// </summary>
    public class StartContentViewModel
        : NotifyPropertyChanged
    {
        private const string FontSmall = "small";
        private const string FontLarge = "large";

        //private static SpeechRecognitionEngine _recognizer = null;

        private string _author = "Marco Acerbis";
        private string _classe = "Classe V° N - 2013/2014";

        public StartContentViewModel()
        {
            //_recognizer = new SpeechRecognitionEngine();
            //_recognizer.LoadGrammar(new Grammar(new GrammarBuilder("docenti")));
            //_recognizer.LoadGrammar(new Grammar(new GrammarBuilder("classi")));

            //_recognizer.SpeechRecognized += _recognizeSpeechAndWriteToConsole_SpeechRecognized; // if speech is recognized, call the specified method
            //_recognizer.SpeechRecognitionRejected += _recognizeSpeechAndWriteToConsole_SpeechRecognitionRejected; // if recognized speech is rejected, call the specified method
            //_recognizer.SetInputToDefaultAudioDevice(); // set the input to the default audio device
            //_recognizer.RecognizeAsync(RecognizeMode.Multiple); // recognize speech asynchronous
        }

        #region VoiceRecognition
        //private static void _recognizeSpeechAndWriteToConsole_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        //{ }

        //private static void _recognizeSpeechAndWriteToConsole_SpeechRecognitionRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        //{ }
        #endregion

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
        #endregion
    }
}
