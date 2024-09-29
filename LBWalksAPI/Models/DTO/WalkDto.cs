﻿using LBWalksAPI.Models.Domain;

namespace LBWalksAPI.Models.DTO
{
    public class WalkDto
    {
        public Guid Id { get; set; }


        public string Name { get; set; } = "";

        public string Description { get; set; } = "";


        public double LengthInKm { get; set; }


        public string? WalkImageUrl { get; set; }


        public RegionDTO Region{ get; set; }


        public DifficultyDto Difficulty { get; set; }
    }
}
