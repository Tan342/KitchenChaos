using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject kitchenObject;
    [SerializeField] Transform iconTemplate;


    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        kitchenObject.OnIngredientAdded += KitchenObject_OnIngredientAdded;
    }

    private void KitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform transform in transform)
        {
            if (transform != iconTemplate)
            {
                Destroy(transform.gameObject);
            }
        }
        foreach(KitchenObjectSO kitchenObjectSO in kitchenObject.GetKitchenObjectSOList())
        {
            Transform iconTranform = Instantiate(iconTemplate, transform);
            iconTranform.gameObject.SetActive(true);
            iconTranform.GetComponent<PlateIconSingleUI>().SetKitchenObjectSO(kitchenObjectSO);
        }
    }
}
