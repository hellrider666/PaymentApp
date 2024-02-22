using PaymentApp.Logger.Classes.Interfaces;
using PaymentApp.Logger.Classes.Writers.Base;
using System.Xml;

namespace PaymentApp.Logger.Classes.Writers
{
    public class XMLWriter : BaseWriter, IWriter
    {
        private XmlNode body { get; set; }
        private XmlDocument document { get; set; }
        private XmlElement root { get; set; }
        private XmlDeclaration declaration { get; set; }

        public void Write(string message)
        {
            string fileName = GetFileName();

            document = new XmlDocument();

            if (File.Exists(fileName))
            {
                document.Load(fileName);
                body = document.SelectSingleNode("body");
            }
            else
                CreateRootNode();

            var messageTag = document.CreateElement("message");
            messageTag.InnerText = message;
            body.AppendChild(messageTag);

            document.Save(fileName);
        }

        private void CreateRootNode()
        {
            declaration = document.CreateXmlDeclaration("1.0", "UTF-8", null);
            root = document.DocumentElement;

            document.InsertBefore(declaration, root);
            body = document.CreateElement("body");

            document.AppendChild(body);
        }

        protected override string GetFileName()
        {
            var dateNow = DateTime.Now;
            string dateStr = string.Format("{0}-{1}-{2}", dateNow.Day, dateNow.Month, dateNow.Year);

            return DirName + $"/Log-{dateStr}.xml";
        }

        public void Dispose()
        {
            body = null;
            document = null;
            root = null;
            declaration = null;

            GC.Collect();
        }
    }
}
