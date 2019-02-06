﻿using System;

namespace TSM.Models
{
    public class Schedule : Entity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime StartAt { get; set; }

        public DateTime EndAt { get; set; }

        public int TeamId { get; set; }

        public bool IsActive => DateTime.Now >= StartAt && DateTime.Now <= EndAt;

        public bool IsNotActive => !IsActive;

        public bool IsDescriptionNotEmpty => !string.IsNullOrWhiteSpace(Description);
    }
}
