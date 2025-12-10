# 🧠 Quiz Master App

Aplicativo de Quiz multiplataforma desenvolvido com **.NET MAUI** e **C#**, focado em experiência do usuário e arquitetura de dados flexível.

## 📱 Funcionalidades Principais

- **6 Categorias Dinâmicas:** Geografia, História, Ciência, Cinema, Esportes e Curiosidades.
- **Modos de Jogo Personalizáveis:**
  - 🧘 **Modo Zen:** Jogue sem pressão de tempo.
  - ♾️ **Modo Infinito:** Responda todas as perguntas disponíveis sem interrupções.
  - ⚙️ **Seletor de Quantidade:** Escolha partidas curtas (5) ou longas (30 perguntas).
- **Banco de Dados JSON:** Conteúdo carregado externamente, permitindo atualizações sem recompilar o código.
- **Gamificação:**
  - Timer visual com `ProgressBar`.
  - Feedback imediato de erros/acertos.
  - Sistema de **High Score** persistente.

## 🛠️ Tecnologias e Conceitos

- **Front-end:** XAML com uso de `Grid`, `Frame`, `ControlTemplates` e `VisualStateManager`.
- **Back-end:** C# (.NET 8.0).
- **Manipulação de Dados:** `System.Text.Json` para desserialização e `LINQ` para filtragem e randomização avançada.
- **Persistência:** `Microsoft.Maui.Storage.Preferences` para salvar recordes locais.
- **Assincronismo:** Uso de `async/await` para leitura de arquivos e fluxo de UI sem travamentos.

## 🚀 Como Executar

1. Clone este repositório.
2. Abra a solução `QuizApp.sln` no Visual Studio 2022.
3. Aguarde a restauração dos pacotes NuGet.
4. Selecione o emulador Android ou "Windows Machine".
5. Pressione **F5**.

---
*Desenvolvido como projeto de portfólio para demonstrar domínio em desenvolvimento Mobile .NET.*
