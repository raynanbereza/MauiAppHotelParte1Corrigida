using System;
using System.Collections.Generic; // Obrigatório para usar a List<>
using Microsoft.Maui.Controls;
using MauiAppHotel.Models;

namespace MauiAppHotel.Views
{
    public partial class ContratacaoHospedagem : ContentPage
    {
        // Criamos uma lista para armazenar os tipos de quartos do hotel
        public List<Quarto> ListaQuartos { get; set; }

        public ContratacaoHospedagem()
        {
            InitializeComponent();

            // 1. Cadastramos os quartos disponíveis e seus respectivos preços por diária
            ListaQuartos = new List<Quarto>
            {
                new Quarto { Id = 1, Descricao = "Suíte Luxo Super Master", DiariaAdulto = 150.00m, DiariaCrianca = 75.00m },
                new Quarto { Id = 2, Descricao = "Suíte Premium Executiva", DiariaAdulto = 110.00m, DiariaCrianca = 55.00m },
                new Quarto { Id = 3, Descricao = "Quarto Standard Familiar", DiariaAdulto = 80.00m, DiariaCrianca = 40.00m }
            };

            // 2. Vinculamos fisicamente essa lista ao componente Picker da tela
            pck_quarto.ItemsSource = ListaQuartos;

            // 3. Define a classe Hospedagem como o contexto de dados para os Bindings funcionarem
            this.BindingContext = new Hospedagem();
        }

        private async void OnAvancarClicked(object sender, EventArgs e)
        {
            var hospedagemAtual = (Hospedagem)BindingContext;

            if (hospedagemAtual.QuartoSelecionado == null)
            {
                await DisplayAlert("Erro", "Por favor, selecione uma acomodação antes de avançar.", "OK");
                return;
            }

            // Avança para a tela de confirmação repassando o objeto preenchido
            await Navigation.PushAsync(new HospedagemContratada(hospedagemAtual));
        }
    }
}