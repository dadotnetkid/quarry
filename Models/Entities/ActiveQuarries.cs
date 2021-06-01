using System.Collections.Generic;

namespace Models.Entities
{
    public class ActiveQuarries
    {
        public ActiveQuarries()
        {
            
        }
        public string Id { get; set; }
        public int Year { get; set; }
        public int QuarryId { get; set; }
        public int PermitteeId { get; set; }
        /*public Quarries Quarries { get; set; }
        public Permitees Permittees { get; set; }*/
    }
}
