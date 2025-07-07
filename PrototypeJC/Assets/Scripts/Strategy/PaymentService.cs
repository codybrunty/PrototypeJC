using UnityEngine;

public class PaymentService: IService {
    private IPaymentStrategy strategy;

    public PaymentService() {
        //default to credit card
        SetPaymentMethod(new CreditCardPaymentStrategy(new FakeCreditCard()));
    }

    public void Execute() {}

    public void Initialize(ServiceInitParamWrapper initParams) {}

    public void SetPaymentMethod(IPaymentStrategy strategy) {
        this.strategy = strategy;
        Debug.Log("PaymentService: Strategy Updated to "+strategy.PaymentName);
    }

    public bool TryPurchase(int amountCents) {
        bool result = strategy.Pay(amountCents);
        if (result) {
            Debug.Log("PaymentService: "+ strategy.PaymentName + " Succeeded!");
        }
        else {
            Debug.LogWarning("PaymentService: "+ strategy.PaymentName + " Failed.");
        }
        return result;
    }
}