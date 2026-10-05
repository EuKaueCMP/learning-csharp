using System;
using System.Collections.Generic;
using BancoAPI.Domains;
using Microsoft.EntityFrameworkCore;

namespace BancoAPI.Contexts;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<conta_usuario> conta_usuario { get; set; }

    public virtual DbSet<deposito> deposito { get; set; }

    public virtual DbSet<log_transferencia> log_transferencia { get; set; }

    public virtual DbSet<movimentacao> movimentacao { get; set; }

    public virtual DbSet<pagamento> pagamento { get; set; }

    public virtual DbSet<saque> saque { get; set; }

    public virtual DbSet<transferencia> transferencia { get; set; }

    public virtual DbSet<usuario> usuario { get; set; }

    public virtual DbSet<usuario_log> usuario_log { get; set; }

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

        modelBuilder.Entity<conta_usuario>(entity =>
        {
            entity.HasKey(e => e.numero_conta).HasName("conta_usuario_pkey");

            entity.ToTable("conta_usuario", "banco");

            entity.HasIndex(e => e.usuario_id, "conta_usuario_usuario_id_key").IsUnique();

            entity.Property(e => e.numero_conta)
                .UseIdentityAlwaysColumn()
                .HasIdentityOptions(100001L, null, null, null, null, null);
            entity.Property(e => e.numero_agencia)
                .HasMaxLength(4)
                .HasDefaultValueSql("'0999'::character varying");
            entity.Property(e => e.saldo)
                .HasPrecision(10, 2)
                .HasDefaultValue(0m);

            entity.HasOne(d => d.usuario).WithOne(p => p.conta_usuario)
                .HasForeignKey<conta_usuario>(d => d.usuario_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("conta_usuario_usuario_id_fkey");
        });

        modelBuilder.Entity<deposito>(entity =>
        {
            entity.HasKey(e => e.deposito_id).HasName("deposito_pkey");

            entity.ToTable("deposito", "banco");

            entity.Property(e => e.data_deposito)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.valor).HasPrecision(10, 2);

            entity.HasOne(d => d.usuario).WithMany(p => p.deposito)
                .HasForeignKey(d => d.usuario_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("deposito_usuario_id_fkey");
        });

        modelBuilder.Entity<log_transferencia>(entity =>
        {
            entity.HasKey(e => e.log_id).HasName("log_transferencia_pkey");

            entity.ToTable("log_transferencia", "banco");

            entity.Property(e => e.data_alteracao)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.transferencia).WithMany(p => p.log_transferencia)
                .HasForeignKey(d => d.transferencia_id)
                .HasConstraintName("log_transferencia_transferencia_id_fkey");
        });

        modelBuilder.Entity<movimentacao>(entity =>
        {
            entity.HasKey(e => e.movimentacao_id).HasName("movimentacao_pkey");

            entity.ToTable("movimentacao", "banco");

            entity.Property(e => e.data_movimentacao)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.informacoes).HasColumnType("jsonb");
            entity.Property(e => e.saldo_anterior).HasPrecision(10, 2);
            entity.Property(e => e.saldo_atual).HasPrecision(10, 2);
            entity.Property(e => e.saldo_movimentado).HasPrecision(10, 2);

            entity.HasOne(d => d.usuario).WithMany(p => p.movimentacao)
                .HasForeignKey(d => d.usuario_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("movimentacao_usuario_id_fkey");
        });

        modelBuilder.Entity<pagamento>(entity =>
        {
            entity.HasKey(e => e.pagamento_id).HasName("pagamento_pkey");

            entity.ToTable("pagamento", "banco");

            entity.Property(e => e.valor).HasPrecision(10, 2);

            entity.HasOne(d => d.usuario).WithMany(p => p.pagamento)
                .HasForeignKey(d => d.usuario_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pagamento_usuario_id_fkey");
        });

        modelBuilder.Entity<saque>(entity =>
        {
            entity.HasKey(e => e.saque_id).HasName("saque_pkey");

            entity.ToTable("saque", "banco");

            entity.Property(e => e.data_saque)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.localizacao_saque).HasColumnType("jsonb");
            entity.Property(e => e.valor).HasPrecision(10, 2);

            entity.HasOne(d => d.usuario).WithMany(p => p.saque)
                .HasForeignKey(d => d.usuario_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("saque_usuario_id_fkey");
        });

        modelBuilder.Entity<transferencia>(entity =>
        {
            entity.HasKey(e => e.transferencia_id).HasName("transferencia_pkey");

            entity.ToTable("transferencia", "banco");

            entity.Property(e => e.data_criacao)
                .HasDefaultValueSql("clock_timestamp()")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.data_transferencia).HasColumnType("timestamp without time zone");
            entity.Property(e => e.valor).HasPrecision(10, 2);

            entity.HasOne(d => d.usuario_destinatario).WithMany(p => p.transferenciausuario_destinatario)
                .HasForeignKey(d => d.usuario_destinatario_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("transferencia_usuario_destinatario_id_fkey");

            entity.HasOne(d => d.usuario_remetente).WithMany(p => p.transferenciausuario_remetente)
                .HasForeignKey(d => d.usuario_remetente_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("transferencia_usuario_remetente_id_fkey");
        });

        modelBuilder.Entity<usuario>(entity =>
        {
            entity.HasKey(e => e.usuario_id).HasName("usuario_pkey");

            entity.ToTable("usuario", "banco");

            entity.HasIndex(e => e.email, "usuario_email_key").IsUnique();

            entity.Property(e => e.email).HasMaxLength(100);
            entity.Property(e => e.senha).HasMaxLength(100);
            entity.Property(e => e.status).HasDefaultValue(true);
        });

        modelBuilder.Entity<usuario_log>(entity =>
        {
            entity.HasKey(e => e.log_id).HasName("usuario_log_pkey");

            entity.ToTable("usuario_log", "banco");

            entity.Property(e => e.email_anterior).HasMaxLength(100);
            entity.Property(e => e.nome_anterior).HasMaxLength(100);
            entity.Property(e => e.senha_anterior).HasMaxLength(100);

            entity.Property(e => e.data_alteracao)
                            .HasDefaultValueSql("clock_timestamp()")
                            .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.usuario).WithMany(p => p.usuario_log)
                .HasForeignKey(d => d.usuario_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("usuario_log_usuario_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
