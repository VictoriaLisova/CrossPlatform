using IdentityModel.Client;
using Lab5.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace Lab5.Controllers
{
    public class AccountingController : Controller
    {
        private readonly HttpClient httpClient;
        private readonly IHttpClientFactory httpClientFactory;
        public AccountingController(HttpClient httpClient, IHttpClientFactory httpClientFactory)
        {
            this.httpClient = httpClient;
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        private bool CheckPhoneNumber(string number)
        {
            // check if number contains +380 and length
            return !string.IsNullOrEmpty(number) && number.Length == 13 && number.StartsWith("+380");
        }

        private bool CheckPasswordHelper(string password)
        {
            var symbols = new char[] { '@', '!', '?', '#', '$', '%', '&', '*' };

            // check if any special symbol contans in password
            var answer = password.Where(p => symbols.Contains(p)).Select(p => p).ToList();
            return answer.Any();
        }
        private bool CheckPassword(string password, string confirm_password)
        {
            // check if two passwords equals and satisfy the requirements for length and contains symbols
            return !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(confirm_password) &&
                password == confirm_password && password.Length >= 8 && password.Length <= 16 && password.Any(char.IsDigit)
                && password.Any(char.IsUpper) && CheckPasswordHelper(password);
        }

        private bool CheckEmail(string email)
        {
            // check email based on regex expresion
            var regex = new Regex(@"^\S+@\S+\.+\S+$");
            return regex.IsMatch(email);
        }

        private bool CheckData(ClientModel model)
        {
            // check all user inputs
            return CheckNames(model.UserName, model.FullName) && CheckPassword(model.Password, model.ConfirmPassword) &&
                CheckPhoneNumber(model.Phone) && CheckEmail(model.Email);
        }

        private bool CheckNames(string username, string fullname)
        {
            // check username and fullname by length
            return !string.IsNullOrEmpty(username) && username.Length <= 50 && !string.IsNullOrEmpty(fullname) && fullname.Length <= 500;
        }

        private async Task<TokenData> GetToken(ClientModel user)
        {
            TokenData access_token = new();
            if (user != null)
            {
                // authenticate user based on user claims
                var user_claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName)
                };
                var claims_identity = new ClaimsIdentity(user_claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claims_identity));

                // get user token
                var token_content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var token_responce = await httpClient.PostAsync("https://localhost:5443/server/get-token", token_content);
                var token_responce_content = await token_responce.Content.ReadAsStringAsync();
                var token = JsonConvert.DeserializeObject<TokenModel>(token_responce_content);

                access_token = JsonConvert.DeserializeObject<TokenData>(token.Result);
            }
            return access_token;
        }

        [HttpPost]
        public async Task<IActionResult> Login(ClientModel model)
        {
            if(model != null && !string.IsNullOrEmpty(model.UserName) && model.UserName.Length <= 50 && CheckPasswordHelper(model.Password))
            {
                try
                {
                    // get and signin user 
                    var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                    var responce = await httpClient.PostAsync("https://localhost:5443/server/login", content);
                    var responce_content = await responce.Content.ReadAsStringAsync();
                    var user = JsonConvert.DeserializeObject<ClientModel>(responce_content);

                    // get access token
                    TokenData access_token = await GetToken(user);
                    if (!string.IsNullOrEmpty(access_token.Access_Token))
                    {
                        HttpContext.Session.SetString("access_token", access_token.Access_Token);
                    }
                    return RedirectToAction("Secret", "Accounting", user);
                }
                catch(Exception ex)
                {
                    return RedirectToAction("Register", "Accounting");
                }
            }
            return View();
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(ClientModel model)
        {
            if(model != null && CheckData(model))
            {
                try
                {
                    // check if user exsist and add new if need
                    var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                    var responce = await httpClient.PostAsync("https://localhost:5443/server/register", content);
                    var responce_content = await responce.Content.ReadAsStringAsync();
                    var user = JsonConvert.DeserializeObject<ClientModel>(responce_content);

                    //return RedirectToAction("Login", "Accounting");
                    TokenData access_token = await GetToken(user);
                    if (!string.IsNullOrEmpty(access_token.Access_Token))
                    {
                        HttpContext.Session.SetString("access_token", access_token.Access_Token);
                    }
                    return RedirectToAction("Secret", "Accounting", user);
                }
                catch (Exception ex)
                {
                    return View();
                }
            }
            return View();  
        }

        [Authorize]
        public async Task<IActionResult> Secret(ClientModel user)
        {
            var access_token = HttpContext.Session.GetString("access_token");
            var claims = User.Claims.ToList();
            var _access_token = new JwtSecurityTokenHandler().ReadJwtToken(access_token);
            var response = await GetSecret(access_token);
            if (response == "successs")
            {
                return RedirectToAction("Index", "Profile", user);
            }
            else  // imposible to authorize -> remove token from session and sign out user
            {
                HttpContext.Session.Clear();
                await HttpContext.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<string> GetSecret(string access_token)
        {
            // get access to protected resourse
            var api = httpClientFactory.CreateClient();
            api.SetBearerToken(access_token);
            var response = await api.GetAsync("https://localhost:5445/api/secret");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
