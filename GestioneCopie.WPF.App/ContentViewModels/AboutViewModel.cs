using System.Text;
using FirstFloor.ModernUI.Presentation;

namespace GestioneCopie.WPF.App.ContentViewModels
{
    public class AboutViewModel : NotifyPropertyChanged
    {
        private string _credits;
        private string _englishLicense;

        public AboutViewModel()
        {
            this.InitCredits();
            this.InitEnglishLicenseTerms();
        }

        #region PublicProperties
        public string Credits
        {
            get { return this._credits; }
            set
            {
                if (this._credits == value)
                    return;

                this._credits = value;
                OnPropertyChanged("Credits");
            }
        }
        public string EnglishLicense
        {
            get { return this._englishLicense; }
            set
            {
                if (this._englishLicense == value)
                    return;

                this._englishLicense = value;
                OnPropertyChanged("EnglishLicense");
            }
        }
        #endregion

        #region Methods

        private void InitCredits()
        {
            this.Credits = "Marco Acerbis - Classe V sez. N - 2013/2014";
        }

        private void InitEnglishLicenseTerms()
        {
            var sbLicense = new StringBuilder();
            sbLicense.Append("Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the 'LeoCopy'), ");
            sbLicense.Append("to deal in the Software without restriction, including without limitation the rights ");
            sbLicense.Append("to use, copy, modify, merge, publish, distribute, sublicense, and/or sell ");
            sbLicense.Append("copies of the Software, and to permit persons to whom the Software is ");
            sbLicense.Append("furnished to do so, subject to the following conditions: ");
            sbLicense.Append("\r\n");
            sbLicense.Append("The above copyright notice and this permission notice shall be included in ");
            sbLicense.Append("all copies or substantial portions of the Software.");
            sbLicense.Append("\r\n");
            sbLicense.Append("THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND,");
            sbLicense.Append("EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. ");
            sbLicense.Append("\r\n");
            sbLicense.Append("IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER ");
            sbLicense.Append("LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, ");
            sbLicense.Append("OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.");

            this.EnglishLicense = sbLicense.ToString();
        }
        #endregion

    }
}
