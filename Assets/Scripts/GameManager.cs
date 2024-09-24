using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int totalRounds = 10; // Total de rodadas
    private int currentRound = 0;

    public float roundDuration = 60f; // Duração da rodada em segundos
    private bool isRoundActive = false;

    public SlotHandler slotHandler;

    public MessageHandler messageHandler;

    public float happiness = 0.6f;

    public Slider happinessSlider;
    public Text resourceLabel;

    public static GameManager Instance;

    public int resources;

    private void Awake()
    {
        Instance = this;
    }

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

            // Escolhe um evento aleatório
            if (currentRound != 1)
                TriggerRandomEvent();

            if (currentRound == totalRounds)
                EndGame();

            if(slotHandler.HasNoParks())
            {
                happiness -= 0.1f;
            }
            else
            {
                happiness += 0.1f;
            }

            slotHandler.StartRound();

            isRoundActive = true;

            // Espera a rodada acabar, seja por tempo ou manualmente
            float elapsedTime = 0f;
            while (isRoundActive && elapsedTime < roundDuration)
            {
                resourceLabel.text = resources.ToString();
                happinessSlider.value = happiness;
                elapsedTime += Time.deltaTime;
                yield return null; // Espera o próximo frame
            }

            Debug.Log("Rodada " + currentRound + " terminada!");

            // Aguarda um segundo antes de iniciar a próxima rodada
            yield return new WaitForSeconds(1);
        }

        Debug.Log("Fim do jogo!");
    }

    // Função pública para encerrar manualmente a rodada
    public void EndRound()
    {
        if (isRoundActive)
        {
            isRoundActive = false;
            Debug.Log("Rodada terminada manualmente.");
        }
    }

    private void EndGame()
    {

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
        // for each event possible, make a custom message and title

        messageHandler.ShowMessage("title", "message");
    }

    // Executa o evento selecionado
    void TriggerRandomEvent()
    {
        int number = Random.Range(0, 5);

        Debug.Log("Evento da rodada: " + number);

        switch (number)
        {
            case 0:
                // Coloque aqui o efeito do aumento de custo
                ShowEvent(Eventos.corte);
                break;

            case 1:
                // Coloque aqui o efeito do corte de orçamento
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
