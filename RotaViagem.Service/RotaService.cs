using AutoMapper;
using RotaViagem.Domain;
using RotaViagem.Repository.IRepository;
using RotaViagem.Service.Dtos;
using RotaViagem.Service.Helpers;
using RotaViagem.Service.IService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AutoMapper.Internal.ExpressionFactory;

namespace RotaViagem.Service
{
    public class RotaService : IRotaService
    {
        private readonly IRotaRepository _rotaRepository;
        private readonly ILocalRepository _localRepository;
        private readonly IMapper _mapper;
        public RotaService(IRotaRepository rotaRepository, 
                                  ILocalRepository localRepository,
                                  IMapper mapper)
        {
            _rotaRepository = rotaRepository;
            _localRepository = localRepository;
            _mapper = mapper;
        }

        public RotaService()
        {
        }

        public async Task<RotaDto> Add(RotaAddDto model)
        {
            try
            {
                var rota = _mapper.Map<Rota>(model);
                _rotaRepository.Add<Rota>(rota);

                if (await _rotaRepository.SaveChangesAsync())
                {
                    return _mapper.Map<RotaDto>(rota);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RotaDto> Update(RotaUpdateDto model)
        {
            try
            {
                var rota = await _rotaRepository.GetByIdAsync(model.Id);
                if (rota == null) return null;

                model.Id = rota.Id;

                _mapper.Map(model, rota);

                _rotaRepository.Update<Rota>(rota);

                if (await _rotaRepository.SaveChangesAsync())
                {
                    return _mapper.Map<RotaDto>(rota);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var rota = await _rotaRepository.GetByIdAsync(id);
                if (rota == null) throw new Exception("Lote para delete não encontrado.");

                _rotaRepository.Delete<Rota>(rota);
                return await _rotaRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IList<RotaDto>> GetAllAsync()
        {
            try
            {
                var rotas = await _rotaRepository.GetAllAsync();
                if (rotas == null) return null;

                return _mapper.Map<IList<RotaDto>>(rotas);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RotaDto> GetByIdAsync(int id)
        {
            try
            {
                var rota = await _rotaRepository.GetByIdAsync(id);
                if (rota == null) return null;

                return _mapper.Map<RotaDto>(rota);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RotaResultadoDto> GetCalculeRotaAsync(string nomeLocalOrigem, string nomeLocalDestino)
        {
            try
            {
                Grafo g = new Grafo(true);

                ArrayList caminho = new ArrayList();
                Vertice origem = g.get_vertice(nomeLocalOrigem);
                Vertice vertice_alvo = g.get_vertice(nomeLocalDestino);
                RotaResultadoDto rotaResultadoDto = new RotaResultadoDto();

                var localOrigem = await _localRepository.GetByNomeAsync(nomeLocalOrigem);
                if (localOrigem == null)
                {
                    rotaResultadoDto.Resultado = "O local da origem informado não foi registrado!";
                    rotaResultadoDto.Correto = false;
                    return rotaResultadoDto;
                }

                var localDestino = await _localRepository.GetByNomeAsync(nomeLocalDestino);
                if (localDestino == null)
                {
                    rotaResultadoDto.Resultado = "O local do destino informado não foi registrado!";
                    rotaResultadoDto.Correto = false;
                    return rotaResultadoDto;
                }

                var locais = await _localRepository.GetAllAsync();
                if (locais == null) return null;

                IList<LocalDto> localDTOs = _mapper.Map<IList<LocalDto>>(locais);
                foreach (var localDTO in localDTOs)
                {
                    g.inserir_vertice(localDTO.Nome);
                }

                var rotas = await _rotaRepository.GetAllAsync();
                if (rotas == null) return null;

                IList<RotaDto> rotasDTO = _mapper.Map<IList<RotaDto>>(rotas);
                foreach (var rotaDto in rotasDTO)
                {
                    g.inserir_aresta(rotaDto.LocalOrigem.Nome, rotaDto.LocalDestino.Nome, Convert.ToInt32(rotaDto.CustoViagem));
                }

                caminho = new ArrayList();
                origem = g.get_vertice(nomeLocalOrigem);
                vertice_alvo = g.get_vertice(nomeLocalDestino);

                Dijkstra(g, origem);

                calcula_caminho(vertice_alvo, caminho);
                StringBuilder sb = new StringBuilder();
                if (caminho.Count > 0)
                {
                    sb.AppendFormat("**");
                    for (int i = (caminho.Count - 1); i >= 0; i--)
                    {
                        Vertice verticeAux = (Vertice)caminho[i];
                        sb.AppendFormat(" - {0}", verticeAux.get_id());
                    }

                    sb.AppendFormat(" ao custo de ${0} **", vertice_alvo.get_distancia()).Replace("** - ", "** ");
                }

                rotaResultadoDto.Resultado = sb.ToString();
                rotaResultadoDto.Correto = true;

                return rotaResultadoDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        static void initialize_single_source(Grafo g, Vertice s)
        {
            foreach (KeyValuePair<string, Vertice> v in g.get_vertices())
            {
                v.Value.set_distancia(int.MaxValue);
            }
            s.set_distancia(0);
        }

        static Vertice extract_min(SortedDictionary<string, Vertice> Q)
        {
            var key = Q.Keys.ToList()[0];
            Vertice min = Q[key];
            foreach (KeyValuePair<string, Vertice> v in Q)
            {
                if (v.Value.get_distancia() < min.get_distancia())
                {
                    min = v.Value;
                }
            }
            Q.Remove(min.get_id());
            return min;
        }

        static void relax(Vertice u, Vertice v)
        {

            int distancia = u.get_distancia() + u.get_peso(v);

            if (v.get_distancia() > distancia)
            {
                v.set_distancia(distancia);
                v.set_vertice_caminho_anterior(u);
                //Console.WriteLine("Atualizei a distância " + distancia + " do vértice " + u.get_id()  + " para o vértice " + v.get_id());
            }
            else
            {
                //Console.WriteLine("NÃO atualizei a distância " + distancia + " do vértice " + u.get_id() + " para o vértice " + v.get_id());
            }
        }

        static void add_S(Vertice u, SortedDictionary<string, Vertice> S)
        {
            Vertice vertice;
            if (S.TryGetValue(u.get_id(), out vertice))
            {
                vertice = u;
            }
            else
            {
                S.Add(u.get_id(), u);
            }
        }

        static void calcula_caminho(Vertice alvo, ArrayList caminho)
        {
            if (caminho.Count == 0)
            {
                caminho.Add(alvo);
            }
            while (alvo.get_vertice_caminho_anterior() != null)
            {
                caminho.Add(alvo.get_vertice_caminho_anterior());
                alvo = alvo.get_vertice_caminho_anterior();
            }
        }

        static void imprime_caminho(ArrayList caminho, Vertice origem, Vertice vertice_alvo)
        {
            Console.Write("O custo caminho do ");
            for (int i = caminho.Count - 1; i >= 0; i--)
            {
                Vertice v = (Vertice)caminho[i];
                Console.Write(v.get_id() + ", ");
            }
            Console.Write(" é " + vertice_alvo.get_distancia() + ".");
            Console.WriteLine("");
        }

        public static void Dijkstra(Grafo g, Vertice s)
        {
            SortedDictionary<string, Vertice> Q = new SortedDictionary<string, Vertice>();

            initialize_single_source(g, s);

            Q = g.get_vertices();

            SortedDictionary<string, Vertice> S = new SortedDictionary<string, Vertice>();

            while (Q.Count > 0)
            {

                Vertice u = extract_min(Q);

                u.set_visitado(true);

                foreach (KeyValuePair<Vertice, int> v in u.get_adjacentes())
                {
                    if (v.Key.get_visitado() == true)
                    {
                        continue;
                    }
                    relax(u, v.Key);
                }
                add_S(u, S);
            }

            /* S tem os pesos finais de caminho mínimos a partir da fonte determinada, assim atualiza o grafo 
             * com os vértices atualizados*/
            g.set_vertices(S);
        }

        public static bool BellmanFord(Grafo g, Vertice s)
        {

            initialize_single_source(g, s);

            foreach (KeyValuePair<string, Vertice> u in g.get_vertices())
            {
                foreach (Tuple<Vertice, Vertice> a in g.get_arestas())
                {
                    relax(a.Item1, a.Item2);
                }
            }

            foreach (Tuple<Vertice, Vertice> a in g.get_arestas())
            {
                if (a.Item2.get_distancia() > a.Item1.get_distancia() + a.Item1.get_peso(a.Item2))
                {
                    return false;
                }
            }

            return true;
        }

       
    }
}
