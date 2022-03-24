using System;

namespace BusinessLogic.BusinessExceptions
{
    public class ItemNotFoundException : BusinessException
    {
        public ItemNotFoundException() { }

        public ItemNotFoundException(string message) : base(message) { }

        public ItemNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}