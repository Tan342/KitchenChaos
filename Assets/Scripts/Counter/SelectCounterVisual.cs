using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualSelectedCounter; 

    private void Start()
    {
        Player.Instance.onSelectedCounterChanged += Player_onSelectedCounterChanged;
    }

    private void Player_onSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if(baseCounter == e.selectedCounter)
        {
            Show();
        }
        else 
        { 
            Hide();
        }
    }

    private void Show()
    {
        foreach(GameObject go in visualSelectedCounter)
        {
            go.SetActive(true);
        }
    }

    private void Hide()
    {
        foreach (GameObject go in visualSelectedCounter)
        {
            go.SetActive(false);
        }
    }
}
