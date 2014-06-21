namespace GestioneCopie.Data.Model
{
    public class DocentiModel
    {
        public int IdDocente { get; set; }
        public string Cognome { get; set; }
        public string Nome { get; set; }
        public string NomeVisualizzato { get; set; }
        public int TotaleDocente { get; set; }
        public int TotaleFotocopie { get; set; }

        public DocentiModel()
        {
            this.IdDocente = 0;
            this.Cognome = string.Empty;
            this.Nome = string.Empty;
            this.NomeVisualizzato = string.Empty;
            this.TotaleDocente = 0;
            this.TotaleFotocopie = 0;
        }
    }
}
