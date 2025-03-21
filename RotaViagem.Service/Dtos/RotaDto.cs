using RotaViagem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotaViagem.Service.Dtos
{
    public class RotaDto
    {
        public int Id { get; set; }

        public int LocalOrigemId { get; set; }
        public LocalDto LocalOrigem { get; set; }

        public int LocalDestinoId { get; set; }
        public LocalDto LocalDestino { get; set; }

        public int CustoViagem { get; set; }
    }
}
