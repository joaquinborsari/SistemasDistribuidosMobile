using System;
namespace SistemasDistribuidos.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool Is_admin { get; }
    }
}

