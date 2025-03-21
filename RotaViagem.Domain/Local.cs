using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotaViagem.Domain
{
    public class Local
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public IEnumerable<Rota> RotaOrigems { get; set; }
        public IEnumerable<Rota> RotaDestinos { get; set; }
    }
}
