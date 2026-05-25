using System;

namespace MauiAppHotel.Models
{
    public class Quarto
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public decimal DiariaAdulto { get; set; }
        public decimal DiariaCrianca { get; set; }
    }
}
