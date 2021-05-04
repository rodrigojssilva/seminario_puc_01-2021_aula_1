using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ControleDeClientes.Models
{
    public class Vendedor
    {
        [Key]
        public int VendedorId { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public float Comissao { get; set; }

        public List<Venda> Vendas { get; set; }
    }
}
