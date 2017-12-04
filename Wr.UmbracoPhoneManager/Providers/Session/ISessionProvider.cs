namespace Wr.UmbracoPhoneManager.Providers
{
    public interface ISessionProvider
    {
        T GetSession<T>(string key = "") where T : class, new();
        bool SetSession<T>(T model, string key = "");
    }
}