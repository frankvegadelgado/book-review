using System.Threading.Tasks;
using BookReview.Models.TokenAuth;
using BookReview.Web.Controllers;
using Shouldly;
using Xunit;

namespace BookReview.Web.Tests.Controllers
{
    public class HomeController_Tests: BookReviewWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}