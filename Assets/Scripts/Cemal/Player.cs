using System;
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

    // Skor her degistiginde tetiklenir; GameManager buna abone olup UI'yi gunceller.
    public event Action<Player> OnScoreChanged;

    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log($"{playerName} score: {score} ({(amount >= 0 ? "+" : "")}{amount})");
        OnScoreChanged?.Invoke(this);
    }

    // Portal node'u tarafindan cagrilir: mevcut toplam puani 2'ye katlar.
    public void DoubleScore()
    {
        score *= 2;
        Debug.Log($"{playerName} score doubled: {score}");
        OnScoreChanged?.Invoke(this);
    }

    public void SetStartNode(BoardNode node)
    {
        currentNode = node;
        transform.position = node.transform.position;
    }
}
