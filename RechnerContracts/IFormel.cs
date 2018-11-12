namespace RechnerContracts
{
    public interface IFormel
    {
        double ZahlLinks { get; }
        double ZahlRechts { get; }
        char Symbol { get; }
    }
}