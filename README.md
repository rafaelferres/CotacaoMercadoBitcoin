# Cotação Mercado Bitcoin
Esse projeto pega e escreve todas as cotações das criptomoedas do Mercado Bitcoin durante um tempo determinado (Exemplo ele retorna a cotação a cada 5 segundos).

# Como Utlizar ?
Para utilizar é simples, basta você dar start no projeto.
Caso queria alterar as configurações (Tempo do Loop ou a Moeda), para alterar o Tempo do Loop, você precisa somente colocar o tempo desejado em segundo na variavel TimeThread. Para alterar a moeda procura pela variavel req (var req = WebRequest.CreateHttp(CriptoCoin.BTC); inicialmente) e alterar de BTC para moeda desejada.

# Dll's utilizadas
- Newtonsoft.Json (Parse JSON)
