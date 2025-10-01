using Entity;

namespace Persistence.Positions
{
    public interface IPositionPersistence
    {
        List<Position> GetAllPositions();
        void InsertPosition(Position position);
        void UpdatePosition(Position position);
        int DeletePosition(int id); 
        Position? GetById(int id);
    }
}
