using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

namespace QuizApp
{
    public class GerenciadorQuiz
    {
        private List<Questao> _todasAsQuestoes;
        private List<Questao> _questoesDaRodada = new List<Questao>(); // Inicialização para evitar CS8618

        // CONFIGURAÇÃO: Quantas perguntas por jogo?
        private const int TamanhoDaRodada = 5;

        public int PontuacaoAtual { get; private set; }
        public int QuestaoAtualIndex { get; private set; }

        public GerenciadorQuiz()
        {
            _todasAsQuestoes = new List<Questao>();
        }

        public async Task CarregarPerguntasAsync()
        {
            try
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync("questoes.json");
                using var reader = new StreamReader(stream);
                var conteudoJson = await reader.ReadToEndAsync();
                var questoes = JsonSerializer.Deserialize<List<Questao>>(conteudoJson);
                _todasAsQuestoes = questoes ?? new List<Questao>();
            }
            catch
            {
                // Se der erro (arquivo não existe), inicia lista vazia para não travar
                _todasAsQuestoes = new List<Questao>();
            }
        }

        public void IniciarJogo(string categoriaEscolhida)
        {
            PontuacaoAtual = 0;
            QuestaoAtualIndex = 0;

            IEnumerable<Questao> listaFiltrada;

            // 1. Filtra pelo tema
            if (categoriaEscolhida == "Tudo")
            {
                listaFiltrada = _todasAsQuestoes;
            }
            else
            {
                listaFiltrada = _todasAsQuestoes
                                .Where(q => q.Categoria == categoriaEscolhida);
            }

            // 2. EMBARALHA e PEGA SÓ UMA QUANTIDADE (Limitador)
            var rnd = new Random();
            _questoesDaRodada = listaFiltrada
                                .OrderBy(item => rnd.Next()) // Embaralha tudo
                                .Take(TamanhoDaRodada)       // Pega só as X primeiras
                                .ToList();
        }

        public Questao ObterProximaPergunta()
        {
            if (_questoesDaRodada != null && QuestaoAtualIndex < _questoesDaRodada.Count)
            {
                return _questoesDaRodada[QuestaoAtualIndex];
            }

            return null; // Acabaram as perguntas da rodada
        }

        public bool VerificarResposta(string resposta)
        {
            var q = _questoesDaRodada[QuestaoAtualIndex];
            bool acertou = (resposta == q.RespostaCorreta);

            if (acertou)
            {
                PontuacaoAtual += q.Pontos;
            }

            QuestaoAtualIndex++;
            return acertou;
        }
    }
}