using System.ComponentModel.DataAnnotations.Schema;

namespace CadastroApi.Models
{
    public class User
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string Name { get; set; }

        [Column(TypeName = "varchar(200)")]
        public string LastName { get; set; }

        [Column(TypeName = "bigint")]
        public long Phone { get; set; }
    }
}
