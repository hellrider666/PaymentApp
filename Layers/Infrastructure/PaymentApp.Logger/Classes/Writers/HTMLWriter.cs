using PaymentApp.Logger.Classes.Interfaces;
using PaymentApp.Logger.Classes.Writers.Base;

namespace PaymentApp.Logger.Classes.Writers
{
    public class HTMLWriter : BaseWriter, IWriter
    {
        public void Write(string message)
        {
            string fileName = GetFileName();
            string content = $"<h3>{message}</h3>";

            if (File.Exists(fileName))
                File.AppendAllText(fileName, content);

            else
            {
                using (var html = File.Create(fileName)) { }
                File.WriteAllText(fileName, content);
            }
        }
        protected override string GetFileName()
        {
            var dateNow = DateTime.Now;
            string dateStr = string.Format("{0}-{1}-{2}", dateNow.Day, dateNow.Month, dateNow.Year);

            return DirName + $"/Log-{dateStr}.html";
        }
        public void Dispose()
        {
            GC.Collect();
        }
    }
}
