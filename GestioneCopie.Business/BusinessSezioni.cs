using System;
using System.Collections.ObjectModel;
using System.Data;

using GestioneCopie.Data.Datasets;
using GestioneCopie.Data.Datasets.datasetLeonardoTableAdapters;
using GestioneCopie.Data.Model;

namespace GestioneCopie.Business
{
    public class BusinessSezioni
    {
        public SezioniModel CreateSezione()
        {
            return new SezioniModel
            {
                CodiceSezione = string.Empty,
                NuovaSezione = true
            };
        }

        public ObservableCollection<SezioniModel> ReadElencoSezioni()
        {
            var elencoSezioni = new ObservableCollection<SezioniModel>();

            using (var dsLeonardo = new datasetLeonardo())
            {
                using (var daSezioni = new SezioniTableAdapter())
                {
                    daSezioni.Fill(dsLeonardo.Sezioni);

                    foreach (DataRow drSezione in dsLeonardo.Sezioni.Rows)
                    {
                        elencoSezioni.Add(new SezioniModel
                        {
                            CodiceSezione = drSezione["CodiceSezione"].ToString(),
                            NuovaSezione = false
                        });
                    }
                }
            }

            return elencoSezioni;
        }

        public int SaveSezione(SezioniModel sezioneToSave)
        {
            var saveResult = -1;

            using (var dsLeonardo = new datasetLeonardo())
            {
                using (var daSezioni = new SezioniTableAdapter())
                {
                    DataRow drSezione = null;
                    try
                    {
                        if (daSezioni.FillByCodiceSezione(dsLeonardo.Sezioni, sezioneToSave.CodiceSezione) > 0)
                            drSezione = dsLeonardo.Sezioni.Rows[0];
                    }
                    catch
                    {
                        drSezione = dsLeonardo.Sezioni.NewRow();
                    }

                    if (drSezione == null) return saveResult;

                    drSezione["CodiceSezione"] = sezioneToSave.CodiceSezione;
                    saveResult = daSezioni.Update(dsLeonardo);
                }
            }

            return saveResult;
        }
    }
}
