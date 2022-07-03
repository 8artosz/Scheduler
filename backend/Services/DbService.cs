using backend.DTOs.Requests;
using backend.DTOs.Responses;
using backend.Helpers;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace backend.Services
{
    public class DbService : IDbService
    {
        private readonly MainDbContext _context;
        private readonly IConfiguration _configuration;
        public DbService(MainDbContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;
        }
        
        public async Task<List<GetExercisesResponse>> GetExercisesAsync()
        {
            var tasks = await _context.Exercise.Select(z => new GetExercisesResponse
                                              {
                                                  IdExercise = z.IdExercise,
                                                  Title = z.Title,
                                                  Description = z.Description,
                                                  Status = z.Status
                                              }).ToListAsync();
            return tasks;
        }

        public async Task<bool> DeleteExerciseAsync(int idExercise)
        {
            var exerciseExists = await _context.Exercise.AnyAsync(e => e.IdExercise == idExercise);
            if (!exerciseExists)
                return false;

            var exercise = new Exercise
            {
                IdExercise = idExercise
            };
            _context.Exercise.Attach(exercise);
            _context.Entry(exercise).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<bool> PostExerciseAsync(PostExerciseRequest exercise)
        {
            var newExercise = new Exercise
            {
                Title = exercise.Title,
                Description = exercise.Description,
                Status = exercise.Status
            };
            await _context.Exercise.AddAsync(newExercise);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> PutExerciseAsync (PostExerciseRequest exercise, int idExercise)
        {
            var exerciseExists = await _context.Exercise.AnyAsync(e => e.IdExercise == idExercise);
            if (!exerciseExists)
            {
                return false;
            }
            var newExercise = new Exercise
            {
                IdExercise = idExercise,
                Title = exercise.Title,
                Description = exercise.Description,
                Status = exercise.Status

            };
            _context.Exercise.Attach(newExercise);
            _context.Entry(newExercise).Property(nameof(Exercise.Title)).IsModified = true;
            _context.Entry(newExercise).Property(nameof(Exercise.Description)).IsModified = true;
            _context.Entry(newExercise).Property(nameof(Exercise.Status)).IsModified = true;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<GetExerciseOperationsResponse>> GetExerciseOperationsAsync(int idExercise)
        {
            var tasks = await _context.Operation.Where(d => d.IdExercise == idExercise).Select(g => new GetExerciseOperationsResponse
            {
                IdOperation = g.IdOperation,
                TimeSpent = g.TimeSpent,
                Description = g.Description
            }).ToListAsync();
            return tasks;
        }
        public async Task<bool> PostExerciseOperationsAsync(PostExerciseOperationsRequest operation, int idExercise)
        {
            var newOperation = new Operation
            {
                IdExercise = idExercise,
                Description = operation.Description,
                TimeSpent = operation.TimeSpent
            };
            await _context.Operation.AddAsync(newOperation);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteOperationAsync(int idOperation)
        {
            var operationExists = await _context.Operation.AnyAsync(e => e.IdOperation == idOperation);
            if (!operationExists)
                return false;

            var operation = new Operation
            {
                IdOperation = idOperation
            };
            _context.Operation.Attach(operation);
            _context.Entry(operation).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<bool> PutOperationAsync(PutOperationRequest operation, int idOperation)
        {
            var operationExists = await _context.Operation.AnyAsync(e => e.IdOperation == idOperation);
            if (!operationExists)
            {
                return false;
            }
            var newOperation = new Operation
            {
                IdOperation = idOperation,
                Description = operation.Description,
                TimeSpent = operation.TimeSpent

            };
            _context.Operation.Attach(newOperation);
            _context.Entry(newOperation).Property(nameof(Operation.Description)).IsModified = true;
            _context.Entry(newOperation).Property(nameof(Operation.TimeSpent)).IsModified = true;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> PostRegisterUserAsync(RegisterRequest model)
        {
            var user = await _context.User.AnyAsync(e => e.Login == model.Login);
            if (user)
                return false;

            var hashedPasswordAndSalt = SecurityHelpers.GetHashedPasswordAndSalt(model.Password);


            var newUser = new AppUser()
            {
                Email = model.Email,
                Login = model.Login,
                Password = hashedPasswordAndSalt.Item1,
                Salt = hashedPasswordAndSalt.Item2,
                RefreshToken = SecurityHelpers.GenerateRefreshToken(),
                RefreshTokenExp = DateTime.Now.AddDays(1)
            };

            _context.User.Add(newUser);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<TokenDtoResponse> LoginAsync(LoginRequest loginRequest)
        {
            AppUser user = await _context.User.Where(u => u.Login == loginRequest.Login).FirstOrDefaultAsync();
            if (user == null)
            {
                return new TokenDtoResponse("1", "1");
            }

            string passwordHashFromDb = user.Password;
            string curHashedPassword = SecurityHelpers.GetHashedPasswordWithSalt(loginRequest.Password, user.Salt);
            if (passwordHashFromDb != curHashedPassword)
            {
                return new TokenDtoResponse("2", "1");
            }


            Claim[] userclaim = new[] {
                    new Claim(ClaimTypes.Name, "s20296"),
                    new Claim(ClaimTypes.Role, "user"),
                    new Claim(ClaimTypes.Role, "admin")
                };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: "https://localhost:5001",
                audience: "https://localhost:5001",
                claims: userclaim,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds
            );

            user.RefreshToken = SecurityHelpers.GenerateRefreshToken();
            user.RefreshTokenExp = DateTime.Now.AddDays(1);
            await _context.SaveChangesAsync();
            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            var refreshToken = user.RefreshToken;
            return new TokenDtoResponse(accessToken, refreshToken);
        }
        public async Task<TokenDtoResponse> NewTokenAsync(string token, RefreshTokenRequest refreshToken)
        {
            AppUser user = await _context.User.Where(u => u.RefreshToken == refreshToken.RefreshToken).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new SecurityTokenException("Invalid refresh token");
            }

            if (user.RefreshTokenExp < DateTime.Now)
            {
                throw new SecurityTokenException("Refresh token expired");
            }
            var login = SecurityHelpers.GetUserIdFromAccessToken(token.Replace("Bearer ", ""), _configuration["SecretKey"]);

            Claim[] userclaim = new[] {
                    new Claim(ClaimTypes.Name, "s20296"),
                    new Claim(ClaimTypes.Role, "user"),
                    new Claim(ClaimTypes.Role, "admin")
                };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtToken = new JwtSecurityToken(
                issuer: "https://localhost:5001",
                audience: "https://localhost:5001",
                claims: userclaim,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: creds
            );

            user.RefreshToken = SecurityHelpers.GenerateRefreshToken();
            user.RefreshTokenExp = DateTime.Now.AddDays(1);
            await _context.SaveChangesAsync();

            return new TokenDtoResponse(new JwtSecurityTokenHandler().WriteToken(jwtToken), user.RefreshToken);
        }
 


    }
}
