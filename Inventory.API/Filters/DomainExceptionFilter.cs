using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Inventory.Domain.Exceptions;
using Maintrols.Shared.SharedKernel.Primitives;

namespace Inventory.API.Filters;

public class DomainExceptionFilter : IExceptionFilter
{
    private static readonly Dictionary<string, string> EnTranslations = new()
    {
        { "InvalidCategoryName", "Invalid category name." },
        { "InvalidCategoryDescription", "Invalid category description." },
        { "InvalidCategoryImageUrl", "Invalid category image URL." },
        { "InvalidCategoryProductCategoryId", "Invalid category ID for category product." },
        { "InvalidCategoryProductProductId", "Invalid product ID for category product." },
        { "InvalidClientName", "Invalid client name." },
        { "InvalidClientPhoneNumber", "Invalid client phone number." },
        { "InvalidInvoiceClientId", "Invalid client ID for invoice." },
        { "InvalidInvoiceTotalAmount", "Invalid invoice total amount." },
        { "InvalidInvoiceTotalDiscount", "Invalid invoice total discount." },
        { "InvalidInvoicePaidAmount", "Invalid invoice paid amount." },
        { "InvalidInvoiceProductSellingPrice", "Invalid selling price for invoice product." },
        { "InvalidInvoiceProductActualSellingPrice", "Invalid actual selling price for invoice product." },
        { "InvalidInvoiceProductInvoiceId", "Invalid invoice ID for invoice product." },
        { "InvalidInvoiceProductProductId", "Invalid product ID for invoice product." },
        { "InvalidInvoiceProductStockProductId", "Invalid stock product ID for invoice product." },
        { "InvalidLocationPointName", "Invalid location point name." },
        { "InvalidProductCode", "Invalid product code." },
        { "InvalidProductBarcode", "Invalid product barcode." },
        { "InvalidProductName", "Invalid product name." },
        { "InvalidProductDescription", "Invalid product description." },
        { "InvalidProductImageUrl", "Invalid product image URL." },
        { "InvalidPurchaseVendorId", "Invalid vendor ID for purchase." },
        { "InvalidStockProductProductPrice", "Invalid product price in stock." },
        { "InvalidStockProductShippingCost", "Invalid shipping cost for stock product." },
        { "InvalidStockProductSellingPrice", "Invalid selling price for stock product." },
        { "InvalidStockProductPurchaseId", "Invalid purchase ID for stock product." },
        { "InvalidStockProductProductId", "Invalid product ID for stock product." },
        { "InvalidStockProductLocationPointId", "Invalid location point ID for stock product." },
        { "InvalidVendorName", "Invalid vendor name." },
        { "EntityNotFound", "Requested entity was not found." },
        { "UnexpectedError", "An unexpected error occurred." }
    };

    private static readonly Dictionary<string, string> ArTranslations = new()
    {
        { "InvalidCategoryName", "اسم القسم غير صالح." },
        { "InvalidCategoryDescription", "وصف القسم غير صالح." },
        { "InvalidCategoryImageUrl", "رابط صورة القسم غير صالح." },
        { "InvalidCategoryProductCategoryId", "معرف القسم غير صالح لقسم المنتج." },
        { "InvalidCategoryProductProductId", "معرف المنتج غير صالح لقسم المنتج." },
        { "InvalidClientName", "اسم العميل غير صالح." },
        { "InvalidClientPhoneNumber", "رقم هاتف العميل غير صالح." },
        { "InvalidInvoiceClientId", "معرف العميل غير صالح للفاتورة." },
        { "InvalidInvoiceTotalAmount", "المبلغ الإجمالي للفاتورة غير صالح." },
        { "InvalidInvoiceTotalDiscount", "إجمالي خصم الفاتورة غير صالح." },
        { "InvalidInvoicePaidAmount", "المبلغ المدفوع للفاتورة غير صالح." },
        { "InvalidInvoiceProductSellingPrice", "سعر البيع لمنتج الفاتورة غير صالح." },
        { "InvalidInvoiceProductActualSellingPrice", "سعر البيع الفعلي لمنتج الفاتورة غير صالح." },
        { "InvalidInvoiceProductInvoiceId", "معرف الفاتورة لمنتج الفاتورة غير صالح." },
        { "InvalidInvoiceProductProductId", "معرف المنتج لمنتج الفاتورة غير صالح." },
        { "InvalidInvoiceProductStockProductId", "معرف مخزون المنتج لمنتج الفاتورة غير صالح." },
        { "InvalidLocationPointName", "اسم نقطة الموقع غير صالح." },
        { "InvalidProductCode", "رمز المنتج غير صالح." },
        { "InvalidProductBarcode", "باركود المنتج غير صالح." },
        { "InvalidProductName", "اسم المنتج غير صالح." },
        { "InvalidProductDescription", "وصف المنتج غير صالح." },
        { "InvalidProductImageUrl", "رابط صورة المنتج غير صالح." },
        { "InvalidPurchaseVendorId", "معرف المورد للمشتريات غير صالح." },
        { "InvalidStockProductProductPrice", "سعر المنتج في المخزن غير صالح." },
        { "InvalidStockProductShippingCost", "تكلفة الشحن لمنتج المخزن غير صالح." },
        { "InvalidStockProductSellingPrice", "سعر البيع لمنتج المخزن غير صالح." },
        { "InvalidStockProductPurchaseId", "معرف الشراء لمنتج المخزن غير صالح." },
        { "InvalidStockProductProductId", "معرف المنتج لمنتج المخزن غير صالح." },
        { "InvalidStockProductLocationPointId", "معرف نقطة الموقع لمنتج المخزن غير صالح." },
        { "InvalidVendorName", "اسم المورد غير صالح." },
        { "EntityNotFound", "الكيان المطلوب غير موجود." },
        { "UnexpectedError", "حدث خطأ غير متوقع." }
    };

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
        var translations = isArabic ? ArTranslations : EnTranslations;
        if (translations.TryGetValue(key, out var translation))
        {
            return translation;
        }
        // Fallback to key or English
        if (!isArabic && EnTranslations.TryGetValue(key, out var enTranslation))
        {
            return enTranslation;
        }
        return key;
    }
}
