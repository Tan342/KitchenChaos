using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCounterVisual : MonoBehaviour
{
    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject visualSelectedCounter; 

    private void Start()
    {
        Player.Instance.onSelectedCounterChanged += Player_onSelectedCounterChanged;
    }

    private void Player_onSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if(clearCounter == e.selectedCounter)
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
        visualSelectedCounter.SetActive(true);
    }

    private void Hide()
    {
        visualSelectedCounter.SetActive(false);
    }
}
