using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Inventory.Domain.Exceptions;
using Inventory.API.Common;
using System.Text.Json;

namespace Inventory.API.Filters;

public class DomainExceptionFilter : IExceptionFilter
{
    private static readonly Dictionary<string, Dictionary<string, string>> Translations = new();

    static DomainExceptionFilter()
    {
        LoadTranslations("en");
        LoadTranslations("ar");
    }

    private static void LoadTranslations(string lang)
    {
        try
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, "Translations", $"{lang}.json");
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                var dict = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
                if (dict != null)
                {
                    Translations[lang] = dict;
                }
            }
        }
        catch
        {
            // Fallback if loading fails
        }
    }

    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception;
        var acceptLanguage = context.HttpContext.Request.Headers["Accept-Language"].ToString();
        var isArabic = acceptLanguage.StartsWith("ar", StringComparison.OrdinalIgnoreCase);

        if (exception is DomainException domainException)
        {
            var key = domainException.Message;
            var errorMessage = GetTranslation(key, isArabic);
            var result = Result.Failure(errorMessage);
            context.Result = new BadRequestObjectResult(result);
            context.ExceptionHandled = true;
        }
        else if (exception is KeyNotFoundException)
        {
            var errorMessage = GetTranslation("EntityNotFound", isArabic);
            var result = Result.Failure(errorMessage);
            context.Result = new NotFoundObjectResult(result);
            context.ExceptionHandled = true;
        }
        else
        {
            var errorMessage = GetTranslation("UnexpectedError", isArabic);
            var result = Result.Failure(errorMessage);
            context.Result = new ObjectResult(result)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
            context.ExceptionHandled = true;
        }
    }

    private static string GetTranslation(string key, bool isArabic)
    {
        var lang = isArabic ? "ar" : "en";
        if (Translations.TryGetValue(lang, out var langDict) && langDict.TryGetValue(key, out var translation))
        {
            return translation;
        }

        // Fallback to English
        if (lang != "en" && Translations.TryGetValue("en", out var enDict) && enDict.TryGetValue(key, out var enTranslation))
        {
            return enTranslation;
        }

        return key;
    }
}
