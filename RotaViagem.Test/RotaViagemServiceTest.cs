using RotaViagem.Service;
using RotaViagem.Service.Dtos;
using RotaViagem.Service.IService;

namespace RotaViagem.Test
{
    [TestClass]
    public class RotaViagemServiceTest
    {
        IRotaService rotaService = new RotaService();

        [TestInitialize]
        public void Setup()
        {
            rotaService = new RotaService();
        }

        [TestMethod]
        public void Teste_CalculaMenorCusto()
        {
            string descricaoLocalOrigem = "GRU";
            string descricaoLocalDestino = "CDG";

            var resultado = rotaService.GetCalculeRotaAsync(descricaoLocalOrigem, descricaoLocalDestino);
            Assert.IsTrue(resultado.IsCompleted);
        }


    }
}