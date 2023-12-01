// Este é meu primeiro programa em C#, no qual farei algumas anotações básicas e farei um teste pedindo ao usuário inserir nome e idade pelo teclado e depois mostrar essas informações na tela.

using System;   // Dizemos ao computador que utilizaremos o namespace 'System'

namespace myfirstnamespace  // Criamos outro namespace chamado 'myfirstnamespace'
{
    class Program   // Criamos uma classe chamada 'Program'
    {
        static void Main()  // Criamos o método principal chamado 'Main', o qual é chamado ao iniciar o programa
        {
            string[] info = GetInfo();  // Criamos um string array chamado info e o atribuímos ao método estático GetInfo
            DisplayWelcomeMessage(info);    // Chama o método estático sem retorno DisplayWelcomeMessage
            Console.ReadKey();
        }

        static string[] GetInfo()   // Cria o método GetInfo estático que retorna um string array
        {
            Console.WriteLine("Insira seu Nome: "); // Pede ao usuário seu nome
            string name = Console.ReadLine();   // Recebe um valor pelo teclado e o atribui à variável name do tipo string
            Console.WriteLine("Insira sua idade: ");    // Pede ao usuário sua idade
            string age = Console.ReadLine();    // Recebe um valor pelo teclado e o atribui à variável age do tipo string
            string[] info = { name, age };  // Cria o string array com as informações
            return info;    // Retorna as informações como um string array
        }

        static void DisplayWelcomeMessage(string[] info)    // Cria um método estático e sem retorno chamado DisplayWelcomeMessage que recebe um string array como parâmetro
        {
            string name = info[0];  // Acessa o primeiro valor do string array info e o armazena na variável name do tipo string
            string age = info[1];   // Acessa o segundo valor do string array info e o armazena na variável age do tipo string
            Console.Write("Bem vindo, " + name + ". Sua idade é " + age);   // Escreve as informações na tela usando concatenação de strings
        }
    }
}
