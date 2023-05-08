using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetMaps.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        [Column("Id")]
        [DisplayName("Código")]
        public Guid Id { get; set; }

        [Column("Nome")]
        [DisplayName("Nome")]
        public string Nome { get; set; }
    }
}
