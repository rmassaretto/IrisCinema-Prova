using AutoMapper;
using Humanizer;
using IrisCinema.API.DTO.Session;
using IrisCinema.API.Models;
using IrisCinema.API.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IrisCinema.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessionsController : ControllerBase
    {
        private readonly CinemaContext _context;
        private readonly IMapper _mapper;

        public SessionsController(CinemaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Sessions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SessionDto>>> GetSessions()
        {
            var sessions = await _context.Sessions
                .Include(s => s.Seats)
                .Include(s => s.Room)
                .ToListAsync();
            
            var sessionDtos = _mapper.Map<List<SessionDto>>(sessions);
            return sessionDtos;
        }

        // GET: api/Sessions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SessionDto>> GetSession(int id)
        {
            var session = await _context.Sessions
                .Include(s => s.Seats)  // Carrega os assentos
                .Include(s => s.Room)   // Carrega a sala
                .FirstOrDefaultAsync(s => s.Id == id);

            if (session == null)
                return NotFound($"Session with ID {id} not found.");

            var sessionDto = _mapper.Map<SessionDto>(session);
            return sessionDto;
        }

        // POST: api/Sessions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SessionDto>> PostSession(CreateSessionDto dto)
        {
            var room = await _context.Rooms.FindAsync(dto.RoomId);
            if (room == null)
                return NotFound($"Room with ID {dto.RoomId} not found.");

            var session = new Session
            {
                StartTime = dto.StartTime,
                Room = room
            };

            session.Seats = Enumerable.Range(1, 20) //Capacity
                .Select(i => new Seat { Number = i })
                .ToList();

            _context.Sessions.Add(session);
            await _context.SaveChangesAsync();

            var sessionDto = _mapper.Map<SessionDto>(session);

            return CreatedAtAction(nameof(GetSession), new { id = session.Id }, sessionDto);
        }

        // DELETE: api/Sessions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSession(int id)
        {
            var session = await _context.Sessions.FindAsync(id);
            if (session == null)
            {
                return NotFound();
            }

            foreach (var seat in session.Seats)
                _context.Seats.Remove(seat);

            _context.Sessions.Remove(session);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SessionExists(int id)
        {
            return _context.Sessions.Any(e => e.Id == id);
        }
    }
}
