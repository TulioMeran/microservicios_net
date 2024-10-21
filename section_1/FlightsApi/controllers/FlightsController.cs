using Microsoft.AspNetCore.Mvc;

namespace FlightsApi.controllers
{
    [Route("api/flight")]
    [ApiController]
    public class FlightsController: ControllerBase 
    {

      private readonly List<Flight> flights = new List<Flight>{
       new Flight() {
        FlightId = 1,
        FlightNumber = "aloha 1",
        Airline = "Jet Blue",
        Capacity = 5,
        ArrivalTime = new DateTime(),
        AvailableSeats = 5,
        Departure = "Santo Domingo",
        Destination = "Santiago",
        DepartureTime = new DateTime()
       }
      };


      [HttpGet]
      public ActionResult Get()
      {
        return Ok(flights);
      }

      [HttpGet]
      [Route("{id}")]
      public ActionResult Get(int id)
      {
        var flight = flights.FirstOrDefault(f=>f.FlightId == id);
        return Ok(flight);
      }


      
      [HttpPost]
      [Route("create")]
      public ActionResult Create([FromBody] Flight flight)
      {
        flight.FlightId = flights.Count + 1;
        flights.Add(flight);
        
        return CreatedAtAction("Get", flight);
      }

    }

    public class Flight
    {
        public int FlightId { get; set; }              // Identificador único del vuelo
        public string FlightNumber { get; set; }       // Número de vuelo
        public string Departure { get; set; }          // Ciudad de salida
        public string Destination { get; set; }        // Ciudad de destino
        public DateTime DepartureTime { get; set; }    // Hora de salida
        public DateTime ArrivalTime { get; set; }      // Hora de llegada
        public string Airline { get; set; }            // Aerolínea que opera el vuelo
        public int Capacity { get; set; }              // Capacidad del vuelo
        public int AvailableSeats { get; set; }        // Asientos disponibles
    }
}