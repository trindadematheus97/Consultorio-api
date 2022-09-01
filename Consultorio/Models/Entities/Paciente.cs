using Consultorio.Models.Dtos;
using System.Collections.Generic;

namespace Consultorio.Models.Entities
{
    public class Paciente : Base
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
        public string Cpf { get; set; }
        public List<Consulta> Consultas { get; set; }

        public static class FactoryPaciente
        {
            public static Paciente Registrar(PacienteAdicionarDto p)
            {
                return new Paciente()
                {
                    Nome = p.Nome,
                    Email = p.Email,
                    Celular = p.Celular,
                    Cpf = p.Cpf
                };
            }
        }
    }
}