using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using GestioneCopie.Data.Datasets;
using GestioneCopie.Data.Datasets.datasetLeonardoTableAdapters;
using GestioneCopie.Data.Model;

using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace GestioneCopie.Business
{
    /// <summary>
    /// Questa classe si occupa della gestione della Entity Fotocopie.
    /// Tutte le operazioni CRUD (Create, Read, Update, Delete) devono necessariamente passare per questa classe
    /// </summary>
    public class BusinessFotocopie
    {
        private readonly DateTimeFormatInfo _dFormat = new CultureInfo("it-IT").DateTimeFormat;

        public ObservableCollection<FotocopieModel> ReadArchivioFotocopie()
        {
            var elencoFotocopie = new ObservableCollection<FotocopieModel>();

            using (var dsLeonardo = new datasetLeonardo())
            {
                using (var daFotocopie = new qryFotocopieTableAdapter())
                {
                    daFotocopie.Fill(dsLeonardo.qryFotocopie);

                    foreach (DataRow drFotocopia in dsLeonardo.qryFotocopie.Rows)
                    {
                        elencoFotocopie.Add(new FotocopieModel
                        {
                            IdDocente = int.Parse(drFotocopia["IdDocente"].ToString()),
                            NominativoDocente = string.Format("{0} {1}", drFotocopia["Cognome"], drFotocopia["Nome"]),
                            IdClasse = drFotocopia["IdClasse"].ToString(),
                            DescrizioneClasse = drFotocopia["Descrizione"].ToString(),
                            Sezione = drFotocopia["Sezione"].ToString(),
                            DataFotocopie = Convert.ToDateTime(drFotocopia["DataFotocopie"].ToString(), this._dFormat),
                            NumeroFotocopie = int.Parse(drFotocopia["NumeroFotocopie"].ToString()),
                            NuovaRegistrazione = false
                        });
                    }
                }
            }

            return elencoFotocopie;
        }

        public int GetTotaleFotocopie()
        {
            var totaleFotocopie = 0;

            using (var dsLeonardo = new datasetLeonardo())
            {
                using (var daFotocopie = new FotocopieTableAdapter())
                {
                    daFotocopie.Fill(dsLeonardo.Fotocopie);
                    totaleFotocopie += dsLeonardo.Fotocopie.Rows.Cast<DataRow>().Sum(drFotocopia => int.Parse(drFotocopia["NumeroFotocopie"].ToString()));
                }
            }

            return totaleFotocopie;
        }

        public ObservableCollection<FotocopieModel> ReadArchivioFotocopiPerDocente(DocentiModel currentDocente)
        {
            var elencoFotocopie = new ObservableCollection<FotocopieModel>();

            using (var dsLeonardo = new datasetLeonardo())
            {
                using (var daFotocopie = new qryFotocopieTableAdapter())
                {
                    daFotocopie.FillByDocente(dsLeonardo.qryFotocopie, currentDocente.IdDocente);

                    foreach (DataRow drFotocopia in dsLeonardo.qryFotocopie.Rows)
                    {
                        elencoFotocopie.Add(new FotocopieModel
                        {
                            IdDocente = int.Parse(drFotocopia["IdDocente"].ToString()),
                            NominativoDocente = string.Format("{0} {1}", currentDocente.Cognome, currentDocente.Nome),
                            IdClasse = drFotocopia["IdClasse"].ToString(),
                            DescrizioneClasse = drFotocopia["Descrizione"].ToString(),
                            Sezione = drFotocopia["Sezione"].ToString(),
                            DataFotocopie = Convert.ToDateTime(drFotocopia["DataFotocopie"].ToString(), this._dFormat),
                            NumeroFotocopie = int.Parse(drFotocopia["NumeroFotocopie"].ToString()),
                            NuovaRegistrazione = false
                        });
                    }
                }
            }

            return elencoFotocopie;
        }

        public ObservableCollection<FotocopieModel> ReadArchivioFotocopiPerClasse(ClassiModel currentClasse)
        {
            var elencoFotocopie = new ObservableCollection<FotocopieModel>();

            using (var dsLeonardo = new datasetLeonardo())
            {
                using (var daFotocopie = new qryFotocopieTableAdapter())
                {
                    daFotocopie.FillByClasse(dsLeonardo.qryFotocopie, currentClasse.IdClasse);

                    foreach (DataRow drFotocopia in dsLeonardo.qryFotocopie.Rows)
                    {
                        elencoFotocopie.Add(new FotocopieModel
                        {
                            IdDocente = int.Parse(drFotocopia["IdDocente"].ToString()),
                            NominativoDocente = string.Format("{0} {1}", drFotocopia["Cognome"], drFotocopia["Nome"]),
                            IdClasse = drFotocopia["IdClasse"].ToString(),
                            DescrizioneClasse = drFotocopia["Descrizione"].ToString(),
                            Sezione = drFotocopia["Sezione"].ToString(),
                            DataFotocopie = Convert.ToDateTime(drFotocopia["DataFotocopie"].ToString(), this._dFormat),
                            NumeroFotocopie = int.Parse(drFotocopia["NumeroFotocopie"].ToString()),
                            NuovaRegistrazione = false
                        });
                    }
                }
            }

            return elencoFotocopie;
        }

        public FotocopieModel NuovaRegistrazione()
        {
            return new FotocopieModel
            {
                DataFotocopie = DateTime.Now,
                NumeroFotocopie = 0,
                NuovaRegistrazione = true
            };
        }

        public int SalvaRegistrazione(FotocopieModel fotocopiaToSave)
        {
            var saveResult = -1;

            if (!fotocopiaToSave.NuovaRegistrazione) return saveResult;

            using (var dsLeonardo = new datasetLeonardo())
            {
                using (var daFotocopie = new FotocopieTableAdapter())
                {
                    DataRow drFotocopia = dsLeonardo.Fotocopie.NewRow();
                    drFotocopia["IdDocente"] = fotocopiaToSave.IdDocente;
                    drFotocopia["IdClasse"] = fotocopiaToSave.IdClasse;
                    drFotocopia["Sezione"] = fotocopiaToSave.Sezione;
                    drFotocopia["DataFotocopie"] = new DateTime(fotocopiaToSave.DataFotocopie.Year, fotocopiaToSave.DataFotocopie.Month, fotocopiaToSave.DataFotocopie.Day);
                    drFotocopia["NumeroFotocopie"] = fotocopiaToSave.NumeroFotocopie;

                    try
                    {
                        dsLeonardo.Fotocopie.Rows.Add(drFotocopia);
                        saveResult = daFotocopie.Update(dsLeonardo);
                    }
                    catch
                    {
                        saveResult = -99;
                    }
                }
            }

            return saveResult;
        }

        public int EliminaRegistrazione(FotocopieModel fotopiaToDelete)
        {
            var deleteResult = -1;

            using (var daFotocopie = new FotocopieTableAdapter())
            {
                deleteResult = daFotocopie.DeleteFotocopia(fotopiaToDelete.IdDocente, fotopiaToDelete.IdClasse, fotopiaToDelete.Sezione,
                    fotopiaToDelete.DataFotocopie);
            }

            return deleteResult;
        }

        public int EliminaTutteLeRegistrazioni()
        {
            var deleteResult = -1;

            using (var daFotocopie = new FotocopieTableAdapter())
            {
                deleteResult = daFotocopie.DeleteAll();
            }

            return deleteResult;
        }

        public string StampaElencoFotocopiePerDocente(DocentiModel currentDocente)
        {
            ObservableCollection<FotocopieModel> elencoFotocopie = this.ReadArchivioFotocopiPerDocente(currentDocente);

            // Acquisisco il percorso della Cartella Documenti dell'utente corrente
            var documentsFolder = string.Format("{0}\\SchedeProfessori", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            var pdfFileName = string.Format("ElencoFocopie_{0}.pdf", currentDocente.Cognome);

            // Verifico la presenza della Cartella "GestioneCopie", se necessario la creo
            if (!Directory.Exists(documentsFolder))
                Directory.CreateDirectory(documentsFolder);

            var pdfCatalog = new Document();
            pdfCatalog.AddCreationDate();
            pdfCatalog.AddAuthor("LeoCopy");
            pdfCatalog.AddCreator("LeoCopy");

            try
            {
                var nomeCompletoFile = string.Format("{0}\\{1}", documentsFolder, pdfFileName);

                // Creazione file PDF
                PdfWriter.GetInstance(pdfCatalog, new FileStream(nomeCompletoFile, FileMode.Create));
                pdfCatalog.Open();

                // Definizioe dei FONT
                var bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                var printDateFont = new Font(bfTimes, 8, Font.ITALIC, BaseColor.BLACK);
                var bodyFont = new Font(bfTimes, 7, Font.NORMAL, BaseColor.BLACK);
                var headerFont = new Font(bfTimes, 8, Font.BOLD, BaseColor.BLACK);
                var titleFont = new Font(bfTimes, 12, Font.NORMAL, BaseColor.BLACK);

                var dateTable = new PdfPTable(1)
                {
                    TotalWidth = 1000f
                };

                var dateCell =
                    new PdfPCell(new Phrase(string.Format("Stampato {0}", DateTime.Now.ToLongDateString()),
                        printDateFont))
                    {
                        HorizontalAlignment = 2,
                        Border = 0
                    };
                dateTable.AddCell(dateCell);

                // Righe di separazione
                var rowSpace = new PdfPCell(new Phrase(char.ConvertFromUtf32(20), printDateFont))
                {
                    Border = 0,
                    Colspan = 3
                };

                // Inserisco tre righe di separazione
                rowSpace.Border = 0;
                rowSpace.Colspan = 3;
                for (var i = 0; i < 2; i++)
                {
                    dateTable.AddCell(rowSpace);
                }

                var titleTable = new PdfPTable(1)
                {
                    TotalWidth = 1200f,
                    HorizontalAlignment = 0
                };

                PdfPCell titoloCella = null;
                titoloCella = new PdfPCell(new Phrase("Scheda Registrazione Fotocopie", titleFont))
                {
                    HorizontalAlignment = 1,
                    Border = 0
                };
                titleTable.AddCell(titoloCella);

                titoloCella = new PdfPCell(new Phrase(string.Format("Docente {0}", currentDocente.NomeVisualizzato), titleFont))
                {
                    HorizontalAlignment = 1,
                    Border = 0
                };
                titleTable.AddCell(titoloCella);

                for (var i = 0; i < 2; i++)
                {
                    titoloCella = new PdfPCell(new Phrase(char.ConvertFromUtf32(20), titleFont))
                    {
                        HorizontalAlignment = 0,
                        Border = 0
                    };
                    titleTable.AddCell(titoloCella);                    
                }

                // Intestazione Colonne
                var headerTable = new PdfPTable(4)
                {
                    TotalWidth = 1200f,
                    HorizontalAlignment = 0
                };
                var widths = new float[] { 3f, 3f, 3f, 3f };
                headerTable.SetWidths(widths);

                PdfPCell copiaCella = null;
                copiaCella = new PdfPCell(new Phrase("Data Richiesta", headerFont))
                {
                    HorizontalAlignment = 0,
                    Border = 0
                };
                headerTable.AddCell(copiaCella);

                copiaCella = new PdfPCell(new Phrase("Classe", headerFont))
                {
                    HorizontalAlignment = 0,
                    Border = 0
                };
                headerTable.AddCell(copiaCella);

                copiaCella = new PdfPCell(new Phrase("Sezione", headerFont))
                {
                    HorizontalAlignment = 0,
                    Border = 0
                };
                headerTable.AddCell(copiaCella);

                copiaCella = new PdfPCell(new Phrase("Numero Fotocopie", headerFont))
                {
                    HorizontalAlignment = 0,
                    Border = 1,
                };
                headerTable.AddCell(copiaCella);

                var totaleFocopie = 0;
                foreach (var fotocopia in elencoFotocopie.OrderByDescending(f => f.DataFotocopie))
                {
                    copiaCella = new PdfPCell(new Phrase(fotocopia.DataFotocopie.ToShortDateString(), bodyFont))
                    {
                        HorizontalAlignment = 0,
                        Border = 1,
                    };
                    headerTable.AddCell(copiaCella);

                    copiaCella = new PdfPCell(new Phrase(fotocopia.IdClasse, bodyFont))
                    {
                        HorizontalAlignment = 0,
                        Border = 1,
                    };
                    headerTable.AddCell(copiaCella);

                    copiaCella = new PdfPCell(new Phrase(fotocopia.Sezione, bodyFont))
                    {
                        HorizontalAlignment = 0,
                        Border = 1,
                    };
                    headerTable.AddCell(copiaCella);

                    copiaCella = new PdfPCell(new Phrase(fotocopia.NumeroFotocopie.ToString("D"), bodyFont))
                    {
                        HorizontalAlignment = 0,
                        Border = 1,
                    };
                    headerTable.AddCell(copiaCella);

                    totaleFocopie += fotocopia.NumeroFotocopie;
                }

                copiaCella = new PdfPCell(new Phrase("", bodyFont))
                {
                    HorizontalAlignment = 0,
                    Border = 1,
                };
                headerTable.AddCell(copiaCella);

                copiaCella = new PdfPCell(new Phrase("", bodyFont))
                {
                    HorizontalAlignment = 0,
                    Border = 1,
                };
                headerTable.AddCell(copiaCella);

                copiaCella = new PdfPCell(new Phrase("Totale", headerFont))
                {
                    HorizontalAlignment = 0,
                    Border = 1,
                };
                headerTable.AddCell(copiaCella);

                copiaCella = new PdfPCell(new Phrase(totaleFocopie.ToString("D"), headerFont))
                {
                    HorizontalAlignment = 0,
                    Border = 1,
                };
                headerTable.AddCell(copiaCella);

                pdfCatalog.Add(dateTable);
                pdfCatalog.Add(titleTable);
                pdfCatalog.Add(headerTable);
            }
            catch 
            {
            }
            finally
            {
                // Chiusura file
                pdfCatalog.Close();
            }

            pdfFileName = string.Format("{0}\\{1}", documentsFolder, pdfFileName);

            if (File.Exists(pdfFileName))
                Process.Start(pdfFileName);

            return pdfFileName;
        }
    }
}
