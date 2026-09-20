using UnityEngine;

// Sahnede tek bir GameObject uzerine eklenmeli.
// Zar atma butonunu buna baglayacaksin (Button OnClick -> RollDiceAndMove).
public class TurnManager : MonoBehaviour
{
    [Header("Oyuncular (2 kisilik icin 2 eleman)")]
    public Player[] players;

    public int currentPlayerIndex = 0;
    private bool isMoving = false;

    public Player CurrentPlayer => players[currentPlayerIndex];

    private void Start()
    {
        GameManager.Instance?.UpdateTurnText(CurrentPlayer);
    }

    // UI butonuna veya bir input olayina baglanacak metot.
    public void RollDiceAndMove()
    {
        if (isMoving) return;
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver) return;

        int roll = Random.Range(1, 7); // 1-6 arasi zar
        Debug.Log($"{CurrentPlayer.playerName} rolled: {roll}");
        GameManager.Instance?.UpdateDiceText(CurrentPlayer, roll);

        isMoving = true;
        StartCoroutine(BoardMover.Instance.MoveSteps(CurrentPlayer, roll, EndTurn));
    }

    private void EndTurn()
    {
        isMoving = false;
        currentPlayerIndex = (currentPlayerIndex + 1) % players.Length;
        Debug.Log($"Turn: {CurrentPlayer.playerName}");
        GameManager.Instance?.UpdateTurnText(CurrentPlayer);
    }
}
