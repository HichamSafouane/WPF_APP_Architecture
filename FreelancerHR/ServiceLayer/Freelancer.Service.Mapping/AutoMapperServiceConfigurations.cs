using AutoMapper;
using FreelancerHR.DTO;
using FreelancerHR.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancerHR.Service.Mapping
{
    [Export(typeof(IAutoMapperServiceConfiguration))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class AutoMapperServiceConfiguration : IAutoMapperServiceConfiguration
    {
        static AutoMapperServiceConfiguration()
        {
            //FreelancerService
            AutoMapper.Mapper.CreateMap<Freelancer, FreelancerDTO>().ForMember(t => t.Skill, m => m.MapFrom(s => s.Skill.Name));

            //CompagnyService
            Mapper.CreateMap<Compagny, CompagnyDTO>().ForMember(t => t.AddressLine1, m => m.MapFrom(s => s.Address.AddressLine1)).
                ForMember(t => t.AddressLine2, m => m.MapFrom(s => s.Address.AddressLine2)).
                ForMember(t => t.City, m => m.MapFrom(s => s.Address.City)).
                ForMember(t => t.PostalCode, m => m.MapFrom(s => s.Address.PostalCode));

            Mapper.CreateMap<CompagnyDTO, Compagny>().ForMember(dest => dest.Address,
                opts => opts.MapFrom(
                    src => new Address
                    {
                        AddressID = src.AddressID,
                        AddressLine1 = src.AddressLine1,
                        AddressLine2 = src.AddressLine2,
                        City = src.City,
                        PostalCode = src.PostalCode
                    }));


            //HiringOfferService
            AutoMapper.Mapper.CreateMap<Freelancer, FreelancerDTO>().ForMember(t => t.Skill, m => m.MapFrom(s => s.Skill.Name));
            Mapper.CreateMap<HiringOfferEmployee, HiringOfferEmployeeDTO>();

            Mapper.CreateMap<HiringOffer, HiringOfferDTO>().
                ForMember(t => t.Compagny, m => m.MapFrom(s => s.Compagny.Name)).
                ForMember(t => t.CompagnyEmail, m => m.MapFrom(s => s.Compagny.EmailAddress)).
                ForMember(t => t.HiredEmployeeIDs, m => m.MapFrom(s => s.HiringOfferEmployee));

        }

        public AutoMapperServiceConfiguration()
        {

        }

        public void Configure()
        {
            
        }

        public static IEnumerable<TOutput> MapEnumerable<TInput, TOutput>(IEnumerable<TInput> source)
        {
            return Mapper.Map<IEnumerable<TInput>, IEnumerable<TOutput>>(source);
        }
    }
}
