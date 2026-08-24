using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Refit;
using TestProject1.DTO;

namespace Tests1.Interfaces
{
    [Headers("x-api-key: free_user_3HxNFNxHA5PT2D4rTd3FTeB0AOM")]
    public interface IUserApi
    {
        [Get("/users/{id}")]
        Task<UserResponseDTO> GetUserAsync(int id);

        [Post("/users")]
        Task<CreateUserResponseDTO> CreateUserAsync([Body] CreateUserRequestDTO request);

        [Delete("/users/{id}")]
        Task<ApiResponse<string>> DeleteUserAsync(int id);
    }
}