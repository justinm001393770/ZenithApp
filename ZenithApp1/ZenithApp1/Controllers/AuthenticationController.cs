using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Web;
using System.Web.ApplicationServices;
using ZenithApp1.Models;
using ZenithApp1.ViewModels;

namespace ZenithApp1
{
    public class AuthenticationController : AuthenticationService
    {
        public ZenithContext db = new ZenithContext();
        private readonly HttpContextBase _context;
        private const string AuthenticationType = "ApplicationCookie";
        public enum LoginResult { Success, Failure, RequireVerification };
        public enum SignUpResult { Success, EmailExists, UsernameExists, Invalid };

        public AuthenticationController(HttpContextBase context)
        {
            _context = context;
        }


        public void SignIn(User user)
        {
            LoggedInUser.setSessionVariables(user);

            IList<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserID.ToString())
        };

            ClaimsIdentity identity = new ClaimsIdentity(claims, AuthenticationType);

            IOwinContext context = _context.Request.GetOwinContext();
            IAuthenticationManager authenticationManager = context.Authentication;

            authenticationManager.SignIn(identity);
        }

        public void SignOut()
        {
            IOwinContext context = _context.Request.GetOwinContext();
            IAuthenticationManager authenticationManager = context.Authentication;

            authenticationManager.SignOut(AuthenticationType);
        }

        public LoginResult AttemptLogin(string email, string password)
        {
            User user = User.getUserByEmail(email);

            if (user != null && validCredentials(email, password))
            {
                if(user.LastLoginDate == null)
                {
                    return LoginResult.RequireVerification;
                }
                return LoginResult.Success;
            }
            else
            {
                return LoginResult.Failure;
            }
        }

        public SignUpResult AttemptSignUp(SignUpViewModel vm)
        {
            if (User.usernameExists(vm.Username))
            {
                return SignUpResult.UsernameExists;
            }

            if (User.emailExists(vm.Email))
            {
                return SignUpResult.EmailExists;
            }

            db.Users.Add(createNewUserObject(vm));
            db.SaveChanges();
            
            if (User.emailExists(vm.Email) && User.usernameExists(vm.Username))
            {
                return SignUpResult.Success;
            }

            return SignUpResult.Invalid;
        }

        private User createNewUserObject(SignUpViewModel vm)
        {
            string newSalt = CreateSalt();
            string hashedPassword = CreatePasswordHash(newSalt, vm.Password);

            User newUser = new User();
            newUser.UserName = vm.Username;
            newUser.Email = vm.Email;
            newUser.Phone = vm.Phone;
            newUser.CreatedDate = DateTime.Now;
            newUser.Password = hashedPassword;
            newUser.Active = true;

            Salt newUserSalt = new Salt();
            newUserSalt.SaltValue = newSalt;
            newUserSalt.SaltName = "PasswordSalt";
            newUser.Salts.Add(newUserSalt);

            return newUser;
        }

        public static string CreatePasswordHash(string salt, string pwd)
        {
            string saltAndPwd = string.Concat(salt, pwd);

            HashAlgorithm hashAlg = new SHA256CryptoServiceProvider();

            byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(saltAndPwd);

            byte[] bytHash = hashAlg.ComputeHash(bytValue);

            return Convert.ToBase64String(bytHash);
        }

        public static string CreateSalt()
        {
            byte[] saltBytes = new byte[16];

            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

            rng.GetBytes(saltBytes);

            return Convert.ToBase64String(saltBytes);
        }

        public bool validCredentials(string email, string enteredPassword)
        {
            User userToCheck = User.getUserByEmail(email);

            if (userToCheck.Password == CreatePasswordHash(userToCheck.Salts.FirstOrDefault().SaltValue, enteredPassword))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}