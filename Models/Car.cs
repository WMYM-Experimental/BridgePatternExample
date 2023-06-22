using BridgePatternExample.Data;
using System.ComponentModel.DataAnnotations;

namespace BridgePatternExample.Models
{
    public class Car
    {
        public int Id { get; set; }
        [StringLength(150)]
        public string Name { get; set; }
        [StringLength(150)]
        public string Model { get; set; }
        [StringLength(150)]
        public string Color { get; set; }
        [StringLength(150)]
        public string Brand { get; set; }
        [Range(1950, int.MaxValue)]
        public int Year { get; set; }
    }

    public abstract class CarService
    {
        protected ICarRepository _repository;

        public CarService(ICarRepository repository)
        {
            _repository = repository;
        }

        public abstract List<Car> GetAllCars();
        public abstract Car GetCar(int id);
        public abstract void CreateCar(Car car);
        public abstract void UpdateCar(Car car);
        public abstract void DeleteCar(int id);
        public abstract bool CarExists(int id);
    }

    // Clase derivada de abstracción
    public class CarServiceBridge : CarService
    {
        public CarServiceBridge(ICarRepository repository) : base(repository)
        {
        }

        public override List<Car> GetAllCars()
        {
            return _repository.GetAll();
        }

        public override Car GetCar(int id)
        {
            return _repository.Get(id);
        }

        public override void CreateCar(Car car)
        {
            _repository.Create(car);
        }

        public override void UpdateCar(Car car)
        {
            _repository.Update(car);
        }

        public override void DeleteCar(int id)
        {
            _repository.Delete(id);
        }

        public override bool CarExists(int id)
        {
            return _repository.CarExists(id);
        }
    }

    // Clase base de implementación
    public interface ICarRepository
    {
        List<Car> GetAll();
        Car Get(int id);
        void Create(Car car);
        void Update(Car car);
        void Delete(int id);
        bool CarExists(int id);
    }

    // Clase derivada de implementación (puedes utilizar una implementación de acceso a datos real aquí)
    public class CarRepository : ICarRepository
    {
        private readonly BridgePatternExampleContext _context;

        public CarRepository(BridgePatternExampleContext context)
        {
            _context = context;
        }

        public List<Car> GetAll()
        {
            return _context.Car.ToList();
        }

        public Car Get(int id)
        {
            return _context.Car.FirstOrDefault(c => c.Id == id);
        }

        public void Create(Car car)
        {
            _context.Car.Add(car);
            _context.SaveChanges();
        }

        public void Update(Car car)
        {
            _context.Car.Update(car);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var car = _context.Car.FirstOrDefault(c => c.Id == id);
            if (car != null)
            {
                _context.Car.Remove(car);
                _context.SaveChanges();
            }
        }

        public bool CarExists(int id)
        {
            return _context.Car.Any(c => c.Id == id);
        }
    }
}
