public interface IPaymentStrategy {
    string PaymentName { get; }
    bool Pay(int amount);
}
