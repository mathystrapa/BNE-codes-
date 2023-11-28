// esse programa é um desafio de lógica de programação e um treino de sintaxe de C#

using System;

namespace PrimeNumber
{
    class Program
    {
        static void Main()  // método chamado ao iniciar o programa
        {
            int integer;    // o inteiro que será testado se é primo ou não
            string answer;  // resposta para saber se o usuário quer fechar o programa ou não
            bool run = true;    // booleano para manter o programa rodando ou não
            while (run == true)
            {
                while (true)
                {
                    Console.WriteLine("\nInsira um número inteiro para verificar se ele é primo ou não:");
                    string user_input = Console.ReadLine();
                    if (!(int.TryParse(user_input, out integer)))   // tenta converter o número em uma string
                    {
                        Console.WriteLine("\nO número inserido não é inteiro. Tente novamente.");
                    }
                    else
                    {
                        if (integer == 1)
                        {
                            Console.WriteLine("\nNão há um consenso matemático para a natureza do número 1. Alguns matemáticos o consideram primo e outros não. Insira outro número.");
                        }
                        else
                        {
                            if (integer < 1)
                            {
                                Console.WriteLine("\nInsira um número maior que 0.");
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }

                int[] integer_dividers = check_prime_number(integer);   // chama o método check_prime_numbers que retorna uma lista de divisores do parâmetro
                if (integer_dividers.Length == 0)   // se não houver divisores
                {
                    Console.WriteLine("\nO número {0} é primo (não possui divisores diferentes de 1 e {1}).", integer, integer);
                }
                else
                {
                    if (integer_dividers.Length == 1)   // se houver apenas 1 divisor (a mensagem é mostrada no singular)
                    {
                        Console.WriteLine("\nO número {0} é composto (não é primo) pois possui 1 divisor: ", integer);
                    }
                    else    // se houver mais de 1 divisor (a mensagem é mostrada no plural)
                    {
                        Console.WriteLine("\nO número {0} é composto (não é primo) pois possui {1} divisores: ", integer, integer_dividers.Length);
                    }

                    for (int cont = 0; cont < integer_dividers.Length; cont++)
                    {
                        if (cont == integer_dividers.Length - 1)    // se for o último divisor da lista
                        {
                            Console.Write(integer_dividers[cont]);
                        }
                        else
                        {
                            Console.Write(integer_dividers[cont] + ", ");
                        }
                    }
                }

                while (true)    // loop de repetição que só fecha após o usuário dar uma resposta válida
                {
                    Console.WriteLine("\nDeseja fechar o programa? ('Y' para sim e 'N' para não.)");
                    answer = Console.ReadLine();

                    if (answer == "Y" || answer == "y")
                    {
                        run = false;    // nega a condição do primeiro while (o que mantém o programa rodando para sempre)
                        break;  // sai do loop pois a resposta é valida
                    }
                    else
                    {
                        if (answer == "N" || answer == "n")
                        {
                            break;  // sai do loop pois a resposta é valida mas não nega a condição do primeiro while pois o usuário não quer fechar o programa
                        }
                        else    // caso o usuário não digite uma resposta válida
                        {
                            Console.WriteLine("\nPor favor, responda com 'Y' para sim e 'N' para não.");
                        }
                    }
                }
            }
        }

        static int[] check_prime_number(int number)     // método que recebe um número inteiro e retorna uma lista de divisores inteiros
        {
            int[] dividers = new int[number / 2];     // cria um array de divisores com um tamanho igual a metade do número (pois um número 'x' tem, no máximo, x/2 divisores
            int divisores = 0;  // contador de divisores
            for (int cont = 2; cont <= number / 2; cont++)  // começa a verificar o resto deixado na divisão do número por cada inteiro começando em 2 e indo até a metade do próprio número
            {
                if (number % cont == 0)
                {
                    dividers[divisores] = cont;
                    divisores++;
                }
            }

            if (divisores == 0)     // se não tem divisores, retorna um array vazio (o número é primo)
            {
                return [];
            }
            else
            {
                int not_0 = 0;  // contador de valores não nulos (pois o array 'dividers' foi criado com um tamanho fixo e pode deixar valores nulos)
                foreach (int value in dividers)
                {
                    if (value != 0)
                    {
                        not_0++;
                    }
                }

                int[] clean_dividers = new int[not_0];  // cria um array com o tamanho necessário para armazenar somente os valores diferentes de 0
                for (int i = 0; i < not_0; i++)
                {
                    clean_dividers[i] = dividers[i];
                }

                return clean_dividers;
            }
        }
    }
}
