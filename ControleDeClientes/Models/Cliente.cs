﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ControleDeClientes.Models
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        [MaxLength(14)]
        public string Documento { get; set; }

        public List<Venda> Vendas { get; set; }
    }
}
