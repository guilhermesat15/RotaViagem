using RotaViagem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotaViagem.Service.Dtos
{
    public class RotaAddDto
    {
        public int LocalOrigemId { get; set; }
        public int LocalDestinoId { get; set; }
        public int CustoViagem { get; set; }
    }
}
