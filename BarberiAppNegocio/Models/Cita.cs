﻿using BarberiApp.WebApi.Models;

namespace BarberiApp.WebApi.Models
{
    public class Cita
    {
        public int CitaID { get; set; }
        public string? Fecha { get; set; }
        public string? Hora { get; set; }
        public string? Estado {  get; set; }
        public int ClienteId {  get; set; }

    }
}