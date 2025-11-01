using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace reservas_de_salas.Models
{
    [Table("Salas")]
    public class Sala
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required(ErrorMessage ="O nome da sala é obrigatoria")]
        [StringLength(100, ErrorMessage ="O nome da sala deve ter no máximo 100 caracteres")]
        public string Nome { get; set; }
        [Required(ErrorMessage ="A capacidade da sala é obrigatoria")]
        [Range(1, int.MaxValue, ErrorMessage ="A capacidade da sala deve ser entre 1 e 1000")]
        public int Capacidade { get; set; }
        [StringLength(500, ErrorMessage ="Os recursos da sala devem ter no máximo 500 caracteres")]
        public string Recursos { get; set; }
    }
}
