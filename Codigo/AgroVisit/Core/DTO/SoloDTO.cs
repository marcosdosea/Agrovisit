﻿namespace Core.DTO
{
    public partial class SoloDTO
    {
        public uint Id { get; set; }

        public string Nome { get; set; } = null!;

        public virtual ICollection<PropriedadeDTO> Propriedades { get; set; } = new List<PropriedadeDTO>();
    }

}