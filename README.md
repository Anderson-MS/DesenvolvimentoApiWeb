# Teste Prático - Desenvolvedor Web

Este é um projeto incrível que oferece uma API para gerenciamento de clientes. Ao criar clientes usando esta API, siga a estrutura abaixo para garantir uma integração suave:

```json
{
  "id": 0,
    "nome": "Joana Batista",
    "email": "joanab@gmail.com",
    "cpf": "862.405.280-72",
    "rg": "25.715.640-9",
    "tipo": "Residencial",
    "ddd": 25,
    "telefone": "964568987",
    "tipoEndereco": "Preferencial",
    "cep": "02420-100",
    "logradouro": "Rua marajoara vicente",
    "numero": "55",
    "bairro": "Vila Anastacia",
    "complemento": "Condominio",
    "cidade": "São Paulo",
    "estado": "Santo Caetano",
    "referencia": "Proximo Atacadao"
}

## Estrutura do Banco de Dados

O sistema utiliza um banco de dados relacional para armazenar informações dos clientes. Abaixo estão os scripts SQL para criar as tabelas necessárias.

### Tabela Cliente

```sql
CREATE TABLE Cliente (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nome VARCHAR(255),
    Email VARCHAR(255),
    CPF VARCHAR(14),
    RG VARCHAR(20)
);

CREATE TABLE Contato (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Tipo VARCHAR(50),
    DDD INT,
    Telefone DECIMAL(11,0)
);

CREATE TABLE Endereco (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Tipo VARCHAR(50),
    CEP VARCHAR(9),
    Logradouro VARCHAR(255),
    Numero INT,
    Bairro VARCHAR(100),
    Complemento VARCHAR(255),
    Cidade VARCHAR(100),
    Estado VARCHAR(50),
    Referencia VARCHAR(255)
);



