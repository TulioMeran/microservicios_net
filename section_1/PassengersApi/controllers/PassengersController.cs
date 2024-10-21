using Microsoft.AspNetCore.Mvc;

namespace PassengersApi.controllers
{
    [Route("api/passenger")]
    [ApiController]
    public class PassengersController: ControllerBase 
    {
        private readonly List<Passenger> passengers = new List<Passenger>{
            new Passenger(){
                DateOfBirth = new DateTime(),
                Email = "rtulio007@gmail.com",
                FirstName = "Rafael",
                LastName = "Meran",
                FlightId = 1,
                Nationality = "Dominican",
                PassengerId = 1,
                PassportNumber = "AB123123",
                PhoneNumber = "8092321212"
            }
        };

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(passengers);
        }

        [Route("{id}")]
        [HttpGet]
        public ActionResult Get(int id)
        {
            var passenger = passengers.FirstOrDefault(p=>p.PassengerId == id);
            return Ok(passenger);
        }

        [Route("create")]
        [HttpPost]
        public ActionResult Post([FromBody] Passenger passenger)
        {
            passenger.PassengerId = passengers.Count + 1;
            passengers.Add(passenger);
            return CreatedAtAction("Get",passenger);
        }
    }

    public class Passenger
    {
        public int PassengerId { get; set; }           // Identificador único del pasajero
        public string FirstName { get; set; }          // Nombre del pasajero
        public string LastName { get; set; }           // Apellido del pasajero
        public string PassportNumber { get; set; }     // Número de pasaporte
        public DateTime DateOfBirth { get; set; }      // Fecha de nacimiento
        public string Nationality { get; set; }        // Nacionalidad del pasajero
        public string Email { get; set; }              // Correo electrónico
        public string PhoneNumber { get; set; }        // Número de teléfono
        public int FlightId { get; set; }              // Identificador del vuelo al que está asociado
    }
}