using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiAppHotel.Models
{
    public class Hospedagem : INotifyPropertyChanged
    {
        // Propriedades de Identificação e Relacionamento
        public int Id { get; set; }
        public int QuartoId { get; set; }

        private Quarto _quartoSelecionado;
        public Quarto QuartoSelecionado
        {
            get => _quartoSelecionado;
            set
            {
                _quartoSelecionado = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ValorTotal)); // Atualiza o valor se o quarto mudar
            }
        }

        // Quantidade de Hóspedes com inicialização padrão amigável
        private int _qtdAdultos = 1;
        public int QtdAdultos
        {
            get => _qtdAdultos;
            set
            {
                _qtdAdultos = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ValorTotal)); // Recalcula o valor com a mudança de adultos
            }
        }

        private int _qtdCriancas = 0;
        public int QtdCriancas
        {
            get => _qtdCriancas;
            set
            {
                _qtdCriancas = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ValorTotal)); // Recalcula o valor com a mudança de crianças
            }
        }

        // Propriedade auxiliar para bloquear datas passadas no calendário do XAML
        public DateTime DataMinimaCheckIn => DateTime.Today;

        // Controle e Validação de Período da Estadia
        private DateTime _dataCheckIn = DateTime.Today;
        public DateTime DataCheckIn
        {
            get => _dataCheckIn;
            set
            {
                _dataCheckIn = value;
                OnPropertyChanged();

                // CORREÇÃO AUTOMÁTICA: Se o novo check-in ultrapassar ou igualar o check-out atual,
                // jogamos o check-out automaticamente para o dia seguinte do novo check-in.
                if (DataCheckOut <= _dataCheckIn)
                {
                    DataCheckOut = _dataCheckIn.AddDays(1);
                }

                OnPropertyChanged(nameof(Estadia));
                OnPropertyChanged(nameof(ValorTotal));
            }
        }

        private DateTime _dataCheckOut = DateTime.Today.AddDays(1);
        public DateTime DataCheckOut
        {
            get => _dataCheckOut;
            set
            {
                // TRAVA DE SEGURANÇA: Se o usuário tentar forçar uma data de saída menor
                // ou igual à de entrada, o sistema redefine a saída para o dia posterior ao check-in.
                if (value <= DataCheckIn)
                {
                    _dataCheckOut = DataCheckIn.AddDays(1);
                }
                else
                {
                    _dataCheckOut = value;
                }

                OnPropertyChanged();
                OnPropertyChanged(nameof(Estadia));
                OnPropertyChanged(nameof(ValorTotal));
            }
        }

        // Propriedade calculada do total de diárias
        public int Estadia
        {
            get
            {
                int dias = (DataCheckOut.Date - DataCheckIn.Date).Days;
                // Mantém a consistência de pelo menos 1 diária caso as datas coincidam
                return dias <= 0 ? 1 : dias;
            }
        }

        // Propriedade calculada do valor financeiro total da reserva
        public decimal ValorTotal
        {
            get
            {
                if (QuartoSelecionado == null)
                    return 0;

                decimal custoAdultos = QtdAdultos * QuartoSelecionado.DiariaAdulto;
                decimal custoCriancas = QtdCriancas * QuartoSelecionado.DiariaCrianca;

                return (custoAdultos + custoCriancas) * Estadia;
            }
        }

        // MECANISMO DE VINCULAÇÃO EM TEMPO REAL (Mete o aviso de modificação para a View)
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}