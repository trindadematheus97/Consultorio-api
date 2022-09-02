using AutoMapper;
using Consultorio.Models.Dtos;
using Consultorio.Models.Entities;
using System.Linq;

namespace Consultorio.Helpers
{
    public class ConsultorioProfile : Profile
    {
        public ConsultorioProfile()
        {
            CreateMap<Paciente, PacienteDetailsDto>().ReverseMap();
            CreateMap<ConsultaDto, Consulta>()
                .ForMember(dest => dest.Profissional, opt => opt.Ignore())
                .ForMember(dest => dest.Especialidade, opt => opt.Ignore());

            CreateMap<Consulta, ConsultaDto>()
                .ForMember(dest => dest.Profissional, opt => opt.MapFrom(src => src.Profissional.Nome))
                .ForMember(dest => dest.Especialidade, opt => opt.MapFrom(src => src.Especialidade.Nome));

            CreateMap<PacienteAdicionarDto, Paciente>();
            CreateMap<PacienteAtualizarDto, Paciente>()
                .ForAllMembers(opt => opt.Condition((src, opt, srcMember)=> srcMember != null));


            CreateMap<Profissional, ProfissionalDetalhesDto>()
                .ForMember(dest => dest.TotalConsultas, opt => opt.MapFrom(src => src.Consultas.Count()))
                .ForMember(dest => dest.Especialidades, opt => opt.MapFrom(src =>
                src.Especialidades.Select(x => x.Nome).ToArray()));

            CreateMap<ProfissionalAdicionarDto, Profissional>();
            CreateMap<ProfissionaAtualizarDto, Profissional>()
                 .ForAllMembers(opt => opt.Condition((src, opt, srcMember) => srcMember != null)); ;


        }
    }
}
