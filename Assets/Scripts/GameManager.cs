using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int totalRounds = 10; // Total de rodadas
    private int currentRound = 0;
    
    public int GetCurrentRound()
    {
        return currentRound;
    }

    public float roundDuration = 180f; // Dura��o da rodada em segundos
    private bool isRoundActive = false;

    public SlotHandler slotHandler;

    public MessageHandler messageHandler;

    public float happiness = 0.6f;

    public Slider happinessSlider;
    public Text resourceLabel;

    public static GameManager Instance;

    public int resources;

    public Dictionary<Slot.State, int> priceDictionary;

    private void Awake()
    {
        Instance = this;

        priceDictionary = new Dictionary<Slot.State, int>();

        priceDictionary[Slot.State.precaria] = 8;
        priceDictionary[Slot.State.reformando] = 10;
        priceDictionary[Slot.State.parque] = 8;
    }

    public SpriteRenderer grass;

    void Start()
    {
        StartCoroutine(RoundSystem());
    }

    IEnumerator RoundSystem()
    {
        while (currentRound < totalRounds)
        {
            currentRound++;
            Debug.Log("Rodada " + currentRound + " iniciada!");

            resources += 10;

            slotHandler.StartRound();

            // Escolhe um evento aleat�rio
            if (currentRound != 1 || currentRound != totalRounds)
                TriggerRandomEvent();

            if (currentRound == totalRounds)
            {
                var states = slotHandler.GetChildrenStates();

                int casas = 0;
                int reformas = 0;
                int precarias = 0;
                int parques = 0;

                if (states.ContainsKey(Slot.State.pronta))
                    casas = states[Slot.State.pronta];
                if (states.ContainsKey(Slot.State.reformando))
                    reformas = states[Slot.State.reformando];
                if (states.ContainsKey(Slot.State.precaria))
                    precarias = states[Slot.State.precaria];
                if (states.ContainsKey(Slot.State.parque))
                    parques = states[Slot.State.parque];

                int pontos = casas * 100 + reformas * 50 + parques * 20 + precarias * 10;

                EndGame("Fim de Jogo!", "Voc� construiu " + precarias + " moradia(s) prec�ria(s), deixou " + reformas + " em reforma, fez " + casas + " moradia(s) digna(s) e " + parques + " parque(s). Totalizando " + pontos + " pontos.");
                break;
            }

            if (slotHandler.HasNoParks())
            {
                happiness -= 0.1f;
                if (happiness <= 0)
                {
                    EndGame("Protesto!", "Os moradores ficaram muito infelizes, eles organizaram um protesto contra voc�!");
                }
            }
            else
            {
                happiness -= slotHandler.HouseParkRatio();
            }

            isRoundActive = true;

            // Espera a rodada acabar, seja por tempo ou manualmente
            float elapsedTime = 0f;
            while (isRoundActive && elapsedTime < roundDuration)
            {
                resourceLabel.text = resources.ToString();
                happinessSlider.value = happiness;
                grass.color = new Color(1, 1, 1, happiness);
                elapsedTime += Time.deltaTime;
                yield return null; // Espera o pr�ximo frame
            }

            Debug.Log("Rodada " + currentRound + " terminada!");

            // Aguarda um segundo antes de iniciar a pr�xima rodada
        }

        Debug.Log("Fim do jogo!");
    }

    // Fun��o p�blica para encerrar manualmente a rodada
    public void EndRound()
    {
        if (isRoundActive)
        {
            isRoundActive = false;
            Debug.Log("Rodada terminada manualmente.");
        }
    }

    private void EndGame(string title, string message)
    {
        messageHandler.SendEnding(title, message);
    }

    public enum Eventos
    {
        corte,
        subsidio,
        inflacao,
        barateamento,
        investimento,
        enchente,
    }

    private void ShowEvent(Eventos evento)
    {
        string title = "";
        string message = "";

        switch (evento)
        {
            case Eventos.corte:
                title = "Corte de Or�amento";
                message = "Houve um corte significativo no or�amento, voc� perdeu alguns recursos.";
                resources -= 5; // Exemplo de efeito
                break;

            case Eventos.subsidio:
                title = "Subs�dio Concedido";
                message = "O governo concedeu um subs�dio, voc� ganhou 10 recursos adicionais.";
                resources += 10; // Exemplo de efeito
                break;

            case Eventos.inflacao:
                title = "Infla��o Alta";
                message = "Os pre�os aumentaram devido � infla��o, ser� mais caro construir.";
                foreach (var key in priceDictionary.Keys.ToList())
                {
                    priceDictionary[key] += 2; // Aumenta os pre�os
                }
                break;

            case Eventos.barateamento:
                title = "Barateamento";
                message = "Os custos de constru��o diminu�ram, agora est� mais barato construir.";
                foreach (var key in priceDictionary.Keys.ToList())
                {
                    priceDictionary[key] -= 2; // Diminui os pre�os
                }
                break;

            case Eventos.investimento:
                title = "Investimento Externo";
                message = "Um investidor externo est� disposto a financiar suas constru��es. 10 recursos extras foram adicionados.";
                resources += 10; // Exemplo de efeito
                break;

            case Eventos.enchente:
                title = "Enchente!";
                message = "Uma enchente destruiu algumas moradias. Ser� necess�rio reconstruir.";
                // Aqui voc� pode definir como a enchente afeta o jogo, como reduzir o n�mero de moradias
                slotHandler.Enchente(); // Exemplo de fun��o para destruir moradias
                break;

            default:
                title = "Evento Desconhecido";
                message = "Algo inesperado aconteceu!";
                break;
        }

        messageHandler.ShowMessage(title, message);
    }


    // Executa o evento selecionado
    void TriggerRandomEvent()
    {
        int number = Random.Range(0, 6);

        Debug.Log("Evento da rodada: " + number);

        switch (number)
        {
            case 0:
                // Coloque aqui o efeito do aumento de custo
                ShowEvent(Eventos.corte);
                break;

            case 1:
                // Coloque aqui o efeito do corte de or�amento
                ShowEvent(Eventos.subsidio);
                break;

            case 2:
                // Coloque aqui o efeito do protesto
                ShowEvent(Eventos.inflacao);
                break;

            case 3:
                // Coloque aqui o efeito do desastre
                ShowEvent(Eventos.barateamento);
                break;

            case 4:
                // Coloque aqui o efeito do desastre
                ShowEvent(Eventos.investimento);
                break;

            case 5:
                // Coloque aqui o efeito do desastre
                ShowEvent(Eventos.enchente);
                break;

            default:
                Debug.Log("Nenhum evento aconteceu.");
                break;
        }
    }
}
