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

        public static void MenuManipularConta()
        {
            Console.WriteLine("O que você precisa?\n");
            Console.WriteLine("1. Saldo da conta");
            Console.WriteLine("2. Detalhes da conta");
            Console.WriteLine("3. Realizar transações");
            Console.WriteLine("4. Sair da conta (Logout)\n");
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

            string senha = "0";
            string senha2 = "1";

            do
            {
                Console.Write("Digite uma nova senha: ");
                senha = EsconderSenha();
                Console.Write("Repita a nova senha: ");
                senha2 = EsconderSenha();
                
                if (senha != senha2)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("As senhas não conferem. Por favor, digite novamente.\n");
                    Console.ResetColor();
                }
            }
            while (senha != senha2);

            Console.ForegroundColor= ConsoleColor.Green;
            Console.WriteLine("Senha salva.");
            Console.ResetColor();
            
            senhas.Add(senha);

            Random numeroAleatorio = new Random();
            int conta = numeroAleatorio.Next(2500, 8750);
            contas.Add(conta);

            
            Console.ForegroundColor= ConsoleColor.Green;
            Console.WriteLine("\nConta nº {0} adicionada com sucesso.\n", conta);
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

        public static void LoginDeAcesso(List<string> cpfs, List<string> senhas)
        {
            Console.WriteLine("Login de Acesso");
            Console.WriteLine("...............\n");

            int indexParaLogar;
            int indexSenhaLogin;

            do
            {
                Console.Write("Digite o CPF: ");
                string cpfParaLogar = Console.ReadLine();
                indexParaLogar = cpfs.FindIndex(cpf => cpf == cpfParaLogar);
                Console.Write("Digite a senha: ");
                string senhaLogin = EsconderSenha();
                indexSenhaLogin = senhas.FindIndex(s => s == senhaLogin);

                if (indexParaLogar == -1 || indexSenhaLogin == -1)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\nNão foi possível acessar a conta.\n" +
                        "CPF ou senha incorretos. Por favor, tente novamente.\n");
                    Console.WriteLine();
                    Console.ResetColor();
                }
                                                                               
            }
            while (indexParaLogar != -1 && indexSenhaLogin != -1); //NÃO ESTÁ FAZENDO O CICLO CORRETAMENTE SE ERRA O CPF E/OU SENHA. CORRIGIR!
            
            
        }
                
        public static void ExcluirConta(List<string> cpfs, List<string> titulares, List<string> chavesPix, List<string> senhas)
        {
            Console.Write("Digite o CPF: ");
            string cpfParaDeletar = Console.ReadLine();
            int indexParaDeletar = cpfs.FindIndex(cpf => cpf == cpfParaDeletar);

            if (indexParaDeletar == -1)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Não foi possível deletar esta conta");
                Console.WriteLine("MOTIVO: Conta não encontrada.");
                Console.ResetColor();
            }

            cpfs.Remove(cpfParaDeletar);
            titulares.RemoveAt(indexParaDeletar);
            senhas.RemoveAt(indexParaDeletar);
            chavesPix.RemoveAt(indexParaDeletar);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nConta deletada com sucesso.\n");
            Console.ResetColor();
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
                        break;
                    case 2:
                        ListarTodasAsContas(contas, titulares, cpfs, chavesPix);    
                        break;
                    case 3:
                        LoginDeAcesso(cpfs, senhas);
                        MenuManipularConta();

                        break;
                    case 4:
                        ExcluirConta(cpfs, titulares, chavesPix, senhas);
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
