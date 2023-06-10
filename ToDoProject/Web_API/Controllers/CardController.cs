using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDo.Application.Services.Card_Service;
using ToDo.Domain.Entities;
using Web_API.Hellper.AuthorizationHandler.ResourceAuthorization;

namespace Web_API.Controllers
{
    [Route("Test/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ToDo.Application.Services.Card_Service.ICardService _CardService;
        private IAuthorizationService _AuthorizationService;

        public CardController(ICardService cardService, IAuthorizationService authorizationService)
        {
            _CardService = cardService;
            _AuthorizationService = authorizationService;
        }

        /// <summary>
        /// در این اکشن با استفاده از سرویس کارد و ورودی دیتا 
        /// اقدام به ثبت تسک جدید می کنیم
        /// </summary>
        /// <param name="Data_Card"></param>
        /// <returns>
        /// فقط یک رشته استرینگ است که کاربر را از درستی یا ناردستی عملیات مطلع کنیم
        /// </returns>
        [Authorize]
        [HttpPost]
        // Create
        public async Task<IActionResult> CreateNewCard(ToDo.Application.DTOs.Create_Card_DTO Data_Card)
        {
            if (!ModelState.IsValid) return BadRequest("وارد کردن اطلاعات به درستی انجام نشده است");

            var Resualt = await _CardService.CreateNewCard(Data_Card);
            if (Resualt)
                return Ok("ثبت تسک با موفقیت انجام شد");
            return BadRequest("ثبت تسک موفقیت آمیز نبود");
        }

        /// <summary>
        /// تمام تسک ها را در این اکشن می توانیم بدون احراز هویت توسط هر کاربری دریافت کنیم
        /// دریافت به صورت مپ شده است
        /// </summary>
        /// <returns></returns>
        // Read
        [HttpGet]
        public async Task<IActionResult> GetAllCard()
        {
            var AllCard = await _CardService.GetAllCards();
            if (AllCard == null
                || AllCard.Count <= 0)
            {
                return NoContent();
            }
            var Cards = new List<ToDo.Application.DTOs.OutPut_Card>();
            foreach (var item in AllCard)
            {
                Cards.Add(new ToDo.Application.DTOs.OutPut_Card()
                {
                    Title = item.Title,
                    Description = item.Description,
                    UserNames = item?.Users?.Select(x => x.UserName).ToList()
                });
            }
            return Ok(Cards);
        }

        /// <summary>
        /// دریافت یک تسک اب استفاده از شناسه آن تسک انجام میشود
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>
        /// اطلاعات یک تسک را به صورت مپ شده به فرمت تعیین شده ارسال می کنیم به فرانت
        /// </returns>

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetOneCard(int Id)
        {
            if (Id == 0)
                return BadRequest("شناسه ای ارسال نشده است");
            var Card = await _CardService.GetCardById(Id);
            if (Card == null)
                return BadRequest("چنین تسکی وجود ندارد");
            return Ok(new ToDo.Application.DTOs.OutPut_Card
            {
                Title = Card.Title,
                Description = Card.Description,
                UserNames = Card.Users.Select(t => t.UserName).ToList()
            });
        }


        /// <summary>
        /// این اکشن تمام تسک های مربوط به یک کاربر را به ما می دهد
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [HttpGet("{UserId}")]
        public async Task<IActionResult> GetCardByUserId([FromRoute]int UserId)
        {
            if (UserId == 0)
                return BadRequest("شناسه ای ارسال نشده است");
            var Card =await  _CardService.GetCardByUserId(UserId);
            if (Card == null
                || Card.Count == 0)
                return NoContent();

            var AllCards = new List<ToDo.Application.DTOs.OutPut_Card>();

            foreach(var item in Card)
            {
                AllCards.Add(new ToDo.Application.DTOs.OutPut_Card
                {
                    Title = item.Title,
                    Description = item.Description,
                    UserNames = item.Users.Select(t => t.UserName).ToList()
                });
            }
            return Ok(AllCards);
        }

        /// <summary>
        /// 
        /// این اکشن تمام تسک های کاربر مدنظر را به ما می دهد
        /// این اکشن به عنوان ورودی رشته استرینگی که نام کاربری کاربر می باشد را می گیرد
        /// و تسک های مبروط به آن نام کاربری را برگشت می دهد
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>

        [HttpGet("{UserName}")]
        public async Task<IActionResult> GetCardsByUserName(string UserName)
        {
            if (string.IsNullOrEmpty(UserName))
                return BadRequest("نام کاربری خالی است");
            var Cards =await _CardService.GetCardByUserName(UserName);
            if (Cards == null
                || Cards.Count == 0)
                return NoContent();
            var AllCards = new List<ToDo.Application.DTOs.OutPut_Card>();

            foreach (var item in Cards)
            {
                AllCards.Add(new ToDo.Application.DTOs.OutPut_Card
                {
                    Title = item.Title,
                    Description = item.Description,
                    UserNames = item.Users.Select(t => t.UserName).ToList()
                });
            }
            return Ok(AllCards);
        }

        /// <summary>
        /// ویرایش یک تسک در این اکشن انجام می شود
        /// تنها کسی که ایجاد کننده در تسک باشد، قابلیت تغییر را دارد
        /// این قابلیت با ایده ئولوژی Resource Authorization انجام شده
        /// سرویس آن نیز به این کنترولر اینجکت شده است
        /// </summary>
        /// <param name="Data_Update"></param>
        /// <returns></returns>
        // Update
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateCard(ToDo.Application.DTOs.Update_Card_DTO Data_Update)
        {
            if (!ModelState.IsValid)
                return BadRequest("مقادیر ورودی به درستی ارسال نشده است");
            var Resource = new CardResourceAuthorization_DTO()
            {
                User_Id = int.Parse(User.Claims.FirstOrDefault(t => t.Type == ClaimTypes.NameIdentifier)?.Value)
            };
            var Resualt_Authorization = await _AuthorizationService.AuthorizeAsync(User, Resource, "IsCardForUser");
            if (Resualt_Authorization != null
                && Resualt_Authorization.Succeeded)
            {
                var Resualt = await _CardService.UpdateCard(Data_Update);
                if (Resualt)
                    return Ok("تغییرات با موفقیت اعمال شد");
                return BadRequest("تغییرات اعمال نشد");
            }else
            {
                return BadRequest("کاربر نمی تواند تسک دیگران را تغییر دهد");
            }
        }

        /// <summary>
        /// از انجایی ک ما از Resource Authorization استفاده کردیم
        /// تنها کسی می تواند آنرا ویرایش کند که سازنده تسک باشد
        /// </summary>
        /// <param name="Card_Id"></param>
        /// <returns></returns>
        // Delete
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteCard(int Card_Id)
        {
            if (Card_Id == 0)
                return BadRequest("تسکی برای حذف مشخص نشده است");

            var Resource = new CardResourceAuthorization_DTO()
            {
                User_Id = int.Parse(User.Claims.FirstOrDefault(t => t.Type == ClaimTypes.NameIdentifier)?.Value)
            };
            var Resualt_Authorization = await _AuthorizationService.AuthorizeAsync(User, Resource, "IsCardForUser");
            if(Resualt_Authorization!=null
                && Resualt_Authorization.Succeeded)
            {
                var Resualt = await _CardService.DeleteCard(Card_Id);
                if (Resualt)
                    return Ok("حذف با موفقیت انجام شد");
                return BadRequest("حذف انجام نشد");
            }else
            {
                return BadRequest("کاربر نمی تواند تسک دیگران را حذف کند");
            }
        }
    }
}
