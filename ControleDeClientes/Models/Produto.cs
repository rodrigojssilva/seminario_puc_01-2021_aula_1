using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ControleDeClientes.Models
{
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }

        [Required]
        public string Descricao { get; set; }

        [DefaultValue(0)]
        public float Quantidade { get; set; }

        [DefaultValue(0)]
        public double Valor { get; set; }
    }
}
