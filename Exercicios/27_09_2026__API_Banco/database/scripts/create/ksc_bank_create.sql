CREATE DATABASE ksc_bank;
CREATE schema banco;

CREATE TYPE banco.tipo_usuario_enum AS ENUM('ADMIN', 'SUPORTE', 'PESSOA_FISICA', 'PESSOA_JURIDICA');

CREATE TABLE banco.usuario
(
	usuario_id SERIAL PRIMARY KEY,
	nome 	TEXT NOT NULL,
	email 	VARCHAR(100) UNIQUE NOT NULL,
	senha 	VARCHAR(100) NOT NULL,
	status	BOOLEAN NOT NULL DEFAULT TRUE,
	tipo_usuario banco.tipo_usuario_enum NOT NULL
);

-- Se o tipo do usuario for pf ou pj...

-- é criada uma conta automaticamente

CREATE TABLE banco.conta_usuario
(
	numero_conta 	BIGINT GENERATED ALWAYS AS IDENTITY (START WITH 100001 INCREMENT BY 1) PRIMARY KEY,
	usuario_id 		INTEGER NOT NULL UNIQUE REFERENCES banco.usuario(usuario_id),
	numero_agencia 	VARCHAR(4) NOT NULL DEFAULT '0999',
	saldo 	NUMERIC(10, 2) DEFAULT 0
);

CREATE TYPE banco.tipo_alteracao_enum AS ENUM('CRIACAO', 'ALTERACAO_SENHA', 'ALTERACAO_DADOS', 'INATIVACAO_CONTA');

CREATE TABLE banco.usuario_log
(
	log_id SERIAL PRIMARY KEY,
	usuario_id INTEGER NOT NULL REFERENCES banco.usuario(usuario_id),
	tipo_alteracao banco.tipo_alteracao_enum NOT NULL,
	senha_anterior VARCHAR(100) NOT NULL,
	nome_anterior VARCHAR(100) NOT NULL,
	email_anterior VARCHAR(100) NOT NULL
);


CREATE TYPE banco.tipo_transferencia_enum AS ENUM('PIX', 'PIX AGENDADO', 'TED', 'DOC', 'TEF', 'SWIFT');
CREATE TYPE banco.status_movimentacao_enum AS ENUM('EM ANDAMENTO', 'CONCLUIDA', 'CANCELADA');

CREATE TABLE banco.transferencia
(
	transferencia_id SERIAL PRIMARY KEY,
	usuario_remetente_id INTEGER NOT NULL REFERENCES banco.usuario(usuario_id),
	usuario_destinatario_id INTEGER NOT NULL REFERENCES banco.usuario(usuario_id),
	valor NUMERIC(10, 2) NOT NULL,
	data_criacao TIMESTAMP DEFAULT CLOCK_TIMESTAMP(),
	data_transferencia TIMESTAMP NOT NULL,
	tipo_transferencia banco.tipo_transferencia_enum NOT NULL,
	status_movimentacao banco.status_movimentacao_enum NOT NULL
);

CREATE TABLE banco.log_transferencia
(
	log_id SERIAL PRIMARY KEY,
	transferencia_id INTEGER REFERENCES banco.transferencia(transferencia_id),
	descricao_log TEXT NOT NULL,
	status_movimentacao_anterior banco.status_movimentacao_enum NOT NULL,
	data_alteracao TIMESTAMP DEFAULT CLOCK_TIMESTAMP()
);

CREATE TYPE banco.tipo_deposito_enum AS ENUM('PIX', 'TED', 'DOC', 'BOLETO', 'DEPOSITOS ESPECIE');

CREATE TABLE banco.deposito
(
	deposito_id SERIAL PRIMARY KEY,
	usuario_id INTEGER NOT NULL REFERENCES banco.usuario(usuario_id),
	valor NUMERIC(10, 2) NOT NULL,
	tipo_deposito banco.tipo_deposito_enum NOT NULL,
	status_movimentacao banco.status_movimentacao_enum NOT NULL,
	data_deposito TIMESTAMP NOT NULL DEFAULT CLOCK_TIMESTAMP()
);

CREATE TYPE banco.tipo_saque_enum AS ENUM('SAQUE ESPECIE', 'SAQUE TRANSFERENCIA', 'SAQUE CHEQUE');

CREATE TABLE banco.saque
( 

	saque_id SERIAL PRIMARY KEY,
	usuario_id INTEGER NOT NULL REFERENCES banco.usuario(usuario_id),
	valor NUMERIC(10, 2) NOT NULL,
	tipo_saque banco.tipo_saque_enum NOT NULL,
	status_movimentacao banco.status_movimentacao_enum NOT NULL,
	data_saque TIMESTAMP NOT NULL DEFAULT CLOCK_TIMESTAMP(),
	localizacao_saque JSONB

);

CREATE TYPE banco.tipo_pagamento_enum AS ENUM('BOLETO', 'DEBITO AUTOMATICO', 'FATURA', 'IMPOSTOS');

CREATE TABLE banco.pagamento
(
	pagamento_id SERIAL PRIMARY KEY,
	usuario_id INTEGER NOT NULL REFERENCES banco.usuario(usuario_id),
	status_movimentacao banco.status_movimentacao_enum NOT NULL,
	tipo_pagamento banco.tipo_pagamento_enum NOT NULL,
	valor NUMERIC(10, 2) NOT NULL
);


CREATE TYPE banco.tipo_movimentacao_enum AS ENUM('ENTRADA', 'SAIDA');

CREATE TABLE banco.movimentacao
(
	movimentacao_id SERIAL PRIMARY KEY,
	usuario_id INTEGER NOT NULL REFERENCES banco.usuario(usuario_id),
	informacoes JSONB,
	status_movimentacao banco.status_movimentacao_enum NOT NULL, 
	tipo_movimentacao banco.tipo_movimentacao_enum NOT NULL,
	saldo_movimentado NUMERIC(10, 2) NOT NULL,
	saldo_anterior NUMERIC(10, 2) NOT NULL,
	saldo_atual NUMERIC(10, 2) NOT NULL,
	data_movimentacao TIMESTAMP DEFAULT CLOCK_TIMESTAMP()

);

-- Implementação futura: INVESTIMENTOS

--

--

--

--

--CREATE TABLE banco.ativo_investimento
--(
--	ativo_id SERIAL PRIMARY KEY,
--	ticker VARCHAR(7) NOT NULL,
--	tipo VARCHAR(20) NOT NULL,
--	tipo_mercado VARCHAR(15) NOT NULL,
--	codigo_mercado VARCHAR(15) NOT NULL,
--	issuer_code VARCHAR(15) NOT NULL,
--	currency VARCHAR(15) NOT NULL,
--	isin VARCHAR(13) NOT NULL,
--);

--CREATE TABLE banco.investimento 
--(
--	investimento_id SERIAL PRIMARY KEY,
--	usuario_id INTEGER REFERENCES banco.usuario(usuario_id),
--	ativo_id INTEGER REFERENCE banco.ativo_investimeno(ativo_id),
--	valor_aplicado NUMERIC(10, 2) NOT NULL,
--	data_investimento TIMESTAMP CLOCK_TIMESTAMP(),
--);

--

--CREATE TYPE banco.tipo_emissor AS ENUM('GOVERNO', 'BANCO', 'EMPRESA');
--CREATE TYPE banco.nivel_emissor AS ENUM('PJ', 'FEDERAL', 'ESTADUAL', 'NACIONAL');
--CREATE TYPE banco.status_emissor AS ENUM('ATIVO', 'INATIVO', 'EM FALIMENTO');

--

--CREATE TABLE emissor_renda_fixa
--(
--	emissor_id SERIAL PRIMARY KEY,
--	razao_social TEXT NOT NULL,
--	cnpj VARCHAR(16) NOT NULL,
--	tipo_emissor tipo_emissor NOT NULL,
--	nivel_emissor nivel_emissor NOT NULL,
--	rating VARCHAR(20) NOT NULL,
--	agencia_rating VARCHAR(30) NOT NULL,
--	cobertura BOOLEAN NOT NULL,
--	limite_fgc NUMBER(10, 2 ) DEFAULT(250.000),
--	status_emissor status_emissor NOT NULL,
--	data_cadastro TIMESTAMP CLOCK_TIMESTAMP()

--);

--

--CREATE TYPE banco.tipo_titulo_rf AS ENUM('CDB', 'CDI', 'LCA', 'LCI', 'CRI', 'CRA', 'TIT', 'DEBENTURE', 'LF', 'LCD');
--CREATE TYPE banco.tipo_remuneracao_rf AS ENUM('PREFIXADA', 'POS_FIXADA', 'HIBRADA');
--CREATE TYPE banco.indice_referencia_rf AS ENUM('CDI', 'SELIC', 'IGP_M', 'TR', 'NULL');
--CREATE TYPE banco.liquidez_rf AS ENUM ('DIARIO', 'NO_VENCIMENTO', 'APOS_CARENCIA');
--CREATE TYPE banco.status_rf AS ENUM ('ATIVO', 'INATIVO', 'ENCERRADO', 'VENCIDO');

--

--CREATE TABLE banco.titulo_renda_fixa
--(
--	titulo_id SERIAL PRIMARY KEY,
--	emissor_id INTEGER REFERENCE banco.emissor_renda_fixa(emissor_id),
--	codigo VARCHAR(20),
--	nome VARCHAR(30) NOT NULL,
--	tipo_titulo tipo_titulo_rf NOT NULL,
--	tipo_remuneracao tipo_remuneracao_rf NOT NULL,
--	taxa DECIMAL(10, 4) NOT NULL,
--	indice_referencia indice_referencia_rf	NOT NULL,
--	liquidez liquidez NOT NULL,
--	isencao_ir BOOLEAN NOT NULL,
--	sujeito_iof BOOLEAN NOT NULL,
--	status_rf status_rf NOT NULL,
--	data_cadastro TIMESTAMP CLOCK_TIMESTAMP(),
--);

--

--CREATE TABLE banco.investimento_renda_fixa 
--(
--	investimento_renda_fixa_id SERIAL PRIMARY KEY,
--	investimento_id INTEGER REFERENCES banco.investimento(investimento_id),
--	
--);