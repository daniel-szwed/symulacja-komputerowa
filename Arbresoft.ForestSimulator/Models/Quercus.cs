namespace Arbresoft.ForestSimulator.Models
{
    public class Quercus : Tree
    {
        public Quercus(Point point) : base(point)
        {

        }

        public override Color GetColor()
        {
            return Colors.RosyBrown;
        }

        public override int GetRadius()
        {
            return (int)(GetAge() * 0.1);
        }

        public override Tree Reproduce()
        {
            this.Reproduced = true;

            return new Quercus(seedLocation);
        }
    }
}

