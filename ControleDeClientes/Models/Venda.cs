using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleDeClientes.Models
{
    public class Venda
    {
        [Key]
        public int VendaId { get; set; }

        [ForeignKey("ClienteId")]
        public Cliente Cliente { get; set; }
        public int ClienteId { get; set; }

        [ForeignKey("VendedorId")]
        public Vendedor Vendedor { get; set; }
        public int VendedorId { get; set; }

        public List<VendaItem> Itens { get; set; }
    }
}
