namespace PaymentApp.Logger.Classes.Writers.Base
{
    public abstract class BaseWriter
    {
        protected const string DirName = "logs";
        protected abstract string GetFileName();
    }
}
