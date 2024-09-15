using CapaEntidades.Dtos;
using CapaEntidades.Modelos;
using Riok.Mapperly.Abstractions;

namespace CapaEntidades.Mapeador
{
    [Mapper]
    public partial class PersonaMapper
    {
        public partial PersonaDto Persona_PersonaDtol(Persona persona);
        public partial Persona PersonaDto_Persona(PersonaDto personaDto);
    }
}
