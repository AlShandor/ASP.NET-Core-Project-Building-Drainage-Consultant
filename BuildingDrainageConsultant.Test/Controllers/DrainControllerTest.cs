namespace BuildingDrainageConsultant.Test.Controllers
{
    using BuildingDrainageConsultant.Controllers;
    using BuildingDrainageConsultant.Test.Mocks;
    using Microsoft.AspNetCore.Mvc;
    using Xunit;

    public class DrainControllerTest
    {

        [Fact]
        public void AddShouldReturnView()
        {
            //Arrange
            var drainsController = new DrainsController(null, MapperMock.Instance); // Mock.Of<IMapper>()

            //Act
            var result = drainsController.Add();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }
    }
}
