using APIEscola.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace APIEscola.Data
{
    public class APIEscolaContext : IdentityDbContext
    {
        //Construtor que recebe as opções de configuração do DbContext
        public APIEscolaContext(DbContextOptions<APIEscolaContext> options) : base(options)
        {
        }
        //Propriedade DbSet para cada tabela
        public DbSet<Aluno> Alunos { get; set; } //Tabela de alunos
        public DbSet<Curso> Cursos { get; set; } //Tabela de cursos
        public DbSet<Turma> Turmas { get; set; } //Tabela de cursos

        public DbSet<Matricula> Matriculas { get; set; } //Tabela de cursos

        //Sobrecarga do método OnModelCreating para configurar o modelo a partir da IdentityDbContext5
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Chama o método OnModelCreating da classe base para a criação das tabelas padrão
            base.OnModelCreating(modelBuilder);

            //Configurar a criação de tabelas adicionais aqui
            modelBuilder.Entity<Aluno>().ToTable("Alunos"); //Configura o nome da tabela Alunos
            modelBuilder.Entity<Curso>().ToTable("Cursos"); //Configura o nome da tabela Alunos
            modelBuilder.Entity<Turma>().ToTable("Turmas"); //Configura o nome da tabela Alunos
            modelBuilder.Entity<Matricula>().ToTable("Matriculas"); //Configura o nome da tabela Alunos
        }
    }
}


