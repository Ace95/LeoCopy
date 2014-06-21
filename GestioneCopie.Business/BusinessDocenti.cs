using System;
using System.Collections.ObjectModel;
using System.Data;

using GestioneCopie.Data.Datasets;
using GestioneCopie.Data.Datasets.datasetLeonardoTableAdapters;
using GestioneCopie.Data.Model;

namespace GestioneCopie.Business
{
    /// <summary>
    /// Questa classe si occupa della gestione della Entity Docenti.
    /// Tutte le operazioni CRUD (Create, Read, Update, Delete) devono necessariamente passare per questa classe
    /// </summary>
    public class BusinessDocenti
    {
        public DocentiModel CreaDocente()
        {
            return new DocentiModel
            {
                Nome = "Nuovo Docente",
                NomeVisualizzato = "Nuovo Docente"
            };
        }

        public ObservableCollection<DocentiModel> ReadElencoDocenti()
        {
            var elencoDocenti = new ObservableCollection<DocentiModel>();

            try
            {
                using (var dsLeonardo = new datasetLeonardo())
                {
                    using (var daDocenti = new DocentiTableAdapter())
                    {
                        daDocenti.Fill(dsLeonardo.Docenti);

                        foreach (DataRow drDocente in dsLeonardo.Docenti.Rows)
                        {
                            elencoDocenti.Add(new DocentiModel
                            {
                                IdDocente = int.Parse(drDocente["IdDocente"].ToString()),
                                Nome = drDocente["Nome"].ToString(),
                                Cognome = drDocente["Cognome"].ToString(),
                                NomeVisualizzato = string.Format("{0} {1}", drDocente["Cognome"], drDocente["Nome"])
                            });
                        }
                    }
                }
            }
            catch
            {
                elencoDocenti.Add(new DocentiModel
                {
                    IdDocente = 1,
                    Nome = "Errore",
                    Cognome = "Lettura DB",
                    NomeVisualizzato = "Errore Lettura Database"
                });
            }
            
            return elencoDocenti;
        }

        public int SaveDocente(DocentiModel docenteToSave)
        {
            var saveResult = -1;

            using (var dsLeonardo = new datasetLeonardo())
            {
                using (var daDocenti = new DocentiTableAdapter())
                {
                    DataRow drDocente;
                    if (docenteToSave.IdDocente == 0)
                    {
                        drDocente = dsLeonardo.Docenti.NewRow();
                        drDocente["Nome"] = docenteToSave.Nome;
                        drDocente["Cognome"] = docenteToSave.Cognome;
                        dsLeonardo.Docenti.Rows.Add(drDocente);
                    }
                    else
                    {
                        if (daDocenti.FillByIdDocente(dsLeonardo.Docenti, docenteToSave.IdDocente) == 1)
                        {
                            drDocente = dsLeonardo.Docenti.Rows[0];
                            drDocente["Nome"] = docenteToSave.Nome;
                            drDocente["Cognome"] = docenteToSave.Cognome;
                        }
                    }

                    saveResult = daDocenti.Update(dsLeonardo);
                }
            }

            return saveResult;
        }

        public int DeleteDocente(DocentiModel docenteToDelete)
        {
            var deleteResult = -1;

            using (var daDocenti = new DocentiTableAdapter())
            {
                deleteResult = daDocenti.DeleteDocente(docenteToDelete.IdDocente);
            }

            return deleteResult;
        }
    }
}
