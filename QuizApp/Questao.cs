using System.Collections.Generic;

namespace QuizApp
{
    public class Questao
    {
        public string Categoria { get; set; } = string.Empty;
        public string PerguntaTexto { get; set; } = string.Empty;
        public List<string> Alternativas { get; set; } = new List<string>();
        public string RespostaCorreta { get; set; } = string.Empty;
        public int Pontos { get; set; }

        // Construtor vazio necessário para o JSON
        public Questao() { }

        // Construtor manual (opcional agora, mas mantivemos)
        public Questao(string categoria, string pergunta, string correta, List<string> erradas, int pontos = 10)
        {
            Categoria = categoria;
            PerguntaTexto = pergunta;
            RespostaCorreta = correta;
            Pontos = pontos;
            Alternativas = new List<string>(erradas);
            Alternativas.Add(correta);
        }
    }
}