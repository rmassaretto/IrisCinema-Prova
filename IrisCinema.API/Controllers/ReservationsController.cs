using AutoMapper;
using Humanizer;
using IrisCinema.API.DTO.Reservation;
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
    public class ReservationsController : ControllerBase
    {
        private readonly CinemaContext _context;
        private readonly IMapper _mapper;

        public ReservationsController(CinemaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Reservations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations()
        {
            return await _context.Reservations.ToListAsync();
        }

        // GET: api/Reservations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reservation>> GetReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);

            if (reservation == null)
            {
                return NotFound();
            }

            return reservation;
        }
        
        // POST: api/Reservations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Reservation>> PostReservation(CreateReservationDto dto)
        {
            var seat = await _context.Seats.FindAsync(dto.SeatId);
            if (seat == null)
                return NotFound("Seat not found.");

            if (seat.Reserved)
                return Conflict("Seat already reserved.");

            seat.Reserved = true;

            var reservation = _mapper.Map<Reservation>(dto);

            _context.Reservations.Add(reservation);

            await _context.SaveChangesAsync();

            var response = _mapper.Map<ReservationResponseDto>(reservation);

            return CreatedAtAction(nameof(GetReservation), new { id = response.Id }, response);
        }

        // DELETE: api/Reservations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var reservation = await _context.Reservations.FirstOrDefaultAsync(r => r.Id == id);
            if (reservation == null)            
                return NotFound();

            var seat = await _context.Seats.FirstOrDefaultAsync(s => s.Id == reservation.SeatId);
            if (seat != null)
                seat.Reserved = false;

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }
    }
}
