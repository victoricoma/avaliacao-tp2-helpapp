using HelpApp.Domain.Entities;
using FluentAssertions;
using HelpApp.Domain.Validation;
using Xunit;

namespace HelpApp.Domain.Test
{
    public class CategoryUnitTest
    {
        #region Testes Positivos
        [Fact(DisplayName = "Create Category With Valid State")]
        public void CreateCategory_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Category(1, "Category Name");
            action.Should().NotThrow<HelpApp.Domain.Validation.DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create a valid category with name only and default id")]
        public void CreateCategory_WithOnlyName_ShouldSetDefaultId()
        {
            Action action = () => new Category("Category Name");
            action.Should().NotThrow<DomainExceptionValidation>();
        }
        #endregion

        #region Testes Negativos

        [Fact(DisplayName = "Create Category With Negative Id")]
        public void CreateCategory_WithNegativeId_ShouldThrowException()
        {
            Action action = () => new Category(-1, "Category Name");
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid Id value.");
        }

        [Theory(DisplayName = "Create Category With Null or Empty Name")]
        [InlineData(null)]
        [InlineData("")]
        public void CreateCategory_WithNullOrEmptyName_ShouldthrowException(string? name)
        {
            Action action = () => new Category(name!);
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid name, name is required");
        }

        [Theory(DisplayName = "Create Category With Shorter Name Than 3 Characters")]
        [InlineData("N")]
        [InlineData("AM")]
        public void CreateCategory_WithShortName_ShouldThrowException(string name)
        {
            Action action = () => new Category(name);
            action.Should().Throw<DomainExceptionValidation>().WithMessage("Invalid name, too short, minimum 3 characters.");
        }
        #endregion
    }
}
