namespace PaymentApp.Logger.Classes.Interfaces
{
    public interface IWriter : IDisposable
    {
        void Write(string message);
    }
}
