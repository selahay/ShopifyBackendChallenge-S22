/*
 * Sarah LaHay
 * Shopify Backend Challenge
 * 
 */ 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Model for inventory items
/// </summary>
namespace ShopifyBackendChallenge.Models
{
    public class Inventory
    {
        [Key]
        public int ItemId { get; set; }

        [Required(ErrorMessage = "Item Name is Required")]
        public string ItemName { get; set; }

        [Required(ErrorMessage = "Item Description is Required")]
        public string ItemDescription { get; set; }
        public bool InStock { get; set; }

        public bool IsDeleted { get; set; }
    }
}
