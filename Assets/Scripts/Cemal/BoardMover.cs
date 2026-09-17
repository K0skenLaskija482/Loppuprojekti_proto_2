using System;
using System.Collections;
using UnityEngine;

// Sahnede TEK bir GameObject uzerine eklenmeli (singleton).
// Zar sonucu kadar node-node ilerlemeyi, kavsaklarda oyuncuya sectirmeyi
// ve piyonun gorsel hareketini yonetir.
public class BoardMover : MonoBehaviour
{
    public static BoardMover Instance { get; private set; }

    private BoardNode chosenNode;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // BoardNode.OnMouseDown() tarafindan cagrilir, sadece highlight acikken calisir.
    public void OnNodeChosen(BoardNode node)
    {
        chosenNode = node;
    }

    // TurnManager tarafindan cagrilir: player'i steps kadar node ilerletir.
    public IEnumerator MoveSteps(Player player, int steps, Action onFinished = null)
    {
        BoardNode current = player.currentNode;

        for (int i = 0; i < steps; i++)
        {
            if (current.nextNodes == null || current.nextNodes.Count == 0)
                break; // yolun sonu, daha fazla ilerlenemez

            BoardNode next;

            if (current.nextNodes.Count == 1)
            {
                next = current.nextNodes[0];
            }
            else
            {
                // Kavsak: birden fazla yol var, oyuncuya tiklattirarak sectir.
                chosenNode = null;

                foreach (var candidate in current.nextNodes)
                    candidate.SetHighlight(true);

                yield return new WaitUntil(() => chosenNode != null);

                foreach (var candidate in current.nextNodes)
                    candidate.SetHighlight(false);

                next = chosenNode;
            }

            yield return StartCoroutine(MoveVisual(player, current, next));
            current = next;
        }

        player.currentNode = current;
        current.OnLand(player);

        onFinished?.Invoke();
    }

    private IEnumerator MoveVisual(Player player, BoardNode from, BoardNode to)
    {
        Vector3 start = from.transform.position;
        Vector3 end = to.transform.position;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * player.moveSpeed;
            player.transform.position = Vector3.Lerp(start, end, t);
            yield return null;
        }

        player.transform.position = end;
    }
}
