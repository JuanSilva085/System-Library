using System;
using System.Collections.Generic;
using System.Linq;


public class Livro
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public bool Disponivel { get; set; }

    public Livro(int id, string titulo, string autor)
    {
        Id = id;
        Titulo = titulo;        
        Autor = autor;
        Disponivel = true;
    }

    public override string ToString()
    {
        return $"ID: {Id}, Título: {Titulo}, Autor: {Autor}, Disponível: {Disponivel}";
    }
}

    public class Usuario
    {
         public int Id { get; set; }
         public string Nome { get; set; }
         
    public List<int> LivrosEmprestados { get; set; }

    public Usuario(int id, string nome)
         {
            Id = id;
            Nome = nome;
            LivrosEmprestados = new List<int>();
         }
    }

public class Biblioteca
{
    private List<Livro> livros = new List<Livro>();
    private List<Usuario> usuarios = new List<Usuario>(); 

    public void AdicionarLivro(Livro livro)
    {
        livros.Add(livro);
        Console.WriteLine($"Livro '{livro.Titulo}' adicionado com sucesso!");
    }

    public void ListarLivros()
    {
        foreach (var livro in livros)
        {
            Console.WriteLine(livro);
        }
    }

    public List<Livro> ObterLivros()
    {
        return livros;
    }

    public void RegistrarUsuario(Usuario usuario)
    {
        usuarios.Add(usuario);
        Console.WriteLine($"Usuário '{usuario.Nome}' registrado com sucesso!");
    }

    public void EmprestarLivro(int livroId, int usuarioId)
    {
        var livro = livros.FirstOrDefault(I => I.Id == livroId);
        var usuario = usuarios.FirstOrDefault(u => u.Id == usuarioId);

        if(livro == null || usuario == null)
        {
            Console.WriteLine("Livro ou usuário não encontrado!");
            return;
        }
        if (!livro.Disponivel)
        {
            Console.WriteLine("Livro não está dosponível");
            return;
        }

        livro.Disponivel = false;
        usuario.LivrosEmprestados.Add(livroId);
        Console.WriteLine($"Livro '{livro.Titulo}' emprestado para '{usuario.Nome}'!");
    }

    public void DevolverLivro(int LivroId, int usuarioId)
    {
        var livro = livros.FirstOrDefault(I => I.Id == LivroId);
        var usuario = usuarios.FirstOrDefault(u => u.Id == usuarioId);

        if(livro == null || usuario == null || usuario.LivrosEmprestados.Contains(LivroId))
        {
            Console.WriteLine("Livro ou usuário não encontrado, ou o livro não foi encontrado.");
            return;
        }

        livro.Disponivel = true;
        usuario.LivrosEmprestados.Remove(LivroId);
        Console.WriteLine($"Livro: '{livro.Titulo}' devolvido por '{usuario.Nome}'!");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Biblioteca biblioteca = new Biblioteca();

        while (true) 
        {
            Console.WriteLine("\n=== Sistema de Biblioteca ===");
  
            Console.WriteLine("\n1. Adicionar Livros");
            Console.WriteLine("2. Listar Livros");
            Console.WriteLine("3. Registrar Usuário");
            Console.WriteLine("4. Emprestar Livro");
            Console.WriteLine("5. Devolver Livro");
            Console.WriteLine("6. Sair");
            Console.Write("Escolha uma opção: ");

            int opcao = int.Parse(Console.ReadLine());

            switch(opcao)
            {
                case 1:
                    Console.Write("ID do livro: USE APENAS NÚMEROS INTEIROS: ");
                    int idLivro = int.Parse(Console.ReadLine());
                    Console.Write("Título: ");
                    string titulo = Console.ReadLine();
                    Console.Write("Autor: ");
                    string autor = Console.ReadLine();
                    biblioteca.AdicionarLivro(new Livro(idLivro, titulo, autor));
                    break;

                case 2:
                    biblioteca.ListarLivros();
                    break;

                case 3:
                    Console.Write("ID do Usuário: ");
                    int idUsuario = int.Parse(Console.ReadLine());
                    Console.Write("Nome: ");
                    string nome = Console.ReadLine();
                    biblioteca.RegistrarUsuario(new Usuario(idUsuario, nome));
                    break;

                case 4:
                    Console.Write("ID do Livro para emprestar: USE APENAS NÚMEROS INTEIROS: ");
                    int idLivroEmprestar = int.Parse(Console.ReadLine());
                    Console.Write("ID do Usuário: ");
                    int idUsuarioEmprestar = int.Parse(Console.ReadLine());
                    biblioteca.EmprestarLivro(idLivroEmprestar, idUsuarioEmprestar); 
                    break;
                
                case 5:
                    Console.Write("ID do Livro para devolver: ");
                    int idLivroDevolver = int.Parse(Console.ReadLine());
                    Console.Write("ID do Usuário: ");
                    int idUsuarioDevolver = int.Parse(Console.ReadLine());
                    biblioteca.DevolverLivro(idLivroDevolver, idUsuarioDevolver);
                    break;

                case 6:
                    Console.WriteLine("Saindo do sistema...");
                    return;

                default:
                    Console.WriteLine("Opção inválida");
                    break;
            }
        }
    }
}