namespace Arbresoft.ForestSimulator.Models
{
    public class Pinus : Tree
    {
        public Pinus(Point point) : base(point)
        {

        }

        public override Color GetColor()
        {
            return Colors.Green;
        }

        public override int GetRadius()
        {
            return (int)(GetAge() * 0.1);
        }

        public override Tree Reproduce()
        {
            this.Reproduced = true;

            return new Pinus(seedLocation);
        }
    }
}

