using System.Reflection;
using System.ComponentModel;

namespace FiasMusikArkiv.Server.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription<T>(this T enumValue)
        {
            var valueString = enumValue.ToString();
            var valueMember = enumValue.GetType().GetMember(valueString)[0];
            var descriptionAttribute = valueMember.GetCustomAttributes().SingleOrDefault(a => a.GetType() == typeof(DescriptionAttribute)) as DescriptionAttribute;

            var description =
                descriptionAttribute != null ? descriptionAttribute.Description :
                string.Empty;

            return description;
        }
    }
}
