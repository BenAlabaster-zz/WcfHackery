using System;
using System.Configuration;

namespace WcfRestAuthentication.Authentication
{
    public class MyConfigurationProvider : IConfigurationProvider
    {
        public TOut GetMandatoryAppSetting<TOut>(string key)
        {
            var setting = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrWhiteSpace(setting))
                throw new ConfigurationErrorsException(string.Format("Missing expected appSetting '{0}' in configuration.", key));

            return ConvertValue<TOut>(setting);
        }

        public TOut GetOptionalAppSetting<TOut>(string key, TOut defaultValue)
        {
            var value = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrWhiteSpace(value))
                return defaultValue;

            return ConvertValue<TOut>(value);
        }

        private TOut ConvertValue<TOut>(string setting)
        {
            if (string.IsNullOrWhiteSpace(setting))
                return default(TOut);

            var t = typeof (TOut).UnderlyingSystemType;
            if (t.IsEnum)
                return (TOut) Enum.Parse(t, setting);

            return (TOut) Convert.ChangeType(setting, Nullable.GetUnderlyingType(t) ?? t);
        }
    }
}