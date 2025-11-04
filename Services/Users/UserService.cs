using DocumentFormat.OpenXml.Office2010.Excel;
using Entity;
using Microsoft.AspNetCore.Http;
using Persistence.Permissions;
using Persistence.Users;
using Services.Password;
using Services.Positions;
using Services.Response;
using Services.Users.Models;
namespace Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserPersistence _functions;
        private readonly IPositionService _positionService;
        private readonly IPermissionPersitence _permissionFunction;
        private readonly IPasswordService _passwordService;
        
        

        public UserService(IUserPersistence functions, IPositionService positionService, IPermissionPersitence permissionFunction, IPasswordService passwordService)
        {
            _functions = functions;
            _positionService = positionService;
            _permissionFunction = permissionFunction;
            _passwordService = passwordService;
        }
        
        public int DeleteUser(int id)
        {
            int response = _functions.DeleteUser(id);
            if(response <= 0)
            {
                throw new ArgumentException("No se ha eliminado el registro");
            }
            return response;
        }

        public ResponseModel GetAllUsers()
        {
            var users = _functions.GetAllUser();

           var response = users.Select(u => new GetAllUserModel
            {
                id = u.id,
                name = u.name,
                positionId = u.positionId,
                position  = u.position?.name ?? string.Empty,
                creationDate = u.creationDate,
            }).ToList();

            return ResponseApiService.Response(200,response);

            
        }

        public ResponseModel GetUserById(int id)
        {
            var user = _functions.GetById(id);
            if(user == null)
            {
                return ResponseApiService.Response(StatusCodes.Status404NotFound,null,"Usuario no encontrado");
            }
            var response = new GetAllUserModel
            {
                id = user.id,
                name = user.name,
                password = user.password,
                positionId = user.positionId,
                position = user.position?.name ?? string.Empty

            };

            return ResponseApiService.Response(200,response);

        }
        public ResponseModel Login(int userId, string password)
        {
            var user = _functions.GetById(userId);
            if (user == null)
            {
                return ResponseApiService.Response(StatusCodes.Status404NotFound, null, "Usuario no encontrado");
            }

            bool isValid = _passwordService.verify(password,user.password);
            if (!isValid)
            {
                return ResponseApiService.Response(StatusCodes.Status401Unauthorized, null, "Contraseña incorrecta");

            }

            var response = new
            {
                id = user.id,
                name = user.name,
                positionId = user.positionId,
                position = user.position?.name
            };

            return ResponseApiService.Response(StatusCodes.Status200OK, response, "Inicio de sesión exitoso");

        }

        public List<UserNotesSummary> GetUserNotesSummaries(DateTime? startDate, DateTime? endDate)
        {

            var summary = _functions.GetAllSumary(startDate, endDate);
            return summary;
        }

        public ResponseModel InsertUser(CreateUserModel model)
        {
            var position = _positionService.GetById(model.positionId);
            if(position == null)
            {
               return ResponseApiService.Response(StatusCodes.Status400BadRequest, null, $"La posición con id {model.positionId} no existe.");
                
            }
            var password = _passwordService.hashpassword(model.password);
            var user = new User
            {
                name = model.name,
                password = password,
                positionId = model.positionId

            };
            var userId = _functions.InsertUser(user);
            
            if(model.permissionIds != null && model.permissionIds.Any())
            {
                _permissionFunction.InsertUserPermissions(userId,model.permissionIds);
            }
            return ResponseApiService.Response(StatusCodes.Status201Created, null, "Usuario insertado correctamente");
        }

        public void UpdateUser(int id, UpdateUserModel model)
        {
            var position = _positionService.GetById(model.positionId);
            if (position == null)
            {
                throw new ArgumentException($"La posición con id {model.positionId} no existe.");
            }
            var userExist = _functions.GetById(id);
            if (userExist == null)
            {
                throw new ArgumentException($"El usuario con id {id} no existe.");
            }

           
            var user = new User
            {
                id = id,
                name = model.name,
                positionId = model.positionId
            };

            _functions.UpdateUser(user);

        }
    }
}
