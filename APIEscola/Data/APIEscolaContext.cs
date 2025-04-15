// Codigo do Arquivo 
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace APIEscola.Data
{
    public class APIEscolaContext : IdentityDbContext
    {
        public APIEscolaContext(DbContextOptions<APIEscolaContext> options) : base(options)
        {
        }

        //Propriedade DbSet para acessar as tabelas

        //Sobrecarga de método OnModelCreating para configurar o modelo a partir da IdentityDbContext
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //chamado o método OnModelCreating da classe base para criação das tabelas padões 
            base.OnModelCreating(modelBuilder);
            //Configurar a criação de tabelas adicionais aqui 
        }
    }
}


// =========================================================================================