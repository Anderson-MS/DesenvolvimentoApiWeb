using System.ComponentModel.DataAnnotations;

namespace TesteAdecco.Models
{
    public class ClienteModel
    {        
        public int Id { get; set; }        
        public string Nome { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }                 
        public string Tipo { get; set; }
        public int DDD { get; set; }
        public string Telefone { get; set; }
        public string TipoEndereco { get; set; }
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Referencia { get; set; }
    }
}
