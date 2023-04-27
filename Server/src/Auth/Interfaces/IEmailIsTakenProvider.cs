namespace Auth.Interfaces
{
    internal interface IEmailIsTakenProvider
    {
        Task<bool> IsTakenAsync(string email);
    }
}
