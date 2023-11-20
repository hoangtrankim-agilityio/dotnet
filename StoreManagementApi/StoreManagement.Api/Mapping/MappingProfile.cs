using AutoMapper;
using StoreManagement.Api.Resources;
using StoreManagement.Core.Models;

namespace StoreManagement.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to Resource
            CreateMap<User, UserResource>();
            CreateMap<Cart, CartResource>();

            // Resource to Domain
            CreateMap<UserResource, User>();
            // CreateMap<SaveMusicResource, Music>();
            CreateMap<CartResource, Cart>();
            // CreateMap<SaveArtistResource, Artist>();
        }
    }
}