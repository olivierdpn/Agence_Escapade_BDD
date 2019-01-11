using System;
namespace BDD_FILROUGE22
{
    public class Appartement
    {
        public int host_id = 2626;
        public int room_id { get; set; }
        public string room_type { get; set; }
        public string neighborhood { get; set; }
        public int borough { get; set; }
        public double overall_satisfaction { get; set; }
        public int bedrooms { get; set; }
        public int price { get; set; }
        public string week { get; set; }
        public string availability { get; set; }



        public override string ToString()
        {
            return string.Format("[Appartement: host_id={0}\nroom_id={1}\nroom_type={2}\nneighborhood={3}\nborough={4}\noverall_satisfaction={5}\nbedrooms={6}\nprice={7}\nweek={8}\navailability={9}]", host_id, room_id, room_type, neighborhood, borough, overall_satisfaction, bedrooms, price, week, availability);
        }
    }
}
