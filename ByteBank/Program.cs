using System;
using System.Net.Mail;

namespace ByteBank
{
    public class Program
    {

        static void showMenu()
        {
            Console.WriteLine("O que você precisa?");
            Console.WriteLine("1. Adicionar nova conta/usuário");
            Console.WriteLine("2. Listar todas as contas registradas");
            Console.WriteLine("3. Acessar uma conta");
            Console.WriteLine("4. Excluir uma conta");
            Console.WriteLine("0. Finalizar programa\n");
            Console.Write("Digite a opção desejada: ");
            
        }

        static void RegistrarNovoUsuario(List<string> titulares, List<string> cpfs, List<string> chavesPix)
        {
            Console.Write("Digite o nome completo: ");
            titulares.Add(Console.ReadLine());
            Console.Write("Digite o CPF: ");
            cpfs.Add(Console.ReadLine());
            Console.Write("Adicione uma chave Pix (uma palavra ou email): ");
            chavesPix.Add(Console.ReadLine());
            Console.WriteLine();
            
        }
        private static void RegistrarSenhas()
        {
            
        }
        static void ListarTodasAsContas(List<string> titulares, List<string> cpfs, List<string> chavesPix)
        {
            for(int i = 0; i < cpfs.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"Titular: {titulares[i]} | CPF: {cpfs[i]} | Chave Pix: {chavesPix[i]}\n");
                Console.ResetColor();
                
            }
        }
        public static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Olá! Bem-Vindo(a) ao ByteBank!\n");
            Console.ResetColor();
            
            List<string> titulares = new List<string>();
            List<string> cpfs = new List<string>();
            List<string> chavesPix = new List<string>();

            string[] senhas = new string[2];
            
            Console.WriteLine() ;

            int opcao;

            do
            {
                showMenu();
                opcao = int.Parse(Console.ReadLine());

                Console.WriteLine();
                Console.WriteLine(".............................");
                Console.WriteLine();

                switch (opcao)
                {
                    case 0: 
                        Console.WriteLine("Finalizando o programa... Até mais!");
                        break;
                    case 1:
                        RegistrarNovoUsuario(titulares, cpfs, chavesPix);
                        RegistrarSenhas(senhas);
                        break;
                    case 2:
                        ListarTodasAsContas(titulares, cpfs, chavesPix);    
                        break;
                    


                }
                                
            }
            while (opcao != 0);
            



        }

        
    }
    
}
