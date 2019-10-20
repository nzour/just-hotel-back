using System;

namespace App.Domain.ChatEntity
{
    public class CantAddMessageException : Exception
    {
        public CantAddMessageException(string message): base(message)
        {
        }

        public static CantAddMessageException UserHasNoAccessForChat()
        {
            return new CantAddMessageException("User has no access for chat.");
        }
    }
}