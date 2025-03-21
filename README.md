# Rota de Viagem<br />
<br />
Escolha a rota de viagem mais barata independente da quantidade de conexões.<br />
Para isso precisamos inserir as rotas.<br />

Origem: GRU, Destino: BRC, Valor: 10<br />
Origem: BRC, Destino: SCL, Valor: 5<br />
Origem: GRU, Destino: CDG, Valor: 75<br />
Origem: GRU, Destino: SCL, Valor: 20<br />
Origem: GRU, Destino: ORL, Valor: 56<br />
Origem: ORL, Destino: CDG, Valor: 5<br />
Origem: SCL, Destino: ORL, Valor: 20<br />
<br />
# Explicando <br />
Uma viajem de **GRU** para **CDG** existem as seguintes rotas:<br />

1. GRU - BRC - SCL - ORL - CDG ao custo de $40<br />
2. GRU - ORL - CDG ao custo de $61<br />
3. GRU - CDG ao custo de $75<br />
4. GRU - SCL - ORL - CDG ao custo de $45<br />

O melhor preço é da rota **1**, apesar de mais conexões, seu valor final é menor.<br />
O resultado da consulta deve ser: **GRU - BRC - SCL - ORL - CDG ao custo de $40**.<br />



<b>Tecnologias e frameworks usados na solução:</b> <br />

ASP.NET Core and C# para p BackEnd versão net 6.0.36 <br />

## Instalação<br />

1. Clone o repositório:<br />

   <b> git clone https://github.com/guilhermesat15/RotaViagem.git </b><br />


## Executando a aplicação <br />

1.Navegue até o outro diretório RotaViagem.API <br />  <br />
- Execute no <b>prompt de comando</b> <br /><br />
<b>cd RotaViagem.API </b> <br /><br />
<b>dotnet run </b> <br /><br /><br />
Aplicação api poderá ser acessada em (https://localhost:44327/swagger/index.html)<br /><br />


## Endpoint. <br />

1 - Cadastre o <b>Local</b><br />
Ex.: api/Local<br />
{<br />
    "nome": "GRU"<br />
}<br />
<br />
{<br />
    "nome": "BRC"<br />
}<br />
<br />
2 - Cadastre a <b>Rota</b><br />
Ex.: api/Rota<br />
{
    "localOrigemId": 1,<br />
    "localDestinoId": 2,<br />
    "custoViagem": 10<br />
}<br />
<br />
3 - Consulta a rota mais barata<br />
ex.: /api/Rota/CalculoRota/{nomeLocalOrigem}/{nomeLocalDestino}<br />
nomeLocalOrigem : "GRU"<br />
nomeLocalDestino : "BRC"<br />


