using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webApi_test.Data;
using webApi_test.Models;
using webApi_test.ViewModels.Booking;
using webApi_test.ViewModels.Resource;

namespace webApi_test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly ApplicationContext _applicationcontext;
        private readonly IMapper _mapper;

        public BookingController(ApplicationContext applicationcontext,IMapper mapper)
        {
            _applicationcontext = applicationcontext;
            _mapper = mapper;
        }

        //To get the list of all available Resources in specific time interval
        //GET: api/BookingModels?parentId?TypeId?startdatetime?enddatetime

        [HttpGet]
        public ActionResult GetAvailable([FromQuery]int parentId, [FromQuery] int TypeId, [FromQuery] DateTime startdatetime, [FromQuery] DateTime enddatetime) {
            var resourceList = _applicationcontext.Resources.Where(x => x.parentId == parentId && x.TypeID == TypeId).ToList();
            List<Resource_ViewModel> AvailableResources = new List<Resource_ViewModel>();
            if (startdatetime != null && enddatetime != null && startdatetime<enddatetime)
            {
                foreach (var item in resourceList)
                {
                    bool Flag = false;
                    var resourceBookingList = _applicationcontext.Bookings.Where(x => x.ResId == item.Id).ToList();
                    foreach (var bookingEntry in resourceBookingList)
                    {
                        if (CheckOverlap(startdatetime, enddatetime, bookingEntry.StartTime, bookingEntry.EndTime))
                        {
                            Flag = true;
                        }
                    }
                    if (Flag == false)
                    {
                        var itemview = _mapper.Map<Resource_ViewModel>(item);
                        AvailableResources.Add(itemview);
                    }

                }

                return Ok(new { Available = AvailableResources });
            }
            else
            {
                return BadRequest(new { message = "Null vaule in startTime or EndTime" });
            }
        }


        // To check the overlap of to diferent Time ranges
        public bool CheckOverlap(DateTime startTime1, DateTime endTime1, DateTime startTime2, DateTime endTime2) {
            return (startTime1 < endTime2) && (startTime2 < endTime1) && (startTime1 < endTime1) && (startTime2 < endTime2);
        }







        // PUT: api/BookingModels/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookingModel(Guid id, BookingModel bookingModel)
        {
            if (id != bookingModel.Id)
            {
                return BadRequest();
            }

            _applicationcontext.Entry(bookingModel).State = EntityState.Modified;

            try
            {
                await _applicationcontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BookingModels
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<BookingModel>> PostBooking(BookingViewModel booking)
        {
            if (booking.StartTime != null && booking.EndTime != null && booking.StartTime < booking.EndTime)
            {
                var resource = _applicationcontext.Resources.Find(booking.ResId);
                if (resource != null)
                {
                    bool Flag = false;
                    var resourceBookingList = _applicationcontext.Bookings.Where(x => x.ResId == resource.Id).ToList();
                    foreach (var bookingEntry in resourceBookingList)
                    {
                        if (CheckOverlap(booking.StartTime, booking.EndTime, bookingEntry.StartTime, bookingEntry.EndTime))
                        {
                            return BadRequest(new { Error = "Already a booking with same parameters" });
                        }
                    }

                    var entity = _applicationcontext.Add(_mapper.Map<BookingModel>(booking)).Entity;

                    _applicationcontext.SaveChanges();
                    return Ok(new { message = "Resource Booking Done ", BookingId = entity.Id });
                }
                else
                    return BadRequest(new { Error = "No Such resource Found" });
            }
            else
            {
                return BadRequest(new { Error = "start time should be less than end time " });
            }
        }

        // DELETE: api/BookingModels/5
        [HttpDelete]
        public async Task<ActionResult<BookingModel>> DeleteBookingModel(Guid id)
        {
            var bookingModel = await _applicationcontext.Bookings.FindAsync(id);
            if (bookingModel == null)
            {
                return NotFound();
            }

            _applicationcontext.Bookings.Remove(bookingModel);
            await _applicationcontext.SaveChangesAsync();

            return Ok(new { message = "Booking Deleted"});
        }

        private bool BookingModelExists(Guid id)
        {
            return _applicationcontext.Bookings.Any(e => e.Id == id);
        }
    }
}
