using PaymentApp.Logger.Classes.Enums;
using PaymentApp.Logger.Classes.Managers;

namespace PaymentApp.Logger
{
    public class LogWriter : IDisposable
    {
        private readonly LogTypeEnum inputType;
        private const string DirName = "logs";
        public LogWriter(LogTypeEnum type)
        {
            inputType = type;
        }

        public void Write(string message)
        {
            try
            {
                if (!Directory.Exists(DirName))
                    Directory.CreateDirectory(DirName);

                using (var writer = new WriterManager().GetWriter(inputType))
                {
                    writer.Write(message);
                }
            }
            catch (Exception exc)
            {
                throw new Exception(exc.Message);
            }
        }

        public void Dispose()
        {
            GC.Collect();
        }
    }
}
