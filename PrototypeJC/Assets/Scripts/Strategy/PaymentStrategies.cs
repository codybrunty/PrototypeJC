
public class CreditCardPaymentStrategy : IPaymentStrategy {
    private readonly FakeCreditCard type;
    public CreditCardPaymentStrategy(FakeCreditCard type) {
        this.type = type;
    }

    public bool Pay(int amountCents) {
        return type.Charge(amountCents);
    }
    public string PaymentName => "Credit Card";
}

public class PaypalPaymentStrategy : IPaymentStrategy {
    private readonly FakePaypal type;
    public PaypalPaymentStrategy(FakePaypal type) {
        this.type=type;
    } 
    public bool Pay(int amountCents) {
        return type.SendPayment(amountCents);
    }
    public string PaymentName => "Paypal";
}

public class ApplePayPaymentStrategy : IPaymentStrategy {
    private readonly FakeApplePay type;
    public ApplePayPaymentStrategy(FakeApplePay type) {
        this.type=type;
    }
    public bool Pay(int amountCents) {
        return type.Process(amountCents);
    }
    public string PaymentName => "Apple Pay";
}
