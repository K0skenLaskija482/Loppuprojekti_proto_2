using UnityEngine;
using TMPro;

// Sahnede tek bir GameObject uzerine eklenmeli.
// P1/P2 skorlarini UI'da gosterir ve biri hedef puana ulasinca kazanma metnini acar.
// TextMeshPro kullanir (Window > TextMeshPro > Import TMP Essential Resources gerekebilir).
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Oyuncular")]
    public Player player1;
    public Player player2;

    [Header("Kazanma Kosulu")]
    public int scoreToWin = 50;

    [Header("UI")]
    public TMP_Text player1ScoreText;
    public TMP_Text player2ScoreText;
    public TMP_Text winMessageText; // oyun basinda kapali olmali
    public TMP_Text turnText;
    public TMP_Text diceText;

    public bool IsGameOver { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        if (winMessageText != null)
            winMessageText.gameObject.SetActive(false);

        player1.OnScoreChanged += HandleScoreChanged;
        player2.OnScoreChanged += HandleScoreChanged;

        UpdateScoreTexts();
    }

    private void OnDestroy()
    {
        if (player1 != null) player1.OnScoreChanged -= HandleScoreChanged;
        if (player2 != null) player2.OnScoreChanged -= HandleScoreChanged;
    }

    private void HandleScoreChanged(Player player)
    {
        UpdateScoreTexts();
        CheckWinCondition(player);
    }

    private void UpdateScoreTexts()
    {
        if (player1ScoreText != null)
            player1ScoreText.text = $"{player1.playerName}: {player1.score}";
        if (player2ScoreText != null)
            player2ScoreText.text = $"{player2.playerName}: {player2.score}";
    }

    private void CheckWinCondition(Player player)
    {
        if (IsGameOver) return;
        if (player.score < scoreToWin) return;

        IsGameOver = true;

        if (winMessageText != null)
        {
            winMessageText.gameObject.SetActive(true);
            winMessageText.text = $"{player.playerName} Wins!";
        }

        Debug.Log($"{player.playerName} wins! Score: {player.score}");
    }

    // TurnManager tarafindan sira degistiginde cagrilir.
    public void UpdateTurnText(Player player)
    {
        if (turnText != null)
            turnText.text = $"{player.playerName}'s turn";
    }

    // TurnManager tarafindan zar atildiginda cagrilir.
    public void UpdateDiceText(Player player, int roll)
    {
        if (diceText != null)
            diceText.text = $"{player.playerName} rolled {roll}";
    }
}
