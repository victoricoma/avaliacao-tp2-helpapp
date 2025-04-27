using FluentAssertions;
using HelpApp.Domain.Entities;
using Xunit;

namespace HelpApp.Domain.Test
{
    public class ProductUnitTest
    {
        #region Testes Positivos

        [Fact(DisplayName = "Create Product With Parameters Full")]
        public void CreateProduct_WithValidParameters_ResultObjectValisState()
        {
            Action action = () => new Product(1,
                "Product Name",
                "Product Description",
                9.99m, 99,
                "https://img/product.jpg");
            action.Should()
                .NotThrow<HelpApp.Domain.Validation.DomainExceptionValidation>();
        }

        #endregion

        #region Testes Negativos

        [Fact(DisplayName = "Create Product With ID Negative")]
        public void CreateProduct_NegativeIdValue_DomainExceptionInvalidId()
        {
            Action action = () => new Product(-1, "Product Name", "Product Description", 9.99m,
                99, "product image");

            action.Should().Throw<HelpApp.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Update Invalid Id value");
        }

        [Fact(DisplayName ="Create Product With Null Name")]
        public void CreateProduct_WithNullName_DomainException()
        {
            Action action = () => new Product(1, null, "Product Description", 9.99m, 99, "Product image");
            action.Should().Throw<HelpApp.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid name, name is required.");
        }

        [Fact(DisplayName = "Create Product With Empty Name")]
        public void CreateProduct_WithEmptyName_DomainException()
        {
            Action action = () => new Product(1, "", "Product Description", 9.99m, 99, "Product image");
            action.Should().Throw<HelpApp.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid name, name is required.");
        }

        [Fact(DisplayName = "Create Product With Short Name")]
        public void CreateProduct_ShortNameValue_DomainExceptionShortName()
        {
            Action action = () => new Product(1, "Pr", "Product Description", 9.99m, 99,
                "product image");
            action.Should().Throw<HelpApp.Domain.Validation.DomainExceptionValidation>()
                 .WithMessage("Invalid name, too short, minimum 3 characters.");
        }

        [Fact(DisplayName = "Create Product With Null Description")]
        public void CreateProduct_WithNullDescription_DomainException()
        {
            Action action = () => new Product(1, "Product Name", null, 9.99m, 99, "Product image");
            action.Should().Throw<HelpApp.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid description, name is required.");
        }

        [Fact(DisplayName = "Create Product With Empty Description")]
        public void CreateProduct_WithEmptyDescription_DomainException()
        {
            Action action = () => new Product(1, "Product Name", "", 9.99m, 99, "Product image");
            action.Should().Throw<HelpApp.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid description, name is required.");
        }

        [Fact(DisplayName = "Create Product With Short Description")]
        public void CreateProduct_WithShortDescription_DomainException()
        {
            Action action = () => new Product(1, "Product Name", "Desc", 9.99m, 99, "Product image");
            action.Should().Throw<HelpApp.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid description, too short, minimum 5 characters.");
        }


        [Fact(DisplayName = "Create Product With Null URL Image")]
        public void CreateProduct_WithNullImageName_NoDomainException()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, null);
            action.Should().NotThrow<HelpApp.Domain.Validation.DomainExceptionValidation>();
        }


        [Fact(DisplayName = "Create Product With URL Image Empty")]
        public void CreateProduct_WithEmptyImageName_NoDomainException()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, "");
            action.Should().NotThrow<HelpApp.Domain.Validation.DomainExceptionValidation>();
        }

        [Theory(DisplayName = "Create Product With Invalid Price")]
        [InlineData(-25)]
        public void CreateProduct_InvalidPriceValue_DomainException(int value)
        {
            Action action = () => new Product(1, "Product Name", "Product Description", value,
                99, "");
            action.Should().Throw<HelpApp.Domain.Validation.DomainExceptionValidation>()
                 .WithMessage("Invalid price negative value.");
        }

        [Theory(DisplayName = "Create Product With Inavlid Stock")]
        [InlineData(-5)]
        public void CreateProduct_InvalidStockValue_ExceptionDomainNegativeValue(int value)
        {
            Action action = () => new Product(1, "Pro", "Product Description", 9.99m, value,
                "product image");
            action.Should().Throw<HelpApp.Domain.Validation.DomainExceptionValidation>()
                   .WithMessage("Invalid stock negative value.");
        }
        [Theory(DisplayName = "Create Product With Long URL Image")]
        [InlineData("https://avatars.githubusercontent.com/u/654654651398798798798798798798798798654654321321321321321321321321321321321321321321321658479879879846465465465465465132198498498465465246549879879846213219849849846521321684684987465132165419687498746541631658468465840123321005408?v=4&size=64")]
        public void CreateProduct_LongImageName_DomainExceptionLongImageName(string url)
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m,
                99, url);
            action.Should()
                .Throw<HelpApp.Domain.Validation.DomainExceptionValidation>()
                 .WithMessage("Invalid image name, too long, maximum 250 characters.");
        }
        #endregion
    }
}
