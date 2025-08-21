
using Entity;


namespace Services.Positions
{
    public interface IPositionService
    {
        int DeletePosition(int id);
        List<Position> GetAllPositions();
        Position GetById(int id);
        void InsertPosition(Position position);
        void UpdatePosition(int id, Position position);


    }
}
