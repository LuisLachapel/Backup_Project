using Entity;
using Persistence.Users;
using Services.Positions;
using Services.Users.Models;
namespace Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserPersistence _functions;
        private readonly IPositionService _positionService;
        

        public UserService(IUserPersistence functions, IPositionService positionService)
        {
            _functions = functions;
            _positionService = positionService;
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

        public List<GetAllUserModel> GetAllUsers()
        {
            var users = _functions.GetAllUser();

            return users.Select(u => new GetAllUserModel
            {
                id = u.id,
                name = u.name,
                positionId = u.positionId,
                position  = u.position?.name ?? string.Empty,
                creationDate = u.creationDate,
            }).ToList();

            
        }

        public GetAllUserModel GetUserById(int id)
        {
            var user = _functions.GetById(id);
            if(user == null)
            {
                return null;
            }
            return new GetAllUserModel
            {
                id = user.id,
                name = user.name,
                positionId = user.positionId,
                position = user.position?.name ?? string.Empty

            };
        }

        public void InsertUser(CreateUserModel model)
        {
            var position = _positionService.GetById(model.positionId);
            if(position == null)
            {
                throw new ArgumentException($"La posición con id {model.positionId} no existe.");
            }
            var user = new User
            {
                name = model.name,
                positionId = model.positionId
            };
            _functions.InsertUser(user);
        }

        public void UpdateUser(int id, CreateUserModel model)
        {
            var position = _positionService.GetById(model.positionId);
            if (position == null)
            {
                throw new ArgumentException($"La posición con id {model.positionId} no existe.");
            }
            var userExist = _functions.GetById(id);
            if (userExist != null)
            {

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
}
