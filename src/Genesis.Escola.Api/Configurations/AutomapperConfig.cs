using AutoMapper;
using Genesis.Escola.Api.ViewModel;
using Genesis.Escola.Business.Models;

namespace Genesis.Escola.Api.Configurations
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Aluno, AlunoViewModel>().ReverseMap();
            CreateMap<Carrossel, CarrosselViewModel>().ReverseMap();
            CreateMap<Cursos, CursosViewModel>().ReverseMap();
            CreateMap<CursoAcadesc, CursoAcadescViewModel>().ReverseMap();
            CreateMap<Noticia, NoticiaViewModel>().ReverseMap();
            CreateMap<Turma, TurmaViewModel>().ReverseMap();
            CreateMap<Galeria, GaleriaViewModel>().ReverseMap();
            CreateMap<GaleriaItens, GaleriaItensViewModel>().ReverseMap();
            CreateMap<Missao, MissaoViewModel>().ReverseMap();
            CreateMap<Sobre, SobreViewModel>().ReverseMap();
            CreateMap<Polo, PoloViewModel>().ReverseMap();
            CreateMap<Filosofia, FilosofiaViewModel>().ReverseMap();
            CreateMap<Comunicado, ComunicadoViewModel>().ReverseMap();
            CreateMap<Config, ConfigViewModel>().ReverseMap();
            CreateMap<EmailContato, EmailContatoViewModel>().ReverseMap();
            CreateMap<TurmaAcadesc, TurmaAcadescViewModel>().ReverseMap();
            CreateMap<Tarefa, TarefaViewModel>().ReverseMap();
            CreateMap<Cronograma, CronogramaViewModel>().ReverseMap();
            CreateMap<Notas, NotasViewModel>().ReverseMap();
            CreateMap<Disciplinas, DisciplinasViewModel>().ReverseMap();
        }
    }
}
