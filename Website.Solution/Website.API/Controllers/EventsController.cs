using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Website.API.DTOs.Application.Event;
using Website.API.Errors;
using Website.API.Helpers;
using Website.Core.Entities.Application;
using Website.Core.ServiceInterfaces;
using Website.Core.Specifications.Product;

namespace Website.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
    private readonly IEventService _eventService;
    private readonly IMapper _mapper;

    public EventsController(IEventService eventService, IMapper mapper)
    {
        _eventService = eventService;
        _mapper = mapper;
    }

    //[Authorize(Roles = "Admin")]
    [HttpPost("add")]
    public async Task<IActionResult> AddEvent([FromBody] EventToAddOrUpdate eventDTO)
    {
        var Event = _mapper.Map<EventToAddOrUpdate, Event>(eventDTO);

        foreach (var item in eventDTO.EventTagsIds)
        {
            Event.EventTags.Add(new EventTag()
            {
                TagId = item,
                EventId = Event.Id
            });

        }
        var added = await _eventService.Add(Event);

        if (!added)
            return BadRequest(new BaseErrorResponse(400));

        return Ok(Event);
    }

    //[Authorize(Roles = "Admin")]
    [HttpPut("update")]
    public async Task<IActionResult> UpdateEvent([FromBody] EventToAddOrUpdate eventDTO)
    {
        var eventFromDb = _eventService.GetEventById(eventDTO.Id);

        if (eventFromDb is null)
            return NotFound(new BaseErrorResponse(404, $"Event with Id {eventDTO.Id} Not Found"));

        //var ImageUrl = await ImageUrlGenerator.GetImageUrl(eventDTO.MainImage);

        //if (ImageUrl is null)
        //	return BadRequest(new BaseErrorResponse(400, "Error while processing the image , images with size > 5MB not allowed"));

        ICollection<EventTag> eventTags = new List<EventTag>();

        foreach (var item in eventDTO.EventTagsIds)
        {
            eventTags.Add(new EventTag()
            {
                TagId = item,
                EventId = eventFromDb.Id
            });

        }
        eventFromDb.Title = eventDTO.Title;
        eventFromDb.TitleAR = eventDTO.TitleAR;
        eventFromDb.MainImageUrl = eventDTO.MainImageUrl;
        eventFromDb.Description = eventDTO.Description;
        eventFromDb.DescriptionAR = eventDTO.DescriptionAR;
        eventFromDb.ShortDescription = eventDTO.ShortDescription;
        eventFromDb.ShortDescriptionAR = eventDTO.ShortDescriptionAR;
        eventFromDb.StartDate = eventDTO.StartDate;
        eventFromDb.EndDate = eventDTO.EndDate;
        eventFromDb.Location = eventDTO.Location;
        eventFromDb.LocationAR = eventDTO.LocationAR;
        eventFromDb.VenueAddress = eventDTO.VenueAddress;
        eventFromDb.MaxAttendees = eventDTO.MaxAttendees;
        eventFromDb.Price = eventDTO.Price;

        eventFromDb.EventTags = eventTags;
        eventFromDb.Status = eventDTO.Status;
        eventFromDb.StatusAR = eventDTO.StatusAR;
        eventFromDb.CategoryId = eventDTO.CategoryId;


        var updated = await _eventService.Update(eventFromDb);

        if (!updated)
            return BadRequest(new BaseErrorResponse(400));

        return Ok(eventDTO);
    }

    //[Authorize(Roles = "Admin")]
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteEvent(string Id)
    {
        var eventFromDb = _eventService.GetEventById(Id);

        if (eventFromDb is null)
            return NotFound(new BaseErrorResponse(404, $"Event with Id {Id} Not Found"));

        var deleted = await _eventService.Delete(eventFromDb);

        if (!deleted)
            return BadRequest(new BaseErrorResponse(400));

        return Ok();
    }

    [HttpGet]
    public ActionResult<IReadOnlyList<EventDTOEn>> GetAll([FromQuery] EventParams eventParams)
    {
        var events = _eventService.GetAllEvents(eventParams);

        var count = _eventService.GetCount(eventParams);

        if (eventParams.Language?.ToLower() == "en")
        {

            var eventDTOs = _mapper.Map<IReadOnlyList<Event>, IReadOnlyList<EventDTOEn>>(events);
            return Ok(new PaginationResponse<EventDTOEn>
                (eventParams.PageSize, eventParams.PageIndex, count, eventDTOs));
        }
        else
        {
            var eventDTOs = _mapper.Map<IReadOnlyList<Event>, IReadOnlyList<EventDTOAR>>(events);
            return Ok(new PaginationResponse<EventDTOAR>
            (eventParams.PageSize, eventParams.PageIndex, count, eventDTOs));
        }
    }

    [HttpGet("{id}")]
    public ActionResult<EventDTOEn> GetById(string id, string language)
    {
        var Event = _eventService.GetEventById(id);

        if (Event is null)
            return NotFound(new BaseErrorResponse(404));



        EventDTO eventDTO;

        if (language == "En")
            eventDTO = _mapper.Map<Event, EventDTOEn>(Event);
        else
            eventDTO = _mapper.Map<Event, EventDTOAR>(Event);

        return Ok(eventDTO);
    }

    //[Authorize(Roles = "User,Admin")]
    [HttpPost("register")]
    public IActionResult RegisterUserForEvent(string userId, string eventId)
    {
        var registered = _eventService.RegisterUserForEvent(userId, eventId);

        if (!registered)
            return BadRequest(new BaseErrorResponse(400));

        return Ok();
    }
    //[Authorize(Roles = "User,Admin")]
    [HttpPost("cancel")]
    public IActionResult CancelEventRegistration(string userId, string eventId)
    {
        var canceled = _eventService.CancelEventRegistration(userId, eventId);

        if (!canceled)
            return BadRequest(new BaseErrorResponse(400));

        return Ok();
    }

}
