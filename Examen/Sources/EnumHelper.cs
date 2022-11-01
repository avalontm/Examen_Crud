
using Microsoft.AspNetCore.Mvc.Rendering;

public static class EnumHelper
{
    public static SelectList EnumToSelectList<TEnum>(this Type enumType, object selectedValue)
    {
        return new SelectList(Enum.GetValues(enumType).Cast<TEnum>().ToList().ToDictionary(n => n), "Key", "Value", selectedValue);
    }
}