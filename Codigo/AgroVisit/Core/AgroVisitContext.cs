using Microsoft.EntityFrameworkCore;

namespace Core;

public partial class AgroVisitContext : DbContext
{
    public AgroVisitContext()
    {
    }

    public AgroVisitContext(DbContextOptions<AgroVisitContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Assinatura> Assinaturas { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Contum> Conta { get; set; }

    public virtual DbSet<Cultura> Culturas { get; set; }

    public virtual DbSet<Engenheiroagronomo> Engenheiroagronomos { get; set; }

    public virtual DbSet<Intervencao> Intervencaos { get; set; }

    public virtual DbSet<Plano> Planos { get; set; }

    public virtual DbSet<Projeto> Projetos { get; set; }

    public virtual DbSet<Propriedade> Propriedades { get; set; }

    public virtual DbSet<Solo> Solos { get; set; }

    public virtual DbSet<Visitum> Visita { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=123456;database=AgroVisit");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Assinatura>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("assinatura");

            entity.HasIndex(e => e.IdPlano, "fkAssinaturaPlano1_idx");

            entity.HasIndex(e => e.IdEngenheiroAgronomo, "fkAssinaturaUsuario1_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Data)
                .HasColumnType("date")
                .HasColumnName("data");
            entity.Property(e => e.DataCancelamento)
                .HasColumnType("date")
                .HasColumnName("dataCancelamento");
            entity.Property(e => e.IdEngenheiroAgronomo).HasColumnName("idEngenheiroAgronomo");
            entity.Property(e => e.IdPlano).HasColumnName("idPlano");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'A'")
                .HasColumnType("enum('A','C')")
                .HasColumnName("status");
            entity.Property(e => e.Valor).HasColumnName("valor");

            entity.HasOne(d => d.IdEngenheiroAgronomoNavigation).WithMany(p => p.Assinaturas)
                .HasForeignKey(d => d.IdEngenheiroAgronomo)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fkAssinaturaUsuario1");

            entity.HasOne(d => d.IdPlanoNavigation).WithMany(p => p.Assinaturas)
                .HasForeignKey(d => d.IdPlano)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fkAssinaturaPlano1");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("cliente");

            entity.HasIndex(e => e.Cpf, "CPF_UNIQUE").IsUnique();

            entity.HasIndex(e => e.IdEngenheiroAgronomo, "fkClienteEngenheiro Agronomo1_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Bairro)
                .HasMaxLength(25)
                .HasColumnName("bairro");
            entity.Property(e => e.Cidade)
                .HasMaxLength(20)
                .HasColumnName("cidade");
            entity.Property(e => e.Cpf)
                .HasMaxLength(11)
                .HasColumnName("CPF");
            entity.Property(e => e.DataNascimento)
                .HasColumnType("date")
                .HasColumnName("dataNascimento");
            entity.Property(e => e.Estado)
                .HasMaxLength(2)
                .HasColumnName("estado");
            entity.Property(e => e.IdEngenheiroAgronomo).HasColumnName("idEngenheiro Agronomo");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .HasColumnName("nome");
            entity.Property(e => e.NumeroCasa).HasColumnName("numeroCasa");
            entity.Property(e => e.Rua)
                .HasMaxLength(60)
                .HasColumnName("rua");
            entity.Property(e => e.Telefone)
                .HasMaxLength(15)
                .HasColumnName("telefone");

            entity.HasOne(d => d.IdEngenheiroAgronomoNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdEngenheiroAgronomo)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fkClienteEngenheiro Agronomo1");
        });

        modelBuilder.Entity<Contum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("conta");

            entity.HasIndex(e => e.IdVisita, "fkContaVisita1_idx");

            entity.HasIndex(e => e.IdProjeto, "fkFinancasProjeto1_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataPagamento)
                .HasColumnType("date")
                .HasColumnName("dataPagamento");
            entity.Property(e => e.IdProjeto).HasColumnName("idProjeto");
            entity.Property(e => e.IdVisita).HasColumnName("idVisita");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'A'")
                .HasColumnType("enum('A','P')")
                .HasColumnName("status");
            entity.Property(e => e.Valor).HasColumnName("valor");

            entity.HasOne(d => d.IdProjetoNavigation).WithMany(p => p.Conta)
                .HasForeignKey(d => d.IdProjeto)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fkFinancasProjeto1");

            entity.HasOne(d => d.IdVisitaNavigation).WithMany(p => p.Conta)
                .HasForeignKey(d => d.IdVisita)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fkContaVisita1");
        });

        modelBuilder.Entity<Cultura>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("cultura");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .HasColumnName("nome");
        });

        modelBuilder.Entity<Engenheiroagronomo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("engenheiroagronomo");

            entity.HasIndex(e => e.Cpf, "CPF_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Celular)
                .HasMaxLength(11)
                .HasColumnName("celular");
            entity.Property(e => e.Cpf)
                .HasMaxLength(11)
                .HasColumnName("CPF");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .HasColumnName("nome");
            entity.Property(e => e.Senha)
                .HasMaxLength(20)
                .HasColumnName("senha");
        });

        modelBuilder.Entity<Intervencao>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("intervencao");

            entity.HasIndex(e => e.IdProjeto, "fkIntervencaoProjeto1_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AreaTratada).HasColumnName("areaTratada");
            entity.Property(e => e.DataAplicacao)
                .HasColumnType("date")
                .HasColumnName("dataAplicacao");
            entity.Property(e => e.Descricao)
                .HasMaxLength(500)
                .HasColumnName("descricao");
            entity.Property(e => e.IdProjeto).HasColumnName("idProjeto");
            entity.Property(e => e.Pratica)
                .HasMaxLength(500)
                .HasColumnName("pratica");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'A'")
                .HasColumnType("enum('A','EX','C')")
                .HasColumnName("status");
            entity.Property(e => e.TipoProduto)
                .HasMaxLength(150)
                .HasColumnName("tipoProduto");

            entity.HasOne(d => d.IdProjetoNavigation).WithMany(p => p.Intervencaos)
                .HasForeignKey(d => d.IdProjeto)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fkIntervencaoProjeto1");
        });

        modelBuilder.Entity<Plano>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("plano");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .HasColumnName("nome");
            entity.Property(e => e.Valor).HasColumnName("valor");
        });

        modelBuilder.Entity<Projeto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("projeto");

            entity.HasIndex(e => e.IdPropriedade, "fkProjetoPropriedade1_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Anexo)
                .HasColumnType("blob")
                .HasColumnName("anexo");
            entity.Property(e => e.DataInicio)
                .HasColumnType("date")
                .HasColumnName("dataInicio");
            entity.Property(e => e.DataPrevista)
                .HasColumnType("date")
                .HasColumnName("dataPrevista");
            entity.Property(e => e.Descricao)
                .HasMaxLength(200)
                .HasColumnName("descricao");
            entity.Property(e => e.IdPropriedade).HasColumnName("idPropriedade");
            entity.Property(e => e.QuantParcela).HasColumnName("quantParcela");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'EX'")
                .HasColumnType("enum('EX','C')")
                .HasColumnName("status");
            entity.Property(e => e.Valor).HasColumnName("valor");

            entity.HasOne(d => d.IdPropriedadeNavigation).WithMany(p => p.Projetos)
                .HasForeignKey(d => d.IdPropriedade)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fkProjetoPropriedade1");
        });

        modelBuilder.Entity<Propriedade>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("propriedade");

            entity.HasIndex(e => e.IdCliente, "fkPropriedadeCliente1_idx");

            entity.HasIndex(e => e.IdCultura, "fkPropriedadeCultura1_idx");

            entity.HasIndex(e => e.IdSolo, "fkPropriedadeSolo1_idx");

            entity.HasIndex(e => e.IdEngenheiroAgronomo, "fkPropriedadeUsuario1_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AreaCultivada).HasColumnName("areaCultivada");
            entity.Property(e => e.AreaPasto).HasColumnName("areaPasto");
            entity.Property(e => e.AreaPreservar).HasColumnName("areaPreservar");
            entity.Property(e => e.AreaReserva).HasColumnName("areaReserva");
            entity.Property(e => e.AreaTotal).HasColumnName("areaTotal");
            entity.Property(e => e.Car)
                .HasMaxLength(50)
                .HasColumnName("car");
            entity.Property(e => e.Ccir)
                .HasMaxLength(50)
                .HasColumnName("ccir");
            entity.Property(e => e.Cidade)
                .HasMaxLength(50)
                .HasColumnName("cidade");
            entity.Property(e => e.Comercializacao)
                .HasColumnType("enum('C','A')")
                .HasColumnName("comercializacao");
            entity.Property(e => e.Cultura)
                .HasMaxLength(50)
                .HasColumnName("cultura");
            entity.Property(e => e.Estado)
                .HasMaxLength(10)
                .HasColumnName("estado");
            entity.Property(e => e.FonteAlimento)
                .HasMaxLength(50)
                .HasColumnName("fonteAlimento");
            entity.Property(e => e.Georreferenciamento)
                .HasColumnType("blob")
                .HasColumnName("georreferenciamento");
            entity.Property(e => e.HistoricoFitossanidade)
                .HasColumnType("blob")
                .HasColumnName("historicoFitossanidade");
            entity.Property(e => e.HistoricoProdAgricola)
                .HasColumnType("blob")
                .HasColumnName("historicoProdAgricola");
            entity.Property(e => e.HistoricoProducao)
                .HasColumnType("blob")
                .HasColumnName("historicoProducao");
            entity.Property(e => e.IdCliente).HasColumnName("idCliente");
            entity.Property(e => e.IdCultura).HasColumnName("idCultura");
            entity.Property(e => e.IdEngenheiroAgronomo).HasColumnName("idEngenheiroAgronomo");
            entity.Property(e => e.IdSolo).HasColumnName("idSolo");
            entity.Property(e => e.Itr)
                .HasMaxLength(50)
                .HasColumnName("itr");
            entity.Property(e => e.MatriculaImovel)
                .HasColumnType("blob")
                .HasColumnName("matriculaImovel");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .HasColumnName("nome");
            entity.Property(e => e.NumAnimais).HasColumnName("numAnimais");
            entity.Property(e => e.QuantFuncionario).HasColumnName("quantFuncionario");
            entity.Property(e => e.Raca)
                .HasMaxLength(50)
                .HasColumnName("raca");
            entity.Property(e => e.TipoSolo)
                .HasMaxLength(50)
                .HasColumnName("tipoSolo");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Propriedades)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fkPropriedadeCliente1");

            entity.HasOne(d => d.IdCulturaNavigation).WithMany(p => p.Propriedades)
                .HasForeignKey(d => d.IdCultura)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fkPropriedadeCultura1");

            entity.HasOne(d => d.IdEngenheiroAgronomoNavigation).WithMany(p => p.Propriedades)
                .HasForeignKey(d => d.IdEngenheiroAgronomo)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fkPropriedadeUsuario1");

            entity.HasOne(d => d.IdSoloNavigation).WithMany(p => p.Propriedades)
                .HasForeignKey(d => d.IdSolo)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fkPropriedadeSolo1");
        });

        modelBuilder.Entity<Solo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("solo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .HasColumnName("nome");
        });

        modelBuilder.Entity<Visitum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("visita");

            entity.HasIndex(e => e.IdPropriedade, "fkVisitaPropriedade_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataHora)
                .HasColumnType("datetime")
                .HasColumnName("dataHora");
            entity.Property(e => e.IdPropriedade).HasColumnName("idPropriedade");
            entity.Property(e => e.Observacoes)
                .HasMaxLength(200)
                .HasColumnName("observacoes");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'A'")
                .HasColumnType("enum('A','C')")
                .HasColumnName("status");

            entity.HasOne(d => d.IdPropriedadeNavigation).WithMany(p => p.Visita)
                .HasForeignKey(d => d.IdPropriedade)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fkVisitaPropriedade");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
