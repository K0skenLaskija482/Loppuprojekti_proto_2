using System.Collections.Generic;
using UnityEngine;

// Tahtadaki her adim noktasini temsil eder.
// Sahnede: bir GameObject, uzerinde bu script, bir Collider2D (tiklama icin)
// ve highlightObject olarak atanacak "secilebilir" gorselini gosteren cocuk obje.
public enum NodeType { Normal, Bonus, Penalty, Portal }

[RequireComponent(typeof(Collider2D))]
public class BoardNode : MonoBehaviour
{
    [Header("Baglantilar")]
    [Tooltip("Bu node'dan gidilebilecek diger node'lar. Duz yolda 1 eleman, kavsakta birden fazla.")]
    public List<BoardNode> nextNodes = new List<BoardNode>();

    [Header("Puan / Tip")]
    public NodeType type = NodeType.Normal;
    public int scoreValue = 10;

    [Header("Gorsel")]
    [Tooltip("Oyuncu bu node'a gidebilecegi zaman aktif edilecek highlight objesi (glow, ok, cember vb).")]
    public GameObject highlightObject;

    private bool isSelectable = false;

    // Bonus ve Portal etkileri sadece ilk basista uygulanir, sonraki gelislerde hicbir sey olmaz.
    private bool effectUsed = false;

    private void Awake()
    {
        if (highlightObject != null)
            highlightObject.SetActive(false);
    }

    // BoardMover kavsakta secim beklerken bu node'lari secilebilir yapar.
    public void SetHighlight(bool state)
    {
        isSelectable = state;
        if (highlightObject != null)
            highlightObject.SetActive(state);
    }

    // 2D collider'a tiklandiginda calisir (Camera.main gerektirir, EventSystem gerekmez).
    private void OnMouseDown()
    {
        if (!isSelectable) return;
        BoardMover.Instance.OnNodeChosen(this);
    }

    // Oyuncu bu node'a vardiginda cagrilir.
    public void OnLand(Player player)
    {
        switch (type)
        {
            case NodeType.Normal:
                // Normal node her basista puan verir.
                player.AddScore(scoreValue);
                break;

            case NodeType.Bonus:
                // Bonus node sadece ilk basista puan verir, sonraki gelislerde hicbir sey olmaz.
                if (!effectUsed)
                {
                    player.AddScore(scoreValue * 2);
                    effectUsed = true;
                }
                break;

            case NodeType.Penalty:
                player.AddScore(-scoreValue);
                break;

            case NodeType.Portal:
                // Portal sadece ilk basista oyuncunun toplam puanini 2'ye katlar, sonraki gelislerde hicbir sey olmaz.
                if (!effectUsed)
                {
                    player.DoubleScore();
                    effectUsed = true;
                }
                break;
        }
    }
}
