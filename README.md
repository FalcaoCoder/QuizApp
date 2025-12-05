# 🧠 Quiz Master App

Um aplicativo de Quiz dinâmico e multiplataforma desenvolvido com **.NET MAUI** e **C#**.
O projeto foca em lógica de manipulação de dados, interface moderna e persistência de dados.

## 📱 Funcionalidades

- **Múltiplas Categorias:** Geografia, História, Ciência, Cinema, Esportes e Curiosidades.
- **Banco de Dados em JSON:** As perguntas são carregadas de um arquivo externo, permitindo fácil expansão de conteúdo sem alterar o código-fonte.
- **Sistema de Rodadas:** Algoritmo que seleciona aleatoriamente 5 perguntas de um banco de dados maior a cada jogo.
- **High Score:** Sistema de persistência local (`Preferences`) que salva o recorde do usuário.
- **Timer Dinâmico:** Barra de progresso visual usando `IDispatcherTimer`.
- **Feedback Visual:** Interface responsiva com validação imediata de erros e acertos.

## 🛠️ Tecnologias Utilizadas

- **C# (.NET 8):** Lógica de negócio e backend.
- **.NET MAUI:** Framework para interface gráfica multiplataforma (Android/Windows).
- **System.Text.Json:** Para desserialização e leitura de dados.
- **LINQ:** Para filtragem, ordenação e manipulação das listas de perguntas.
- **XAML:** Construção de layout com Grid, StackLayouts e Frames.

## 📂 Estrutura do Projeto

O destaque técnico do projeto é a separação de responsabilidades:
- `MainPage.xaml`: Camada de Apresentação (UI).
- `GerenciadorQuiz.cs`: Camada de Lógica (Regras do jogo, pontuação, filtro).
- `Questao.cs`: Modelo de Dados.
- `questoes.json`: Fonte de Dados (Data Source).

## 🚀 Como Rodar

1. Clone o repositório.
2. Abra a solução no **Visual Studio 2022**.
3. Aguarde a restauração dos pacotes NuGet.
4. Selecione o emulador (Android) ou Windows Machine.
5. Execute o projeto (F5).

---
*Desenvolvido por Igor Falcão como parte de portfólio de desenvolvimento Mobile.*
