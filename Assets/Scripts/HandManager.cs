using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using DG.Tweening;
using UnityEngine.InputSystem;

public class HandManager : MonoBehaviour
{
    [SerializeField] private int maxHandSize;
    [SerializeField] private GameObject cardPrefab;

    [SerializeField] private SplineContainer splineContainer;
    [SerializeField] private Transform spawnPoint;

    private List<GameObject> handCards = new List<GameObject>();

    private void Update() {
        if (Keyboard.current == null) return;

        if (Keyboard.current.downArrowKey.wasPressedThisFrame) {
            DrawCard();
        }

        if (Keyboard.current.upArrowKey.wasPressedThisFrame) {
            DiscardCard();
        }
    }
    private void DrawCard() {
        if (handCards.Count >= maxHandSize) return;

        GameObject c = Instantiate(cardPrefab, spawnPoint.position, spawnPoint.rotation);
        handCards.Add(c);
        UpdateCardPositions();
    }
    private void DiscardCard() {
        if (handCards.Count == 0) return;

        GameObject c = handCards[handCards.Count - 1];
        handCards.RemoveAt(handCards.Count - 1);
        Destroy(c);
        UpdateCardPositions();
    }
    private void UpdateCardPositions() {
        if (handCards.Count == 0) return;
        float cardSpacing = 1f / maxHandSize; 
        float firstCardPosition = 0.5f - (handCards.Count - 1) * cardSpacing / 2;
        Spline spline = splineContainer.Spline;

        for (int i = 0; i < handCards.Count; i++) {
            float p = firstCardPosition + i * cardSpacing;
            Vector3 splinePosition = spline.EvaluatePosition(p);
            Vector3 splineForward = spline.EvaluateTangent(p);
            Vector3 splineUp = spline.EvaluateUpVector(p);
            Quaternion rotation = Quaternion.LookRotation(splineUp, Vector3.Cross(splineForward, splineUp)).normalized;
            handCards[i].transform.DOMove(splinePosition, 0.5f);
            handCards[i].transform.DOLocalRotateQuaternion(rotation, 0.5f); 
        }
    }
}
