namespace GamesApi.Helpers
{
    public interface IPasswordHash
    {
        void createPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
    }
}
