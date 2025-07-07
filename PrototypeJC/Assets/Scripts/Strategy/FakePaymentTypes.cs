using UnityEngine;

public class FakeCreditCard {
    public bool Charge(int amount) {
        if (amount > 0) {
            Debug.Log("Success: FakeCreditCard Charge " + amount);
            return true;
        }
        else {
            Debug.LogWarning("Fail: FakeCreditCard Charge " + amount);
            return false;
        }

    }
}
public class FakePaypal {
    public bool SendPayment(int amount) {
        if (amount > 0) {
            Debug.Log("Success: FakePaypal SendPayment " + amount);
            return true;
        }
        else {
            Debug.LogWarning("Fail: FakePaypal SendPayment " + amount);
            return false;
        }

    }
}
public class FakeApplePay {
    public bool Process(int amount) {
        if (amount > 0) {
            Debug.Log("Success: FakeApplePay Process " + amount);
            return true;
        }
        else {
            Debug.LogWarning("Fail: FakeApplePay Process " + amount);
            return false;
        }

    }
}