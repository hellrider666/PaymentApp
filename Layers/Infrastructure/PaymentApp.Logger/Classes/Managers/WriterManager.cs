using PaymentApp.Logger.Classes.Enums;
using PaymentApp.Logger.Classes.Interfaces;
using PaymentApp.Logger.Classes.Writers;

namespace PaymentApp.Logger.Classes.Managers
{
    internal class WriterManager
    {
        internal IWriter GetWriter(LogTypeEnum logType)
        {
            switch (logType)
            {
                case LogTypeEnum.HTML:
                    return new HTMLWriter();
                case LogTypeEnum.XML:
                    return new XMLWriter();
                case LogTypeEnum.TXT:
                    return new TXTWriter();
                default:
                    throw new ArgumentException("Log Type is not supported");
            }
        }
    }
}
