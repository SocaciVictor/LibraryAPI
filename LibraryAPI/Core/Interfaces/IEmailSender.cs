﻿namespace LibraryAPI.Core.Interfaces
{
    public interface IEmailSender
    {
        Task SendAsync(string to, string subject, string htmlMessage);
    }
}
