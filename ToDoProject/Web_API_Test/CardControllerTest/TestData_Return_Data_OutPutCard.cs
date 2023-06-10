using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_API_Test.CardControllerTest
{
    internal class TestData_Return_Data_OutPutCard
    {
        public async Task<ToDo.Application.DTOs.OutPut_Card> GetCardData()
        {
            return new ToDo.Application.DTOs.OutPut_Card
            {
                Title = "Test",
                Description = "Description Test",
                UserNames = new List<string> { "AliMoosaei" }
            };
        }
        public async Task<List<ToDo.Application.DTOs.OutPut_Card>> GetAllDataCardTest()
        {
            return new List<ToDo.Application.DTOs.OutPut_Card>
           {
               new ToDo.Application.DTOs.OutPut_Card
               {
                   Title = "Test",
                   Description="Description Test",
                   UserNames = new List<string> {"AliMoosaei"}
               }
           };
        }
    }
}
