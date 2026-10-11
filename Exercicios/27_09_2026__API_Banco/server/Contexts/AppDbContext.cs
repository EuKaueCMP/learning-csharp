using System;
using System.Collections.Generic;
using BancoAPI.Domains;
using Microsoft.EntityFrameworkCore;

namespace BancoAPI.Contexts;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ContaUsuario> ContaUsuario { get; set; }

    public virtual DbSet<Deposito> Deposito { get; set; }

    public virtual DbSet<LogTransferencia> LogTransferencia { get; set; }

    public virtual DbSet<Movimentacao> Movimentacao { get; set; }

    public virtual DbSet<Pagamento> Pagamento { get; set; }

    public virtual DbSet<Saque> Saque { get; set; }

    public virtual DbSet<Transferencia> Transferencia { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    public virtual DbSet<UsuarioLog> UsuarioLog { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;Port=5432;User Id=postgres;Password=Admin123!;Database=ksc_bank;Trust Server Certificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("banco", "status_movimentacao_enum", new[] { "EM ANDAMENTO", "CONCLUIDA", "CANCELADA" })
            .HasPostgresEnum("banco", "tipo_alteracao_enum", new[] { "CRIACAO", "ALTERACAO_SENHA", "ALTERACAO_DADOS", "INATIVACAO_CONTA" })
            .HasPostgresEnum("banco", "tipo_deposito_enum", new[] { "PIX", "TED", "DOC", "BOLETO", "DEPOSITOS ESPECIE" })
            .HasPostgresEnum("banco", "tipo_movimentacao_enum", new[] { "ENTRADA", "SAIDA" })
            .HasPostgresEnum("banco", "tipo_pagamento_enum", new[] { "BOLETO", "DEBITO AUTOMATICO", "FATURA", "IMPOSTOS" })
            .HasPostgresEnum("banco", "tipo_saque_enum", new[] { "SAQUE ESPECIE", "SAQUE TRANSFERENCIA", "SAQUE CHEQUE" })
            .HasPostgresEnum("banco", "tipo_transferencia_enum", new[] { "PIX", "PIX AGENDADO", "TED", "DOC", "TEF", "SWIFT" })
            .HasPostgresEnum("banco", "tipo_usuario_enum", new[] { "ADMIN", "SUPORTE", "PESSOA_FISICA", "PESSOA_JURIDICA" });

        modelBuilder.Entity<ContaUsuario>(entity =>
        {
            entity.HasKey(e => e.NumeroConta).HasName("conta_usuario_pkey");

            entity.ToTable("conta_usuario", "banco");

            entity.HasIndex(e => e.UsuarioId, "conta_usuario_usuario_id_key").IsUnique();

            entity.Property(e => e.NumeroConta)
                .UseIdentityAlwaysColumn()
                .HasIdentityOptions(100001L, null, null, null, null, null)
                .HasColumnName("numero_conta");
            entity.Property(e => e.NumeroAgencia)
                .HasMaxLength(4)
                .HasDefaultValueSql("'0999'::character varying")
                .HasColumnName("numero_agencia");
            entity.Property(e => e.Saldo)
                .HasPrecision(10, 2)
                .HasDefaultValue(0m)
                .HasColumnName("saldo");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Usuario).WithOne(p => p.ContaUsuario)
                .HasForeignKey<ContaUsuario>(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("conta_usuario_usuario_id_fkey");
        });

        modelBuilder.Entity<Deposito>(entity =>
        {
            entity.HasKey(e => e.DepositoId).HasName("deposito_pkey");

            entity.ToTable("deposito", "banco");

            entity.Property(e => e.DepositoId).HasColumnName("deposito_id");
            entity.Property(e => e.DataDeposito)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("data_deposito");
            entity.Property(e => e.NumeroAgencia)
                .HasMaxLength(4)
                .HasColumnName("numero_agencia");
            entity.Property(e => e.NumeroConta).HasColumnName("numero_conta");
            entity.Property(e => e.Valor)
                .HasPrecision(10, 2)
                .HasColumnName("valor");

            entity.HasOne(d => d.NumeroContaNavigation).WithMany(p => p.Deposito)
                .HasForeignKey(d => d.NumeroConta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("deposito_numero_conta_fkey");
        });

        modelBuilder.Entity<LogTransferencia>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("log_transferencia_pkey");

            entity.ToTable("log_transferencia", "banco");

            entity.Property(e => e.LogId).HasColumnName("log_id");
            entity.Property(e => e.DataAlteracao)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("data_alteracao");
            entity.Property(e => e.DescricaoLog).HasColumnName("descricao_log");
            entity.Property(e => e.TransferenciaId).HasColumnName("transferencia_id");

            entity.HasOne(d => d.Transferencia).WithMany(p => p.LogTransferencia)
                .HasForeignKey(d => d.TransferenciaId)
                .HasConstraintName("log_transferencia_transferencia_id_fkey");
        });

        modelBuilder.Entity<Movimentacao>(entity =>
        {
            entity.HasKey(e => e.MovimentacaoId).HasName("movimentacao_pkey");

            entity.ToTable("movimentacao", "banco");

            entity.Property(e => e.MovimentacaoId).HasColumnName("movimentacao_id");
            entity.Property(e => e.DataMovimentacao)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("data_movimentacao");
            entity.Property(e => e.Informacoes)
                .HasColumnType("jsonb")
                .HasColumnName("informacoes");
            entity.Property(e => e.SaldoAnterior)
                .HasPrecision(10, 2)
                .HasColumnName("saldo_anterior");
            entity.Property(e => e.SaldoAtual)
                .HasPrecision(10, 2)
                .HasColumnName("saldo_atual");
            entity.Property(e => e.SaldoMovimentado)
                .HasPrecision(10, 2)
                .HasColumnName("saldo_movimentado");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Movimentacao)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("movimentacao_usuario_id_fkey");
        });

        modelBuilder.Entity<Pagamento>(entity =>
        {
            entity.HasKey(e => e.PagamentoId).HasName("pagamento_pkey");

            entity.ToTable("pagamento", "banco");

            entity.Property(e => e.PagamentoId).HasColumnName("pagamento_id");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");
            entity.Property(e => e.Valor)
                .HasPrecision(10, 2)
                .HasColumnName("valor");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Pagamento)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pagamento_usuario_id_fkey");
        });

        modelBuilder.Entity<Saque>(entity =>
        {
            entity.HasKey(e => e.SaqueId).HasName("saque_pkey");

            entity.ToTable("saque", "banco");

            entity.Property(e => e.SaqueId).HasColumnName("saque_id");
            entity.Property(e => e.AgenciaConta)
                .HasMaxLength(4)
                .HasColumnName("agencia_conta");
            entity.Property(e => e.DataSaque)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("data_saque");
            entity.Property(e => e.LocalizacaoSaque)
                .HasColumnType("jsonb")
                .HasColumnName("localizacao_saque");
            entity.Property(e => e.NumeroConta).HasColumnName("numero_conta");
            entity.Property(e => e.Valor)
                .HasPrecision(10, 2)
                .HasColumnName("valor");

            entity.HasOne(d => d.NumeroContaNavigation).WithMany(p => p.Saque)
                .HasForeignKey(d => d.NumeroConta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("saque_numero_conta_fkey");
        });

        modelBuilder.Entity<Transferencia>(entity =>
        {
            entity.HasKey(e => e.TransferenciaId).HasName("transferencia_pkey");

            entity.ToTable("transferencia", "banco");

            entity.Property(e => e.TransferenciaId).HasColumnName("transferencia_id");
            entity.Property(e => e.DataCriacao)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("data_criacao");
            entity.Property(e => e.DataTransferencia)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("data_transferencia");
            entity.Property(e => e.UsuarioDestinatarioId).HasColumnName("usuario_destinatario_id");
            entity.Property(e => e.UsuarioRemetenteId).HasColumnName("usuario_remetente_id");
            entity.Property(e => e.Valor)
                .HasPrecision(10, 2)
                .HasColumnName("valor");

            entity.HasOne(d => d.UsuarioDestinatario).WithMany(p => p.TransferenciaUsuarioDestinatario)
                .HasForeignKey(d => d.UsuarioDestinatarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("transferencia_usuario_destinatario_id_fkey");

            entity.HasOne(d => d.UsuarioRemetente).WithMany(p => p.TransferenciaUsuarioRemetente)
                .HasForeignKey(d => d.UsuarioRemetenteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("transferencia_usuario_remetente_id_fkey");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("usuario_pkey");

            entity.ToTable("usuario", "banco");

            entity.HasIndex(e => e.Email, "usuario_email_key").IsUnique();

            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Nome).HasColumnName("nome");
            entity.Property(e => e.Senha)
                .HasMaxLength(100)
                .HasColumnName("senha");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
        });

        modelBuilder.Entity<UsuarioLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("usuario_log_pkey");

            entity.ToTable("usuario_log", "banco");

            entity.Property(e => e.LogId).HasColumnName("log_id");
            entity.Property(e => e.DataAlteracao)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("data_alteracao");
            entity.Property(e => e.EmailAnterior)
                .HasMaxLength(100)
                .HasColumnName("email_anterior");
            entity.Property(e => e.NomeAnterior)
                .HasMaxLength(100)
                .HasColumnName("nome_anterior");
            entity.Property(e => e.SenhaAnterior)
                .HasMaxLength(100)
                .HasColumnName("senha_anterior");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Usuario).WithMany(p => p.UsuarioLog)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("usuario_log_usuario_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
