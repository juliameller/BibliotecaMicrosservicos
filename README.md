# Sistema de Gestão de Biblioteca (BibliotecaMicrosservicos)

## Sobre

Este projeto é um sistema de biblioteca baseado em arquitetura de microsserviços que oferece funcionalidades para cadastro, consulta e gerenciamento de livros, membros e empréstimos.

## Requisitos
### Cadastro
- RF001: Livros devem ser cadastrados com informações como título, autor, status de disponibilidade e data do cadastro; <br>
- RF002: Membros devem ser cadastrados com informações como nome, email, ativo/inativo e data de cadastro; <br>
- RF003: Empréstimos devem associar membros e livros, registrando datas de empréstimo e devolução.

### Empréstimos
- RF004: Consulta da disponibilidade do livro e status do membro antes de registrar um empréstimo; <br>
- RF005: Atualização do status do livro para "disponível" após a devolução.

## Microsserviços
## Livro
- Cadastrar e inserir livros; <br>
- Verificar e alterar a disponibilidade do livro.

## Membros
- Cadastrar e inserir membros.

## Empréstimos
- Cadastrar um novo empréstimo associando livros e membros;
- Consultar empréstimo.

  



