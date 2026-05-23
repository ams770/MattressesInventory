using Inventory.Domain.Exceptions;

namespace Inventory.Domain.Entities;

public class CategoryProduct : Entity<Guid>
{
    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; } = null!;
    public Guid ProductId { get; private set; }
    public Product Product { get; private set; } = null!;

    private CategoryProduct()
    {
    }

    public static CategoryProduct Create(Guid categoryId, Guid productId)
    {
        ValidateCategoryId(categoryId);
        ValidateProductId(productId);

        return new CategoryProduct
        {
            Id = Guid.NewGuid(),
            CategoryId = categoryId,
            ProductId = productId
        };
    }

    // Setters (if needed; usually category and product IDs aren't mutated on join entities, but let's provide them for completeness)
    public void SetCategoryId(Guid categoryId)
    {
        ValidateCategoryId(categoryId);
        CategoryId = categoryId;
    }

    public void SetProductId(Guid productId)
    {
        ValidateProductId(productId);
        ProductId = productId;
    }

    // Validators
    private static void ValidateCategoryId(Guid categoryId)
    {
        if (categoryId == Guid.Empty)
            throw new CategoryProductExceptions.InValidCategoryIdException();
    }

    private static void ValidateProductId(Guid productId)
    {
        if (productId == Guid.Empty)
            throw new CategoryProductExceptions.InValidProductIdException();
    }
}