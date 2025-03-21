using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotaViagem.Domain
{
    public class Rota
    {
        public int Id { get; set; }

        public int LocalOrigemId { get; set; }
        public Local LocalOrigem { get; set; }

        public int LocalDestinoId { get; set; }
        public Local LocalDestino { get; set; }
        
        public int CustoViagem { get; set; }
    }
}
