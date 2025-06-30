using System.Collections.Generic;

namespace PrototipoSistema
{
    internal class OsPdfDocument
    {
        public string Cliente { get; set; }
        public string Documento { get; set; }
        public string Telefone { get; set; }
        public string Placa { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Ano { get; set; }
        public string Km { get; set; }
        public object Observacao { get; set; }
        public object DtCadastro { get; set; }
        public object DtSaida { get; set; }
        public object Pago { get; set; }
        public decimal Total { get; set; }
        public decimal TotalPecas { get; set; }
        public decimal TotalServicos { get; set; }
        public List<(string, string, string)> Pecas { get; set; }
        public List<(string, string, string)> Servicos { get; set; }
    }
}