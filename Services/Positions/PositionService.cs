using Entity;
using Persistence.Positions;

namespace Services.Positions
{
    public class PositionService : IPositionService
    {
        private readonly IPositionPersistence _functions;

        public PositionService(IPositionPersistence functions) {  
            _functions = functions; 
        }
        public int DeletePosition(int id)
        {
            int response = _functions.DeletePosition(id);
            if(response <= 0)
            {
                throw new ArgumentException("No se ha eliminado el registro");
            }
            return response;
        }

        public List<Position> GetAllPositions()
        {
            return _functions.GetAllPositions();
        }

        public Position GetById(int id)
        {
            return _functions.GetById(id);
        }

        public void InsertPosition(Position position)
        {
            _functions.InsertPosition(position);
        }

        public void UpdatePosition(int id, Position position)
        {
            var positionExits = _functions.GetById(id);
            if(positionExits != null)
            {
                position.id = id;
                _functions.UpdatePosition(position);
            }
            
        }
    }
}
