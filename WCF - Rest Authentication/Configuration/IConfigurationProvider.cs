namespace WcfRestAuthentication.Authentication
{
    public interface IConfigurationProvider
    {
        TOut GetMandatoryAppSetting<TOut>(string key);
        TOut GetOptionalAppSetting<TOut>(string key, TOut defaultValue);
    }
}