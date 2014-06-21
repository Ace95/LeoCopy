using System.Collections.ObjectModel;
using System.Data;

using GestioneCopie.Data.Datasets;
using GestioneCopie.Data.Datasets.datasetLeonardoTableAdapters;
using GestioneCopie.Data.Model;

namespace GestioneCopie.Business
{
    public class BusinessClassi
    {
        public ClassiModel CreateClasse()
        {
            return new ClassiModel
            {
                IdClasse = string.Empty,
                Descrizione = "Nuova Classe",
                NuovaClasse = true,
                TotaleFotocopie = 0
            };
        }

        public ObservableCollection<ClassiModel> ReadElencoClassi()
        {
            var elencoClassi = new ObservableCollection<ClassiModel>();

            using (var dsLeonardo = new datasetLeonardo())
            {
                using (var daClassi = new ClassiTableAdapter())
                {
                    daClassi.Fill(dsLeonardo.Classi);

                    foreach (DataRow drClasse in dsLeonardo.Classi.Rows)
                    {
                        elencoClassi.Add(new ClassiModel
                        {
                            IdClasse = drClasse["IdClasse"].ToString(),
                            Descrizione = drClasse["Descrizione"].ToString(),
                            NuovaClasse = false
                        });
                    }
                }
            }

            return elencoClassi;
        }
    }
}
