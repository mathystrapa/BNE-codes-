// PROBLEMA 1:	Crie uma Tabela que calcule pela fórmula o Rendimento de um Investimento

using System;

namespace InvestmentNamespace
{
    class Program
    {
        static void Main()
        {
            
        }
    }

    class Investment
    {
        public float Montante;
        public float Lucro;
        public float Lucro_porcentagem;

        public Investment(float capital_inicial, float taxa, float tempo, int tipo)
        {
            Montante = capital_inicial * ((1 + taxa) ^ tempo);
            Lucro = Montante - capital_inicial;]
            Lucro_porcentagem = Lucro / capital_inicial
        }
    }
}