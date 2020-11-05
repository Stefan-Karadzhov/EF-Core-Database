﻿using Cinema_Database.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cinema_Database.ImportDTO
{
   public class HallDTO
    {

        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        [MinLength(3)]
        public string Name { get; set; }

        public bool Is4Dx { get; set; }

        public bool Is3D { get; set; }
        [Range(1,int.MaxValue)]
        public int Seats { get; set; }


    }
}