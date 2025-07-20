using Microsoft.AspNetCore.Mvc;
using room_service.Data;
using room_service.Models;

namespace room_service.Controllers
{
   // [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly RoomDbContext _context;

        public RoomController(RoomDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        public IActionResult GetAllRooms()
        {
            var rooms = _context.Rooms?.ToList();
            if (rooms == null || rooms.Count == 0)
                return NotFound("No rooms found.");

            return Ok(rooms);
        }

        [HttpGet("{id}")]
        public IActionResult GetRoomById(int id)
        {
            var room = _context.Rooms?.Find(id);
            if (room == null)
                return NotFound();

            return Ok(room);
        }
        [HttpGet("available")]
        public IActionResult GetAvailableRooms()
        {
            if (_context.Rooms == null)
                return NotFound("No available rooms found.");

            var availableRooms = _context.Rooms
                .Where(r => r.IsAvailable == true)
                .ToList();

            if (availableRooms.Count == 0)
                return NotFound("No available rooms found.");

            return Ok(availableRooms);
        }
[HttpGet("by-roomnumber/{roomNumber}")]
public IActionResult GetRoomByRoomNumber(int roomNumber)
{if (_context.Rooms == null)
        return NotFound("No available rooms found.");
    var room = _context.Rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);
    if (room == null)
        return NotFound($"Room with number {roomNumber} not found.");

    return Ok(room);
}
[HttpPut("{id}/availability")]
public IActionResult UpdateRoomAvailability(int id, [FromQuery] bool isAvailable)
{if (_context.Rooms == null)
        return NotFound("No available rooms found.");
    var room = _context.Rooms.Find(id);
    if (room == null)
        return NotFound($"Room with ID {id} not found.");

    room.IsAvailable = isAvailable;
    _context.SaveChanges();

    return Ok($"Room {id} availability updated to {isAvailable}.");
}


        [HttpPost]
        public IActionResult AddRoom([FromBody] Room room)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Rooms?.Add(room);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetRoomById), new { id = room.RoomId }, room);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRoom(int id, [FromBody] Room updatedRoom)
        {
            var room = _context.Rooms?.Find(id);
            if (room == null)
                return NotFound();

            room.RoomNumber = updatedRoom.RoomNumber;
            room.RoomType = updatedRoom.RoomType;
            room.Occupancy = updatedRoom.Occupancy;
            room.RoomPrice = updatedRoom.RoomPrice;
            room.IsAvailable = updatedRoom.IsAvailable;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRoom(int id)
        {
            var room = _context.Rooms?.Find(id);
            if (room == null)
                return NotFound();

            if (_context.Rooms != null)
            {
                _context.Rooms.Remove(room);
                _context.SaveChanges();
            }
            return NoContent();
        }
    }
}
