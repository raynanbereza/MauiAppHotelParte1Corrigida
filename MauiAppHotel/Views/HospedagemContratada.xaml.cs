using System;
using Microsoft.Maui.Controls;
using MauiAppHotel.Models;

namespace MauiAppHotel.Views
{
    public partial class HospedagemContratada : ContentPage
    {
        // Alteramos o construtor para receber a hospedagem que veio da tela anterior
        public HospedagemContratada(Hospedagem hospedagemRecebida)
        {
            InitializeComponent();

            // Define a hospedagem recebida como o contexto de dados da página
            this.BindingContext = hospedagemRecebida;
        }

        // Ação do botão Confirmar
        private async void OnConfirmarClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Sucesso", "Sua reserva foi confirmada com sucesso! Esperamos por você.", "OK");

            // Retorna para a tela inicial limpando a pilha de navegação
            await Navigation.PopToRootAsync();
        }

        // Ação do botão Voltar
        private async void OnVoltarClicked(object sender, EventArgs e)
        {
            // Apenas volta para a página anterior (ContratacaoHospedagem)
            await Navigation.PopAsync();
        }
    }
}