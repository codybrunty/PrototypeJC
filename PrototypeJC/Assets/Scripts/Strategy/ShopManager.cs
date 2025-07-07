using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour{

    [SerializeField] private int price;
    PaymentService paymentService;
    private void Start() {
        paymentService = (PaymentService)ServiceLocator.GetService<PaymentService>();
    }
    public void UseCreditCard() {
        paymentService.SetPaymentMethod(new CreditCardPaymentStrategy(new FakeCreditCard()));
    }
    public void UsePaypal() {
        paymentService.SetPaymentMethod(new PaypalPaymentStrategy(new FakePaypal()));
    }
    public void UseApplyPay() {
        paymentService.SetPaymentMethod(new ApplePayPaymentStrategy(new FakeApplePay()));
    }

    public void Buy() {
        paymentService.TryPurchase(price);
    }
}
