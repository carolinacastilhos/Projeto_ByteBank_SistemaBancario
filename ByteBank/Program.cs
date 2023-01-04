using System;
using System.Net.Mail;
using System.Reflection.Metadata.Ecma335;

namespace ByteBank
{
    public class Program
    {
        public static void MenuPrincipal()
        {
            Console.WriteLine("O que você precisa?\n");
            Console.WriteLine("1. Adicionar nova conta");
            Console.WriteLine("2. Listar todas as contas registradas");
            Console.WriteLine("3. Acessar/Manipular uma conta");
            Console.WriteLine("4. Excluir uma conta");
            Console.WriteLine("5. Finalizar programa\n");
            Console.Write("Digite a opção desejada: ");
            
        }


        public static void RegistrarNovoUsuario(List<string> titulares, List<string> cpfs, List<string> chavesPix, List<string> senhas, List<int> contas)
        {
            Console.Write("Digite o nome completo: ");
            titulares.Add(Console.ReadLine());
            Console.Write("Digite o CPF: ");
            cpfs.Add(Console.ReadLine());
            Console.Write("Adicione uma chave Pix (uma palavra ou email): ");
            chavesPix.Add(Console.ReadLine());
            Console.Write("Digite uma nova senha (4 a 6 caracteres): ");
            senhas.Add(Console.ReadLine());
            Console.WriteLine();
            //Console.Write("Repita a nova senha: ");
            //string entrada = Console.ReadLine();

            Random numeroAleatorio = new Random();
            int conta = numeroAleatorio.Next(2500, 8750);
            contas.Add(conta);

            Console.ForegroundColor= ConsoleColor.Green;
            Console.WriteLine("Conta nº {0} adicionada com sucesso.\n", conta);
            Console.ResetColor();
        }
        
        public static string EsconderSenha()
        {
            var pass = string.Empty;
            ConsoleKey key;

            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    Console.Write("\b \b");
                    pass = pass[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    pass += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);

            Console.WriteLine();
            return pass;
        }

        public static void ListarTodasAsContas(List<int> contas, List<string> titulares, List<string> cpfs, List<string> chavesPix)
        {
            for(int i = 0; i < cpfs.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;

                Console.WriteLine($"Conta: {contas[i]} | Titular: {titulares[i]} | CPF: {cpfs[i]} | Chave Pix: {chavesPix[i]}\n");
                Console.ResetColor();
                
            }
        }

        public static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Olá! Bem-Vindo(a) ao ByteBank!\n");
            Console.ResetColor();

            List<int> contas = new List<int>();
            List<string> titulares = new List<string>();
            List<string> cpfs = new List<string>();
            List<string> chavesPix = new List<string>();
            List<string> senhas = new List<string>();
            
            Console.WriteLine();

            int opcao;

            do
            {
                MenuPrincipal();
                opcao = int.Parse(Console.ReadLine());

                Console.WriteLine();
                Console.WriteLine(".............................");
                Console.WriteLine();

                switch (opcao)
                {
                    case 1:
                        RegistrarNovoUsuario(titulares, cpfs, chavesPix, senhas, contas);
                        //RegistrarSenhas(senhas);
                        break;
                    case 2:
                        ListarTodasAsContas(contas, titulares, cpfs, chavesPix);    
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        Console.WriteLine("Finalizando o programa... Até mais!");
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("Opção inválida. Por favor, tente novamente.");
                        Console.ResetColor();
                        break;


                }
                                
            }
            while (opcao != 5);
            



        }

        
    }
    
}
