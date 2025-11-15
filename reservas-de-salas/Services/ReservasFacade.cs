using reservas_de_salas.Interfaces;
using reservas_de_salas.Models;
using reservas_de_salas.Services.Strategy;

namespace reservas_de_salas.Services
{
    public class ReservasFacade
    {
        private readonly IReservaService _reservaService;
        private readonly IUsuarioService _usuarioService;
        private readonly ISalaService _salaService;
        private readonly IValidadorDeReservaStrategy _validadorHorario;
        private readonly IValidadorDeReservaStrategy _validadorCapacidade;

        public ReservasFacade(IReservaService reservaService, IUsuarioService usuarioService, ISalaService salaService,
                ValidadorDeReservaHorario validadorHorario, ValidadorDeReservaCapacidade validadorCapacidade)
        {
            _reservaService = reservaService;
            _usuarioService = usuarioService;
            _salaService = salaService;
            _validadorHorario = validadorHorario;
            _validadorCapacidade = validadorCapacidade;
        }

        public async Task<List<Reserva>> ListarReservasAsync()
        {
            var reservas = await _reservaService.GetAllAsync();
            return reservas.ToList();
        }
        public async Task<List<Usuario>> ListarUsuariosAsync()
        {
            var usuarios = await _usuarioService.GetAllAsync();
            return usuarios.ToList();
        }
        public async Task<List<Sala>> ListarSalasAsync()
        {
            var salas = await _salaService.GetAllSalasAsync();
            return salas.ToList();

        }
        public async Task<Dictionary<string, long>> GetIndicadoresAsync()
        {
            var totalReservas = (await _reservaService.GetAllAsync()).Count();
            var totalUsuarios = (await _usuarioService.GetAllAsync()).Count();
            var totalSalas = (await _salaService.GetAllSalasAsync()).Count();
            return new Dictionary<string, long>
            {
                [ "totalReservas" ] = totalReservas ,
                [ "totalUsuarios"]= totalUsuarios ,
                [ "totalSalas"]= totalSalas 
            };
        }

        public async Task<string> ReservaAsync(Reserva reserva)
        {
            if (reserva.HoraFim <= reserva.HoraInicio)
            {
                 return "Hora de fim deve ser maior que hora de início.";
            }

            if( reserva.Data.Date < DateTime.Today)
            {
                   return "Data da reserva não pode ser no passado.";
            }

            if(await _usuarioService.GetByIdAsync(reserva.UsuarioId) is null)
            {
                 return "Usuário não encontrado.";
            }

            if(await _salaService.GetByIdAsync(reserva.UsuarioId) is null)
            {
                 return "Sala não encontrada.";
            }

            _reservaService.setValidador(_validadorHorario);
            if(!await _reservaService.ValidateAsync(reserva))
            {
                 return "Horário da reserva conflita com outra reserva existente.";
            }

            _reservaService.setValidador(_validadorCapacidade);
            if(!await _reservaService.ValidateAsync(reserva))
            {
                 return "Número de pessoas excede a capacidade da sala.";
            }

            await _reservaService.SaveASync(reserva);
            return "Reserva realizada com sucesso.";
        }
        public async Task<string> AtualizarAsync(Reserva model)
        {
          try 
          {
            var reserva = await _reservaService.GetByIdAsync(model.Id);
                reserva.UsuarioId = model.UsuarioId;
                reserva.SalaId = model.SalaId;
                reserva.Data = model.Data;
                reserva.HoraInicio = model.HoraInicio;
                reserva.HoraFim = model.HoraFim;
                reserva.NumeroPessoas = model.NumeroPessoas;

                return await ReservaAsync(reserva);


            }
          catch(Exception ex)
          {
            return $"Erro ao atualizar a reserva: {ex.Message}";
           }
        }
        public async Task DeleteAsync(long id)
        {
            await _reservaService.DeleteAsync(id);
        }
        public async Task<Reserva> GetByIdASync(long id)
        {
            return await _reservaService.GetByIdAsync(id);
        }
    }
}
