using Microsoft.Extensions.Primitives;
using ProductsManagement.Application.DTOs.Account;
using ProductsManagement.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductsManagement.Application.Interfaces
{
    public interface IAccountService
    {
        Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress);
        Task<Response<string>> RegisterAsync(RegisterRequest request, string origin);
        Task<Response<string>> ConfirmEmailAsync(string userId, string code);
        Task ForgotPassword(ForgotPasswordRequest model, string origin);
        Task<Response<string>> ResetPassword(ResetPasswordRequest model);
    }
}
