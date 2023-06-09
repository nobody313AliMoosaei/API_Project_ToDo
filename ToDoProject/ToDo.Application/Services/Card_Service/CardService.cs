using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.DTOs;
using ToDo.Application.InterfaceContext;
using ToDo.Domain.Entities;

namespace ToDo.Application.Services.Card_Service
{
    public class CardService : ICardService
    {
        private IDatabaseContext _Context;
        private UserManager<Domain.Entities.User> _UserManager;

        public CardService(IDatabaseContext context, UserManager<Domain.Entities.User> usermanager)
        {
            _Context = context;
            _UserManager = usermanager;
        }

        #region Create Card
        public async Task<bool> CreateNewCard(Create_Card_DTO Data_Creat_Card)
        {
            try
            {
                if (Data_Creat_Card.User_IDs == null
                    || Data_Creat_Card.User_IDs.Count == 0)
                {
                    return false;
                }

                if (string.IsNullOrEmpty(Data_Creat_Card.Title))
                    return false;
                var NewCard = new Domain.Entities.Card()
                {
                    Title = Data_Creat_Card.Title,
                    Description = Data_Creat_Card.Description,
                    Status = Domain.Enums.CardStatus.ToDo,
                    Users = new List<Domain.Entities.User>()
                };
                foreach (var item in Data_Creat_Card.User_IDs)
                {
                    var user = await _UserManager.FindByIdAsync(item.ToString());
                    if (user != null)
                    {
                        NewCard.Users.Add(user);
                    }
                }

                _Context.Cards.Add(NewCard);
                await _Context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Read Card
        public async Task<List<Card>> GetAllCards()
        {
            try
            {
                return await _Context.Cards
                    .Include(t=>t.Users)
                    .ToListAsync();
            }
            catch
            {
                return new List<Card>();
            }
        }

        public async Task<Card> GetCardById(int Card_Id)
        {
            try
            {
                var Card = await _Context.Cards
                    .Include(t=>t.Users)
                    .Where(t => t.Id == Card_Id)
                    .FirstOrDefaultAsync();

                return Card;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Card> GetCardByTitle(string Title)
        {
            try
            {
                var Card = await _Context.Cards
                    .Include(t => t.Users)
                    .Where(t => t.Title == Title)
                    .FirstOrDefaultAsync();
                if (Card == null)
                    return null;
                return Card;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<Card>> GetCardByUserId(int User_Id)
        {
            try
            {
                var Cards = await _Context.Cards
                    .Include(t => t.Users)
                    .Where(t => t.Users.Any(t => t.Id == User_Id))
                    .ToListAsync();
                if (Cards == null
                    || Cards.Count <= 0)
                    return new List<Card>();
                return Cards;
            }
            catch
            {
                return new List<Card>();
            }
        }

        public async Task<List<Card>> GetCardByUserName(string UserName)
        {
            try
            {
                var Cards = await _Context.Cards
                    .Include(t => t.Users)
                    .Where(t => t.Users.Any(t => t.UserName == UserName))
                    .ToListAsync();

                if (Cards == null
                    || Cards.Count <= 0)
                    return new List<Card>();
                return Cards;
            }
            catch
            {
                return new List<Card>();
            }
        }
        #endregion

        #region Update Card
        public async Task<bool> UpdateCard(Update_Card_DTO Data_Update)
        {
            try
            {
                if (string.IsNullOrEmpty(Data_Update.Title)
                || Data_Update.Card_Id == 0)
                    return false;

                var Card = await _Context.Cards
                    .Where(t => t.Id == Data_Update.Card_Id)
                    .Include(t => t.Users)
                    .FirstOrDefaultAsync();

                if (Card == null) return false;

                // Change Title
                Card.Title= Data_Update.Title;

                // Change Description
                if (!string.IsNullOrEmpty(Data_Update.Description))
                    Card.Description = Data_Update.Description;
                else
                    Card.Description = "";

                // FindUsers And Change Users
                if (Data_Update.User_Ids != null
                    && Data_Update.User_Ids.Count > 0)
                {
                    var users = new List<Domain.Entities.User>();
                    foreach (var item in Data_Update.User_Ids)
                    {
                        var user = await _UserManager.FindByIdAsync(item.ToString());

                        if(user != null) users.Add(user);
                    }
                    Card.Users= users;
                }
                _Context.Cards.Update(Card);
                await _Context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Delete Card
        public async Task<bool> DeleteCard(int Card_Id)
        {
            try
            {
                if (Card_Id == 0) return false;
                
                var Card = await _Context.Cards.Where(t=>t.Id==Card_Id).FirstOrDefaultAsync();
                
                if (Card == null) return false;

                _Context.Cards.Remove(Card);
                await _Context.SaveChangesAsync();
                return true;
            }catch
            {
                return false;
            }
        }
        #endregion
    }
}
