using backend.DTOs.Requests;
using backend.DTOs.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Services
{
    public interface IDbService
    {
        public Task<List<GetExercisesResponse>> GetExercisesAsync();
        public Task<bool> DeleteExerciseAsync(int idExercise);
        public Task<bool> PostExerciseAsync(PostExerciseRequest exercise);
        public Task<bool> PutExerciseAsync(PostExerciseRequest exercise, int idExercise);
        public Task<List<GetExerciseOperationsResponse>> GetExerciseOperationsAsync(int idExercise);
        public Task<bool> PostExerciseOperationsAsync(PostExerciseOperationsRequest operation, int idExercise);
        public Task<bool> PutOperationAsync(PutOperationRequest operation, int idOperation);
        public Task<bool> DeleteOperationAsync(int idOperation);
        public Task<bool> PostRegisterUserAsync(RegisterRequest model);
        public Task<TokenDtoResponse> LoginAsync(LoginRequest loginRequest);
        public Task<TokenDtoResponse> NewTokenAsync(string token, RefreshTokenRequest refreshToken);

    }
}
