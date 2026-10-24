namespace SalesDb.Domain.ValueObjects;

public readonly record struct Money
{
    public decimal Value { get; }

    public Money(decimal amount)
    {
        if (amount < 0) throw new ArgumentException("SỐ TIỀN KHÔNG ĐƯỢC NHỎ HƠN 0", nameof(amount));

        Value = amount;
    }
    // THAY VÌ Ở NHỮNG NƠI 
}