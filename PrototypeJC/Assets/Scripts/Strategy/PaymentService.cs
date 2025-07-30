using UnityEngine;

public class PaymentService: IService {
    private IPaymentStrategy strategy;

    public void Execute() {}

    public void Initialize(ServiceInitParamWrapper initParams) {}

    public bool TryPurchase(IPaymentStrategy strategy, int amountCents) {
        this.strategy = strategy;
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