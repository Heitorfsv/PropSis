using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrototipoSistema.classes
{
    public class ConsultaCep
    {
            [JsonProperty(PropertyName = "cep")]
            public string CEP { get; set; }

            [JsonProperty(PropertyName = "logradouro")]
            public string rua { get; set; }

            [JsonProperty(PropertyName = "complemento")]
            public string complemento { get; set; }

            [JsonProperty(PropertyName = "bairro")]
            public string bairro { get; set; }

            [JsonProperty(PropertyName = "localidade")]
            public string cidade { get; set; }

            [JsonProperty(PropertyName = "uf")]
            public string UF { get; set; }
    }
}
