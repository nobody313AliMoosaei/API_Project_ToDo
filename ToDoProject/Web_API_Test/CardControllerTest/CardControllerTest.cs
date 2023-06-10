using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.DTOs;
using ToDo.Application.Services.Card_Service;
using Web_API.Controllers;
using Xunit;

namespace Web_API_Test.CardControllerTest
{
    public class CardControllerTest
    {
        private TestData_Return_Data_OutPutCard TestData;
        public CardControllerTest()
        {
            TestData = new TestData_Return_Data_OutPutCard();
        }

        // HttpPost
        [Fact]
        public async void CreateNewCardTest()
        {
            // Areange
            var moq = new Mock<ToDo.Application.Services.Card_Service.ICardService>();
            var moq2 = new Mock<IAuthorizationService>();

            Web_API.Controllers.CardController CardController = new Web_API.Controllers.CardController(moq.Object, moq2.Object);

            ToDo.Application.DTOs.Create_Card_DTO data = new ToDo.Application.DTOs.Create_Card_DTO
            {
                Title = "Test",
                Description = "Description Test",
                User_IDs = new List<int> { 1, 2, 3, 4 }
            };
            // Act
            var Resualt = await CardController.CreateNewCard(data);
            //Assert

            Assert.IsType<OkObjectResult>(Resualt);
        }


        // HttpGet
        public async void GetAllTest()
        {
            // Areange
            var moq = new Mock<ToDo.Application.Services.Card_Service.ICardService>();
            var moq2 = new Mock<IAuthorizationService>();
            moq.Setup(t => t.GetAllCards()).Returns(TestData.GetCardData);
            CardController controller = new CardController(moq.Object, moq2.Object);

            // Act
            var Result = controller.GetAllCard();
            // Assert
            Assert.IsType<OkObjectResult>(Result);
            Assert.IsType<List<OutPut_Card>>(Result);
        }


        //HttpGet
        [Theory]
        [InlineData(1, -1)]
        public async void GetCardTest(int IdInvalid, int IdValid)
        {
            // ---------------- Valid Id -----------------
            // Areange 
            var moq = new Mock<ICardService>();
            var moq2 = new Mock<IAuthorizationService>();
            moq.Setup(t => t.GetCardByUserId(IdValid)).Returns(TestData.GetCardData);
            Web_API.Controllers.CardController controller = new CardController(moq.Object, moq2.Object);
            // Act
            var Result = controller.GetCardByUserId(IdValid);

            // Assert
            Assert.IsType<OkObjectResult>(Result);





            // ------------- Invalid Id ------------------
            // Areange
            moq.Setup(t => t.GetCardByUserId(IdInvalid)).Returns(TestData.GetCardData);
            controller = new CardController(moq.Object, moq2.Object);
            // Act
            var result = controller.GetCardByUserId(IdInvalid);
            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }


        // HttpDelete
        [Theory]
        [InlineData(1, -1)]
        public async void DeleteCardTest(int ValidId, int InvalidId)
        {
            // Areange
            var moq = new Mock<ICardService>();
            var moq2 = new Mock<IAuthorizationService>();
            moq.Setup(t=>t.DeleteCard(ValidId)).Returns(TestData.GetCardData);
            var controller = new Web_API.Controllers.CardController(moq.Object, moq2.Object);

            // act
            var resualt1 = controller.DeleteCard(ValidId);
            var resualt2 = controller.DeleteCard(InvalidId);
            // assert
            Assert.IsType<OkObjectResult>(resualt1);
            Assert.IsType<BadRequestObjectResult>(resualt2);
        }
    }
}
