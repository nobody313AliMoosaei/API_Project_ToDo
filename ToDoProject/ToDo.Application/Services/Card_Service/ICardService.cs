using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Application.Services.Card_Service
{
    public interface ICardService
    {
        // Create
        Task<bool> CreateNewCard(DTOs.Create_Card_DTO Data_Creat_Card);

        // Read
        Task<List<ToDo.Domain.Entities.Card>> GetAllCards();
        Task<List<ToDo.Domain.Entities.Card>> GetCardByUserId(int User_Id);
        Task<List<ToDo.Domain.Entities.Card>> GetCardByUserName(string UserName);
        Task<ToDo.Domain.Entities.Card> GetCardByTitle(string Title);
        Task<ToDo.Domain.Entities.Card> GetCardById(int Card_Id);

        // Update
        Task<bool> UpdateCard(ToDo.Application.DTOs.Update_Card_DTO Data_Update);
        // Delete
        Task<bool> DeleteCard(int Card_Id);
    }
}
