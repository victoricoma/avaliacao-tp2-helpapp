using HelpApp.Domain.Entities;
using FluentAssertions;
using HelpApp.Domain.Validation;
using Xunit;
using System.Runtime.Intrinsics.Arm;

namespace HelpApp.Domain.Test
{
    public class ProductUnitTest
    {
        #region Testes Positivos
        [Fact(DisplayName ="Create Product With Parameters Full")]
        public void CreateProduct_WithValidParameters_ResultObjectValisState()
        {
            Action action= () => new Product(1, 
                "Product Name", 
                "Product Description", 
                9.99m, 99, 
                "https://img/product.jpg");
            action.Should()
                .NotThrow<DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create a Valid Product")]
        public void CreateProduct_WithValidParameters_ShouldCreateObject()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 99.0m, 9, "/image/ProductImage.png");
            action.Should().NotThrow<DomainExceptionValidation>() ;
        }

        #endregion

        #region Testes Negativos
        [Fact(DisplayName ="Create Product With ID Negative")]
        public void CreateProduct_NegativeIdValue_DomainExceptionInvalidId()
        {
            Action action = () => new Product(-1, "Product Name", "Product Description", 9.99m,
                99, "product image");

            action.Should().Throw<DomainExceptionValidation>().WithMessage("Update Invalid Id value");
        }

        [Theory(DisplayName = "Create Product With Null or Empty Name")]
        [InlineData(null)]
        [InlineData("")]
        public void CreateProduct_WithNullOrEmptyName_ShouldThrowException(string? name)
        {
            Action action = () => new Product(name!, "Product Description", 99.0m, 9, "/img/ProductImage.png");
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid name, name is required.");
        }

        [Fact(DisplayName = "Create Product With Short Name")]
        public void CreateProduct_ShortNameValue_ShouldThrowException()
        {
            Action action = () => new Product(1, "Pr", "Product Description", 9.99m, 99,
                "product image");
            action.Should().Throw<HelpApp.Domain.Validation.DomainExceptionValidation>()
                 .WithMessage("Invalid name, too short, minimum 3 characters.");
        }

        [Theory(DisplayName = "Create Product With Null or Empty Description")]
        [InlineData(null)]
        [InlineData("")]
        public void CreateProduct_WithNullOrEmptyDescription_ShouldThrowException(string? description)
        {
            Action action = () => new Product(1, "Product Name", description!, 99.0m, 9, "/img/ProductImage.png");
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid description, description is required");
        }

        [Fact(DisplayName = "Create Product With Too Short Description")]
        public void CreateProduct_WithTooShortDescription_ShouldThrowException()
        {
            Action action = () => new Product(1, "Product Name", "Pr", 9.99m, 99, "/img/ProductImage.png");
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid description, too short, minimum 5 charactes.");
        }


        [Fact(DisplayName = "Create Product With Negative Price")]
        public void CreateProduct_WithNegativePrice_ShouldThrowException()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", -9.99m, 99, "/img/ProductImage.png");
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid price negative value.");
        }

        [Fact(DisplayName = "Create Product With Negative Stock")]
        public void CreateProduct_WithNegativeStock_ShouldThrowException()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, -99, "/img/ProductImage.png");
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid stock negative value.");
        }

        [Theory(DisplayName = "Create Product With Null or Empty Image")]
        [InlineData(null)]
        [InlineData("")]
        public void CreateProduct_WithNullOrEmptyImage_ShouldThrowException(string? image)
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 99.0m, 9, "/img/ProductImage.png");
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid image address, image is required.");
        }

        [Theory(DisplayName = "Create Product With Long URL Image")]
        [InlineData("https://avatars.githubusercontent.com/u/654654651398798798798798798798798798654654321321321321321321321321321321321321321321321658479879879846465465465465465132198498498465465246549879879846213219849849846521321684684987465132165419687498746541631658468465840123321005408?v=4&size=64")]
        public void CreateProduct_LongImageName_DomainExceptionLongImageName(string url)
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m,
                99, url);
            action.Should()
                .Throw<DomainExceptionValidation>()
                 .WithMessage("Invalid image name, too long, maximum 250 characters.");
        }
        #endregion
    }
}
