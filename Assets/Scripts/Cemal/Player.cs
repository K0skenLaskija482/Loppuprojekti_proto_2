using UnityEngine;

// Oyuncu piyonu. Sahnede: bir GameObject (SpriteRenderer ile piyon gorseli),
// uzerinde bu script. Baslangicta SetStartNode ile ilk node'a yerlestirilir.
public class Player : MonoBehaviour
{
    [Header("Kimlik")]
    public string playerName = "Oyuncu 1";

    [Header("Durum")]
    public BoardNode currentNode;
    public int score = 0;

    [Header("Hareket")]
    public float moveSpeed = 4f;

    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log($"{playerName} puan: {score} ({(amount >= 0 ? "+" : "")}{amount})");
        // TODO: Buraya UI skor gostergesini guncelleyen bir event/cagri ekleyebilirsin.
    }

    public void SetStartNode(BoardNode node)
    {
        currentNode = node;
        transform.position = node.transform.position;
    }
}
