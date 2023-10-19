using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BW_Team_5_ClinicaVeterinaria.Models.Classi
{
    public class ProdottoCarrello
    {
        public int IdProdotti { get; set; }


        public string Nome { get; set; }

        public string Descrizione { get; set; }

        public int QuantitaDisponibile { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal PrezzoUnitario { get; set; }

        public int IdFornitori { get; set; }

        public int IdCategoria { get; set; }


        public int IdCassetti { get; set; }

        public int QuantitaAcquistata { get; set; }

        public ProdottoCarrello() { }

        public ProdottoCarrello (int idProdotti, string nome, string descrizione, int quantitaDisponibile, decimal prezzoUnitario, int idFornitori, int idCategoria, int idCassetti, int quantitaAcquistata)
        {
            IdProdotti = idProdotti;
            Nome = nome;
            Descrizione = descrizione;
            QuantitaDisponibile = quantitaDisponibile;
            PrezzoUnitario = prezzoUnitario;
            IdFornitori = idFornitori;
            IdCategoria = idCategoria;
            IdCassetti = idCassetti;
            QuantitaAcquistata = quantitaAcquistata;
        }
    }
}