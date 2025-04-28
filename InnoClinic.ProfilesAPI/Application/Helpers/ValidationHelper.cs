using Microsoft.AspNetCore.Http;

namespace Application.Helpers;

public static class ValidationHelper
{
    public static bool BeAValidImage(IFormFile? file)
    {
        if (file is null) return false;

        var allowedExtensions = new[] { ".jpg", ".png" };
        var extension = Path.GetExtension(file.FileName).ToLower();
        return allowedExtensions.Contains(extension);
    }

    public static bool BeAValidYear(string yearStr)
    {
        if (int.TryParse(yearStr, out var year))
        {
            var currentYear = DateTime.Today.Year;
            return year > 1900 && year <= currentYear;
        }
        return false;
    }

    public static TProperty GetPropertyValue<TProperty>(object obj, string propertyName)
    {
        var property = obj.GetType().GetProperty(propertyName);

        if (property is null) throw new ArgumentException($"Property '{propertyName}' not found on type '{obj.GetType().Name}'.");

        return (TProperty)property.GetValue(obj)!;
    }
}