using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour{

    [SerializeField] private int price;
    PaymentService paymentService;
    private void Start() {
        paymentService = (PaymentService)ServiceLocator.GetService<PaymentService>();
    }

    public void Buy() {
        paymentService.TryPurchase(new CreditCardPaymentStrategy(new FakeCreditCard()),price);
    }
}
