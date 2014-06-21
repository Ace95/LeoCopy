
namespace GestioneCopie.Data.Model
{
    public class ClassiModel
    {
        public string IdClasse { get; set; }
        public string Descrizione { get; set; }
        public int TotaleClasse { get; set; }
        public int TotaleFotocopie { get; set; }
        public bool NuovaClasse { get; set; }

        public ClassiModel()
        {
            this.IdClasse = string.Empty;
            this.Descrizione = "Nuova Classe";
            this.TotaleClasse = 0;
            this.TotaleFotocopie = 0;
            this.NuovaClasse = true;
        }
    }
}
