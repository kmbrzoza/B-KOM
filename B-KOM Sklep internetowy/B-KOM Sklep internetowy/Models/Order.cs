using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace B_KOM_Sklep_internetowy.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Wprowadź imię")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Wprowadź nazwisko")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Wprowadź ulice")]
        [StringLength(100)]
        public string Street { get; set; }

        [Required(ErrorMessage = "Wprowadź Miasto")]
        [StringLength(100)]
        public string City { get; set; }

        [Required(ErrorMessage = "Wprowadź kod pocztowy")]
        [StringLength(6)]
        public string PostCode { get; set; }

        [Required(ErrorMessage = "Wprowadź numer telefonu")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Wprowadź e-mail")]
        [EmailAddress(ErrorMessage = "Błędny format e-mail")]
        public string Email { get; set; }

        public string Comment { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal OrderValue { get; set; }

        public OrderStatus OrderStatus { get; set; }


        public List<OrderItem> OrderItems { get; set; }
    }


    public enum OrderStatus
    {
        Oczekujące,
        Realizacja,
        Wysłane,
        Zakończone
    }
}