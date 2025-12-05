using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage; // Necessário para salvar o Recorde
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp
{
    public partial class MainPage : ContentPage
    {
        GerenciadorQuiz gerenciador = new GerenciadorQuiz();
        Questao? questaoAtual; // Corrigido: Tornar anulável
        IDispatcherTimer? timer; // Corrigido: Tornar anulável
        double tempoRestante;

        public MainPage()
        {
            InitializeComponent();
            ConfigurarTimer();

            // Carrega o recorde visualmente ao iniciar
            int recorde = Preferences.Get("RecordeMaximo", 0);
            LblRecorde.Text = $"🏆 Recorde: {recorde}";
        }

        // --- NOVO: Carrega as perguntas do JSON quando a tela aparece ---
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                await gerenciador.CarregarPerguntasAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", "Não foi possível carregar as perguntas: " + ex.Message, "OK");
            }
        }
        // ---------------------------------------------------------------

        private void ConfigurarTimer()
        {
            timer = Dispatcher.CreateTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += (s, e) =>
            {
                tempoRestante -= 0.01;
                BarraTempo.Progress = tempoRestante;

                if (tempoRestante <= 0)
                {
                    timer.Stop();
                    TratarErro("TEMPO ESGOTADO!");
                }
            };
        }

        private void OnCategoriaClicked(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (btn?.CommandParameter is not string categoria || string.IsNullOrWhiteSpace(categoria))
            {
                DisplayAlert("Erro", "Categoria inválida ou não selecionada.", "OK");
                return;
            }

            // Segurança: Garante que as perguntas carregaram antes de iniciar
            try
            {
                gerenciador.IniciarJogo(categoria);
                LayoutMenu.IsVisible = false;
                LayoutJogo.IsVisible = true;
                CarregarProximaPergunta();
            }
            catch
            {
                DisplayAlert("Aguarde", "As perguntas ainda estão carregando...", "OK");
            }
        }

        private void CarregarProximaPergunta()
        {
            questaoAtual = gerenciador.ObterProximaPergunta();

            if (questaoAtual != null)
            {
                LblPergunta.Text = questaoAtual.PerguntaTexto;
                // --- NOVO: Atualiza o valor na tela ---
                LblValorPergunta.Text = $"✨ Vale {questaoAtual.Pontos} pontos";
                // --------------------------------------
                LblPontos.Text = $"{gerenciador.PontuacaoAtual} pts";
                LblFeedback.Text = "";

                tempoRestante = 1.0;
                BarraTempo.Progress = 1.0;
                timer.Start();

                // --- CORREÇÃO AQUI ---
                // 1. Cria uma lista nova copiando as alternativas (que são as erradas no JSON)
                var todasOpcoes = new List<string>(questaoAtual.Alternativas);

                // 2. Adiciona a resposta correta nessa lista
                todasOpcoes.Add(questaoAtual.RespostaCorreta);

                // 3. Agora sim, embaralha a lista completa (4 itens)
                var rnd = new Random();
                var ops = todasOpcoes.OrderBy(a => rnd.Next()).ToList();
                // ---------------------

                ConfigurarBotao(BtnOp1, ops, 0);
                ConfigurarBotao(BtnOp2, ops, 1);
                ConfigurarBotao(BtnOp3, ops, 2);
                ConfigurarBotao(BtnOp4, ops, 3);
            }
            else
            {
                GameOver();
            }
        }

        private void ConfigurarBotao(Button btn, List<string> ops, int index)
        {
            if (index < ops.Count)
            {
                btn.Text = ops[index];
                btn.IsVisible = true;
                btn.IsEnabled = true;
                btn.BackgroundColor = Color.FromArgb("#333");
            }
            else
            {
                btn.IsVisible = false;
            }
        }

        private async void OnRespostaClicked(object sender, EventArgs e)
        {
            timer.Stop();
            var btn = sender as Button;
            bool acertou = gerenciador.VerificarResposta(btn.Text);

            if (acertou)
            {
                LblFeedback.Text = "BOA! +10";
                LblFeedback.TextColor = Colors.LightGreen;
                btn.BackgroundColor = Colors.Green;
                await Task.Delay(1000);
                CarregarProximaPergunta();
            }
            else
            {
                TratarErro($"Errado! Era: {questaoAtual.RespostaCorreta}");
            }
        }

        private async void TratarErro(string msg)
        {
            LblFeedback.Text = msg;
            LblFeedback.TextColor = Colors.Red;
            await Task.Delay(2500);
            CarregarProximaPergunta();
        }

        private async void GameOver()
        {
            timer.Stop();

            // Lógica do Recorde
            int recordeAtual = Preferences.Get("RecordeMaximo", 0);
            string mensagem = $"Pontuação Final: {gerenciador.PontuacaoAtual}";

            if (gerenciador.PontuacaoAtual > recordeAtual)
            {
                Preferences.Set("RecordeMaximo", gerenciador.PontuacaoAtual);
                mensagem += "\n🔥 NOVO RECORDE! 🔥";
                LblRecorde.Text = $"🏆 Recorde: {gerenciador.PontuacaoAtual}";
            }

            await DisplayAlert("Fim de Jogo", mensagem, "Voltar ao Menu");

            LayoutJogo.IsVisible = false;
            LayoutMenu.IsVisible = true;
        }

        private void OnVoltarMenuClicked(object sender, EventArgs e)
        {
            timer.Stop();
            LayoutJogo.IsVisible = false;
            LayoutMenu.IsVisible = true;
        }
    }
}