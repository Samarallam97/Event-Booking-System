using AutoMapper;
using Website.API.DTOs.Application.Category;
using Website.API.DTOs.Application.Event;
using Website.API.DTOs.Application.Tag;
using Website.Core.Entities.Application;

namespace Website.API.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<TagToAddOrUpdate, Tag>();
        CreateMap<Tag, TagDTOEn>();
        CreateMap<Tag, TagDTOAR>();

        CreateMap<CategoryToAddOrUpdate, Category>();

        CreateMap<Category, CategoryDTOEn>();
        CreateMap<Category, CategoryDTOAR>();

        CreateMap<EventToAddOrUpdate, Event>();



        CreateMap<Event, EventDTOEn>()
             .ForMember(dest => dest.EventTagsNames,
             opt => opt.MapFrom(src => src.EventTags != null
            ? src.EventTags.Select(t => t.Tag.Name).ToList()
            : new List<string>()));


        CreateMap<Event, EventDTOAR>()
             .ForMember(dest => dest.EventTagsNamesAR,
             opt => opt.MapFrom(src => src.EventTags != null
            ? src.EventTags.Select(t => t.Tag.NameAR).ToList()
            : new List<string>()));


        #region Old

        //CreateMap<CategoryToAdd, Category>()
        //	.ForMember(dest => dest.IconUrl, opt => opt.MapFrom( src => 
        //	ImageUrlGenerator.GetImageUrl(src.IconImage))); ;

        //CreateMap<CategoryToUpdate, Category>().ForMember(dest => dest.IconUrl, opt => opt.MapFrom(src =>
        //	ImageUrlGenerator.GetImageUrl(src.IconImage)));

        //CreateMap<Category, CategoryToReturnDTO>(); 
        #endregion

        #region Old

        //CreateMap<EventToAdd, Event>()
        //	.ForMember(dest => dest.MainImageUrl, opt => opt.MapFrom(src =>
        //	ImageUrlGenerator.GetImageUrl(src.MainImage)))
        //	.ForMember(des => des.EventTags, opt => opt.MapFrom(src =>
        //	MapFromTagIdToEventTagEntity(src.EventTags ,src.Id)));

        //CreateMap<EventToUpdate, Event>().ForMember(dest => dest.MainImageUrl, opt => opt.MapFrom(src =>
        //	ImageUrlGenerator.GetImageUrl(src.MainImage)));

        //CreateMap<Event, EventToReturn>();

        //CreateMap<Event, EventDTO>().ForMember(des => des.EventTags, opt => opt.MapFrom(src =>
        //src.EventTags.Select(e => e.TagId))); 
        #endregion


    }

    private ICollection<EventTag> MapFromTagIdToEventTagEntity(ICollection<string> eventTags, string eventId)
    {
        ICollection<EventTag> EventTages = new List<EventTag>();

        foreach (var item in eventTags)
        {
            EventTages.Add(new EventTag()
            {
                EventId = eventId,
                TagId = item
            });

        }

        return EventTages;

    }
}
