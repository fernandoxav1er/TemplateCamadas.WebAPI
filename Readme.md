# Template .NET - Camadas API

Este repositório contém um **template padrão de API com arquitetura em camadas**, criado para facilitar a geração de novos projetos com estrutura base já definida.

---

## ✅ Funcionalidades do Template

* Estrutura de camadas (API, Domain, Infrastructure)
* Separação clara de responsabilidades
* Suporte para injeção de dependência
* Pronto para integração com Swagger, Entity Framework e validações

---

## 📦 Instalação do Template

Para instalar o template localmente:

```bash
dotnet new -i ./
```

Ou, caso prefira um caminho absoluto ou nome do diretório específico:

```bash
dotnet new -i ./caminho/do/template
```

---

## ❌ Desinstalar o Template

Caso deseje remover o template da sua máquina:

```bash
dotnet new -u ./
```

---

## 🚀 Criar um novo projeto a partir do Template

Após a instalação, você pode gerar um novo projeto com o seguinte comando:

```bash
dotnet new camadasapi -n MeuNovoProjeto -o MeuNovoProjeto.WebAPI
```

* `-n`: Nome da solução/projeto
* `-o`: Diretório de saída para o novo projeto

---

## 🔪 Verificar se o Template foi instalado

Para conferir se o template foi instalado corretamente:

```bash
dotnet new --list
```

Você deverá ver algo como:

```
Template Name              Short Name            Language    Tags
------------------------  --------------------  ----------  -----------------------
Camadas API Template      template-camadas-api  [C#]        Web/API
```

---

## 🛠 Reinstalando o Template

Se quiser atualizar ou reinstalar o template localmente, primeiro desinstale e depois instale novamente:

```bash
dotnet new -u ./
dotnet new -i ./
```

---

## 🗂 Estrutura do Projeto Gerado

```text
MeuNovoProjeto.WebAPI/
├──src
	├── MeuNovoProjeto.API/
	├── MeuNovoProjeto.Domain/
	└── MeuNovoProjeto.Infrastructure/
└── MeuNovoProjeto.sln/
```

---

## 📄 Licença

Este projeto está licenciado sob a [MIT License](LICENSE).

---

Desenvolvido com 💻 e ☕
