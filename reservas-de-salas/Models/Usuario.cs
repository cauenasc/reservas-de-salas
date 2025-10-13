using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace reservas_de_salas.Models
{
    [Table("Usuarios")]
    [Index(nameof(Email), IsUnique = true)]
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail não é válido.")]
        [MaxLength(100, ErrorMessage = "O e-mail não pode ter mais de 100 caracteres.")]
        public string Email { get; set; }
        [Required]
        public bool Administrador { get; set; }
    }
}
