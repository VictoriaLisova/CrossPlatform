using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Server.Data;

namespace Server.Controllers
{
    public class AccessController : Controller
    {
        private readonly HttpClient _httpClient;
        private SignInManager<UserModel> _signInManager;
        private UserManager<UserModel> _userManager;
        private AspNetIdentityDbContext _db;

        public AccessController(HttpClient httpClient, SignInManager<UserModel> signInManager,
            UserManager<UserModel> userManager, AspNetIdentityDbContext db)
        {
            _httpClient = httpClient;
            _signInManager = signInManager;
            _userManager = userManager;
            _db = db;
        }

        [HttpPost]
        [Route("server/register")]
        public async Task<IActionResult> Register([FromBody] UserModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _db.Users.FirstOrDefault(u => u.UserName == model.UserName && u.Password == model.Password);
                // if new user
                if(user == null)
                {
                    var new_user = new UserModel
                    {
                        UserName = model.UserName,
                        FullName = model.FullName,
                        Phone = model.Phone,
                        Email = model.Email,
                        Password = model.Password
                    };
                    var result = await _userManager.CreateAsync(new_user, model.Password);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return Ok(new_user);
                    }
                }
                else // if user exist already
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return Ok(user);
                }
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("server/login")]
        public async Task<IActionResult> Login([FromBody] UserModel model)
        {
            // check if user exists
            var user = _db.Users.FirstOrDefault(u => u.UserName == model.UserName && u.Password == model.Password);
            if(user != null)
            {
                await _signInManager.SignInAsync(user, isPersistent:false);
                return Ok(user);
            }
            return BadRequest("user not found in db");
        }

        [HttpPost("server/get-token")]
        public async Task<IActionResult> GetToken([FromBody] UserModel model)
        {
            var body = new Dictionary<string, string>
            {
                { "grant_type", "password" },
                { "client_id", "password_client" },
                { "client_secret", "Client1_Secret" },
                { "username", model.UserName },
                { "password", model.Password },
                { "scope", "openid profile Lab5API.read Lab5API.write" }
            };
            var content = new FormUrlEncodedContent(body);

            // send request to token endpoint to get access token
            var responce = await _httpClient.PostAsync("https://localhost:5443/connect/token", content);
            if (responce.IsSuccessStatusCode)
            {
                var token = responce.Content.ReadAsStringAsync();
                return Ok(token);
            }
            return BadRequest("Can`t get token");
        }
    }
}
