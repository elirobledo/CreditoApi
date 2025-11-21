using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreditoService.Api.Models
{
    [Table("credito")]
    public class Credito
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("numero_credito")]
        public string NumeroCredito { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("numero_nfse")]
        public string NumeroNfse { get; set; }

        [Required]
        [Column("data_constituicao")]
        public DateTime DataConstituicao { get; set; }

        [Required]
        [Column("valor_issqn", TypeName = "decimal(15,2)")]
        public decimal ValorIssqn { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("tipo_credito")]
        public string TipoCredito { get; set; }

        [Column("simples_nacional")]
        public bool SimplesNacional { get; set; }

        [Column("aliquota", TypeName = "decimal(5,2)")]
        public decimal Aliquota { get; set; }

        [Column("valor_faturado", TypeName = "decimal(15,2)")]
        public decimal ValorFaturado { get; set; }

        [Column("valor_deducao", TypeName = "decimal(15,2)")]
        public decimal ValorDeducao { get; set; }

        [Column("base_calculo", TypeName = "decimal(15,2)")]
        public decimal BaseCalculo { get; set; }
    }
}