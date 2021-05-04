using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleDeClientes.Models
{
    public class VendaItem
    {
        [Key]
        public int VendaItemId { get; set; }


        [ForeignKey("VendaId")]
        public Venda Venda { get; set; }
        public int VendaId { get; set; }

        [ForeignKey("ProdutoId")]
        public Produto Produto { get; set; }
        public int ProdutoId { get; set; }

        [Required]
        public int Quantidade { get; set; }
        
        [Required]
        public double Valor { get; set; }
        
        [Required]
        public double Total { get; set; }
    }
}
