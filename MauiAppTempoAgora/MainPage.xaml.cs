using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txt_cidade.Text))
                {
                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);

                    if (t != null)
                    {
                        string dados_previsao = "";

                        if (t.cod == 200)
                        {
                            dados_previsao = $"Latitude: {t.lat}\n" +
                                             $"Longitude: {t.lon}\n" +
                                             $"Nascer do Sol: {t.sunrise}\n" +
                                             $"Por do Sol: {t.sunset}\n" +
                                             $"Temperatura Máxima: {t.temp_max}ºC\n" +
                                             $"Temperatura Mínima: {t.temp_min}ºC\n" +
                                             $"Descrição: {t.description}\n" +
                                             $"Velocidade do Vento: {t.speed}m/s\n" +
                                             $"Visibilidade: {t.visibility/1000.0}km\n";
                        } else
                        {
                            dados_previsao = $"ERRO!\nCódigo de Saída: {t.cod}\n" + $"Não foi possível encontrar a cidade/país {txt_cidade.Text}";
                        }

                        lbl_response.Text = dados_previsao;
                    }
                } else
                {
                    lbl_response.Text = "Preencha a Cidade!";
                }

            } catch (Exception ex)
            {
                await DisplayAlertAsync("Ops", ex.Message, "OK");
            }
        }
    }
}
